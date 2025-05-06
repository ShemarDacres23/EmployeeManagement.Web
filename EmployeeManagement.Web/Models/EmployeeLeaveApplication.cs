using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models
{
    public class EmployeeLeaveApplication
    {
        public int Id { get; set; }

        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Display(Name = "Leave Type")]
        [Required]
        public string LeaveType { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "No. Leave Days")]
        public int NumberOfDays { get; set;  }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending"; // Default value
    }
}
