using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAssessment.Models
{
    public class CommodityPrice : StockPrice
    {
        public new int Dy
        {
            get { return 3; }
            //set {_Dy = value; }
        }
    }
}