using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required]
        [MaxLength(200)]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        public int CityId { get; set; }


        public virtual ICollection<Person> People { get; set; }

        public virtual City City { get; set; }
    }
}