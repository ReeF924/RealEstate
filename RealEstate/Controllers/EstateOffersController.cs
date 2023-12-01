using Microsoft.AspNetCore.Mvc;
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
                this._context.Images?.Where(image => image.IdOffer == offer.Id).ToList().ForEach(image =>
                {
                    offer.Images?.Add(image);
                });
            });

            this.ViewBag.Offers = offers;
            return View();
        }

        //public IActionResult Detail(int id)
        //{
        //    Offer offer = this._context.Offers?.First(offer => offer.Id == id)!;

        //    offer.Images = this._context.Images?.Where(image => image.IdOffer == offer.Id).ToList();
        //    this.ViewBag.Offer = offer;
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
