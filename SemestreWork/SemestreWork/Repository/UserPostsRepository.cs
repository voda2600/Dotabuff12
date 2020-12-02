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
    public class UserPostsRepository:IUserPosstRepository
    {
        IConfiguration _configuration;

        public UserPostsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public int Add(UserPosts news)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO UserPosts(UserId, Text, Picture, Time,Intro) VALUES(@UserId, @Text,@Picture,@Time,@Intro);";
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

        public int DeletePost(int id,int UserId)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM UserPosts WHERE (Id =" + id+" AND UserId="+UserId+")";
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

        public int EditPost(UserPosts news)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE UserPosts SET Text = @Text, Picture = @Picture, Intro=@Intro WHERE (Id = @Id AND UserId=@UserId)";
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
        

        public List<UserPosts> GetList(int UserId)
        {
            var connectionString = this.GetConnection();
            List<UserPosts> products = new List<UserPosts>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM UserPosts WHERE UserId="+UserId+" ORDER BY Id";
                    products = con.Query<UserPosts>(query).ToList();
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


        public UserPosts GetPost(int id)
        {
            var connectionString = this.GetConnection();
            UserPosts product = new UserPosts();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {

                    con.Open();
                    var query = "SELECT * FROM UserPosts WHERE Id =" + id;
                    product = con.Query<UserPosts>(query).FirstOrDefault();
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
