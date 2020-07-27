﻿using Alllive.Models;
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
        // GET: Billing
        public ActionResult AccountList()
        {
            var accounts = Dc.UserAccounts.Where(a => a.UserID == currentUser.UserId);

            return PartialView(accounts);
        }
        public ActionResult AddAccount()
        {
            var m = new PaymentMethodViewModel() { UserID = currentUser.UserId };
            return View(m);
        }

        [HttpGet]
        [Route("create-payment-intent")]
        public ActionResult CheckOut(PaymentIntentCreateRequest request)
        {
            return View();
            //StripeConfiguration.ApiKey = "sk_test_51H4bEIAVJDEhYcbP8AniC54IhmNxi8AOAkQpTgSCdwJjXwd8eoYEZmpBdZPOn7mpkBhQWkuzYYIFUv1y8Y3ncnKO008t1vsMSK";

            //var paymentIntents = new PaymentIntentService();
            //var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            //{
            //    Amount = CalculateOrderAmount(request.Items),
            //    Currency = "usd",
            //});

            //return Json(new { clientSecret = paymentIntent.ClientSecret },JsonRequestBehavior.AllowGet);
        }
        private int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 1400;
        }
        public ActionResult CreatePaymentMethod(PaymentMethodViewModel m)
        {
            m.AccountName = m.CardType + " ending in " + m.CardNumber.Substring(m.CardNumber.Length - 4);
            StripeConfiguration.ApiKey = "sk_test_51H4bEIAVJDEhYcbP8AniC54IhmNxi8AOAkQpTgSCdwJjXwd8eoYEZmpBdZPOn7mpkBhQWkuzYYIFUv1y8Y3ncnKO008t1vsMSK";
            var paymentMethodService = new PaymentMethodService();
            var paymentMethod = paymentMethodService.Create(new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardCreateOptions
                {
                    Number = m.CardNumber,
                    Cvc = m.CvcCode,
                    ExpMonth = m.ExpMonth,
                    ExpYear = m.ExpYear
                }

            });
            var userAccount = new UserAccount()
            {
                AccountName = m.AccountName,
                BankReference = paymentMethod.Id,
                UserID = currentUser.UserId
            };
            Dc.UserAccounts.Add(userAccount);
            Dc.SaveChanges();
            return RedirectToAction("ViewProfile", "User");

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