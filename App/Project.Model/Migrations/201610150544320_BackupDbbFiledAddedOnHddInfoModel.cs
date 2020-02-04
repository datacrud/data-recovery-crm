namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackupDbbFiledAddedOnHddInfoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HddInfo", "BackupHdd", c => c.String());
            AlterColumn("dbo.HddInfo", "CaseNo", c => c.String(maxLength: 20));
            CreateIndex("dbo.HddInfo", "CaseNo", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.HddInfo", new[] { "CaseNo" });
            AlterColumn("dbo.HddInfo", "CaseNo", c => c.String());
            DropColumn("dbo.HddInfo", "BackupHdd");
        }
    }
}
