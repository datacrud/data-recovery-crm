namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevenueModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Revenue",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Revenue", new[] { "Id" });
            DropTable("dbo.Revenue");
        }
    }
}
