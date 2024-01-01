using Microsoft.AspNetCore.Mvc;
using RealEstate.Attributes;
using RealEstate.Models.ViewModels;
using RealEstate.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Models;
using Org.BouncyCastle.Tls.Crypto;


namespace RealEstate.Controllers
{
	public class AdminController : BaseEstateController
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);
			this.ViewBag.FooterVisible = false;
		}

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

		[Authorize(usersAllowed: false)]
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

		public IActionResult ChangeUserType(int idUser)
		{
			User user = this._context.Users!.Find(idUser)!;

			user.Type = user.Type == 'u' ? 'b' : 'u';
			this._context.SaveChanges();

			return RedirectToAction("Users");
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
		//[HttpGet]
		public IActionResult Messages(int idChat = 0)
		{
			User user = this.ViewBag.User;

			var data = this._context.Chats.ToList();

			if (user.Type != 'a')
			{
				data = data.Where(chat => chat.IdUser1 == user.Id || chat.IdUser2 == user.Id).ToList();
			}



			List<ChatView> chats = new();

			if (data.Count == 0) return View();

			foreach (Chat item in data)
			{
				int idOther = item.IdUser1 == user.Id ? item.IdUser2 : item.IdUser1;
				User otherUser = this._context.Users.Find(idOther)!;
				Message? message = this._context.Messages!.Find(item.IdLastMessage);
				chats.Add(new ChatView(item, otherUser, message));
			}

			if (user.Type == 'a')
			{
				chats.ForEach(chatView =>
				{
					if (chatView.IdUser1 == user.Id)
						chatView.Sender = this._context.Users!.Find(chatView.IdUser1)!;
					else
						chatView.Sender = this._context.Users!.Find(chatView.IdUser2)!;
				});
			}

			chats.Sort(ChatView.OrderByDate);
			this.ViewBag.Chats = chats;

			if (chats.Count == 0)
			{
				this.ViewBag.Messages = new List<MessageView>();
			}

			int selected = idChat != 0 ? chats.FindIndex(chat => chat.Id == idChat) : 0;
			Chat chat = chats[selected];

			var dataMessages = this._context.Messages!.Where(mess => mess.IdChat == chat.Id).ToList();
			List<MessageView> messages = new List<MessageView>();
			List<User> chatters = new List<User>();

			dataMessages.ForEach(mess =>
			{
				User? sender = chatters.Find(user => user.Id == mess.IdUser);

				if (sender == null)
				{
					sender = this._context.Users!.Find(mess.IdUser)!;
					chatters.Add(sender);
				}
				messages.Add(new MessageView(mess, sender));
			});


			this.ViewBag.Messages = messages;
			this.ViewBag.Selected = selected;

			return View();
		}

		[Authorize]
		[HttpPost]
		public IActionResult SendMessage(int idChat, StringInputModel input)
		{
			var message = new Message()
			{
				IdChat = idChat,
				IdUser = this.ViewBag.User.Id,
				Text = input.Value,
				IsRead = false,
				DateTimeSentUtc = DateTime.Now
			};
			Chat chat = this._context.Chats!.Find(idChat)!;

			this._context.Messages!.Add(message);
			this._context.SaveChanges();

			chat.IdLastMessage = message.Id;

			this._context.SaveChanges();

			return RedirectToAction("Messages", new { idChat });
		}

		[Authorize]
		[HttpPost]
		public IActionResult NewChat(StringInputModel input)
		{
			User user = this.ViewBag.User;
			User? recipent = this._context.Users!.FirstOrDefault(user => user.Email == input.Value)!;
			recipent = recipent != null ? recipent : this._context.Users!.FirstOrDefault(user => user.Username == input.Value)!;

			if (recipent.Email == user.Email || recipent.Username == user.Username)
			{
				this.ViewBag.Error = "You cannot send a message to yourself";
				return RedirectToAction("Messages");
			}

			if (recipent == null)
			{
				this.ViewBag.Error = "User not found";
				return RedirectToAction("Messages");
			}

			Chat? existing = this._context.Chats!
				.FirstOrDefault(chat => (chat.IdUser1 == user.Id && chat.IdUser2 == recipent.Id) ||
							   (chat.IdUser2 == user.Id && chat.IdUser1 == recipent.Id));

			if (existing != null)
			{
				return RedirectToAction("Messages", new { idChat = existing.Id });
			}

			var chat = new Chat()
			{
				IdInquiry = null,
				IdUser1 = this.ViewBag.User.Id,
				IdUser2 = recipent.Id,
				IdLastMessage = null,
				Subject = null,
				CreatedAt = DateTime.Now
			};

			this._context.Chats!.Add(chat);
			this._context.SaveChanges();

			return RedirectToAction("Messages", new { idChat = chat.Id });
		}

		[Authorize(false, false)]
		public IActionResult Parameters()
		{
			var parameters = this._context.Parameters!.OrderBy(param => param.Value).ToList();
			this.ViewBag.Parameters = parameters;

			return View();
		}

		[Authorize(false, false)]
		public IActionResult AddParameter(StringInputModel input)
		{
			if (input.Value.Length > 30)
				return RedirectToAction("Parameters");

			if(this._context.Parameters!.Any(param => param.Value == input.Value))
			{
				return RedirectToAction("Parameters");
			}

			var parameter = new Parameter()
			{
				Value = input.Value
			};

			this._context.Parameters!.Add(parameter);
			this._context.SaveChanges();

			return RedirectToAction("Parameters");
		}
		[Authorize(false, false)]
		public IActionResult DeleteParameter(int idParameter)
		{
			var parameter = this._context.Parameters!.Find(idParameter);
			this._context.Parameters!.Remove(parameter!);
			this._context.SaveChanges();

			return RedirectToAction("Parameters");
		}
		[Authorize(false)]
		[HttpGet]
		public IActionResult EditOffer(int idOffer)
		{
			User user = this.ViewBag.User;
			Offer offer = this._context.Offers!.Find(idOffer)!;


			var users = this._context.Users.Where(user => user.Type != 'u').OrderBy(user => user.Surname).ToList();
			users.Remove(user);


			List<ParameterView> parameterViews = new();

			this._context.Parameters!.ToList().ForEach(param =>
			{
				OfferParameter? offerParam = this._context.OfferParameters!.Where(o => o.IdParameter == param.Id).FirstOrDefault();

				parameterViews.Add(new(param, offerParam));
			});

			OfferEdit offerEdit = new(offer, parameterViews);

			this.ViewBag.Brokers = users;
			this.ViewBag.Offer = offerEdit;
			return View();
		}
		[Authorize(false)]
		[HttpPost]
		public IActionResult EditOffer(OfferEdit input, int idOffer)
		{
			Offer offer = this._context.Offers!.Find(idOffer)!;
			offer.OfferParameters = this._context.OfferParameters!.Where(o => o.IdOffer == offer.Id).ToList();

			offer.IdBroker = input.IdBroker;
			offer.Name = input.Name;
			offer.Description = input.Description;
			offer.ShortDescription = input.ShortDescription;
			offer.Category = input.Category;
			offer.Price = input.Price;
			offer.EnergyClass = input.EnergyClass;
			offer.Location = input.Location;

			this._context.SaveChanges();
			//input.Parameters.ForEach(param =>
			//{
			


			//})

			return RedirectToAction("Detail", "EstateOffers", new {id = idOffer});

		}

	}
}