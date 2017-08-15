using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Common;

namespace TNSalesSystem.Domain.Card
{
    public class CardDetails
    {
        public int CardId { get; set; }
        public string CardCode { get; set; }
        public int RegisterId { get; set; }
        public bool IsAAYCard { get; set; }
        public int NoOfCylinders { get; set; }
        public int CityType { get; set; }
        public string CardHolderName { get; set; }
        public int BiggersCount { get; set; }
        public int SmallersCount { get; set; }
        public decimal TotalUnits { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string LandLineNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int District { get; set; }
        public int Taluk { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<CardMember> CardMember { get; set; }
        public IList<string> BiggerName { get; set; }
        public IList<bool> BiggerStatus { get; set; }
        public IList<string> BiggerAge { get; set; }
        public IList<string> BiggerGender { get; set; }
        public IList<string> SmallerName { get; set; }
        public IList<string> SmallerAge { get; set; }
        public IList<string> SmallerGender { get; set; }
        public IList<bool> SmallerStatus { get; set; }
        public int CardType { get; set; }
        public int CreatedBy { get; set; }
        public string Frompage { get; set; }
        public string HolderImage { get; set; }
    }

}
