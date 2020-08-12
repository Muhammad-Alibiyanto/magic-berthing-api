using magicBerthing.DataLogics.Monitoring;
using magicBerthing.Models.Monitoring;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDermagaController : ControllerBase
    {
        [Route("api/MagicBerthing/createBooking")]
        [HttpPost]
        [ActionName("createBooking")]
        public IActionResult createBooking(BookingDermagaHeader DermagaHeader)
        {
            BookingDermagaDL execute = new BookingDermagaDL();

            return Ok(execute.createBooking(DermagaHeader));
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class GetBookingDermagaController : ControllerBase
    {
        [Route("api/MagicBerthing/getBookingDermaga")]
        [HttpPost]
        [ActionName("getBookingDermaga")]
        public IActionResult getBookingDermaga(BookingDermagaParam DermagaParam)
        {

            BookingDermagaDL dal = new BookingDermagaDL();
            BookingDermagaModel hasil = new BookingDermagaModel();

            IEnumerable<GetBookingDermagaList> result = dal.getBookingDermaga(DermagaParam);    
            hasil.message = "Success";
            hasil.status = "S";
            hasil.count = result.Cast<Object>().Count();
            hasil.data = result.ToList();

            return Ok(hasil);
        }
    }
}
