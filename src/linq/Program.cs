public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }

    public Person()
    {
        Name = "";
        Age = 0;
        City = "";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30, City = "New York" },
            new Person { Name = "Bob", Age = 25, City = "London" },
            new Person { Name = "Charlie", Age = 35, City = "New York" },
            new Person { Name = "David", Age = 25, City = "Paris" },
            new Person { Name = "Eve", Age = 40, City = "London" }
        };
        var oldPeopleQuery = from p in people
                             where p.Age > 30
                             select p;

        foreach (var p in oldPeopleQuery)
        {
            Console.WriteLine($"- {p.Name}, {p.Age}");
        }

        var oldPeopleMethod = people.Where(p => p.Age > 30);
        foreach (var p in oldPeopleMethod)
        {
            Console.WriteLine($"- {p.Name}, {p.Age}");
        }

        // Ordering
        // Query Syntax
        var sortedPeopleQuery = from p in people
                                orderby p.Age ascending, p.Name descending
                                select p;

        Console.WriteLine("\nSorted People (Query Syntax):");
        foreach (var p in sortedPeopleQuery)
        {
            Console.WriteLine($"- {p.Name}, {p.Age}, {p.City}");
        }

        // Method Syntax
        Console.WriteLine("\nSorted People (Method Syntax):");
        var sortedPeopleMethod = people.OrderBy(p => p.Age).ThenByDescending(p => p.Name);
        foreach (var p in sortedPeopleMethod)
        {
            Console.WriteLine($"- {p.Name}, {p.Age}, {p.City}");
        }

        // Projection (Select)
        // Query Syntax (creates an anonymous type)
        var namesAndCitiesQuery = from p in people
                                  select new { p.Name, p.City };
        Console.WriteLine("\nNames and Cities (Query Syntax):");
        foreach (var item in namesAndCitiesQuery)
        {
            Console.WriteLine($"- Name: {item.Name}, City: {item.City}");
        }

        // Method syntax (creates an anonymous type)
        var namesAndCityMethod = people.Select(p => new { p.Name, p.City });
        Console.WriteLine("\nNames and Cities (Method Syntax):");
        foreach (var item in namesAndCityMethod)
        {
            Console.WriteLine($"- Name: {item.Name}, City: {item.City}");
        }

        // Grouping (GroupBy)
        // Query Syntax
        var peopleByCityQuery = from p in people
                                group p by p.City into cityGroup
                                select cityGroup;
        Console.WriteLine("\nPeople by City (Query Syntax):");
        foreach (var group in peopleByCityQuery)
        {
            Console.WriteLine($" - City: {group.Key}");
            foreach (var person in group)
            {
                Console.WriteLine($" -- {person.Name}");
            }
        }
        // Method syntax
        var peopleByCityMethod = people.GroupBy(p => p.City);
        Console.WriteLine("\nPeople by City (Method Syntax):");
        foreach (var group in peopleByCityMethod)
        {
            Console.WriteLine($" - City: {group.Key}");
            foreach (var person in group)
            {
                Console.WriteLine($" -- {person.Name}");
            }
        }

        // Joining (Join)

        List<Pet> pets = new List<Pet>()
        {
            new Pet {Name = "Buddy", OwnerName = "Alice"}
            ,
            new Pet {Name = "Lucy", OwnerName = "Bob"},
            new Pet {Name = "Max", OwnerName = "Alice"},
            new Pet {Name = "Daisy", OwnerName = "Charlie"}
        };

        // Join people with their pets
        // query syntax
        var peopleWithPetsQuery = from p in people
                                  join pet in pets on p.Name equals pet.OwnerName
                                  select new { PersonName = p.Name, PetName = pet.Name };
        Console.WriteLine("\nPeople with Pets (Query Syntax):");
        foreach (var item in peopleWithPetsQuery)
        {
            Console.WriteLine($"- {item.PersonName} owns {item.PetName}");
        }

        // Method Syntax
        var peopleWithPetsMethod = people.Join(
            pets,
            person => person.Name,
            pet => pet.OwnerName,
            (person, pet) => new { PersonName = person.Name, PetName = pet.Name });

        Console.WriteLine("\nPeople with Pets (Method Syntax):");
        foreach (var item in peopleWithPetsMethod)
        {
            Console.WriteLine($"- {item.PersonName} owns {item.PetName}");
        }
    }
}

public class Pet
{
    public string Name { get; set; }
    public string OwnerName { get; set; }
}