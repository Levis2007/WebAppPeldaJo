using AspNetCoreGeneratedDocument;
using MySql.Data.MySqlClient;
using WebAppPelda.Models;

namespace WebAppPelda.Services
{
    public class CustomerService
    {
        public string PostCustomer(Customer customer)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= webapppeldadb;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "INSERT INTO vasarlo(Nev, Cim, Email, Telefon, Pontszam) Values(@nev,@cim,@email,@telefon,@pontszam)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nev", customer.Name);
                cmd.Parameters.AddWithValue("@cim", customer.Address);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@telefon", customer.Phone);
                cmd.Parameters.AddWithValue("@pontszam", customer.Score);

                int sorokSzama = cmd.ExecuteNonQuery();
                conn.Clone();
                if (sorokSzama > 0)
                {
                    return "Sikeres beszúrás!";
                }
                else
                {
                    return "Sikertelen beszúrás";
                }
            }
            catch (Exception ex)
            {
                return "Hiba történt az adatok beszúrása során! " + ex.Message;
            }

        }

        public string DeleteCustomer(int id)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                                  "DATABASE= webapppeldadb;" +
                                  "UID = root;" +
                                  "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "DELETE FROM vasarlo WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id", id);
                int eredmeny = cmd.ExecuteNonQuery();
                conn.Close();
                if (eredmeny > 0)
                {
                    return "Sikeres törlés!";
                }
                else
                {
                    return "Sikertelen törlés!";
                }
            }
            catch (Exception ex)
            {
                return "Hiba történt az adatok törlése során! " + ex.Message;
            }

        }

        public List<Customer> GetAllCustomer()
        {
            try
            {
                List<Customer> customers = new List<Customer>();

                string connectionString = "SERVER = localhost;" +
                              "DATABASE= webapppeldadb;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "SELECT * FROM vasarlo";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.Id = reader.GetInt32("Id");
                    customer.Name = reader.GetString("Nev");
                    customer.Address = reader.GetString("Cim");
                    customer.Email = reader.GetString("Email");
                    customer.Phone = reader.GetString("Telefon");
                    customer.Score = reader.GetInt32("Pontszam");
                    customers.Add(customer);
                }
                conn.Close();

                return customers;
            }
            catch (Exception ex) 
            {
                List<Customer> customers = new List<Customer>();
                return customers;
            }
        }

        public Customer GetById(int id) 
        {
            Customer result = new Customer();
            try
            {
                List<Customer> customers = new List<Customer>();

                string connectionString = "SERVER = localhost;" +
                              "DATABASE= webapppeldadb;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "SELECT * FROM vasarlo WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                { 
                    result.Id = reader.GetInt32("Id");
                    result.Name = reader.GetString("Nev");
                    result.Address = reader.GetString("Cim");
                    result.Email = reader.GetString("Email");
                    result.Phone = reader.GetString("Telefon");
                    result.Score = reader.GetInt32("Pontszam");
                }
                else
                {
                    Console.WriteLine("Nincs ilyen vásárló!");
                }
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public string PutCustomer(Customer customer)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= webapppeldadb;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "UPDATE vasarlo SET Nev = @nev, Cim = @cim, Email = @email, Telefon = @telefon, Pontszam = @pontszam WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nev", customer.Name);
                cmd.Parameters.AddWithValue("@cim", customer.Address);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@telefon", customer.Phone);
                cmd.Parameters.AddWithValue("@pontszam", customer.Score);
                cmd.Parameters.AddWithValue("@id", customer.Id);
                int sorokSzama = cmd.ExecuteNonQuery();
                conn.Clone();
                if (sorokSzama > 0)
                {
                    return "Sikeres frissítés!";
                }
                else
                {
                    return "Ismeretlen vásárló!";
                }
            }
            catch (Exception ex)
            {
                return "Hiba történt az adatok frissítése során! " + ex.Message;
            }
        }
    }
}
