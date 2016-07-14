namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_TypeId_To_Activity : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Activities", name: "Type_Id", newName: "TypeId");
            RenameIndex(table: "dbo.Activities", name: "IX_Type_Id", newName: "IX_TypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Activities", name: "IX_TypeId", newName: "IX_Type_Id");
            RenameColumn(table: "dbo.Activities", name: "TypeId", newName: "Type_Id");
        }
    }
}
