using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace DataModels
{
    public class Interest
    {
        public Interest()
        {
            People = new HashSet<Person>();
        }

        public int InterestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public virtual ICollection<Person> People { get; set; }
    }
}