using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAssessment.Models;

namespace WebAssessment.Controllers
{
    public class PriceController : ApiController
    {

        
        public PriceController()
        {
            if (!(StockPrice.PriceColl != null && StockPrice.PriceColl.Count > 0))
            {
                StockPrice.LoadData();
            }
        }

        public IEnumerable<StockPrice> GetAllPriceData()
        {
            return StockPrice.PriceColl;
        }

        public IHttpActionResult GetPriceDay(string id1)
        {

            StockPrice sValue = StockPrice.ReadLastData();
            return Ok(sValue);
        }

        public IHttpActionResult GetPrice(string id2)
        {
            float value = Convert.ToSingle(id2);
            StockPrice.AddStockPrice(value);
            return Ok("Done");
        }

        public IEnumerable<StockPrice> GetPriceNumberDays(string id3)
        {
            int num = Convert.ToInt32(id3);
            return StockPrice.ReadLastNumberDays(num);  
        }

    }
}
