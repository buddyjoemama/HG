using HumanGavel.Data;
using HumanGavel.Data.Entities;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace HGTests
{
    public class DestructiveDBTests : IDbConnectionFactory
    {
        private static string db_instance_name = DateTime.Now.Ticks.ToString();

        public DestructiveDBTests()
        {
            Database.DefaultConnectionFactory = this;

            using (HGDataContext context = HGDataContext.ForTest)
            {
                context.Database.CreateIfNotExists();
            }

            CreateSingleUser();
        }

        private void CreateSingleUser()
        {
            using (HGDataContext cont = HGDataContext.ForTest)
            {
                User u = new User();
                u.CreateDateTimeUTC = DateTime.UtcNow;

                cont.Users.Add(u);
                cont.SaveChanges();
            }
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbConnection conn = new SqlConnection(
                String.Format("Data Source=.;Database=HumanGavelDb_{0}_{1};Integrated Security=SSPI",
                    nameOrConnectionString,
                    db_instance_name));

            return conn;
        }
    }
}