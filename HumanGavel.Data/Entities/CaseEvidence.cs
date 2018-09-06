using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public enum EvidenceType
    {
        Link = 0 // Default
    }

    public class CaseEvidence
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CaseEvidenceId { get; set; }

        public EvidenceType EvidenceType { get; set; }
        
        [Required, MaxLength(250)]
        public String Name { get; set; }

        [MaxLength(5000)]
        public String Value { get; set; }

        [MaxLength(500), Required]
        public String Description { get; set; }

        public DateTime CreateDateTimeUTC { get; set; }

        public DateTime? ModifiedDateTimeUTC { get; set; }
    }
}
