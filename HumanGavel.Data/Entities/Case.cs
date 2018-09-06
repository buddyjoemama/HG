using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public enum CaseType
    {
        Open = 0, // No need for user for user acceptance
        Targeted = 1 // requires defendants approval.
    }

    public enum Category
    {
        Trending = 3,
        Popular = 4,
        Sports = 0,
        People = 1,
        Lifestyle = 2
    }

    public partial class Case
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CaseId { get; set; }

        [Index]
        [Index("IX_CAT_MARK_ENABLED", 0)]
        public Category Category { get; set; }

        [MaxLength(512)]
        public String ImageUrl { get; set; }

        [MaxLength]
        public String ThumbnailImageUrl { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(250)]
        [Required]
        public String Name { get; set; }

        [MaxLength(1024)]
        public String Keywords { get; set; }

        public ICollection<Participant> Participants { get; set; }

        public ICollection<User> UsersFollowing { get; set; }

        [Index]
        public User CreatedBy { get; set; }

        [Index("IX_FEATURED", 0)]
        public bool IsFeatured { get; set; }

        [Index]
        [Index("IX_CAT_MARK_ENABLED", 2)]
        [Index("IX_MARK_ENABLED", 0)]
        [Index("IX_FEATURED", 1)]
        public bool IsEnabled { get; set; }

        [Index]
        [Index("IX_CAT_MARK_ENABLED", 1)]
        [Index("IX_MARK_ENABLED", 1)]
        [Index("IX_FEATURED", 2)]
        public bool MarkForDelete { get; set; }

        public ICollection<CaseEvidence> Evidence { get; set; }

        public String CaseLayoutMetadata { get; set; }

        [Index("IX_EXP_DATE")]
        public DateTime? ExpirationDate { get; set; }

        public DateTime CreateDateTimeUTC { get; set; }

        [NotMapped]
        public DateTime MostRecentVoteDateTimeUTC { get; set; }
    }
}
