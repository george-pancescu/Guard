# Guard.NET

[![NuGet](https://img.shields.io/nuget/v/Guard.NET.svg)](https://www.nuget.org/packages/Guard.NET/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Guard.NET.svg)](https://www.nuget.org/packages/Guard.NET/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

A simple, zero-dependency library that facilitates runtime checks of code and allows you to define preconditions and invariants within a method.

Its main purpose is to **leverage the precondition checks** that appear in almost all methods, through a clean interface that accentuates intention and eliminates confusion.

**Supported targets:** .NET 10, .NET 8, .NET Standard 2.1

## Installation

```
Install-Package Guard.NET
```

Or via the .NET CLI:

```
dotnet add package Guard.NET
```

## Quick Start

```csharp
public void AddUser(User user)
{
    Guard.NotNull(user, nameof(user));

    // ...
}
```

## Usage

Every guard method supports three overload patterns:

1. **Standard** — throws a framework exception with the parameter name and an optional message.
2. **Custom message** — same as above, but with your own error message.
3. **Custom exception** — throws any exception type you provide.

### Null Checks

```csharp
// throws ArgumentNullException
Guard.NotNull(user, nameof(user));
Guard.NotNull(user, nameof(user), "User must be provided");

// throws a custom exception
Guard.NotNull(user, new InvalidOperationException("No user"));
```

### String Checks

```csharp
// throws ArgumentException when null or empty
Guard.NotNullOrEmpty(name, nameof(name));
Guard.NotNullOrEmpty(name, nameof(name), "Name is required");

// throws ArgumentException when null, empty, or whitespace
Guard.NotNullOrWhitespace(name, nameof(name));
Guard.NotNullOrWhitespace(name, nameof(name), "Name cannot be blank");
```

### Range Checks

All range checks work with any type that implements `IComparable<T>`.

```csharp
// throws ArgumentOutOfRangeException
Guard.NotGreaterThan(pageSize, maxPageSize, nameof(pageSize));
Guard.NotGreaterThanOrEqualTo(index, count, nameof(index));
Guard.NotLessThan(age, 0, nameof(age));
Guard.NotLessThanOrEqualTo(quantity, 0, nameof(quantity));
```

### Equality Checks

```csharp
// throws a custom exception when values are equal
Guard.NotEqualTo<int, ArgumentException>(status, 0);
Guard.NotEqualTo(status, 0, new InvalidOperationException("Status must be set"));
```

### Collection Checks

```csharp
// throws ArgumentException when the collection is empty
Guard.NotAny(items, nameof(items));
Guard.NotAny(items, nameof(items), "At least one item is required");
```

### Regex Checks

```csharp
// throws ArgumentException when the pattern does not match
Guard.NotIsMatch(email, nameof(email), @"^[\w.-]+@[\w.-]+\.\w+$");
Guard.NotIsMatch(email, nameof(email), @"^[\w.-]+@[\w.-]+\.\w+$", "Invalid email format");
```

### Custom Predicate

For any condition not covered by the built-in methods:

```csharp
Guard.For<ArgumentException>(() => userId < 0);
Guard.For<ArgumentException>(() => userId < 0, "User ID cannot be negative");
Guard.For(() => userId < 0, new ArgumentException(nameof(userId)));
```

### Custom Exception Types

All guard methods have generic overloads that let you throw any exception type:

```csharp
Guard.NotNull<User, InvalidOperationException>(user);
Guard.NotNullOrEmpty<InvalidOperationException>(name, "Name is required");
Guard.NotGreaterThan<int, BusinessRuleException>(pageSize, maxPageSize);
Guard.NotAny<Order, ValidationException>(orders, "Order list cannot be empty");
```

## API Reference

| Method | Default Exception | Description |
|---|---|---|
| `NotNull` | `ArgumentNullException` | Ensures a reference is not `null` |
| `NotNullOrEmpty` | `ArgumentException` | Ensures a string is not `null` or empty |
| `NotNullOrWhitespace` | `ArgumentException` | Ensures a string is not `null`, empty, or whitespace |
| `NotGreaterThan` | `ArgumentOutOfRangeException` | Ensures a value does not exceed a threshold |
| `NotGreaterThanOrEqualTo` | `ArgumentOutOfRangeException` | Ensures a value is strictly less than a threshold |
| `NotLessThan` | `ArgumentOutOfRangeException` | Ensures a value is not below a threshold |
| `NotLessThanOrEqualTo` | `ArgumentOutOfRangeException` | Ensures a value is strictly greater than a threshold |
| `NotEqualTo` | *(custom only)* | Ensures two values are not equal |
| `NotAny` | `ArgumentException` | Ensures a collection is not empty |
| `NotIsMatch` | `ArgumentException` | Ensures a string matches a regex pattern |
| `For` | *(custom only)* | Evaluates a predicate and throws if `true` |

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).
