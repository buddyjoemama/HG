using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public partial class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        public ICollection<Votes> Votes { get; set; }

        public ICollection<Case> Following { get; set; }

        public String Metadata { get; set; }

        [ConcurrencyCheck]
        [MaxLength(50)]
        public String ModifiedDateTimeUTC { get; set; }

        public DateTime CreateDateTimeUTC { get; set; }
    }
}
