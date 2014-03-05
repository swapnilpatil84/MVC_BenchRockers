namespace BenchRockers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmpId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.String(),
                        Account = c.String(),
                        TotalExp = c.Single(nullable: false),
                        Location = c.String(),
                        IsOnBench = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmpId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SkillId);
            
            CreateTable(
                "dbo.EmployeeSkills",
                c => new
                    {
                        EmpSkillId = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpSkillId)
                .ForeignKey("dbo.Employees", t => t.EmpId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.EmpId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                        SupervisorName = c.String(),
                        Recommendation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmpId, cascadeDelete: true)
                .Index(t => t.EmpId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Recommendations", new[] { "EmpId" });
            DropIndex("dbo.EmployeeSkills", new[] { "SkillId" });
            DropIndex("dbo.EmployeeSkills", new[] { "EmpId" });
            DropForeignKey("dbo.Recommendations", "EmpId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.EmployeeSkills", "EmpId", "dbo.Employees");
            DropTable("dbo.Recommendations");
            DropTable("dbo.EmployeeSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.Employees");
        }
    }
}
