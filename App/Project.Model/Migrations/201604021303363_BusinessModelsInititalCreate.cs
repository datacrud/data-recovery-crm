namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusinessModelsInititalCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        CompanyName = c.String(),
                        Reference = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
            CreateTable(
                "dbo.HddInfo",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CustomerId = c.Guid(nullable: false),
                        CaseNo = c.String(),
                        Brand = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Capacity = c.String(nullable: false),
                        InterfaceType = c.String(nullable: false),
                        Sl = c.String(),
                        RequiredData = c.String(nullable: false),
                        Note = c.String(),
                        Status = c.Int(nullable: false),
                        ReceiveDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        DueAmount = c.Double(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.Id, unique: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Expense",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        HddInfoId = c.Guid(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        Amount = c.Single(nullable: false),
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
            DropForeignKey("dbo.Payment", "HddInfoId", "dbo.HddInfo");
            DropForeignKey("dbo.HddInfo", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Payment", new[] { "HddInfoId" });
            DropIndex("dbo.Payment", new[] { "Id" });
            DropIndex("dbo.Expense", new[] { "Id" });
            DropIndex("dbo.HddInfo", new[] { "CustomerId" });
            DropIndex("dbo.HddInfo", new[] { "Id" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropTable("dbo.Payment");
            DropTable("dbo.Expense");
            DropTable("dbo.HddInfo");
            DropTable("dbo.Customer");
        }
    }
}
