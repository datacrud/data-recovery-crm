namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountPropertyAddedOnHddInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HddInfo", "DiscountPercent", c => c.Double(nullable: false));
            AddColumn("dbo.HddInfo", "DiscountAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HddInfo", "DiscountAmount");
            DropColumn("dbo.HddInfo", "DiscountPercent");
        }
    }
}
