using System.Collections.Immutable;
using System.Text;

namespace Specular.Analyzers.ScanReport;

// Renders the same scan-report entries as a Mermaid flowchart: file/call-site -> scanner expression ->
// discovered assembly/type. Plain StringBuilder text generation - there is no Mermaid syntax tree to
// build against, so correctness rests entirely on Escape() below covering every Mermaid-reserved
// character that can appear in a selector's file path, kind, or a discovered assembly/type name.
internal static class MermaidReportBuilder
{
    public static string Build(ImmutableArray<ScanReportBuilder.Entry> entries)
    {
        var sb = new StringBuilder();
        sb.Append("flowchart TD\n");

        var index = 0;
        foreach (var entry in entries)
        {
            var fileNode = $"F{index}";
            var exprNode = $"E{index}";
            sb.Append("    ").Append(fileNode).Append('[').Append(Escape($"{Path.GetFileName(entry.FilePath)}:{entry.LineNumber}")).Append("] --> ").Append(exprNode).Append('{').Append(Escape(entry.Kind)).Append("}\n");

            var assemblyIndex = 0;
            foreach (var (AssemblyName, Expression) in entry.Assemblies)
            {
                sb.Append("    ").Append(exprNode).Append(" --> ").Append(exprNode).Append('A').Append(assemblyIndex).Append('[').Append(Escape(AssemblyName)).Append("]\n");
                assemblyIndex++;
            }

            var typeIndex = 0;
            foreach (var type in entry.Types)
            {
                sb.Append("    ").Append(exprNode).Append(" --> ").Append(exprNode).Append('T').Append(typeIndex).Append('[').Append(Escape(type.Type)).Append("]\n");
                typeIndex++;
            }

            index++;
        }

        return sb.ToString();
    }

    private static string Escape(string value) =>
        value
           .Replace("\\", "\\\\")
           .Replace("\"", "&quot;")
           .Replace("<", "&lt;")
           .Replace(">", "&gt;")
           .Replace("[", "&#91;")
           .Replace("]", "&#93;")
           .Replace("{", "&#123;")
           .Replace("}", "&#125;");
}
