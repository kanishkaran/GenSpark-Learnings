# Open/Closed Principle Example

This project demonstrates the **Open/Closed Principle** from the SOLID principles using a simple order processing system in C#.

## What is the Open/Closed Principle?

The **Open/Closed Principle** states that:
> *Software entities (classes, modules, functions, etc.) should be open for extension, but closed for modification.*

This means you should be able to add new functionality without changing existing code, reducing the risk of introducing bugs and making the system easier to maintain and extend.

## Project Structure

- [`Models/Order.cs`](Models/Order.cs): Represents an order.
- [`Repository/OrderRepository.cs`](Repository/OrderRepository.cs): Handles saving orders.
- [`Interfaces/INotifier.cs`](Interfaces/INotifier.cs): Interface for notification services.
- [`Services/EmailNotifier.cs`](Services/EmailNotifier.cs): Sends email notifications.
- [`Services/SmsNotifier.cs`](Services/SmsNotifier.cs): Sends SMS notifications.
- [`Services/Notifier.cs`](Services/Notifier.cs): Example of a notifier that does not follow the Open/Closed Principle.
- [`Services/OrderProcessor.cs`](Services/OrderProcessor.cs): Processes orders and sends notifications.

## How the Example Demonstrates the Principle

### Bad Design (Closed for Extension)

The [`Notifier`](Services/Notifier.cs) class is a simple notification sender. If you want to add new notification types (e.g., SMS, push notifications), you would have to modify this class, violating the Open/Closed Principle.

```csharp
public class Notifier
{
    public void SendNotification(Order order)
    {
        //Send Notification
        Console.WriteLine($"Normal Notification Sent for order: {order.OrderDetails}");
    }
}
```

### Good Design (Open for Extension, Closed for Modification)

The system uses the [`INotifier`](Interfaces/INotifier.cs) interface. New notification types can be added by creating new classes that implement this interface, without modifying existing code.

```csharp
public interface INotifier
{
    void SendNotification(Order order);
}
```

Implementations:
- [`EmailNotifier`](Services/EmailNotifier.cs)
- [`SmsNotifier`](Services/SmsNotifier.cs)

The [`OrderProcessor`](Services/OrderProcessor.cs) uses a list of `INotifier` objects. To add a new notification type, simply add a new class implementing `INotifier` and include it in the list—no need to change existing classes.

```csharp
public void Process(Order order)
{
    repo.Save(order);
    List<INotifier> notifiers = new List<INotifier>
    {
        new EmailNotifier(),
        new SmsNotifier()
    };

    foreach (var n in notifiers)
    {
        n.SendNotification(order);
    }
}
```
