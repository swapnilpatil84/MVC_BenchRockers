namespace BenchRockers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeRoleRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "RoleId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Employees", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            CreateIndex("dbo.Employees", "RoleId");
            DropColumn("dbo.Employees", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Role", c => c.String());
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropColumn("dbo.Employees", "RoleId");
        }
    }
}
