namespace Project.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Audit_Entity_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AuditUserId = c.String(),
                        ChangeType = c.String(),
                        ObjectType = c.String(),
                        FromJson = c.String(),
                        ToJson = c.String(),
                        TableName = c.String(),
                        IdentityJson = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Audit");
        }
    }
}
