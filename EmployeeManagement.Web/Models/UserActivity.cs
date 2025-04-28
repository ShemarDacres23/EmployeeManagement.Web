namespace EmployeeManagement.Web.Models
{
    public class UserActivity
    {
        public string? CreatedById { get; set; } //? means nullable
        public DateTime CreatedOn { get; set; }

        public string? ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class ApprovalActivity: UserActivity
    {
        public string? ApprovedId { get; set; } //? means nullable
        public DateTime ApprovedOn { get; set; }

    }
}
