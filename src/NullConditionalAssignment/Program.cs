Person? me = new Person { Name = "Nelson" };
Person? you = null;
Person? he = null;

me?.Name = "Nelson Nobre"; // This will assign because "me" is not null

you?.Name = "John Doe"; // This will not assign because "you" is null, but it won't throw an exception

if (he is not null)
{
    he.Name = "Tommy"; // Here the previous approach to prevent throwing an exception
}


Console.WriteLine($"me: {me?.Name}, you: {you?.Name}, he: {he?.Name}"); // Output: me: Nelson Nobre, you: null, he: null


class Person
{
    public string? Name { get; set; }
}
