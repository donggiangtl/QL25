using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeManager.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [ForeignKey(""Class"")]
        public int ClassID { get; set; }

        public virtual Class Class { get; set; }
    }
}
