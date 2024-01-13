using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static School.Static_Data.SD;

namespace School.Models
{
    public class ExamResult
    {
       
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ExamId { get; set; }
        public Student Student { get; set; }
        public Exam Exam { get; set; }
        public Grade Grade { get; set; }
    }
}
