using Microsoft.AspNetCore.Mvc;
using RealEstate.Controllers;
using RealEstate.Models.DatabaseModels;

namespace RealEstate.Components
{
    public class NewMessagesComponent : ViewComponent
    {
        public IViewComponentResult Invoke(User user)
        {
            this.ViewBag.User = user;
            MessageController messageController = new MessageController();
            this.ViewBag.Messages = messageController.GetMessages(user.Id, 4);

            return View();
        }

    }
}
