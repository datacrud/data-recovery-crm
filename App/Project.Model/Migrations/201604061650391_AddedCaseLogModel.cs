namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCaseLogModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseLog",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        HddInfoId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HddInfo", t => t.HddInfoId, cascadeDelete: true)
                .Index(t => t.Id, unique: true)
                .Index(t => t.HddInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaseLog", "HddInfoId", "dbo.HddInfo");
            DropIndex("dbo.CaseLog", new[] { "HddInfoId" });
            DropIndex("dbo.CaseLog", new[] { "Id" });
            DropTable("dbo.CaseLog");
        }
    }
}
