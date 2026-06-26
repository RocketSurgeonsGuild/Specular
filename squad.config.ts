import { defineSquad, defineTeam, defineAgent, defineRouting } from '@bradygaster/squad-sdk';

// Install the SDK if not present: npm install -D @bradygaster/squad-sdk

export default defineSquad({
    team: defineTeam({
        name: 'Indago',
        agents: [
            defineAgent({
                name: 'roslyn-engineer',
                role: 'Roslyn incremental source generator specialist',
                model: 'claude-sonnet-4-6',
                status: 'active',
                capabilities: [
                    { name: 'Roslyn IIncrementalGenerator', level: 'expert' },
                    { name: 'C# source generation (netstandard2.0)', level: 'expert' },
                    { name: 'Roslyn symbol visitors', level: 'expert' },
                    { name: 'Cross-assembly JSON cache (ctpjson)', level: 'expert' },
                    { name: 'Multi-Roslyn-version compat (4.8/4.14/5.0)', level: 'proficient' },
                    { name: 'AOT/trim-safe code emission', level: 'proficient' },
                ],
            }),
            defineAgent({
                name: 'dotnet-engineer',
                role: '.NET runtime API, DI, and AOT specialist',
                model: 'claude-sonnet-4-6',
                status: 'active',
                capabilities: [
                    { name: 'IIndagoProvider API design', level: 'expert' },
                    { name: '.NET 8 + .NET 10 multi-targeting', level: 'expert' },
                    { name: 'Microsoft.Extensions.DependencyInjection', level: 'expert' },
                    { name: 'AOT/NativeAOT and IL trimming', level: 'expert' },
                    { name: 'NuGet packaging (Central Package Management)', level: 'expert' },
                    { name: 'PublicAPI tracking (RS0017)', level: 'proficient' },
                ],
            }),
            defineAgent({
                name: 'docs-engineer',
                role: 'VitePress documentation site and GitHub Pages specialist',
                model: 'claude-haiku-4-5',
                status: 'active',
                capabilities: [
                    { name: 'VitePress 2.0 alpha configuration', level: 'expert' },
                    { name: 'Markdown technical writing', level: 'expert' },
                    { name: 'GitHub Actions (Pages deployment)', level: 'expert' },
                    { name: 'ESLint 9 flat config (Vue 3 + TS + MD)', level: 'proficient' },
                    { name: 'Vue 3 SFC components', level: 'proficient' },
                ],
            }),
            defineAgent({
                name: 'qa-engineer',
                role: 'Snapshot tests and generator QA specialist',
                model: 'claude-haiku-4-5',
                status: 'active',
                capabilities: [
                    { name: 'TUnit on Microsoft.Testing.Platform', level: 'expert' },
                    { name: 'Verify snapshot testing', level: 'expert' },
                    { name: 'Roslyn source generator testing', level: 'expert' },
                    { name: 'Cross-Roslyn-version test coverage', level: 'proficient' },
                    { name: 'AOT publish smoke testing', level: 'proficient' },
                ],
            }),
            defineAgent({
                name: 'ralph',
                role: 'Persistent memory agent',
                model: 'claude-haiku-4-5',
                status: 'active',
                capabilities: [{ name: 'Cross-session context retention', level: 'expert' }],
            }),
            defineAgent({
                name: 'Scribe',
                role: 'Session logging and documentation specialist',
                model: 'claude-haiku-4-5',
                status: 'active',
                capabilities: [
                    { name: 'Session logging', level: 'expert' },
                    { name: 'Decision records', level: 'expert' },
                ],
            }),
            defineAgent({
                name: 'Rai',
                role: 'Responsible AI reviewer',
                model: 'claude-haiku-4-5',
                status: 'active',
                capabilities: [
                    { name: 'Content safety review', level: 'expert' },
                    { name: 'Credential detection', level: 'expert' },
                    { name: 'Bias detection', level: 'expert' },
                ],
            }),
        ],
    }),

    routing: defineRouting({
        strategy: 'capability-match',
        rules: [
            {
                pattern: /\bIIncrementalGenerator|syntax provider|symbol visitor|AssemblyProvider|ctpjson|IndagoProviderGenerator\b/i,
                agent: 'roslyn-engineer',
            },
            {
                pattern: /\bRoslyn (4\.8|4\.14|5\.0)|EnforceExtendedAnalyzerRules|Polyfill\b/i,
                agent: 'roslyn-engineer',
            },
            {
                pattern: /\bIIndagoProvider|ServiceRegistration|ExcludeFromIndago|AddIndago\b/i,
                agent: 'dotnet-engineer',
            },
            {
                pattern: /\bPublishAot|trim warn|AOT compat|IL trim\b/i,
                agent: 'dotnet-engineer',
            },
            {
                pattern: /\bVitePress|docs\/(guide|reference|architecture)|sidebar|navbar\b/i,
                agent: 'docs-engineer',
            },
            {
                pattern: /\beslint|hk fix|hk check|deploy-docs|GitHub Pages\b/i,
                agent: 'docs-engineer',
            },
            {
                pattern: /\bsnapshot|\.received\.|\.verified\.|dotnet verify|TUnit|Shouldly\b/i,
                agent: 'qa-engineer',
            },
            {
                pattern: /\btest(ing)?|coverage|GeneratorTest|TestAssembly|treenode-filter\b/i,
                agent: 'qa-engineer',
            },
        ],
        modelTiers: {
            full: 'claude-opus-4',
            standard: 'claude-sonnet-4-6',
            lightweight: 'claude-haiku-4-5',
        },
    }),
});
