# Interface Segregation Principle (ISP) Example

This folder demonstrates the **Interface Segregation Principle** (the "I" in SOLID) using a restaurant worker example in C#.

## What is the Interface Segregation Principle?

The Interface Segregation Principle states:

> **Clients should not be forced to depend on interfaces they do not use.**

In other words, it's better to have several small, specific interfaces than a single large, general-purpose one.

## Example in This Folder

The code in [`Program.cs`](Program.cs) shows both a violation and a correct application of ISP.

### ISP Violation (Bad Design)

The [`IWorker`](Interfaces/IWorker.cs) interface is a "fat" interface that requires all implementers to provide `Cook()`, `Clean()`, and `Serve()` methods:

```csharp
public interface IWorker
{
    void Cook();
    void Clean();
    void Serve();
}
```

The [`Chef`](Models/Chef.cs) class implements `IWorker`, but only actually cooks. The `Clean()` and `Serve()` methods throw `NotImplementedException`:

```csharp
public class Chef : IWorker
{
    public void Cook() { Console.WriteLine("Cooking"); }
    public void Clean() { throw new NotImplementedException(); }
    public void Serve() { throw new NotImplementedException(); }
}
```

This is a violation of ISP: `Chef` is forced to implement methods it doesn't need.

### ISP Correct Design (Good Design)

Instead of one large interface, the responsibilities are split into smaller interfaces:

- [`ICook`](Interfaces/ICook.cs): `void Cooks();`
- [`ICleaner`](Interfaces/ICleaner.cs): `void Cleans();`
- [`IServer`](Interfaces/IServer.cs): `void Serve();`

Now, classes implement only the interfaces relevant to them:

- [`Cook`](Models/Cook.cs) implements `ICook`
- [`Janitor`](Models/Janitor.cs) implements `ICleaner`
- [`Waiter`](Models/Waiter.cs) implements `IServer`

This allows each class to have only the methods it actually needs.

## How the Example Demonstrates the Principle

- **Bad Design:** `Chef` is forced to implement unrelated methods, leading to runtime exceptions.
- **Good Design:** Each worker class implements only the interface(s) relevant to its role.


---

**This project is a practical demonstration of how to apply the Interface Segregation Principle in C#.**