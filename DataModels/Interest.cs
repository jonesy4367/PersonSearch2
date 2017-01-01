using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Interest
    {
        public int InterestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public virtual ICollection<Person> People { get; set; }
    }
}