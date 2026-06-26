# Contract: VitePress Site Configuration

**File**: `docs/.vitepress/config.mts`

This document specifies the required shape of the VitePress configuration. It is the contract
between the content authors and the site build.

## Required exports

```typescript
import { defineConfig } from 'vitepress';

export default defineConfig({
    // Site identity
    title: 'Indago',
    description: 'Compile-time assembly scanning for .NET — AOT safe, zero reflection',
    lang: 'en-US',
    base: '/Indago/', // MUST match GitHub Pages project-site URL prefix

    // URL hygiene
    cleanUrls: true,
    lastUpdated: true,

    themeConfig: {
        // Top-level nav (required entries)
        nav: [
            { text: 'Guide', link: '/guide/' },
            { text: 'Reference', link: '/reference/iindago-provider' },
            { text: 'Architecture', link: '/architecture/how-it-works' },
            { text: 'GitHub', link: 'https://github.com/RocketSurgeonsGuild/Indago', rel: 'external' },
        ],

        // Sidebar (required sections)
        sidebar: {
            '/guide/': [
                /* see data-model.md */
            ],
            '/reference/': [
                /* see data-model.md */
            ],
            '/architecture/': [
                /* see data-model.md */
            ],
        },

        // Edit link (required)
        editLink: {
            pattern: 'https://github.com/RocketSurgeonsGuild/Indago/edit/main/docs/:path',
            text: 'Edit this page on GitHub',
        },

        // Social links (required)
        socialLinks: [{ icon: 'github', link: 'https://github.com/RocketSurgeonsGuild/Indago' }],

        // Search (optional but recommended)
        search: { provider: 'local' },
    },
});
```

## Validation rules

- `base` MUST end with `/`.
- All `link` values in `nav` and `sidebar` MUST resolve to an actual `.md` file in `docs/`
  (enforced by `vitepress build` — dead links fail the build).
- The config MUST import only from `vitepress` — no runtime .NET packages.

---

# Contract: ESLint Flat Configuration

**File**: `eslint.config.mjs` (repo root)

```javascript
// @ts-check
import eslint from '@eslint/js';
import tseslint from 'typescript-eslint';
import pluginVue from 'eslint-plugin-vue';
import pluginMarkdown from 'eslint-plugin-markdown';

export default tseslint.config(
    // Global ignores
    { ignores: ['node_modules', 'docs/.vitepress/dist', 'docs/.vitepress/cache', '**/*.generated.*'] },

    // TypeScript files
    {
        files: ['**/*.{ts,mts,cts}'],
        extends: [eslint.configs.recommended, ...tseslint.configs.recommendedTypeChecked],
        languageOptions: {
            parserOptions: {
                projectService: true,
                tsconfigRootDir: import.meta.dirname,
            },
        },
    },

    // Vue SFCs (docs theme components)
    {
        files: ['**/*.vue'],
        extends: [...pluginVue.configs['flat/vue3-recommended'], ...tseslint.configs.recommended],
        languageOptions: {
            parserOptions: { parser: tseslint.parser },
        },
    },

    // Markdown fenced code blocks
    {
        files: ['**/*.md'],
        plugins: { markdown: pluginMarkdown },
        processor: pluginMarkdown.processors.markdown,
    }
);
```

## Validation rules

- No ESLint errors on `**/*.vue` files in `docs/.vitepress/theme/`.
- No ESLint errors on `docs/.vitepress/config.mts`.
- Markdown fenced blocks tagged `typescript` or `javascript` must be syntactically valid.
- `eslint .` exits 0 in CI.

---

# Contract: GitHub Pages Deploy Workflow

**File**: `.github/workflows/deploy-docs.yml`

## Required structure

```yaml
name: Deploy Docs

on:
    push:
        branches: [main]
    workflow_dispatch:

permissions:
    contents: read
    pages: write
    id-token: write

concurrency:
    group: pages
    cancel-in-progress: false

jobs:
    build:
        runs-on: ubuntu-latest
        steps:
            - uses: actions/checkout@v7
              with:
                  fetch-depth: 0 # required for lastUpdated
            - uses: jdx/mise-action@v4
              with:
                  install: true
                  cache: true
                  github_token: ${{ secrets.GITHUB_TOKEN }}
            - run: mise run docs-build
            - uses: actions/upload-pages-artifact@v3
              with:
                  path: docs/.vitepress/dist

    deploy:
        needs: build
        runs-on: ubuntu-latest
        environment:
            name: github-pages
            url: ${{ steps.deployment.outputs.page_url }}
        steps:
            - id: deployment
              uses: actions/deploy-pages@v4
```

## Validation rules

- Workflow MUST pass `actionlint` (already in mise at 1.7.12).
- `deploy` job MUST depend on `build` job.
- `cancel-in-progress` MUST be `false` (never cancel an in-flight deploy).
- `fetch-depth: 0` is required for VitePress `lastUpdated` to work.
