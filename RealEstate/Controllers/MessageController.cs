using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Models.DatabaseModels;

namespace RealEstate.Controllers
{
    public class MessageController : BaseEstateController
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<Message> GetMessages(int userId, int? count)
        {
            List<Message> messages = this._context.Messages!.Where(m => m.IdSender == userId || m.IdRecipient == userId).Take(10).ToList();
            messages.ForEach(message => message.OtherUser = this._context.Users!.Find(message.IdSender == userId ? message.IdRecipient : message.IdSender)!);

            messages = count != null ? messages.Take(count.Value).ToList() : messages;


            return messages;
        }   
    }
}
