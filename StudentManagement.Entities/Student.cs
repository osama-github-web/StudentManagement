using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }



        //Relationship
        public int? CountryId { get; set; }

        [NotMapped]
        public Country? Country { get; set; }


        public Student Update(Student student)
        {
            if (!string.IsNullOrWhiteSpace(student.Title))
                this.Title = student.Title;
            if (!string.IsNullOrWhiteSpace(student.Name))
                this.Name = student.Name;
            if (!string.IsNullOrWhiteSpace(student.Email))
                this.Email = student.Email;
            if (student.DOB.HasValue)
                this.DOB = student.DOB;
            if (student.CountryId.HasValue)
                this.CountryId = student.CountryId;

            return this;
        }
    }
}
