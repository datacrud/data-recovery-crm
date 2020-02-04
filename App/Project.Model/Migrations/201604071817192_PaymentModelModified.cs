namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentModelModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "CustomerId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Payment", "Amount", c => c.Double(nullable: false));
            CreateIndex("dbo.Payment", "CustomerId");
            AddForeignKey("dbo.Payment", "CustomerId", "dbo.Customer", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Payment", new[] { "CustomerId" });
            AlterColumn("dbo.Payment", "Amount", c => c.Single(nullable: false));
            DropColumn("dbo.Payment", "CustomerId");
        }
    }
}
