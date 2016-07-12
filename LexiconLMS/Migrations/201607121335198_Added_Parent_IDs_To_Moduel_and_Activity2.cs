namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Parent_IDs_To_Moduel_and_Activity2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropIndex("dbo.Activities", new[] { "ModuleID" });
            DropIndex("dbo.Modules", new[] { "CourseId" });
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int());
            AlterColumn("dbo.Modules", "CourseId", c => c.Int());
            CreateIndex("dbo.Activities", "ModuleId");
            CreateIndex("dbo.Modules", "CourseId");
            AddForeignKey("dbo.Activities", "ModuleId", "dbo.Modules", "Id");
            AddForeignKey("dbo.Modules", "CourseId", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropIndex("dbo.Modules", new[] { "CourseId" });
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            AlterColumn("dbo.Modules", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Modules", "CourseId");
            CreateIndex("dbo.Activities", "ModuleID");
            AddForeignKey("dbo.Modules", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "ModuleID", "dbo.Modules", "Id", cascadeDelete: true);
        }
    }
}
