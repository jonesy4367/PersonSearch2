using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace DataAccess.Models
{
    public class Address
    {
        public Address()
        {
            People = new HashSet<Person>();
        }

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