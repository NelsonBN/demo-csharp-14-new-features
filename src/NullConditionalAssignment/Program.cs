Person? me = new Person { Name = "Nelson" };
Person? you = null;

me?.Name = "Nelson Nobre"; // This will assign because "me" is not null
you?.Name = "John Doe"; // This will not assign because "you" is null, but it won't throw an exception

Console.WriteLine($"me: {me?.Name}, you: {you?.Name}"); // Output: me: Nelson Nobre, you: null

class Person
{
    public string? Name { get; set; }
}
