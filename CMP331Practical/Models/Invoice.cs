using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP331Practical.Models
{
    public class Invoice : BaseEntity
    {
        public float AmountDue { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DueDate { get; set; }
        public string PropertyId { get; set; }

        public Invoice(float AmountDue, bool IsPaid, DateTime DueDate, string PropertyId)
        {
            this.AmountDue = AmountDue;
            this.IsPaid = IsPaid;
            this.DueDate = DueDate;
            this.PropertyId = PropertyId;
        }
    }
}
