namespace HumanGavel.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        CaseId = c.Guid(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        ImageUrl = c.String(maxLength: 512),
                        ThumbnailImageUrl = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        Keywords = c.String(maxLength: 1024),
                        IsFeatured = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        MarkForDelete = c.Boolean(nullable: false),
                        CaseLayoutMetadata = c.String(),
                        ExpirationDate = c.DateTime(),
                        CreateDateTimeUTC = c.DateTime(nullable: false),
                        CreatedBy_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.CaseId)
                .ForeignKey("dbo.Users", t => t.CreatedBy_UserId)
                .Index(t => t.Category)
                .Index(t => new { t.Category, t.MarkForDelete, t.IsEnabled }, name: "IX_CAT_MARK_ENABLED")
                .Index(t => t.Name, unique: true)
                .Index(t => new { t.IsFeatured, t.IsEnabled, t.MarkForDelete }, name: "IX_FEATURED")
                .Index(t => t.IsEnabled)
                .Index(t => new { t.IsEnabled, t.MarkForDelete }, name: "IX_MARK_ENABLED")
                .Index(t => t.MarkForDelete)
                .Index(t => t.ExpirationDate, name: "IX_EXP_DATE")
                .Index(t => t.CreatedBy_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        Metadata = c.String(),
                        ModifiedDateTimeUTC = c.String(maxLength: 50),
                        CreateDateTimeUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        ParticipantVoteId = c.Guid(nullable: false, identity: true),
                        ParticipantId = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                        Value = c.Int(nullable: false),
                        VoteCastDateTimeUTC = c.DateTime(nullable: false),
                        VoterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ParticipantVoteId)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.VoterId, cascadeDelete: true)
                .Index(t => t.CaseId)
                .Index(t => t.VoterId);
            
            CreateTable(
                "dbo.CaseEvidences",
                c => new
                    {
                        CaseEvidenceId = c.Guid(nullable: false, identity: true),
                        EvidenceType = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        Value = c.String(),
                        Description = c.String(nullable: false, maxLength: 500),
                        CreateDateTimeUTC = c.DateTime(nullable: false),
                        ModifiedDateTimeUTC = c.DateTime(),
                        Case_CaseId = c.Guid(),
                    })
                .PrimaryKey(t => t.CaseEvidenceId)
                .ForeignKey("dbo.Cases", t => t.Case_CaseId)
                .Index(t => t.Case_CaseId);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ParticipantId = c.Guid(nullable: false, identity: true),
                        CaseId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        NameHash = c.String(nullable: false, maxLength: 250),
                        ParticipantLayoutMetadata = c.String(),
                        UserAsParticpiant_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.ParticipantId)
                .ForeignKey("dbo.Users", t => t.UserAsParticpiant_UserId)
                .ForeignKey("dbo.Cases", t => t.CaseId)
                .Index(t => new { t.ParticipantId, t.CaseId }, name: "IX_PART_CASE")
                .Index(t => t.CaseId)
                .Index(t => t.NameHash, unique: true)
                .Index(t => t.UserAsParticpiant_UserId);
            
            CreateTable(
                "dbo.Following",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CaseId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.Participants", "UserAsParticpiant_UserId", "dbo.Users");
            DropForeignKey("dbo.CaseEvidences", "Case_CaseId", "dbo.Cases");
            DropForeignKey("dbo.Cases", "CreatedBy_UserId", "dbo.Users");
            DropForeignKey("dbo.Votes", "VoterId", "dbo.Users");
            DropForeignKey("dbo.Votes", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.Following", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.Following", "UserId", "dbo.Users");
            DropIndex("dbo.Following", new[] { "CaseId" });
            DropIndex("dbo.Following", new[] { "UserId" });
            DropIndex("dbo.Participants", new[] { "UserAsParticpiant_UserId" });
            DropIndex("dbo.Participants", new[] { "NameHash" });
            DropIndex("dbo.Participants", new[] { "CaseId" });
            DropIndex("dbo.Participants", "IX_PART_CASE");
            DropIndex("dbo.CaseEvidences", new[] { "Case_CaseId" });
            DropIndex("dbo.Votes", new[] { "VoterId" });
            DropIndex("dbo.Votes", new[] { "CaseId" });
            DropIndex("dbo.Cases", new[] { "CreatedBy_UserId" });
            DropIndex("dbo.Cases", "IX_EXP_DATE");
            DropIndex("dbo.Cases", new[] { "MarkForDelete" });
            DropIndex("dbo.Cases", "IX_MARK_ENABLED");
            DropIndex("dbo.Cases", new[] { "IsEnabled" });
            DropIndex("dbo.Cases", "IX_FEATURED");
            DropIndex("dbo.Cases", new[] { "Name" });
            DropIndex("dbo.Cases", "IX_CAT_MARK_ENABLED");
            DropIndex("dbo.Cases", new[] { "Category" });
            DropTable("dbo.Following");
            DropTable("dbo.Participants");
            DropTable("dbo.CaseEvidences");
            DropTable("dbo.Votes");
            DropTable("dbo.Users");
            DropTable("dbo.Cases");
        }
    }
}
