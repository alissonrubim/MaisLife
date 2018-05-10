using MaisLifeModel.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisLifeModel
{
    public class DatabaseContext
    {
        private static MaislifeEntities instance;
        public static MaislifeEntities Model
        {
            get
            {
                if (instance == null)
                {
                    instance = new MaislifeEntities();
                }

                return instance;
            }
        }

        /* public static string StringConexao = "server=localhost;port=3306;database=maislife;user id=root;password=root;SslMode=none";

         private static MySqlConnection connection;
         private static EntitiesModel instance;
         public static EntitiesModel Model
         {
             get
             {
                 if (instance == null)
                 {
                     connection = new MySqlConnection(StringConexao);
                     connection.Open();
                     instance = new EntitiesModel(connection, false);
                 }

                 return instance;
             }
         }*/
    }
}
