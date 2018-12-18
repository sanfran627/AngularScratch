using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AngularScratch.Controllers
{
	[Route("api/[controller]")]
	public class SampleDataController : Controller
	{
    MyDbContext _db;

    public SampleDataController(MyDbContext db) => _db = db;

    private static string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

    [EnableCors("CorsPolicy")]
    [HttpGet("users")]
    public JsonResult Users( bool add = false )
    {

      if (_db.User.Count() == 0 || add )
      {
        var user = new User
        {
          UserId = Guid.NewGuid(),
          FirstName = "Matthew",
          LastName = "Aman",
          DOB = new DateTime(2000, 01, 01),
          Title = "Father"
        };

        _db.User.Add(user);

        // not awaiting and asyncing this function because I'll need to re-write the logic below
        _db.SaveChanges();

        // if doing a transaction (just reminding myself)
        //using (var transaction = _db.Database.BeginTransaction())
        //{
        //  try
        //  {
        //    _db.User.Add(user);
        //    transaction.Commit();
        //  }
        //  catch (Exception)
        //  {
        //    // TODO: Handle failure
        //  }
        //}
      }


      return Json(_db.User.Select(c => new
      {
        userId = c.UserId.ToString().Replace("-", "").ToLower(),
        firstName = c.FirstName,
        lastName = c.LastName,
        title = c.Title,
        age = Math.Floor(DateTime.Now.Date.Subtract(c.DOB).TotalDays / 365.25)
      }));
    }

    [EnableCors("CorsPolicy")]
    [HttpGet("weather")]
    public IEnumerable<WeatherForecast> WeatherForecasts()
    {
      var rng = new Random();
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
      });
    }

		public class WeatherForecast
		{
			public string DateFormatted { get; set; }
			public int TemperatureC { get; set; }
			public string Summary { get; set; }

			public int TemperatureF
			{
				get
				{
					return 32 + (int)(TemperatureC / 0.5556);
				}
			}
		}
	}
}
