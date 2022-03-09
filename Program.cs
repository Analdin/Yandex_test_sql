using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace YaTest
{
    class Program
    {
        public static int laborsCount { get; set; }

        static void Main(string[] args)
        {

            MsqlHelper.DbHelper connect = new MsqlHelper.DbHelper();
            connect.OpenConnection();

            var query = @"SELECT `id` FROM `Ya_Compaines` WHERE 1";

            List<string> allids = new List<string>();
            List<int> laborsLst = new List<int>();

            var command = new MySqlCommand(query, connect.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                allids.Add(reader.GetString(0));
            }

            reader.Close();

            var query2 = @"SELECT `labors` FROM `Ya_Compaines` WHERE 1";

            var command2 = new MySqlCommand(query2, connect.Connection);
            var reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                laborsLst.Add(reader2.GetInt32(0));
            }

            reader2.Close();

            for (int i = 0; i < laborsLst.Count; i ++)
            {
                int loneLabor = laborsLst[i];
                Console.WriteLine("Количество сотрудников - " + loneLabor);
                if (loneLabor > 1000) Console.WriteLine("Количество сотрудников больше 1000");
            }

            //Проверяем, где находится штаб квартира компании
            List<int> citiesIds = new List<int>();
            List<int> companiesIds = new List<int>();

            var query3 = @"SELECT `id` FROM `Ya_cities` WHERE 1";

            var command3 = new MySqlCommand(query3, connect.Connection);
            var reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {
                citiesIds.Add(reader3.GetInt32(0));
            }

            reader3.Close();

            var query4 = @"SELECT `city_id` FROM `Ya_Compaines` WHERE 1";

            var command4 = new MySqlCommand(query4, connect.Connection);
            var reader4 = command4.ExecuteReader();

            while (reader4.Read())
            {
                companiesIds.Add(reader4.GetInt32(0));
            }

            reader4.Close();

            Console.WriteLine("citiesIds.Count - " + citiesIds.Count);
            Console.WriteLine("companiesIds.Count - " + companiesIds.Count);

            for (int t = 0; t < citiesIds.Count; t ++)
            {
                int id = citiesIds[t];
                Console.WriteLine("idt - " + id);
                int cmpId = companiesIds[0];
                Console.WriteLine("cmpId - " + cmpId);
                if (id == cmpId)
                {
                    Console.WriteLine("Штаб квартира компании находится в городе - " + id);
                }
                else
                {
                    Console.WriteLine("Штаб квартира не определена - " + id);
                }
                companiesIds.RemoveAt(0);
                companiesIds.Add(cmpId);
            }

            connect.CloseConnection();
        }
    }
}
