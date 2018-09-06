using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using HumanGavel.Data;
using HumanGavel.Data.Documents;
using HumanGavel.Data.Documents.Entites;
using System.Threading.Tasks;
using System.Linq;
using HumanGavel.Data.Entities;
using System.Data.Entity.Infrastructure;
using HumanGavel.Common;

namespace HGTests
{
    [TestClass]
    public class DBTests : DestructiveDBTests
    {
        [TestMethod]
        public void TestVoteConcurrencyPass()
        {
            using (HGDataContext context = HGDataContext.ForTest)
            {
                var user1 = context.Users.First();
                var user2 = context.Users.First();

                user1.ModifiedDateTimeUTC = DateTime.Today.ToString();
                user1.Metadata = "some data";
                context.SaveChanges();
            }

            using (HGDataContext c = HGDataContext.ForTest)
            {
                var user1 = c.Users.First();

                user1.ModifiedDateTimeUTC = DateTime.UtcNow.ToString();
                user1.Metadata = "This is a test";

                c.SaveChanges();
            }
        }

        //[TestMethod]
        //public async Task TestSimpleDocumentAdd()
        //{
        //    DocumentDbContext context = new DocumentDbContext();
        //    Assert.IsNotNull(context);

        //    await context.Cases.CreateDocumentAsync(new CaseDocument
        //    {

        //    });

        //    var items = context.Cases.AsEnumerable().ToList();

        //    Assert.IsTrue(items.Count > 0);
        //}
    }
}
