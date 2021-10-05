# Guard.NET

A simple library that facilitates runtime checks of code and allows to define preconditions and invariants within a method.

Its main purpose is to **leverage the precondition checks** that appear in almost all methods, through a clean interface that accentuates intention and eliminates confusion.

## Usage 



```<language>
public void AddUser(User user)
{
    Guard.NotNull(user, nameof(user), "optional custom error message");  // throws ArgumentNullException
	
    // OR use an explicit Exception
    
    var invalidOperationException = new InvalidOperationException("custom message");
    Guard.NotNull(user, invalidOperationException); 
    
    ...

}
```

```<language>
public void GetUserByName(string name)
{
    Guard.NotNullOrWhitespace(name, nameof(name), "optional custom error message"); // throws ArgumentException

    // OR use an explicit Exception
    
    var invalidOperationException = new InvalidOperationException("custom message");
    Guard.NotNullOrWhitespace(userName, invalidOperationException);
    
    ...
	
}
```

```<language>
public void GetUsers(int pageSize)
{
    Guard.NotGreaterThan(pageSize, _maxPageSize, nameof(pageSize), "optional custom error message"); // throws ArgumentOutOfRangeException

    // OR use an explicit Exception
    
    var invalidOperationException = new InvalidOperationException("custom message");
    Guard.NotGreaterThan(pageSize, _maxPageSize, invalidOperationException);
    
    ...
	
}
```

```<language>
public void UpdateEmailAddress(int userId, string newEmailAddress)
{
    Guard.For(() => userId < 0, new ArgumentException(nameof(userId)));
    Guard.For(() => Regex.IsMatch(newEmailAddress, _emailRegexPattern), new ArgumentException(nameof(newEmailAddress)));

    ...
	
}
```


## Installation
The Guard class can be used by installing **Guard.NET nuget package** available [here](https://www.nuget.org/packages/Guard.NET/).

```<language>
Install-Package Guard.NET
```

The package has no external dependencies.
