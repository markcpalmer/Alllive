using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public class PaymentMethodViewModel
    {
        public string AccountName { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string NameOnCard { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CvcCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserID { get; set; }


    }
}