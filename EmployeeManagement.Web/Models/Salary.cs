using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Web.Models
{
    public class Salary : UserActivity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal BasicSalary { get; set; }

        [Precision(18, 2)]
        public decimal Overtime { get; set; }

        // Deductions calculated from BasicSalary
        [Precision(18, 2)]
        public decimal NIS
        {
            get
            {
                return Math.Round(BasicSalary * 0.03m, 2); // 3%
            }
        }

        [Precision(18, 2)]
        public decimal NHT
        {
            get
            {
                return Math.Round(BasicSalary * 0.02m, 2); // 2%
            }
        }

        [Precision(18, 2)]
        public decimal EducationTax
        {
            get
            {
                return Math.Round(BasicSalary * 0.0225m, 2); // 2.25%
            }
        }

        // Total deductions = fixed deduction + statutory deductions
        public decimal TotalDeductions
        {
            get
            {
                return NIS + NHT + EducationTax + Deduction;
            }
        }

        // Manual/other deduction
        [Precision(18, 2)]
        public decimal Deduction { get; set; }

        [Precision(18, 2)]
        public decimal NetPay
        {
            get
            {
                return (BasicSalary + Overtime) - TotalDeductions;
            }
        }

        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}

