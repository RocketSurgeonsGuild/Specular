import { defineConfig } from 'vitepress';

export default defineConfig({
    title: 'Indago',
    description: 'Compile-time assembly scanning for .NET — AOT safe, zero reflection',
    lang: 'en-US',
    base: '/Indago/',
    cleanUrls: true,
    lastUpdated: true,

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

        search: { provider: 'local' },

        footer: {
            message: 'Released under the MIT License.',
            copyright: 'Copyright © Rocket Surgeons Guild',
        },
    },
});
