# Liskov Substitution Principle (LSP) Example

This folder demonstrates the **Liskov Substitution Principle** (the "L" in SOLID) using C# animal class examples.

## What is the Liskov Substitution Principle?

The Liskov Substitution Principle states:

> **Objects of a superclass should be replaceable with objects of a subclass without altering the correctness of the program.**

In practice, this means subclasses should honor the contracts of their base classes. If a subclass cannot fully implement the expected behavior, substituting it can break the program.

## Example in This Folder

The code in [`Program.cs`](Program.cs) shows both a violation and a correct application of LSP.

### LSP Violation

In the first example:
- `Animal` has methods `Hunt()` and `Eat()`.
- `Tiger` (a subclass) implements both.
- `Deer` (a subclass) cannot hunt, but is forced to override `Hunt()`. It throws a `NotImplementedException`.

This violates LSP: code expecting to use any `Animal` cannot safely use a `Deer` without risking exceptions.

### LSP Correct Design

In the improved example:
- `BaseAnimal` only has `Eat()`.
- `IPredator` interface defines `Hunt()`.
- `Lion` inherits `BaseAnimal` and implements `IPredator`.
- `Elephant` inherits `BaseAnimal` but does not hunt.

Now, only animals that can hunt implement the `Hunt()` method. Any `BaseAnimal` can be substituted for another without breaking code, and only predators are used where hunting is expected.



