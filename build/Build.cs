#!/usr/bin/env dotnet
#:package Microsoft.VisualStudio.SolutionPersistence
#:package Sourcy.Git
#:package Sourcy.DotNet
#:package Rocket.Surgery.ModularPipelines.Extensions
#:package Joinery.Dovetail.ModularPipelines
#:package Specular
#:include ./Samples.cs
#:include ./Docs.cs
#:property JsonSerializerIsReflectionEnabledByDefault=true
#:property ExperimentalFileBasedProgramEnableTransitiveDirectives=true

using Build;
using Joinery.Dovetail;
using ModularPipelines;
using Rocket.Surgery.ModularPipelines.Extensions;
using Rocket.Surgery.ModularPipelines.Extensions.Modules;

// Dovetail reads build/appsettings.* (the folder of this file) and applies the library's joints: its modules and settings, found by
// Specular when this host compiles, and the run history and module cache under .modularpipelines/.
// SiteDocsModule (Docs.cs) builds the docs in place of the library's DocsModule, without waiting for the tests.
await using var pipeline = await Pipeline.CreateBuilder(args).ConfigureDovetail(z => z.Set<ModuleDelegate>(type => type != typeof(DocsModule)));
await pipeline.RunAsync();
