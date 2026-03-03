using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Extensions.Testing.SourceGenerators;

namespace Indago.Tests;

public record GeneratorTestResultsWithServices(GeneratorTestResults Results, IEnumerable<ServiceDescriptor> Services);
public record GeneratorTestResultsWithCacheFiles(GeneratorTestResults Results, string TempDirectory);
