using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using RealEstate.Models;
using System.Diagnostics;

namespace RealEstate.Controllers
{
    public class EstateOffersController : BaseEstateController
    {
        public IActionResult Index(int viewCount = 6, char? filter = null)
        {
            this.ViewBag.ViewCount = viewCount;

            var offers = this._context.Offers?.ToList();
            offers?.ForEach(offer =>
            {
                this._context.Images!.Where(image => image.IdOffer == offer.Id).ForEachExt(image =>
                {
                    offer.Images!.Add(image);
                });
            });

            this.ViewBag.CategoryCounts = offers!.GetCategoryCount();

            if (filter != null)
                offers = offers!.Where(offer => offer.Category == filter).ToList();

            this.ViewBag.Offers = offers;

            Dictionary<string, string> dic = new();
            foreach (var item in HttpContext.Request.Query)
            {
                dic[item.Key] = item.Value;
            }
            foreach (var item in HttpContext.Request.RouteValues)
            {
                dic[item.Key] = item.Value.ToString();
            }

            this.ViewBag.RouteData = dic;
            return View();
        }
        //[HttpGet]
        public IActionResult Detail(int id)
        {
            this.DetailInit(id);
            return View(new Inquiry());
        }
        [HttpPost]
        public IActionResult Detail(int id, Inquiry input)
        {
            //modelstate

            Inquiry inquiry = new() { IdOffer = id, Name = input.Name, Email = input.Email,
                PhoneNumber = input.PhoneNumber, AdditionalInformation = input.AdditionalInformation,
                IdUser = input.IdUser, Surname = input.Surname, DateTimeSent = DateTime.Now };

            this.ViewBag.InquirySent = true;
            this._context.Inquiries.Add(inquiry);
            this._context.SaveChanges();   
            this.DetailInit(id);
            return View(inquiry);
        }
        private void DetailInit(int id)
        {
            Offer offer = this._context.Offers?.First(offer => offer.Id == id)!;

            offer.Images = this._context.Images!.Where(image => image.IdOffer == offer.Id)!.ToList();
            List<KeyValuePair<string, string>> offerParameters = new();


            var offerParams = this._context.OfferParameters!.Where(param => param.IdOffer == offer.Id)!.ToList();
            offerParams.ForEach(param =>
            {
                this._context.Labels!.Where(label => label.Id == param.IdParameter).ForEachExt(label =>
                {
                    offerParameters.Add(new KeyValuePair<string, string>(label.Value, param.Value));
                });
            });
            this.ViewBag.Offer = offer;
            this.ViewBag.OfferParameters = offerParameters;
            this.ViewBag.Admin = this._context.Admins!.Find(offer.IdAdmin);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
