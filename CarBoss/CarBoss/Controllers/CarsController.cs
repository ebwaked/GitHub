using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insight.Database;
using Microsoft.AspNet.Identity.Owin;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using CarBoss.Models.DataInterfaces;
using CarBoss.Models;
using Newtonsoft.Json.Linq;
using Bing;
using System.Web.Helpers;

namespace CarBoss.Controllers
{
    [RoutePrefix("api/cars")]
    public class CarsController : ApiController
    {
       private ICarsAccess db;

        public CarsController()
        {
            db = HttpContext.Current.GetOwinContext().Get<SqlConnection>().As<ICarsAccess>();
        }

        //[HttpGet, Route("getCars")]
        //public IHttpActionResult GetCars()
        //{
        //    return Ok("It's a car!");
        //}

        [HttpGet, HttpPost, Route("getYears")]
        public async Task<IHttpActionResult> GetYears()
        {
            return Ok(await db.GetYears());
        }

        [HttpGet, HttpPost, Route("getMakes")]
        public async Task<IHttpActionResult> GetMakes(int year)
        {
            return Ok(await db.GetMakes(year));
        }

        [HttpGet, HttpPost, Route("getModels")]
        public async Task<IHttpActionResult> GetModels(int year, string make)
        {
            return Ok(await db.GetModels(year, make));
        }

        [HttpGet, HttpPost, Route("getTrims")]
        public async Task<IHttpActionResult> GetTrims(int year, string make, string model)
        {
            return Ok(await db.GetTrims(year, make, model));
        }

        [HttpGet, HttpPost, Route("getCars")]
        public async Task<IHttpActionResult> GetCars(int year, string make, string model, string trim)
        {
            return Ok(await db.GetCars(year, make, model, trim));
        }
        
        [HttpGet, HttpPost, Route("getCar")]
        public async Task<IHttpActionResult> getCar(int Id)
        {
            HttpResponseMessage response;
            string content = "";
            var car = new carViewModel
            {
                Car = await db.GetCar(Id),
                Recalls = content,
                Image = ""

            };

            //Get recall Data

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + car.Car.year.ToString() + "/make/" + car.Car.make + "/model/" + car.Car.model + "?format=json");
                    content = await response.Content.ReadAsStringAsync();
                    
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }

            car.Recalls = content;
            var image = new BingSearchContainer(
            new Uri("https://api.datamarket.azure.com/Bing/search/"));
            image.Credentials = new NetworkCredential("accountKey", "5u/0CzVmYrTKDOjlxPePfPkh/G8llMIfVJ7QC/oNEvQ");
            var marketData = image.Composite(
                "image",
                car.Car.year + " " + car.Car.make + " " + car.Car.model + " " + car.Car.trim,
                null,
                null,                
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
                ).Execute();

            car.Image = marketData.First().Image.First().MediaUrl;

            return Ok(car);

            //Get ImageUrl
            // carViewModel.imageData = ImageUrl;
        }
    }
}
