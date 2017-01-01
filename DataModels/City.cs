using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class City
    {
        public int CityId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int StateId { get; set; }


        public virtual ICollection<Address> Addresses { get; set; }

        public virtual State State { get; set; }
    }
}