using System;
using UnityEngine;
using AmarilisLib;

public class LensExample : MonoBehaviour
{
    class Address
    {
        public string Country { get; }
        public string City { get; }

        public Address(string country, string city)
        {
            City = city;
        }

        public Address WithCity(string city)
        {
            return new Address(Country, city);
        }
    }

    class Person
    {
        public string Name { get; }
        public int Age { get; }
        public Address Address { get; }

        public Person(string name, int age, Address address)
        {
            Name = name;
            Age = age;
            Address = address;
        }

        public Person WithAddress(Address address)
        {
            return new Person(Name, Age, address);
        }
    }

    // Lens usage example 1 : Operations on immutable objects
    public void Example1()
    {
        // Lens to address property
        var addressLens = new Lens<Person, Address>(
            getter: person => person.Address,
            setter: (person, newAddress) => person.WithAddress(newAddress)
        );

        // Lens to City property
        var cityLens = new Lens<Address, string>(
            getter: address => address.City,
            setter: (address, newCity) => address.WithCity(newCity)
        );

        // Combine Lens to Person's City property
        var personCityLens = addressLens.Compose(cityLens);

        // Generate data
        var person = new Person("Syunta", 30, new Address("Japan", "Aichi"));

        // Get City
        Debug.Log(personCityLens.Get(person));

        // Update City
        var updatedPerson = personCityLens.Set(person, "Tokyo");
        Debug.Log(personCityLens.Get(updatedPerson));

        // Check that the original data has not changed
        Debug.Log(personCityLens.Get(person));
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Lens usage example 1"))
        {
            Example1();
        }
    }
}