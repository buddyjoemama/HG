using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public class Votes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ParticipantVoteId { get; set; }

        //public Guid ParticipantOneId { get; set; }

        //public Guid ParticipantTwoId { get; set; }

        public Guid ParticipantId { get; set; }

        [Index]
        public Case Case { get; set; }

        public Guid CaseId { get; set; }

        [Index("IX_VOTER_ID")]
        public User Voter { get; set; }
        
        public int Value { get; set; }

        public DateTime VoteCastDateTimeUTC { get; set; }
    }
}
