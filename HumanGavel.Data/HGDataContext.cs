using HumanGavel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data
{
    public class HGDataContext : DbContext
    {
        private static string s_dbName;

        public HGDataContext() :
            base("DefaultConnection")
        {
            
        }

#if DEBUG
        public HGDataContext(String name) :
            base(name)
        {
        
        }

        public static HGDataContext ForTest
        {
            get
            {
                if (s_dbName == null)
                    s_dbName = "Test";

                return new HGDataContext(s_dbName);
            }
        }
#endif

        public DbSet<Case> Cases { get; set; }

        public DbSet<CaseEvidence> Evidence { get; set; }

        public DbSet<Votes> Votes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Case>(s => s.Following)
                .WithMany(c => c.UsersFollowing)
                .Map(s =>
                {
                    s.MapLeftKey("UserId");
                    s.MapRightKey("CaseId");
                    s.ToTable("Following");
                });

            modelBuilder.Entity<User>()
                .HasMany(s => s.Votes)
                .WithRequired(s => s.Voter)
                .Map(s =>
                {
                    s.MapKey("VoterId");
                });

            //modelBuilder.Entity<Participant>()
            //    .HasMany(s => s.Votes)
            //    .WithRequired(s => s.VotedForParticipant)
            //    .Map(s =>
            //    {
            //        s.MapKey("VotedForParticipantId");
            //    });

            modelBuilder.Entity<Case>()
                .HasMany(s => s.Participants)
                .WithRequired(s => s.Case)
                .WillCascadeOnDelete(false);
            //modelBuilder.Entity<ParticipantVotes>()
            //    .HasRequired(s => s.Case)
            //    .WithOptional()
            //    .Map(s =>
            //    {
            //        s.MapKey("CaseId");
            //    });
        }
    }
}
