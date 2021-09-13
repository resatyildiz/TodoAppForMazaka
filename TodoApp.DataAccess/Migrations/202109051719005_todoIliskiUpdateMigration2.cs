namespace TodoApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class todoIliskiUpdateMigration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Todoes", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Todoes", "Content", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Todoes", "Content", c => c.String(maxLength: 250));
            AlterColumn("dbo.Todoes", "Title", c => c.String(maxLength: 100));
        }
    }
}
