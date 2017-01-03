using System.Collections.Generic;
using System.Data.Entity;
using DataAccess.Models;

namespace DataAccess
{
    public class PersonDbInitializer : DropCreateDatabaseAlways<PersonContext>
    {
        private Person _person1;
        private Person _person2;

        private Address _address1;
        private Address _address2;

        private City _city1;
        private City _city2;

        private State _state1;
        private State _state2;

        private Interest _interest1;
        private Interest _interest2;

        protected override void Seed(PersonContext context)
        {
            base.Seed(context);

            GenerateData();

            context.People.Add(_person1);
            context.People.Add(_person2);

            context.Addresses.Add(_address1);
            context.Addresses.Add(_address2);

            context.Cities.Add(_city1);
            context.Cities.Add(_city2);

            context.States.Add(_state1);
            context.States.Add(_state2);

            context.Interests.Add(_interest1);
            context.Interests.Add(_interest2);
        }

        private void GenerateData()
        {
            BuildPeople();
            BuildAddresses();
            BuildCities();
            BuildStates();
            BuildInterests();

            _person1.Address = _address1;
            _person1.Interests.Add(_interest1);

            _person2.Address = _address2;
            _person2.Interests.Add(_interest1);
            _person2.Interests.Add(_interest2);

            _address1.City = _city1;
            _address1.People.Add(_person1);

            _address2.City = _city2;
            _address2.People.Add(_person2);

            _city1.State = _state1;
            _city1.Addresses.Add(_address1);

            _city2.State = _state2;
            _city2.Addresses.Add(_address2);

            _state1.Cities.Add(_city1);
            _state2.Cities.Add(_city2);

            _interest1.People.Add(_person1);
            _interest1.People.Add(_person2);

            _interest2.People.Add(_person2);
        }

        private void BuildPeople()
        {
            _person1 = new Person
            {
                FirstName = "Bob",
                LastName = "Lawblaw",
                Age = 43,
                ImageFileName = "WIN_20160814_11_02_20_Pro.jpg",
                Interests = new List<Interest>()
            };

            _person2 = new Person
            {
                FirstName = "Steve",
                LastName = "Schmevel",
                Age = 36,
                Interests = new List<Interest>()
            };
        }

        private void BuildAddresses()
        {
            _address1 = new Address
            {
                StreetAddress = "123 Street St",
                ZipCode = "83483",
                People = new List<Person>()
            };

            _address2 = new Address
            {
                StreetAddress = "456 Avenue Ave",
                ZipCode = "90298",
                People = new List<Person>()
            };
        }

        private void BuildCities()
        {
            _city1 = new City
            {
                Name = "Salt Lake City",
                Addresses = new List<Address>()
            };

            _city2 = new City
            {
                Name = "Nashville",
                Addresses = new List<Address>()
            };
        }

        private void BuildStates()
        {
            _state1 = new State
            {
                Name = "Utah",
                Abbreviation = "UT",
                Cities = new List<City>()
            };

            _state2 = new State
            {
                Name = "Tennessee",
                Abbreviation = "TN",
                Cities = new List<City>()
            };
        }

        private void BuildInterests()
        {
            _interest1 = new Interest
            {
                Name = "Coffee",
                People = new List<Person>()
            };

            _interest2 = new Interest
            {
                Name = "Tacos",
                People = new List<Person>()
            };
        }
    }
}
