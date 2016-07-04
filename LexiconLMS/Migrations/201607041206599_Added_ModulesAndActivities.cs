namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ModulesAndActivities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Module_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.Module_Id)
                .ForeignKey("dbo.ActivityTypes", t => t.Type_Id)
                .Index(t => t.Module_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "Type_Id", "dbo.ActivityTypes");
            DropForeignKey("dbo.Modules", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Activities", "Module_Id", "dbo.Modules");
            DropIndex("dbo.Modules", new[] { "Course_Id" });
            DropIndex("dbo.Activities", new[] { "Type_Id" });
            DropIndex("dbo.Activities", new[] { "Module_Id" });
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Modules");
            DropTable("dbo.Activities");
        }
    }
}
