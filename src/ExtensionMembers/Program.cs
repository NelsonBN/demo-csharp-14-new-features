using System.Text.Json;

IEnumerable<Person> people = new List<Person>
{
    new("", "", false),
    new("Alice", "US", true),
    new("Bob", "UK", true),
    new("Eve", "UK", false),
    new("Charlie", "CA", false),
    new("David", "US", true),
    new("Alice", "US", false),
};

Console.WriteLine($"There are {people.IsEmpty} people in the list.");

foreach (var person in people.OnlyActives())
{
    Console.WriteLine($"Active -> Name: {person.Name}, Country: {person.Country}, IsActive: {person.IsActive} [{person.Status}]");
}

foreach (var person in people)
{
    Console.WriteLine($"JSON {person.ToJson()}");
}

Console.WriteLine($"Second and last person are exactly the same: {Person.ExactlyTheSame(people.Second, people.Last())}");

Console.WriteLine($"The first is empty: {Person.ExactlyTheSame(Person.Empty, people.Last())}");




public static class Enumerable
{
    // Extension block
    extension<TSource>(IEnumerable<TSource> source) // extension members for IEnumerable<TSource>
        where TSource : Person
    {
        // Extension property:
        public bool IsEmpty => !source.Any();

        public TSource Second
        {
            get
            {
                using var enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                    throw new IndexOutOfRangeException("No elements in the collection.");
                if (!enumerator.MoveNext())
                    throw new IndexOutOfRangeException("No elements in the collection.");
                return enumerator.Current;
            }
        }

        // Extension method:
        public IEnumerable<TSource> Only(Func<TSource, bool> predicate)
            => source.Where(predicate);

        public IEnumerable<TSource> OnlyActives()
            => source.Only(p => p.IsActive);
    }

    extension(Person person)
    {
        // Extension method:
        public string Status => person.IsActive
            ? "Active"
            : "Inactive";

        // Extension method:
        public string ToJson()
            => JsonSerializer.Serialize(
                person,
                new JsonSerializerOptions()
                {
                    WriteIndented = true,
                });
    }

    // extension block, with a receiver type only
    extension(Person) // static extension members for Person
    {
        // static extension method:
        public static bool ExactlyTheSame(Person left, Person right)
        {
            return left.Name == right.Name
                && left.Country == right.Country;
        }

        // static extension property:
        public static Person Empty => new Person(string.Empty, string.Empty, false);
    }
}


public class Person(string Name, string Country, bool IsActive = true)
{
    public string Name { get; private set; } = Name;
    public string Country { get; private set; } = Country;
    public bool IsActive { get; private set; } = IsActive;
}
