using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.DatabaseModels;
using RealEstate.Models;
using System.Diagnostics;
using RealEstate.Attributes;
using System.Text.Json;
using System.Text;
using RealEstate.Models.ComponentModels;

namespace RealEstate.Controllers
{
    public class EstateOffersController : BaseEstateController
    {
        [HttpGet]
        public IActionResult Index(string? filter = null, char? category = null)
        {
            List<Offer> offers = this._context.Offers!.ToList();

            if (category != null)
                offers = offers.Where(offer => offer.Category == category).ToList();

            FilterModel filterEncoded = new();

            if (filter != null)
            {
                var filterBytes = Convert.FromBase64String(filter);
                var filterJson = Encoding.UTF8.GetString(filterBytes);

                filterEncoded = JsonSerializer.Deserialize<FilterModel>(filterJson)!;
            }

            this.IndexInit(offers, filterEncoded, new());

            return View();
        }
        [HttpPost]
        public IActionResult Index(FilterModel filter, StringInputModel search)
        {
            var offers = this._context.Offers!.ToList();

            if (filter.PriceMax == null)
            {
                var filterToken = this.HttpContext.Session.GetString("filterToken");
                if (filterToken != null)
                {
                    var filterBytes = Convert.FromBase64String(filterToken!);
                    var filterJson = Encoding.UTF8.GetString(filterBytes);

                    filter = JsonSerializer.Deserialize<FilterModel>(filterJson)!;
                }
            }

            this.IndexInit(offers, filter, search);

            return View();
        }

        private void IndexInit(List<Offer> offers, FilterModel filter, StringInputModel search)
        {
            this.ViewBag.NavUnderline = "Home";


            offers?.ForEach(offer =>
            {
                this._context.Images!.Where(image => image.IdOffer == offer.Id).ForEachExt(image =>
                {
                    offer.Images!.Add(image);
                });
            });

            List<string> regions = new();

            this._context.Offers!.ForEachExt(offer =>
            {
                if (!regions.Contains(offer.Region))
                    regions.Add(offer.Region);
            });

            ulong maxPrice = this._context.Offers!.Max(offer => offer.Price);

            string json = JsonSerializer.Serialize(filter);
            string filterBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            this.HttpContext.Session.SetString("filterToken", filterBase64);


            this.ViewBag.CategoryCounts = this._context.Offers!.GetCategoryCount();
            this.ViewBag.Offers = this.FilterOffers(offers!, filter, search);
            this.ViewBag.FilterComponentParameter = new FilterComponentParameter(filter, regions, maxPrice);
        }

        public IActionResult GetOffersPartial()
        {



            return PartialView("_Offers");
        }

        private List<Offer> FilterOffers(List<Offer> offers, FilterModel filter, StringInputModel search)
        {
            if (search != null && search!.Value != null)
            {
                search!.Value = search.Value.ToLower().Trim();
                offers = this.FindSearchOffers(offers!, search.Value);
            }

            if (filter != null)
            {
                IEnumerable<Offer> filtered = offers!;

                if (filter.Region != null)
                {
                    filtered = filtered.Where(offer => offer.Region == filter.Region).ToList();
                }

                if (filter.EnergyClass != null)
                {
                    filtered = filtered.Where(offer => offer.EnergyClass == filter.EnergyClass).ToList();
                }
                if (filter.PriceMin != null && filter.PriceMax != null)
                {
                    filtered = filtered!.Where(offer => offer.Price >= filter.PriceMin && offer.Price <= filter.PriceMax
                                                 /*&& offer.Area >= filter.AreaMin && offer.Area <= filter.AreaMax*/).ToList();
                }

                offers = filtered.ToList();
            }

            return offers;
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

            if (this.ViewBag.User != null)
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
