using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using RealEstate.Models;
using System.Diagnostics;

namespace RealEstate.Controllers
{
    public class EstateOffersController : BaseEstateController
    {
        [HttpGet]
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

            return View();
        }
        [HttpPost]
        public IActionResult Index(SearchModel search)
        {
            var offers = this._context.Offers?.ToList();
            offers?.ForEach(offer =>
            {
                this._context.Images!.Where(image => image.IdOffer == offer.Id).ForEachExt(image =>
                {
                    offer.Images!.Add(image);
                });
            });
            this.ViewBag.CategoryCounts = offers!.GetCategoryCount();
            this.ViewBag.ViewCount = 6;

            //pak upravit, tohle je jak by to psal hanak
            offers = offers!.Where(offer => offer.Name.ToLower().StartsWith(search.Value)).ToList();
            this._context.Offers!.Where(offer => offer.Name.Contains(search.Value) && !(offer.Name.ToLower().StartsWith(search.Value)))
                .ForEachExt(offer => offers.Add(offer));


            this.ViewBag.Offers = offers;

            return View();
        }
        public IActionResult Detail(int id, int? idInquiry)
        {
            this.DetailInit(id);
            this.ViewBag.Inquiry = idInquiry != null ? this._context.Inquiries.Find(idInquiry) : null;
            return View();
        }

        public IActionResult SendInquiry(int idOffer, Inquiry inquiry)
        {
            //modelstate - validace

            inquiry.Id = 0;
            inquiry.IdOffer = idOffer;
            inquiry.DateTimeSent = DateTime.Now;

            //this.ViewBag.InquirySent = true;
            this._context.Inquiries.Add(inquiry);
            this._context.SaveChanges();
            this.DetailInit(idOffer);
            //return View();
            return RedirectToAction("Detail", new {id = idOffer, idInquiry = inquiry.Id});
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
