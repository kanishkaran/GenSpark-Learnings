# Dependency Inversion Principle (DIP) Example

This project demonstrates the **Dependency Inversion Principle** (the "D" in SOLID) using a simple order processing system in C#.

## What is the Dependency Inversion Principle?

The Dependency Inversion Principle states:

> High-level modules should not depend on low-level modules. Both should depend on abstractions.

This means:
- High-level business logic should not directly depend on concrete implementations of lower-level services.
- Both should depend on interfaces or abstract classes.
- Concrete implementations should be injected, not hard-coded.

## Example in This Folder

This example processes an order and sends notifications using different channels (Email, SMS). The notification mechanism is abstracted using the `INotifier` interface.

### Key Components

- [`INotifier`](Interfaces/INotifier.cs): Interface for sending notifications.
- [`EmailNotifier`](Services/EmailNotifier.cs) and [`SmsNotifier`](Services/SmsNotifier.cs): Implementations of `INotifier`.
- [`OrderRepository`](Repository/OrderRepository.cs): Handles order persistence.
- [`OrderProcessor`](Services/OrderProcessor.cs): High-level class that processes orders and sends notifications. **Depends on abstractions, not concrete classes.**
- [`Program.cs`](Program.cs): Wires everything together and demonstrates dependency injection.

### How DIP is Applied

- `OrderProcessor` does **not** create `EmailNotifier`, `SmsNotifier`, or `OrderRepository` directly.
- Instead, it receives them via its constructor as abstractions (`INotifier` and `OrderRepository`).
- This allows you to add new notification types or change the repository implementation without modifying `OrderProcessor`.

### Example Code

```csharp
// In Program.cs
var order = new Order("Order #123");
var repo = new OrderRepository();
var notifiers = new List<INotifier>
{
    new EmailNotifier(),
    new SmsNotifier()
};
var processor = new OrderProcessor(repo, notifiers);
processor.Process(order);
```

```csharp
// In OrderProcessor.cs
public class OrderProcessor
{
    private readonly OrderRepository _repo;
    private readonly IEnumerable<INotifier> _notifiers;

    public OrderProcessor(OrderRepository repo, IEnumerable<INotifier> notifiers)
    {
        _repo = repo;
        _notifiers = notifiers;
    }

    public void Process(Order order)
    {
        _repo.Save(order);
        foreach (var n in _notifiers)
        {
            n.SendNotification(order);
        }
    }
}
```

## Benefits

- **Extensible:** Add new notification types without changing business logic.
- **Testable:** Easily mock dependencies for unit testing.
- **Decoupled:** High-level logic is not tied to specific implementations.

---
