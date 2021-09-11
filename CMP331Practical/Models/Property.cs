using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP331Practical.Models
{
    public class Property : BaseEntity
    {
        public bool Available { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string MonthlyRent { get; set; }
        public string RequiredMaintainance { get; set; }
        public DateTime QuarterlyInspection { get; set; }
        public DateTime AnnualGasInspection { get; set; }
        public DateTime FiveYearElectricalInspection { get; set; }
        public string MaintainanceStaffId { get; set; }
        public string LettingAgentId { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Type { get; set; }

        public Property() { }
        public Property(bool Available, string AddressLine1, string AddressLine2, string PostCode, string MonthlyRent, string RequiredMaintainance, DateTime QuarterlyInspection, DateTime AnnualGasInspection, DateTime FiveYearElectricalInspection, string MaintainanceStaffId, string LettingAgentId, int Bedrooms, int Bathrooms, string Type)
        {
            this.Available = Available;
            this.AddressLine1 = AddressLine1;
            this.AddressLine2 = AddressLine2;
            this.PostCode = PostCode;
            this.MonthlyRent = MonthlyRent;
            this.RequiredMaintainance = RequiredMaintainance;
            this.QuarterlyInspection = QuarterlyInspection;
            this.AnnualGasInspection = AnnualGasInspection;
            this.FiveYearElectricalInspection = FiveYearElectricalInspection;
            this.MaintainanceStaffId = MaintainanceStaffId;
            this.LettingAgentId = LettingAgentId;
            this.Bedrooms = Bedrooms;
            this.Bathrooms = Bathrooms;
            this.Type = Type;
        }
    }
}
