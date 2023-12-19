using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using RealEstate.Models;
using System.Diagnostics;
using RealEstate.Attributes;

namespace RealEstate.Controllers
{
    public class EstateOffersController : BaseEstateController
    {
        [HttpGet]
        public IActionResult Index(int viewCount = 6, char? filter = null)
        {
            this.ViewBag.NavUnderline = "Home";
            this.ViewBag.ViewCount = viewCount;

            List<Offer> offers = this._context.Offers?.ToList()!;

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
        public IActionResult Index(StringInputModel search)
        {
            this.ViewBag.NavUnderline = "Home";
            search.Value = search.Value.ToLower().Trim();
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

            offers = this.FindSearchOffers(offers!, search.Value);

            this.ViewBag.Offers = offers;

            return View();
        }

        private List<Offer> FindSearchOffers(IEnumerable<Offer> offers, string search)
        {
            List<Offer> results = new();
            List<Offer> notFound = new();
            search = search.ToLower().Trim();

            foreach (Offer offer in offers)
            {
                if (offer.Name.ToLower().StartsWith(search))
                {
                    results.Add(offer);
                    continue;
                }
                notFound.Add(offer);
            }
            notFound.ForEach(offer =>
            {
                if (offer.Name.ToLower().Contains(search))
                    results.Add(offer);
            });

            return results;
        }

        public IActionResult Detail(int id, int? idInquiry)
        {
            this.ViewBag.NavUnderline = "Katalog";
            this.DetailInit(id);

            //Predelat na TempData (misto ViewBag) potom
            this.ViewBag.Inquiry = idInquiry != null ? this._context.Inquiries.Find(idInquiry) : null;

            return View();
        }

        public IActionResult SendInquiry(int idOffer, Inquiry inquiry)
        {
            this.ViewBag.NavUnderline = "Katalog";
            //modelstate - validace

            inquiry.Id = 0;
            inquiry.IdOffer = idOffer;
            inquiry.DateTimeSent = DateTime.Now;

            if(this.ViewBag.User != null)
            {
                inquiry.IdUser = this.ViewBag.User.Id;
            }

            this._context.Inquiries.Add(inquiry);
            this._context.SaveChanges();
            this.DetailInit(idOffer);
            return RedirectToAction("Detail", new { id = idOffer, idInquiry = inquiry.Id });
        }
        private void DetailInit(int id)
        {
            Offer offer = this._context.Offers?.First(offer => offer.Id == id)!;

            offer.Images = this._context.Images!.Where(image => image.IdOffer == offer.Id)!.ToList();
            List<KeyValuePair<string, string>> offerParameters = new();


            var offerParams = this._context.OfferParameters!.Where(param => param.IdOffer == offer.Id)!.ToList();
            offerParams.ForEach(param =>
            {
                this._context.Parameters!.Where(label => label.Id == param.IdParameter).ForEachExt(label =>
                {
                    offerParameters.Add(new KeyValuePair<string, string>(label.Value, param.Value));
                });
            });
            this.ViewBag.Offer = offer;
            this.ViewBag.OfferParameters = offerParameters;
            this.ViewBag.Broker = this._context.Users!.Find(offer.IdBroker);

            if (this.ViewBag.User == null) return;

            User user = (User)this.ViewBag.User;
            FavouriteOffer? favOff = this._context.FavouriteOffers!.FirstOrDefault(offer => offer.IdOffer == id && offer.IdUser == user.Id);
            this.ViewBag.Favourite = favOff != null;
        }
        [Authorize]
        public IActionResult AddFavourite(int id, int? idInquiry)
        {
            User user = (User)this.ViewBag.User;
            this._context.FavouriteOffers!.Add(new FavouriteOffer()
            {
                IdOffer = id,
                IdUser = user.Id
            });

            this._context.SaveChanges();
            return RedirectToAction("Detail", new { id, idInquiry });
        }
        [Authorize]
        public IActionResult RemoveFavourite(int id, int? idInquiry)
        {
            User user = (User)this.ViewBag.User;
            var favOff = this._context.FavouriteOffers!.First(offer => offer.IdOffer == id && offer.IdUser == user.Id);
            this._context.FavouriteOffers!.Remove(favOff!);
            this._context.SaveChanges();

            return RedirectToAction("Detail", new { id, idInquiry });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
