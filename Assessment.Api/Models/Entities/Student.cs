using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Api.Models.Entities
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Column("DOB", TypeName = "date")]
        public DateTime DOB { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal GPA { get; set; }
    }
}
