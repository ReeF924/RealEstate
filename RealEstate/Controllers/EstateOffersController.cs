using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using RealEstate.Models;
using System.Diagnostics;

namespace RealEstate.Controllers
{
    public class EstateOffersController : BaseEstateController
    {
        public IActionResult Index()
        {
            var offers = this._context.Offers?.ToList();
            offers?.ForEach(offer =>
            {
                this._context.Images!.Where(image => image.IdOffer == offer.Id).ToList().ForEach(image =>
                {
                    offer.Images!.Add(image);
                });
            });

            this.ViewBag.Offers = offers;
            return View();
        }
        //[HttpGet]
        public IActionResult Detail(int id)
        {
            this.DetailInit(id);
            return View(new Inquiry());
        }
        [HttpPost]
        public IActionResult Detail(int id, Inquiry inquiry)
        {
            //if(!this.ModelState.IsValid)
            //{
            //    this.DetailInit(id);
            //    return View(inquiry);
            //}
            var inq = inquiry;
            string s = "";


            inquiry.IdOffer = id;
            inquiry.DateTimeSent = DateTime.UtcNow;

            this._context.Inquiries.Add(inquiry);
            this._context.SaveChanges();   
            this.DetailInit(id);
            return View(new Inquiry());
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
