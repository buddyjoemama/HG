using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Common.Entities
{
    public class CaseResult
    {
        public Guid CaseId { get; set; }
        public Guid ParticipantId { get; set; }
        public int Votes { get; set; }
        public String CaseName { get; set; }
        public String ParticipantName { get; set; }
        public String ImageUrl { get; set; }
        public DateTime MostRecentVoteCaseDateTimeUTC { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
