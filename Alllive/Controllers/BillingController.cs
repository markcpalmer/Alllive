using Alllive.Models;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alllive.Controllers
{
    public class BillingController : AllLiveControllerBase
    {
        public ActionResult Index()
        {
            //TODO: need to save customer
            var options = new SetupIntentCreateOptions
            {
                Customer = createACustomer()
            };
            var service = new SetupIntentService();
            var intent = service.Create(options);
            ViewData["ClientSecret"] = intent.ClientSecret;
            return View();

        }
        public string createACustomer()
        {
            StripeConfiguration.ApiKey = "sk_test_51H4bEIAVJDEhYcbP8AniC54IhmNxi8AOAkQpTgSCdwJjXwd8eoYEZmpBdZPOn7mpkBhQWkuzYYIFUv1y8Y3ncnKO008t1vsMSK";

            var options = new CustomerCreateOptions { };
            var service = new CustomerService();
            var customer = service.Create(options);
            return customer.Id;
        }
        // GET: Billing
        public ActionResult AccountList(UserAccount ua)
        {
            if (ua != null)
            {

            }
            var accounts = Dc.UserAccounts.Where(a => a.UserID == currentUser.UserId);
            //3 = american express/diners club 4= visa 5 = master 6= discover
            return View(accounts);
        }

        [HttpGet]
        public ActionResult AddAccount()
        {
            //if user is not here then send back to login page
            var m = new PaymentMethodViewModel() { UserID = currentUser.UserId };
            return View(m);
        }

        [HttpGet]
        [Route("create-payment-intent")]
        public ActionResult CheckOut(long rate)
        {
            // return View();
            StripeConfiguration.ApiKey = "sk_test_51H4bEIAVJDEhYcbP8AniC54IhmNxi8AOAkQpTgSCdwJjXwd8eoYEZmpBdZPOn7mpkBhQWkuzYYIFUv1y8Y3ncnKO008t1vsMSK";

            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                //Note: need to fix amounts. come up with a calculated amount
                Amount = CalculateOrderAmount(rate),
                Currency = "usd",
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                TransferGroup ="{ORDER10}"
               // Source = 
            };
            var paymentIntentService = new PaymentIntentService();
            var paymentIntents = new PaymentIntentService();

            var transferOptions = new TransferCreateOptions
            {
                Amount = 50,
                Currency = "usd",
                Destination = "{{addStripeAccountID}}",
                TransferGroup = "{ORDER10}",
            };

            var transferService = new TransferService();
            var transfer = transferService.Create(transferOptions);

            var secondTransferOptions = new TransferCreateOptions
            {
                Amount = 50,
                Currency = "usd",
                Destination = "{{addStripeAccountID}}",//NOTE : need to set up account for tutors
                TransferGroup = "{ORDER10}",
            };
            var secondTransfer = transferService.Create(secondTransferOptions);

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        private long CalculateOrderAmount(long rate)
        {
            
            rate *= 100;
            return rate;
        }

        [HttpPost]
        public ActionResult AddAccount(PaymentMethodViewModel m)
        {
            var expMonthInt = Int32.Parse(m.ExpMonth);
            var expYearInt = Int32.Parse(m.ExpYear);
            if (ModelState.IsValid) {
                m.AccountName = m.CardType + " ending in " + m.CardNumber.Substring(m.CardNumber.Length - 4);
                StripeConfiguration.ApiKey = "sk_test_51H4bEIAVJDEhYcbP8AniC54IhmNxi8AOAkQpTgSCdwJjXwd8eoYEZmpBdZPOn7mpkBhQWkuzYYIFUv1y8Y3ncnKO008t1vsMSK";
                var paymentMethodService = new PaymentMethodService();
                //need to send the first digit of the card number to the account list and user account table
                var paymentMethod = paymentMethodService.Create(new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardCreateOptions
                    {
                        Number = m.CardNumber,
                        Cvc = m.CvcCode,
                        ExpMonth = expMonthInt,
                        ExpYear = expYearInt,
                    }

                });

                ModelState.AddModelError(String.Empty, paymentMethod.Id);

                var options = new CustomerCreateOptions
                {
                    Description = m.AccountName
                };
                var services = new CustomerService();
                var cust=services.Create(options);


                var userAccount = new UserAccount()
                {
                    AccountName = m.AccountName,
                    BankReference = paymentMethod.Id,
                    UserID = currentUser.UserId,
                    Customer = cust.Id
                };
                Dc.UserAccounts.Add(userAccount);
                Dc.SaveChanges();

                return RedirectToAction("AccountList", "Billing", userAccount);//redirects user to different action"                    
            }
            return View(m);

        }
        public class Item
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }
        
    }
}