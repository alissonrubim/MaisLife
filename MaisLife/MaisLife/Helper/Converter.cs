using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class Converter
    {
        public static decimal ConvertMoney(string value)
        {
            if(value != null)
            {
                return Convert.ToDecimal(value);
            }
            return 0;   
        }
    }
}