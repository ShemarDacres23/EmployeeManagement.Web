using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models
{
    public class LeaveApplication : ApprovalActivity
    {
        public int Id { get; set; }
        [Display(Name = "Employee Number")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Number of Leave Days")]
        public int NoOfDays { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date  ")]
        public DateTime EndDate { get; set; }

        public int DurationId { get; set; }
        public SystemCodeDetail Duration { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public string? Attachment { get; set; }
        public string Description { get; set; }

        [Display(Name = "Application Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail ApplicationStatus { get; set; }
    }
}

