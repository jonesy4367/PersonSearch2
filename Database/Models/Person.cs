using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace DataAccess.Models
{
    public class Person
    {
        public Person()
        {
            Interests = new HashSet<Interest>();
        }

        public int PersonId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public short Age { get; set; }

        public int AddressId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImagePath { get; set; }


        public virtual Address Address { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }
    }
}