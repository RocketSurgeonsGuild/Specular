#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DOCS_DIR="$(dirname "$SCRIPT_DIR")"
REPO_ROOT="$(dirname "$DOCS_DIR")"
API_OUTPUT_DIR="$DOCS_DIR/src/content/docs/api"
SOURCE_BASE="https://github.com/RocketSurgeonsGuild/Indago/blob/main/src"

# Locate xmldocmd — prefer mise shim, then dotnet tools global, then PATH
if command -v xmldocmd &>/dev/null; then
  XMLDOCMD="xmldocmd"
elif [[ -x "$HOME/.local/share/mise/shims/xmldocmd" ]]; then
  XMLDOCMD="$HOME/.local/share/mise/shims/xmldocmd"
elif [[ -x "$HOME/.dotnet/tools/xmldocmd" ]]; then
  XMLDOCMD="$HOME/.dotnet/tools/xmldocmd"
else
  echo "ERROR: xmldocmd not found. Install with: dotnet tool install -g xmldocmd" >&2
  exit 1
fi

# Map: src/ directory name → api/ slug
declare -A PACKAGES=(
  ["Indago"]="indago"
)

echo ""
echo "==> Generating API reference docs..."

generated=0
skipped=0
fallback_generated=0

for PKG in "${!PACKAGES[@]}"; do
  SLUG="${PACKAGES[$PKG]}"
  PKG_DIR="$REPO_ROOT/src/$PKG"

  if [[ ! -d "$PKG_DIR" ]]; then
    echo "  SKIP  $PKG — directory does not exist"
    ((skipped++)) || true
    continue
  fi

  CSPROJ=$(find "$PKG_DIR" -maxdepth 1 -name "*.csproj" | head -1 || true)
  if [[ -z "$CSPROJ" ]]; then
    echo "  SKIP  $PKG — project file not found"
    ((skipped++)) || true
    continue
  fi

  EXPECTED_ASSEMBLY=$(sed -n 's:.*<AssemblyName>\([^<]*\)</AssemblyName>.*:\1:p' "$CSPROJ" | head -1)
  if [[ -z "$EXPECTED_ASSEMBLY" ]]; then
    EXPECTED_ASSEMBLY="$(basename "$CSPROJ" .csproj)"
  fi

  # Generate from the Release **bin** (not a publish) — net8.0 is the documented source TFM (T025).
  DLL=""
  TFM_USED=""
  for TFM in net10.0 net8.0 netstandard2.0; do
    TFM_DIR="$PKG_DIR/bin/Release/$TFM"
    CANDIDATE="$TFM_DIR/$EXPECTED_ASSEMBLY.dll"
    if [[ -f "$CANDIDATE" ]]; then
      DLL="$CANDIDATE"
      TFM_USED="$TFM"
      break
    fi
  done

  if [[ -z "$DLL" ]]; then
    echo "  SKIP  $PKG — no DLL found (tried net8.0, net10.0, netstandard2.0)"
    ((skipped++)) || true
    continue
  fi

  # Precondition (T026): the XML doc is REQUIRED — fail clearly rather than emit a partial API ref.
  XML="${DLL%.dll}.xml"
  if [[ ! -f "$XML" ]]; then
    echo "  ERROR $PKG — required XML doc missing: $XML" >&2
    echo "        Ensure <GenerateDocumentationFile>true</GenerateDocumentationFile> for $PKG." >&2
    exit 1
  fi

  # xmldocmd reflection-loads the assembly, so its runtime dependency assemblies must sit next to it.
  # Libraries don't copy NuGet deps to bin by default; populate them so xmldocmd can load (no fallback).
  dotnet build "$CSPROJ" -c Release -f "$TFM_USED" -p:CopyLocalLockFileAssemblies=true \
    --no-restore -v quiet -clp:ErrorsOnly >/dev/null 2>&1 || true

  OUT_DIR="$API_OUTPUT_DIR/$SLUG"
  mkdir -p "$OUT_DIR"
  rm -rf "${OUT_DIR:?}"/*

  # xmldocmd is MANDATORY (no XML-only fallback): a load failure must fail the docs build loudly.
  echo "  GEN   $PKG ($TFM_USED) → api/$SLUG/"
  "${XMLDOCMD}" "$DLL" "$OUT_DIR/" \
    --namespace "$PKG" \
    --source "$SOURCE_BASE/$PKG/" \
    --skip-compiler-generated \
    --skip-unbrowsable \
    --clean
  ((generated++)) || true
done

echo ""
echo "==> Injecting Starlight frontmatter..."
node "$DOCS_DIR/scripts/add-api-frontmatter.mjs"

echo ""
echo "==> Done. Generated: $generated package(s), skipped: $skipped, fallback-used: $fallback_generated."
