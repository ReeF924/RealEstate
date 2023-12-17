using RealEstate.Models.DatabaseModels;

namespace RealEstate.Models.ViewModels
{
    public class MessageView : Message
    {
        public User OtherUser { get; set; }

        public MessageView(Message message, User otherUser)
        {
            this.Id = message.Id;
            this.IdSender = message.IdSender;
            this.IdRecipient = message.IdRecipient;
            this.IdInquiry = message.IdInquiry;
            this.Text = message.Text;
            this.DateTimeSentUtc = message.DateTimeSentUtc;
            this.IsRead = message.IsRead;
            this.OtherUser = otherUser;
        }
    }
}
