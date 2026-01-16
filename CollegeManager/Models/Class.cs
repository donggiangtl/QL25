using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollegeManager.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }

        [Required, StringLength(100)]
        public string ClassName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
