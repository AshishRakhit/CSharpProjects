using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebAssessment.Models
{
    public class StockPrice
    {
        private int _Dy;
        private float _price;

        private static readonly object XLock = new object();

        public static List<StockPrice> PriceColl = null;

        public int Dy
        {
            get { return _Dy; }
            set { _Dy = value; }
        }

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private static string FilePath = "C:\\Personal\\AverageStockPrice.csv"; 

        public static void LoadData()
        {
            //Save Path Name
            PriceColl = new List<StockPrice>();
            char token = ',';

            //Open file and read data
            lock (XLock)
            {
                //string FilePath = "C:\\Personal\\AverageStockPrice.csv";
                FileStream fs = new FileStream(FilePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string[] sarray = null;
                string str;
                //int count = 0;
                while ((str = sr.ReadLine()) != null)
                {
                    sarray = str.Split(token);
                    StockPrice Data = new StockPrice();

                    //Fill Data
                    string s = sarray[0];
                    Data.Dy = Convert.ToInt32(s);
                    Data.Price = Convert.ToSingle(sarray[1]);

                    PriceColl.Add(Data);
                }

                fs.Close();
            }

        }

        public static StockPrice ReadLastData()
        {
            StockPrice value = PriceColl.Last();
            return value;
        }

        public static List<StockPrice> ReadLastNumberDays(int num)
        {
            List<StockPrice> coll = new List<StockPrice>();
            int index = PriceColl.Count - 1;
            for(int i = index; i >= index - (num - 1); i--)
            {
                coll.Add(PriceColl[i]);
            }

            return coll;           
        }

        public static void AddStockPrice(float value)
        {

            //Add to the list
            int count = PriceColl.Count;
            StockPrice X = new StockPrice();
            X.Dy = count + 1;
            X.Price = value;

            PriceColl.Add(X);
           
            lock (XLock)
            { 
                FileStream fs = new FileStream(FilePath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);

                string Val1 = X.Dy.ToString();
                string Val2 = X.Price.ToString();

                string V = Val1 + "," + Val2 + "\n";

                sw.WriteLine(V);
                sw.Flush();

                fs.Close();
            }

        }


    }
}