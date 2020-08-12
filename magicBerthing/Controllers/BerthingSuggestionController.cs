
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magicBerthing.DataLogics.Monitoring;
using magicBerthing.Models.Monitoring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BerthingSuggestionController : Controller
    {
        [Route("api/MagicBerthing/BerthSuggestion")]
        [HttpPost]
        [ActionName("BerthSuggestion")]
        public IActionResult BerthSuggestion(ParamBerthSuggestion data)
        {

            BerthingSuggestionDL dal = new BerthingSuggestionDL();
            BerthSuggestionModel hasil = new BerthSuggestionModel();

            IEnumerable<SuggestionList> result = dal.getSuggestion(data);
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = result.ToList();

            return Ok(hasil);
        }
    }
}