using System.Collections.Generic;

namespace Models.PersonSearch
{
    public class Person
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public byte[] Photo { get; set; }

        public ICollection<string> Interests { get; set; }
    }
}