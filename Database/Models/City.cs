using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace DataAccess.Models
{
    public class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public int CityId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int StateId { get; set; }


        public virtual ICollection<Address> Addresses { get; set; }

        public virtual State State { get; set; }
    }
}