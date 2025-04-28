using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models
{
    public class EmployeeSalary
    {
        public int Id { get; set; }

        [Display(Name = "Employee ID Number")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Basic Salary")]
        public decimal BasicSalary { get; set; }

        [Display(Name = "Bonus")]
        public decimal Bonus { get; set; }

        // Automatically calculate deductions based on salary rates
        private decimal _eduTaxRate = 0.0225m;  // 2.25% Education Tax
        private decimal _nisRate = 0.03m;       // 3% National Insurance Scheme
        private decimal _nhtRate = 0.03m;       // 3% National Housing Trust

        [Display(Name = "Education Tax (EduTax) Fee")]
        public decimal EduTax
        {
            get
            {
                return (BasicSalary + Bonus) * _eduTaxRate;
            }
        }

        [Display(Name = "National Insurance Scheme (NIS) Fee")]
        public decimal NIS
        {
            get
            {
                return (BasicSalary + Bonus) * _nisRate;
            }
        }

        [Display(Name = "National Housing Trust (NHT) Fee")]
        public decimal NHT
        {
            get
            {
                return (BasicSalary + Bonus) * _nhtRate;
            }
        }


        [Display(Name = "Total Deductions")]
        public decimal TotalDeductions
        {
            get
            {
                return EduTax + NIS + NHT;
            }
        }

        [Display(Name = "Total Salary")]
        public decimal TotalSalary
        {
            get
            {
                return BasicSalary + Bonus - TotalDeductions;
            }
        }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        public string Description { get; set; }
    }
}
