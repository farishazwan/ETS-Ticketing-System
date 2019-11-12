using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Process
{
    public class DbAccessBulletin
    {
        public IEnumerable<Bulletin> GetDbBulletin()
        {
            IList<Bulletin> dbBulletin = new List<Bulletin>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.
            ConnectionStrings["connStrMrtTicketing"].ConnectionString);
            string sql = @"SELECT * FROM Bulletin";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dbBulletin.Add(new Bulletin()
                {
                    newsID = reader.GetInt32(0),
                    newsTitle = reader.GetString(1),
                    newsDescription = reader.GetString(2)
                });
            }
            conn.Close();
            return dbBulletin;
        }
    }
}