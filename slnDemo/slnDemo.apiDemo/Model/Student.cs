using System.ComponentModel.DataAnnotations;
namespace slnDemo.apiDemo
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
