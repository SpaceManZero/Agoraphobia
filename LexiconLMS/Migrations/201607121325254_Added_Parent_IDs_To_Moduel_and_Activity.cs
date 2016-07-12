namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Parent_IDs_To_Moduel_and_Activity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "Module_Id", "dbo.Modules");
            DropForeignKey("dbo.Modules", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Activities", new[] { "Module_Id" });
            DropIndex("dbo.Modules", new[] { "Course_Id" });
            RenameColumn(table: "dbo.Activities", name: "Module_Id", newName: "ModuleID");
            RenameColumn(table: "dbo.Modules", name: "Course_Id", newName: "CourseId");
            AlterColumn("dbo.Activities", "ModuleID", c => c.Int(nullable: false));
            AlterColumn("dbo.Modules", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Activities", "ModuleID");
            CreateIndex("dbo.Modules", "CourseId");
            AddForeignKey("dbo.Activities", "ModuleID", "dbo.Modules", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Modules", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Activities", "ModuleID", "dbo.Modules");
            DropIndex("dbo.Modules", new[] { "CourseId" });
            DropIndex("dbo.Activities", new[] { "ModuleID" });
            AlterColumn("dbo.Modules", "CourseId", c => c.Int());
            AlterColumn("dbo.Activities", "ModuleID", c => c.Int());
            RenameColumn(table: "dbo.Modules", name: "CourseId", newName: "Course_Id");
            RenameColumn(table: "dbo.Activities", name: "ModuleID", newName: "Module_Id");
            CreateIndex("dbo.Modules", "Course_Id");
            CreateIndex("dbo.Activities", "Module_Id");
            AddForeignKey("dbo.Modules", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Activities", "Module_Id", "dbo.Modules", "Id");
        }
    }
}
