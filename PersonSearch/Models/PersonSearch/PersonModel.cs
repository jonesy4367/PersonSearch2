using System.Collections.Generic;

namespace PersonSearch.Models.PersonSearch
{
    public class PersonModel
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public short Age { get; set; }

        public byte[] Photo { get; set; }

        public ICollection<string> Interests { get; set; }
    }
}