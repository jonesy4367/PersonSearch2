using System.Collections.Generic;

namespace PersonSearchServices.Dtos
{
    public class PersonDto
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public short Age { get; set; }

        public byte[] Photo { get; set; }

        public ICollection<string> Interests { get; set; }
    }
}
