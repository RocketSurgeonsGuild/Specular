import { defineCollection } from 'astro:content';
import { docsSchema } from '@astrojs/starlight/schema';
import { changelogsLoader } from 'starlight-changelogs/loader';
import { starlightTagsExtension } from 'starlight-tags/schema';
import { dotnetXmlApiLoader } from './loaders/dotnet-xml-api';

export const collections = {
    docs: defineCollection({
        // Custom loader: hand-written Markdown (via Starlight's docsLoader) plus API reference
        // pages parsed from each assembly's compiled XML documentation (all target frameworks).
        loader: dotnetXmlApiLoader({
            assemblies: [{ projectDir: '../src/Indago' }],
            includeNamespaces: ['Indago'],
            basePath: 'api',
        }),
        schema: docsSchema({ extend: starlightTagsExtension }),
    }),
    changelogs: defineCollection({
        loader: changelogsLoader([
            {
                provider: 'github',
                base: 'changelog',
                owner: 'RocketSurgeonsGuild',
                repo: 'Indago',
                token: import.meta.env.GH_API_TOKEN,
            },
        ]),
    }),
};
