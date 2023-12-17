using RealEstate.Models.DatabaseModels;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.ViewModels
{
    public class InquiryList : Inquiry
    {
        public string OfferName { get; set; }


        public InquiryList(Inquiry inquiry, string offerName)
        {
            this.Id= inquiry.Id;
            this.IdOffer = inquiry.IdOffer;
            this.IdUser = inquiry.IdUser;
            this.Name = inquiry.Name;
            this.Surname = inquiry.Surname;
            this.PhoneNumber = inquiry.PhoneNumber;
            this.Email = inquiry.Email;
            this.AdditionalInformation = inquiry.AdditionalInformation;
            this.DateTimeSent = inquiry.DateTimeSent;
            this.OfferName = offerName;
        }
    }
}
