using System.Collections.Generic;
using System.Data.Entity;
using DataAccess.Models;

namespace DataAccess
{
    public class PersonDbInitializer : DropCreateDatabaseAlways<PersonContext>
    {
        private Person _bobLawblaw;
        private Person _steveSchmeve;
        private Person _pizzaMcCheeseface;
        private Person _lemonjelloPuddin;
        private Person _johnSmith;

        private Address _123Street;
        private Address _456Avenue;
        private Address _789Boulevard;
        private Address _437Street;

        private City _saltLakeCity;
        private City _nashville;
        private City _logan;

        private State _utah;
        private State _tennessee;

        private Interest _coffee;
        private Interest _tacos;
        private Interest _bearWrangling;
        private Interest _tvAppearences;
        private Interest _playingDead;

        protected override void Seed(PersonContext context)
        {
            base.Seed(context);

            GenerateData();

            context.People.Add(_bobLawblaw);
            context.People.Add(_steveSchmeve);
            context.People.Add(_pizzaMcCheeseface);
            context.People.Add(_lemonjelloPuddin);
            context.People.Add(_johnSmith);

            context.Addresses.Add(_123Street);
            context.Addresses.Add(_456Avenue);
            context.Addresses.Add(_789Boulevard);
            context.Addresses.Add(_437Street);

            context.Cities.Add(_saltLakeCity);
            context.Cities.Add(_nashville);
            context.Cities.Add(_logan);

            context.States.Add(_utah);
            context.States.Add(_tennessee);

            context.Interests.Add(_coffee);
            context.Interests.Add(_tacos);
            context.Interests.Add(_bearWrangling);
            context.Interests.Add(_tvAppearences);
            context.Interests.Add(_playingDead);
        }

        private void GenerateData()
        {
            BuildPeople();
            BuildAddresses();
            BuildCities();
            BuildStates();
            BuildInterests();

            // People
            _bobLawblaw.Address = _123Street;
            _bobLawblaw.Interests.Add(_coffee);

            _steveSchmeve.Address = _456Avenue;
            _steveSchmeve.Interests.Add(_coffee);
            _steveSchmeve.Interests.Add(_tacos);

            _pizzaMcCheeseface.Address = _123Street;
            _pizzaMcCheeseface.Interests.Add(_bearWrangling);
            _pizzaMcCheeseface.Interests.Add(_playingDead);

            _lemonjelloPuddin.Address = _789Boulevard;
            _lemonjelloPuddin.Interests.Add(_tvAppearences);

            _johnSmith.Address = _437Street;
            //_johnSmith.Interests.Add(_playingDead);

            // Addresses
            _123Street.City = _saltLakeCity;
            _123Street.People.Add(_bobLawblaw);

            _456Avenue.City = _nashville;
            _456Avenue.People.Add(_steveSchmeve);

            _789Boulevard.City = _nashville;
            _789Boulevard.People.Add(_lemonjelloPuddin);

            _437Street.City = _logan;
            _437Street.People.Add(_johnSmith);

            // Cities
            _saltLakeCity.State = _utah;
            _saltLakeCity.Addresses.Add(_123Street);

            _nashville.State = _tennessee;
            _nashville.Addresses.Add(_456Avenue);
            _nashville.Addresses.Add(_789Boulevard);

            _logan.State = _utah;
            _logan.Addresses.Add(_437Street);

            // States
            _utah.Cities.Add(_saltLakeCity);
            _utah.Cities.Add(_logan);

            _tennessee.Cities.Add(_nashville);

            // Interests
            _coffee.People.Add(_bobLawblaw);
            _coffee.People.Add(_steveSchmeve);

            _tacos.People.Add(_steveSchmeve);

            _bearWrangling.People.Add(_pizzaMcCheeseface);

            _tvAppearences.People.Add(_lemonjelloPuddin);

            _playingDead.People.Add(_pizzaMcCheeseface);
            //_playingDead.People.Add(_johnSmith);
        }

        private void BuildPeople()
        {
            _bobLawblaw = new Person
            {
                FirstName = "Bob",
                LastName = "Lawblaw",
                Age = 43,
                ImageFileName = "WIN_20160814_11_02_20_Pro.jpg",
                Interests = new List<Interest>()
            };

            _steveSchmeve = new Person
            {
                FirstName = "Steve",
                LastName = "Schmeve",
                Age = 36,
                Interests = new List<Interest>()
            };

            _pizzaMcCheeseface = new Person
            {
                FirstName = "Pizza",
                LastName = "McCheeseface",
                Age = 102,
                ImageFileName = "55059238.jpg",
                Interests = new List<Interest>()
            };

            _lemonjelloPuddin = new Person
            {
                FirstName = "Lemonjello",
                LastName = "Puddin",
                Age = 17,
                Interests = new List<Interest>()
            };

            _johnSmith = new Person
            {
                FirstName = "John",
                LastName = "Smith",
                Age = 52,
                Interests = new List<Interest>()
            };
        }

        private void BuildAddresses()
        {
            _123Street = new Address
            {
                StreetAddress = "123 Street St",
                ZipCode = "83483",
                People = new List<Person>()
            };

            _456Avenue = new Address
            {
                StreetAddress = "456 Avenue Ave",
                ZipCode = "90298",
                People = new List<Person>()
            };

            _789Boulevard = new Address
            {
                StreetAddress = "789 Boulevard Blvd",
                ZipCode = "92018-8908",
                People = new List<Person>()
            };

            _437Street = new Address
            {
                StreetAddress = "437 Street Blvd",
                ZipCode = "38198",
                People = new List<Person>()
            };
        }

        private void BuildCities()
        {
            _saltLakeCity = new City
            {
                Name = "Salt Lake City",
                Addresses = new List<Address>()
            };

            _nashville = new City
            {
                Name = "Nashville",
                Addresses = new List<Address>()
            };

            _logan = new City
            {
                Name = "Logan",
                Addresses = new List<Address>()
            };
        }

        private void BuildStates()
        {
            _utah = new State
            {
                Name = "Utah",
                Abbreviation = "UT",
                Cities = new List<City>()
            };

            _tennessee = new State
            {
                Name = "Tennessee",
                Abbreviation = "TN",
                Cities = new List<City>()
            };
        }

        private void BuildInterests()
        {
            _coffee = new Interest
            {
                Name = "Coffee",
                People = new List<Person>()
            };

            _tacos = new Interest
            {
                Name = "Tacos",
                People = new List<Person>()
            };

            _bearWrangling = new Interest
            {
                Name = "Bear Wrangling",
                People = new List<Person>()
            };

            _tvAppearences = new Interest
            {
                Name = "Appearing in the Background on TV",
                People = new List<Person>()
            };

            _playingDead = new Interest
            {
                Name = "Playing Dead",
                People = new List<Person>()
            };
        }
    }
}
