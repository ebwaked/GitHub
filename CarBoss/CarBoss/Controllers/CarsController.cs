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
    }
}
