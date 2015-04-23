using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CarBoss.Models.DataInterfaces
{
    public interface ICarsAccess
    {
        /// <summary>
        /// Interfaces with the SQL Server
        /// </summary>
        /// <returns>Returns the stored procedurs for that function</returns>
        Task<List<string>> GetYears();
        Task<List<string>> GetMakes(int year);
        Task<List<string>> GetModels(int year, string make);
        Task<List<string>> GetTrims(int year, string make, string model);
        Task<List<Cars>> GetCars(int year, string make, string model, string trim);


        // In line SQL commands set to the GetCar Function
        [Sql("Select * from Cars where Id = @ID")]
        Task<Cars> GetCar(int Id);
    }
}