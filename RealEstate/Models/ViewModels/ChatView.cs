using Org.BouncyCastle.Asn1.Crmf;
using RealEstate.Models.DatabaseModels;

namespace RealEstate.Models.ViewModels
{
    public class ChatView : Chat
    {
        public User? Sender { get; set; }
        public User OtherUser { get; set; }
        public Message? LastMessage { get; set; }
        public ChatView(Chat chat, User otherUser, Message? lastMessage, User? recipent = null)
        {
            this.Id = chat.Id;
            this.IdUser1 = chat.IdUser1;
            this.IdUser2 = chat.IdUser2;
            this.IdInquiry = chat.IdInquiry;
            this.Subject = chat.Subject;
            this.IdLastMessage = chat.IdLastMessage;
            this.CreatedAt = chat.CreatedAt;
            this.OtherUser = otherUser;
            this.LastMessage = lastMessage;
        }

        public static int OrderByDate(ChatView x, ChatView y)
        {
            DateTime xd = x.LastMessage != null ? x.LastMessage.DateTimeSentUtc : x.CreatedAt;
            DateTime yd = y.LastMessage != null ? y.LastMessage.DateTimeSentUtc : y.CreatedAt;

            return yd.CompareTo(xd);
        }
    }
}
