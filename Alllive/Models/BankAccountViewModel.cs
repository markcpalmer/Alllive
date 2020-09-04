using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public class BankAccountViewModel
    {
        public string UserId { get; set; }
        public string UserAccountId { get; set; }
        public string StripeAccountId { get; set; }
        public string AccountName { get; set; }
    }
}