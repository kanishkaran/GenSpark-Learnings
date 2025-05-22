# Single Responsibility Principle Example

This demonstrates the **Single Responsibility Principle (SRP)**, the first principle of SOLID design.

## What is the Single Responsibility Principle?

The Single Responsibility Principle states that a class should have only one reason to change, meaning it should have only one job or responsibility.

## Example in This Project

### Bad Design

The `Orders` class in [Program.cs](Program.cs) violates SRP because it handles both saving the order and sending notifications:

```csharp
class Orders
{
    public void ProcessOrder(string order)
    {
        // Save Logic
        Console.WriteLine($"Order Saved {order}");

        // Notification Logic
        Console.WriteLine($"Notification Sent for the order {order}");
    }
}
```

### Good Design

The good design separates responsibilities into different classes:

- [`Order`](Models/Order.cs): Represents the order data.
- [`OrderRepository`](Repository/OrderRepository.cs): Handles saving the order.
- [`Notifier`](Services/Notifier.cs): Handles sending notifications.
- [`OrderProcessor`](Services/OrderProcessor.cs): Coordinates the process by using the repository and notifier.

The main program ([Program.cs](Program.cs)) uses these classes:

```csharp
Order order = new Order("Hammer");
OrderProcessor orderProcessor = new OrderProcessor();
orderProcessor.Process(order);
```

This way, each class has a single responsibility, making the code easier to maintain and extend.
