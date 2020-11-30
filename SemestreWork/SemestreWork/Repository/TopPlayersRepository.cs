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
    public class TopPlayersRepository
    {
        IConfiguration _configuration;

        public TopPlayersRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Add(TopPlayer cyber)
        {

            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO TopPlayers(Name, Date, Country, Team, Picture) VALUES(@Name, @Date, @Country, @Team,@Picture);";
                    count = con.Execute(query, cyber);
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

        public int DeleteCyber(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM TopPlayers WHERE Id =" + id;
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



        public List<TopPlayer> GetList()
        {
            var connectionString = this.GetConnection();
            List<TopPlayer> products = new List<TopPlayer>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM TopPlayers ORDER BY Id";
                    products = con.Query<TopPlayer>(query).ToList();
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
        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("MyData").Value;
            return connection;
        }
    }
}
