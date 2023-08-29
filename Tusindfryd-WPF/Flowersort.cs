using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace Tusindfryd_WPF
{
    internal class Flowersort
    {
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public int ProductionTime { get; set; }
        public int HalfLife { get; set; }
        public int Size { get; set; }

        public Flowersort(string name, string picturePath, int productionTime, int halfLife, int size)
        {
            Name = name;
            PicturePath = picturePath;
            ProductionTime = productionTime;
            HalfLife = halfLife;
            Size = size;
        }

        public void InsertIntoDatabase(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Flowersorts (Name, PicturePath, ProductionTime, HalfLife, Size) VALUES (@Name, @PicturePath, @ProductionTime, @HalfLife, @Size)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@PicturePath", PicturePath);
                    command.Parameters.AddWithValue("@ProductionTime", ProductionTime);
                    command.Parameters.AddWithValue("@HalfLife", HalfLife);
                    command.Parameters.AddWithValue("@Size", Size);

                    command.ExecuteNonQuery();
                }
            }
        }

    }

}
