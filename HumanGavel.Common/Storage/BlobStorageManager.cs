using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using HumanGavel.Common.Configuration;
using System.IO;

namespace HumanGavel.Common.Storage
{
    public class BlobStorageManager
    {
        private static CloudBlobClient blobClient = null;

        static BlobStorageManager()
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(ApplicationConfigurationManager.CloudStorageConnectionString);

            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public static CloudBlobContainerOperation Images
        {
            get
            {
                var container = blobClient.GetContainerReference("images");
                container.CreateIfNotExists();

                container.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                return new CloudBlobContainerOperation(container);
            }
        }
    }

    public class CloudBlobContainerOperation
    {
        private CloudBlobContainer _container = null;

        public CloudBlobContainerOperation(CloudBlobContainer container)
        {
            _container = container;
        }

        public bool RemoveBlob(Guid blobKey)
        {
            var blob = _container.GetBlobReference(blobKey.ToString());
            return blob.DeleteIfExists();
        }

        /// <summary>
        /// Store bytes in cloud storage.
        /// </summary>
        public CloudBlockBlob StoreBytes(byte[] data, Guid blobKey)
        {
            var blob = _container.GetBlockBlobReference(blobKey.ToString());
            blob.UploadFromByteArray(data, 0, data.Length);

            return blob;
        }
    }
}
