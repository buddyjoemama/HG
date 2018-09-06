using HumanGavel.Common.Configuration;
using HumanGavel.Data.Documents.Entites;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Documents
{
    public class DocumentDbContext
    {
        public static string DATABASE_NAME = "hg_docs";
        public static string DATABASE_PATH = $"dbs/{DATABASE_NAME}";
        public static string COLLECTIONS_PATH = $"/{DATABASE_PATH}/colls/";

        internal DocumentClient client = null;

        public DocumentDbContext()
        {
            client = new DocumentClient(new Uri(ApplicationConfigurationManager.DocumentDbURI), ApplicationConfigurationManager.DocumentDbKey);
        }

        private Database GetDatabase()
        {
            var db = client.CreateDatabaseQuery()
                .Where(s => s.Id == DATABASE_NAME)
                .AsEnumerable()
                .FirstOrDefault();

            if (db == null)
                db = client.CreateDatabaseAsync(new Database { Id = DATABASE_NAME }).Result;

            return db;
        }

        [Collection]
        public DocumentCollectionsContext<CaseDocument> Cases
        {
            get
            {
                String collectionName = $"{COLLECTIONS_PATH}cases";

                return new DocumentCollectionsContext<CaseDocument>(this,
                    collectionName,
                    client.CreateDocumentQuery<CaseDocument>(collectionName));
            }
        }
    }
}
