using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace DataAccess.Models
{
    public class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public int StateId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string Abbreviation { get; set; }


        public virtual ICollection<City> Cities { get; set; }
    }
}