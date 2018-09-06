using HumanGavel.Common.Storage;
using HumanGavel.Data;
using HumanGavel.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanGavel.Common;

namespace HGTests
{
    [TestClass]
    public class CaseTests
    {
        [TestMethod]
        public void CleanData()
        {
            using (HGDataContext context = new HGDataContext())
            {
                context.Database.ExecuteSqlCommand("delete from Participants");
                context.Database.ExecuteSqlCommand("delete from cases");
            }
        }

        [TestMethod]
        public void CreateCases()
        {
            String configuration = File.ReadAllText(@"Cases\cases.js");

            var data = JsonConvert.DeserializeAnonymousType(configuration,
                new
                {
                    cases = new List<JSONCase>()
                });

            using (HGDataContext context = new HGDataContext())
            {
                foreach (var c in data.cases)
                {
                    Case dbCase = new Case();
                    dbCase.Name = c.name;
                    dbCase.CreateDateTimeUTC = DateTime.UtcNow;
                    dbCase.IsFeatured = c.isFeatured;
                    dbCase.IsEnabled = true;
                    dbCase.Category = (Category)Enum.Parse(typeof(Category), c.category);
                    dbCase.Participants = new List<Participant>();
                    dbCase.Keywords = String.Join(",", c.keywords);

                    DateTime date = DateTime.Now;
                    if(DateTime.TryParse(c.expiration, out date))
                    {
                        dbCase.ExpirationDate = date;
                    }

                    foreach(var p in c.participants)
                    {
                        Participant dbP = new Participant();
                        dbP.Name = p;
                        dbP.NameHash = p.HashString();
                        dbCase.Participants.Add(dbP);
                    }

                    byte[] image = File.ReadAllBytes(@"Cases\" + c.image);

                    //Write to blob storage.
                    var blob = BlobStorageManager.Images.StoreBytes(image, image.HashBytes());

                    dbCase.ImageUrl = blob.Uri.ToString();
                    context.Cases.Add(dbCase);
                    context.SaveChanges();
                }
            }
        }
    }

    public class JSONCase
    {
        public string name { get; set; }
        public string image { get; set; }
        public bool isFeatured { get; set; }
        public string[] keywords { get; set; }
        public String category { get; set; }
        public string[] participants { get; set; }
        public string expiration { get; set; }
    }
}
