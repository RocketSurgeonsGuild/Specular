#!/usr/bin/env dotnet
#:package Microsoft.VisualStudio.SolutionPersistence
#:package Sourcy.Git
#:package Sourcy.DotNet
#:package Rocket.Surgery.ModularPipelines.Extensions
#:package Joinery.Dovetail.ModularPipelines
#:package Specular
#:include ./Samples.cs
#:property JsonSerializerIsReflectionEnabledByDefault=true
#:property ExperimentalFileBasedProgramEnableTransitiveDirectives=true

using Build;
using Joinery.Dovetail;
using ModularPipelines;

// Dovetail reads build/appsettings.* (the folder of this file) and applies the library's joints: its modules and settings, found by
// Specular when this host compiles, and the run history and module cache under .modularpipelines/.
await using var pipeline = await Pipeline.CreateBuilder(args).ConfigureDovetail();
await pipeline.RunAsync();
