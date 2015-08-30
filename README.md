# jcEHL
A PCL for C# programmers with handy helper methods

You might find youself in a situation where you have a base class and then extend it with a few more properties like so:

```c#
public class Person {
  public string FirstName { get; set; }
  
  public string LastName { get; set; }
}

public class Employee : Person {
  public int YearsEmployed { get; set; }
}
```

Traditionally, you would have to in your Employee class's constructor initialize the FirstName and LastName properties.  Which for this small of a class isn't a huge deal, but most classes have 5+ properties and can change.  I often found myself wishing C# had the ability to simply say - initialize the members of the base class members and then allow me to just focus on the extended class's properties.

Now you can with one line of code in your base class's constructor, using the class above:

```C#
public class Person {
  public string FirstName { get; set; }
  
  public string LastName { get; set; }
  
  public Person() { }
  
  public Person(Person basePerson) { Copy.Init(basePerson, this); }
}

public class Employee : Person {
  public int YearsEmployed { get; set; }
  
  public Employee() { } 
  
  public Employee(Person basePerson) : base(basePerson) { }
}
```
