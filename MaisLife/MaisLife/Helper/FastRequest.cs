using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class FastRequest{

        public HttpRequestBase Request { get; set; }

        public FastRequest(HttpRequestBase r) {
            this.Request = r;
        }

        public int ToInt(string field) 
        {
            return Convert.ToInt32(this.Request.Form[field]);
        }

        public double ToDouble(string field)
        {
            return Convert.ToDouble(this.Request.Form[field]);            
        }

        public string ToString(string field)
        {
            return this.Request.Form[field];
        }

        public decimal ToDecimal(string field)
        {
            return Convert.ToDecimal(this.Request.Form[field]);                    
        }

    }
}