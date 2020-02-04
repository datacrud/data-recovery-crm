namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeesTypeAddedOnPaymentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "FeesType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "FeesType");
        }
    }
}
