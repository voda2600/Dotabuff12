using Dapper;

using Microsoft.Extensions.Configuration;
using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace SemestreWork.Repository
{
    public class NewsRepository : IRepository
    {
        IConfiguration _configuration;
   
        public NewsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public int Add(NewsPost news)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO NewsPosts(Name, Intro, Text, Picture) VALUES(@Name, @Intro, @Text,@Picture); SELECT CAST(SCOPE_IDENTITY() as INT);";
                    count = con.Execute(query, news);
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

        public int DeleteNews(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM NewsPosts WHERE Id =" + id;
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

        public int EditNews(NewsPost news)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE NewsPosts SET Name = @Name, Intro = @Intro, Text = @Text, Picture = @Picture WHERE Id = @Id";
                    count = con.Execute(query, news);
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

        public List<NewsPost> GetList()
        {
            var connectionString = this.GetConnection();
            List<NewsPost> products = new List<NewsPost>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM NewsPosts";
                    products = con.Query<NewsPost>(query).ToList();
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

     
        public NewsPost GetNews(int id)
        {
            var connectionString = this.GetConnection();
            NewsPost product = new NewsPost();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                
                    con.Open();
                    var query = "SELECT * FROM NewsPosts WHERE Id =" + id;
                    product = con.Query<NewsPost>(query).FirstOrDefault();
                }
                catch
                {
                    throw new Exception("Нет данного поста");
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
