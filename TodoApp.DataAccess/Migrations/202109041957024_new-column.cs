namespace TodoApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolumn : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Todoes", name: "UpdatedTo_Id", newName: "TodoFrom_Id");
            RenameIndex(table: "dbo.Todoes", name: "IX_UpdatedTo_Id", newName: "IX_TodoFrom_Id");
            AddColumn("dbo.Todoes", "TodoTo_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Todoes", "UpdatedFrom_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Todoes", "TodoTo_Id");
            CreateIndex("dbo.Todoes", "UpdatedFrom_Id");
            AddForeignKey("dbo.Todoes", "TodoTo_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Todoes", "UpdatedFrom_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todoes", "UpdatedFrom_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Todoes", "TodoTo_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Todoes", new[] { "UpdatedFrom_Id" });
            DropIndex("dbo.Todoes", new[] { "TodoTo_Id" });
            DropColumn("dbo.Todoes", "UpdatedFrom_Id");
            DropColumn("dbo.Todoes", "TodoTo_Id");
            RenameIndex(table: "dbo.Todoes", name: "IX_TodoFrom_Id", newName: "IX_UpdatedTo_Id");
            RenameColumn(table: "dbo.Todoes", name: "TodoFrom_Id", newName: "UpdatedTo_Id");
        }
    }
}
