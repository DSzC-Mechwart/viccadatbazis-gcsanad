using Microsoft.AspNetCore.Mvc;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    public class ViccekController : Controller
    {
        [HttpGet]
        [Route("[controller]")]
        public string Get()
        {
            Vicc ujVicc = new();
            ujVicc.Tartalom = " A Facebook olyan, mint a hűtőszekrény. 15 percenként kinyitogatod, és mindig ugyanaz van benne. ";
            ujVicc.Feltolto = "Rasputin";
            ujVicc.Tetszik = 764;
            ujVicc.NemTetszik = 96;
            ujVicc.Aktiv = true;

            
            return $"{ujVicc.Tartalom}\nFeltöltő: {ujVicc.Feltolto}\nLikeok: {ujVicc.Tetszik}\nDiszlájkok: {ujVicc.NemTetszik}";
            
        }
    }
}
