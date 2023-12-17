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
            User user = this.ViewBag.User;

            if (user.Type == 'u')
            {
                this.FavouriteOffers();
                return View();
            }

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

            users.Take(Math.Min(users.Count, 20));
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
        public IActionResult Messages(List<MessageView>? messages = null)
        {
            if (messages.Count != 0)
            {
                this.ViewBag.Messages = messages;
                return View();
            }

            User user = this.ViewBag.User;
            var data = this._context.Messages!.Where(message => message.IdSender == user.Id || message.IdRecipient == user.Id).ToList();
            messages = new(50);

            foreach (Message mess in data)
            {
                MessageView? foundMess = messages.FirstOrDefault(found => found.OtherUser.Id == mess.IdRecipient || found.OtherUser.Id == mess.IdSender);
                if (foundMess == null)
                {
                    int otherUserId = mess.IdSender == user.Id ? mess.IdRecipient : mess.IdSender;
                    User otherUser = this._context.Users.Find(otherUserId)!;
                    messages.Add(new MessageView(mess, otherUser!));
                    continue;
                }
                if(mess.DateTimeSentUtc > foundMess.DateTimeSentUtc)
                {
                    int index = messages.IndexOf(foundMess);
                    messages[index] = foundMess;
                }
            }

            messages = messages.OrderByDescending(mess => mess.DateTimeSentUtc).ToList();
            this.ViewBag.Messages = messages;

            return View();
        }

        [Authorize]
        public IActionResult GetChatMessages(List<MessageView> messages, int idUser)
        {
            List<MessageView> chatMessages = new(30);
            User user = this.ViewBag.User;

            this._context.Messages!.Where(mess => (mess.IdSender == idUser || mess.IdRecipient == idUser) && (mess.IdSender == user.Id || mess.IdRecipient == user.Id)).ForEachExt(mess =>
            {
                User? user = this._context.Users.Find(mess.IdSender);
                chatMessages.Add(new MessageView(mess, user!));
            });

            this.ViewBag.Messages = chatMessages;

            return RedirectToAction("Messages", new { messages });
        }
    }
}