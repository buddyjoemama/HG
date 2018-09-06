using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public class VoterHistory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserID{ get; set; }

        public ICollection<Participant> Cases { get; set; }

        [Index]
        public VoteType VoteType { get; set; }

        public DateTime VoteCastDateTimeUTC { get; set; }
    }
}
