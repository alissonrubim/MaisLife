using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisLifeModel
{
    public class ConfigDB
    {

        public static string StringConexao =
           "Persist Security Info=False;server=localhost;port=3306;database=maislife;uid=root";

        private static EntitiesModel _instance;
        public static EntitiesModel Model
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EntitiesModel(StringConexao);
                }

                return _instance;
            }
        }
    }
}
