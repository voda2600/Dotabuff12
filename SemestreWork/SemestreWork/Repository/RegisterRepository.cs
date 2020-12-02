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
                    var query = "INSERT INTO MyUsers(Nick, Email, Password, Hero, MMR,Role) VALUES(@Nick, @Email, @Password, @Hero, @MMR,@Role);";
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
                    var query = "DELETE FROM MyUsers WHERE Id =" + id;
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
                    var query = "UPDATE MyUsers SET Nick=@Nick, Email=@Email, Password=@Password, Hero=@Hero, MMR=@MMR, Role=@Role, Image=@Image WHERE Id = @Id";
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
                    var query = "SELECT * FROM MyUsers";
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
                    var query = "SELECT * FROM MyUsers WHERE Id =" + id;
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
        public RegisterModel GetAuthUser(string email, string password)
        {
            var connectionString = this.GetConnection();
            RegisterModel product = new RegisterModel();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM MyUsers WHERE ( Email = '"+email+"' AND Password = '"+password+"');";
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
