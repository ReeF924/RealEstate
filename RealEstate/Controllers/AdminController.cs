using Microsoft.AspNetCore.Mvc;
using RealEstate.Attributes;
using RealEstate.Models.ViewModels;
using RealEstate.Models.DatabaseModels;

namespace RealEstate.Controllers
{
    public class AdminController : BaseEstateController
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Offers()
        {
            var offers = this._context.Offers!.ToList();
            List<OfferList> listOffers = new();


            int n = offers.Count();
            for (int i = 0; i < Math.Min(20, n); i++)
            {
                Offer offer = offers[i];

                string brokerUsername = offer.IdBroker != null ? this._context.Users!.FirstOrDefault(user => user.Id == offer.IdBroker)!.Username : "No broker assigned";
                int offersMade = this._context.Inquiries.Where(inquiry => inquiry.IdOffer == offer.Id).Count();
                listOffers.Add(new OfferList(offer, brokerUsername, offersMade));
            }

            this.ViewBag.Offers = listOffers;

            return View();
        }

        [Authorize(false)]
        public IActionResult Users()
        {
            var users = this._context.Users!.Where(users => users.Type != 'a').ToList();
            User user = (User)this.ViewBag.User;

            if (user.Type == 'b')
            {
                users = users.Where(user => user.Type == 'u').ToList();
            }

            users.Take(Math.Min(users.Count, 20));
            this.ViewBag.Users = users;

            return View();
        }
    }
}
