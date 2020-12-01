using Dapper;
using Microsoft.Extensions.Configuration;
using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public class RegisterRepository:IRegisterRepository
    {
        IConfiguration _configuration;

        public RegisterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public int AddUser(RegisterModel user)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Users(Nick, Email, Password, Hero, MMR) VALUES(@Nick, @Email, @Password, @Hero, @MMR);";
                    count = con.Execute(query, user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public int DeleteUser(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Users WHERE Id =" + id;
                    count = con.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public int EditUser(RegisterModel user)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE Users SET Nick=@Nick, Email=@Email, Password=@Password, Hero=@Hero, MMR=@MMR WHERE Id = @Id";
                    count = con.Execute(query, user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public List<RegisterModel> GetList()
        {
            var connectionString = this.GetConnection();
            List<RegisterModel> products = new List<RegisterModel>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Users";
                    products = con.Query<RegisterModel>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return products;
            }
        }


        public RegisterModel GetUser(int id)
        {
            var connectionString = this.GetConnection();
            RegisterModel product = new RegisterModel();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {

                    con.Open();
                    var query = "SELECT * FROM Users WHERE Id =" + id;
                    product = con.Query<RegisterModel>(query).FirstOrDefault();
                }
                catch
                {
                    throw new Exception("Нет данного пользователя");
                }
                finally
                {
                    con.Close();
                }

                return product;
            }
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("MyData").Value;
            return connection;
        }
    }
}
