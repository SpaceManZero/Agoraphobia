namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed_Typo_in_Activity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "CourseId", c => c.Int());
            DropColumn("dbo.Activities", "CourceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activities", "CourceId", c => c.Int());
            DropColumn("dbo.Activities", "CourseId");
        }
    }
}
