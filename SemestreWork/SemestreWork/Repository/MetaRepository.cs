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
    public class MetaRepository : IMetaRepository
    {

        IConfiguration _configuration;

        public MetaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int EditMeta(MetaPost metaPost)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE MetaPosts SET Text = @Text WHERE Id = @Id";
                    count = con.Execute(query, metaPost);
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
        public MetaPost GetMeta(int id)
        {
            var connectionString = this.GetConnection();
            MetaPost product = new MetaPost();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {

                    con.Open();
                    var query = "SELECT * FROM MetaPosts WHERE Id =" + id;
                    product = con.Query<MetaPost>(query).FirstOrDefault();
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
