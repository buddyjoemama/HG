using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Documents.Entites
{
    [Serializable]
    public abstract class DocumentBase
    {
        [JsonProperty(PropertyName = "id")]
        public Guid DocumentGuid { get; set; }
    }
}
