using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Formula1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DBFormula1Context _context;

        public ChartController(DBFormula1Context context)
        {
            _context = context;
        }

        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var countries = _context.Countries.ToList();
            List<object> list = new List<object>();
            list.Add(new[] {"Країна", "Кількість трас"});
            foreach (var country in countries)
            {
                var circuits = _context.Circuites.Where(c=> c.CountryId == country.Id).ToList();
                list.Add(new object[] {country.Name, circuits.Count()});
            }
            return new JsonResult(list);
        }

        [HttpGet("JsonData2")]
        public JsonResult JsonData2()
        {
            var countries = _context.Countries.ToList();
            List<object> list = new List<object>();
            list.Add(new[] { "Країна", "Кількість гонщиків" });
            foreach (var country in countries)
            {
                var drivers = _context.Drivers.Where(c=> c.CountryId == country.Id).ToList();
                list.Add(new object[] { country.Name, drivers.Count() });
            }
            return new JsonResult(list);
        }
    }
}
