//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alllive.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserAccount
    {
        public int UserAccountID { get; set; }
        public int UserID { get; set; }
        public string BankReference { get; set; }
        public string AccountName { get; set; }
        public string Customer { get; set; }
        public string CardType { get; set; }
        public string StripeAccountID { get; set; }
    }
}
