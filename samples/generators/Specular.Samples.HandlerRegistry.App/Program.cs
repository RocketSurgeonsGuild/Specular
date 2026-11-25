using Specular.Samples.HandlerRegistry;

// Specular.Samples.HandlerRegistry.App: uses the list HandlerRegistryGenerator wrote at compile time (HandlerTypes.All), holding the
// handlers of the referenced Specular.Samples.HandlerRegistry.Handlers library and those of this app. Nothing searches for them at run
// time; they are only created here, to show they are ordinary types.

foreach (var type in HandlerTypes.All)
{
    var handler = (IHandler)Activator.CreateInstance(type)!;
    Console.WriteLine($"{type.Assembly.GetName().Name}: {type.FullName} -> {handler.Handle("hello")}");
}

return HandlerTypes.All.Count > 0 ? 0 : 1;
