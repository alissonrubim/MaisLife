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
                String money = value.Replace(".", "");
                return Convert.ToDecimal(money);
            }
            return 0;   
        }
    }
}