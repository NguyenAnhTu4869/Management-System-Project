namespace ADManageSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadteUser10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Topics", "Description", c => c.String());
            AlterColumn("dbo.TraineeClasses", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TraineeClasses", "Grade", c => c.String(nullable: false));
            AlterColumn("dbo.Topics", "Description", c => c.String(nullable: false));
        }
    }
}
