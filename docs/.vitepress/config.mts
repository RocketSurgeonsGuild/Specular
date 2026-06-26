import { defineConfig } from 'vitepress';
import { TypesenseSearchPlugin } from 'vitepress-plugin-typesense';

export default defineConfig({
    title: 'Indago',
    description: 'Compile-time assembly scanning for .NET — AOT safe, zero reflection',
    lang: 'en-US',
    base: '/Indago/',
    cleanUrls: true,
    lastUpdated: true,

    vite: {
        plugins: [
            TypesenseSearchPlugin({
                // Collection name in your Typesense instance
                typesenseCollectionName: process.env.TYPESENSE_COLLECTION_NAME ?? 'indago-docs',
                typesenseServerConfig: {
                    // Search-only API key (safe to expose in the browser)
                    apiKey: process.env.TYPESENSE_SEARCH_API_KEY ?? 'xyz',
                    nodes: [
                        {
                            url: process.env.TYPESENSE_HOST ?? 'https://search.example.com',
                        },
                    ],
                },
                typesenseSearchParameters: {},
                indexing: {
                    // Set to true during CI/deploy to index pages into Typesense
                    enabled: process.env.TYPESENSE_INDEX === 'true',
                    hostname: process.env.DOCS_HOSTNAME ?? 'https://rocketsurgeonsguild.github.io/Indago/',
                    typesenseServerConfig: {
                        // Admin API key with write permissions — keep in .env, never commit
                        apiKey: process.env.TYPESENSE_ADMIN_API_KEY ?? '',
                        nodes: [
                            {
                                url: process.env.TYPESENSE_HOST ?? 'https://search.example.com',
                            },
                        ],
                    },
                },
            }),
        ],
    },

    themeConfig: {
        nav: [
            { text: 'Guide', link: '/guide/' },
            { text: 'Reference', link: '/reference/iindago-provider' },
            { text: 'Architecture', link: '/architecture/how-it-works' },
            {
                text: 'GitHub',
                link: 'https://github.com/RocketSurgeonsGuild/Indago',
                rel: 'external',
            },
            {
                text: 'NuGet',
                link: 'https://www.nuget.org/packages/Indago',
                rel: 'external',
            },
        ],

        sidebar: {
            '/guide/': [
                {
                    text: 'Guide',
                    items: [
                        { text: 'What is Indago?', link: '/guide/' },
                        { text: 'Installation', link: '/guide/installation' },
                        { text: 'Quickstart', link: '/guide/quickstart' },
                        { text: 'AOT Publishing', link: '/guide/aot-publishing' },
                    ],
                },
            ],
            '/reference/': [
                {
                    text: 'Reference',
                    items: [
                        { text: 'IIndagoProvider', link: '/reference/iindago-provider' },
                        { text: 'Type Filters', link: '/reference/type-filters' },
                        {
                            text: 'ServiceRegistrationAttribute',
                            link: '/reference/service-registration',
                        },
                        {
                            text: 'ExcludeFromIndagoAttribute',
                            link: '/reference/exclude-from-indago',
                        },
                    ],
                },
            ],
            '/architecture/': [
                {
                    text: 'Architecture',
                    items: [
                        { text: 'How It Works', link: '/architecture/how-it-works' },
                        {
                            text: 'Cross-Assembly Caching',
                            link: '/architecture/cross-assembly-caching',
                        },
                    ],
                },
            ],
        },

        editLink: {
            pattern: 'https://github.com/RocketSurgeonsGuild/Indago/edit/main/docs/:path',
            text: 'Edit this page on GitHub',
        },

        socialLinks: [{ icon: 'github', link: 'https://github.com/RocketSurgeonsGuild/Indago' }],

        footer: {
            message: 'Released under the MIT License.',
            copyright: 'Copyright © Rocket Surgeons Guild',
        },
    },
});
