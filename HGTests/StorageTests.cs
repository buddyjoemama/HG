using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGTests
{
    [TestClass]
    public class StorageTests
    {
        [TestMethod]
        public void TestCloudStorageConnection()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["CloudStorageConnectionString"]);

            storageAccount
                .CreateCloudBlobClient()
                .GetContainerReference("images")
                .CreateIfNotExists();

            bool exists = storageAccount
                .CreateCloudBlobClient()
                .GetContainerReference("images")
                .Exists();

            Assert.IsTrue(exists);
        }
    }
}
