using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public class Participant
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index("IX_PART_CASE", 0)]
        public Guid ParticipantId { get; set; }

        /// <summary>
        /// Optional user
        /// </summary>
        [Index]
        public User UserAsParticpiant { get; set; }

        [Index]
        public Case Case { get; set; }

        [Index]
        [Index("IX_PART_CASE", 1)]
        public Guid CaseId { get; set; }

        [Required]
        public String Name { get; set; }

        [MaxLength(250), Required, Index(IsUnique =true)]
        public String NameHash { get; set; }

        public String ParticipantLayoutMetadata { get; set; }
    }
}
