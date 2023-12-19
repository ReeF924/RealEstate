using RealEstate.Models.DatabaseModels;

namespace RealEstate.Models.ViewModels
{
    public class MessageView : Message
    {
        public User Sender { get; set; }
        public MessageView(Message message, User sender)
        {
            this.Id = message.Id;
            this.IdChat = message.IdChat;
            this.IdUser = message.IdUser;
            this.Text = message.Text;
            this.IsRead = message.IsRead;
            this.DateTimeSentUtc = message.DateTimeSentUtc;
            this.Sender = sender;
        }
    }
}
