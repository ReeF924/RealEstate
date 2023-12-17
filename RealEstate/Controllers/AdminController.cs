using Microsoft.AspNetCore.Mvc;
using RealEstate.Attributes;
using RealEstate.Models.ViewModels;
using RealEstate.Models.DatabaseModels;
using System;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;

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
            User user = this.ViewBag.User;

            if (user.Type == 'u')
            {
                this.FavouriteOffers();
                return View();
            }

            var offers = this._context.Offers!.ToList();
            List<OfferList> listOffers = new();


            int n = offers.Count;
            for (int i = 0; i < Math.Min(20, n); i++)
            {
                Offer offer = offers[i];

                string brokerUsername = offer.IdBroker != null ? this._context.Users!.FirstOrDefault(user => user.Id == offer.IdBroker)!.Username : "No broker assigned";
                int offersMade = this._context.Inquiries.Where(inquiry => inquiry.IdOffer == offer.Id).Count();
                listOffers.Add(new OfferList(offer, brokerUsername, offersMade));
            }

            this.ViewBag.Offers = listOffers;
            this.ViewBag.IsAdminView = true;

            return View();
        }
        public void FavouriteOffers()
        {
            User user = (User)this.ViewBag.User;
            var favourites = this._context.FavouriteOffers!.Where(fav => fav.IdUser == user.Id).ToList();
            List<OfferList> offers = new(favourites.Count);
            favourites.ForEach(fav => offers.Add(new OfferList(this._context.Offers!.FirstOrDefault(offer => offer.Id == fav.IdOffer)!)));

            this.ViewBag.Offers = offers;
            this.ViewBag.IsAdminView = false;
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

            users = users.Take(Math.Min(users.Count, 20)).ToList();
            this.ViewBag.Users = users;

            return View();
        }

        [Authorize]
        public IActionResult Inquiries()
        {
            User user = (User)this.ViewBag.User;
            var inquiries = this._context.Inquiries.Where(inquiry => inquiry.IdUser == user.Id).ToList();
            List<InquiryList> inquiriesWithNames = new();


            inquiries.ForEachExt(inquiry =>
            {
                Offer offer = this._context.Offers!.FirstOrDefault(offer => offer.Id == inquiry.IdOffer)!;
                inquiriesWithNames.Add(new InquiryList(inquiry, offer.Name));
            });


            this.ViewBag.Inquiries = inquiriesWithNames;
            return View();
        }

        [Authorize]
        public IActionResult Messages(int selected = 0, int take = 10)
        {
            User user = this.ViewBag.User;

            var data = this._context.Chats
                .Where(chat => chat.IdUser1 == user.Id || chat.IdUser2 == user.Id).ToList();

            List<ChatView> chats = new();

            int counter = 0;
            foreach (Chat item in data)
            {
                int idOther = item.IdUser1 == user.Id ? item.IdUser2 : item.IdUser1;
                User otherUser = this._context.Users.Find(idOther)!;
                Message? message = this._context.Messages!.Find(item.IdLastMessage);
                chats.Add(new ChatView(item, otherUser, message));

                if (counter++ >= take) break;

            }

            chats.Sort(ChatView.OrderByDate);
            this.ViewBag.Chats = chats;

            if (chats.Count > 0)
            {
                Chat chat = chats[selected];
                this.ViewBag.Messages = this._context.Messages!
                .Where(mess => mess.IdChat == chat.Id).ToList();
                this.ViewBag.Selected = selected;
            }
            return View();
        }
    }
}