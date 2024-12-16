using System;
using System.Collections;
using System.Collections.Generic;

public class Animal : IComparable<Animal>
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }

    // Конструктор для ініціалізації об'єкта
    public Animal(string name, double weight, double height)
    {
        Name = name;
        Weight = weight;
        Height = height;
    }

    // Реалізація методу CompareTo для порівняння за вагою
    public int CompareTo(Animal other)
    {
        if (other == null) return 1;
        return this.Weight.CompareTo(other.Weight);
    }
}

public class AnimalComparer : IComparer<Animal>
{
    // Порівняння за вагою і зростом
    public int Compare(Animal x, Animal y)
    {
        if (x == null || y == null)
            return 0;

        // Спочатку порівнюємо за вагою
        int weightComparison = x.Weight.CompareTo(y.Weight);

        // Якщо вага однакова, порівнюємо за зростом
        if (weightComparison == 0)
        {
            return x.Height.CompareTo(y.Height);
        }

        return weightComparison;
    }
}

public class AnimalCollection : IEnumerable<Animal>
{
    private List<Animal> animals;

    public AnimalCollection()
    {
        animals = new List<Animal>();
    }

    public void Add(Animal animal)
    {
        animals.Add(animal);
    }

    // Реалізація методу GetEnumerator для IEnumerable
    public IEnumerator<Animal> GetEnumerator()
    {
        return animals.GetEnumerator();
    }

    // Потрібно для сумісності з .NET Framework
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    // Метод для сортування за вагою
    public void SortByWeight()
    {
        animals.Sort();
    }

    // Метод для сортування за вагою та зростом
    public void SortByWeightAndHeight(IComparer<Animal> comparer)
    {
        animals.Sort(comparer);
    }
}

class Program
{
    static void Main()
    {
        // Створення колекції тварин
        AnimalCollection animalCollection = new AnimalCollection();

        // Додавання тварин
        animalCollection.Add(new Animal("Elephant", 5000, 3.5));
        animalCollection.Add(new Animal("Tiger", 250, 1.1));
        animalCollection.Add(new Animal("Lion", 190, 1.2));
        animalCollection.Add(new Animal("Rabbit", 2, 0.3));

        // Сортуємо тварин за вагою
        animalCollection.SortByWeight();

        // Виведення списку тварин після сортування
        Console.WriteLine("Sorted by Weight:");
        foreach (var animal in animalCollection)
        {
            Console.WriteLine($"Name: {animal.Name}, Weight: {animal.Weight}, Height: {animal.Height}");
        }

        // Створення компаратора для порівняння за вагою та зростом
        var comparer = new AnimalComparer();

        // Додавання додаткових тварин
        animalCollection.Add(new Animal("Giraffe", 800, 5.0));
        animalCollection.Add(new Animal("Zebra", 350, 1.4));

        // Сортування за вагою та зростом
        animalCollection.SortByWeightAndHeight(comparer);

        // Виведення списку тварин після сортування за вагою та зростом
        Console.WriteLine("\nSorted by Weight and Height:");
        foreach (var animal in animalCollection)
        {
            Console.WriteLine($"Name: {animal.Name}, Weight: {animal.Weight}, Height: {animal.Height}");
        }
    }
}
