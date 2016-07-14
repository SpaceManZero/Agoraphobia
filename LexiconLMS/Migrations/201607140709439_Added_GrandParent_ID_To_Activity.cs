namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_GrandParent_ID_To_Activity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "CourceId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "CourceId");
        }
    }
}
