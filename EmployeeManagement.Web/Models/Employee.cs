using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models
{
    public class Employee : UserActivity
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
