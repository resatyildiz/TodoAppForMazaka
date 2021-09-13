namespace TodoApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class todoIliskiMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Todoes", name: "CreatedFrom_Id", newName: "CreatedFromId");
            RenameColumn(table: "dbo.Todoes", name: "TodoFrom_Id", newName: "TodoFromId");
            RenameColumn(table: "dbo.Todoes", name: "TodoTo_Id", newName: "TodoToId");
            RenameColumn(table: "dbo.Todoes", name: "UpdatedFrom_Id", newName: "UpdatedFromId");
            RenameIndex(table: "dbo.Todoes", name: "IX_TodoFrom_Id", newName: "IX_TodoFromId");
            RenameIndex(table: "dbo.Todoes", name: "IX_TodoTo_Id", newName: "IX_TodoToId");
            RenameIndex(table: "dbo.Todoes", name: "IX_CreatedFrom_Id", newName: "IX_CreatedFromId");
            RenameIndex(table: "dbo.Todoes", name: "IX_UpdatedFrom_Id", newName: "IX_UpdatedFromId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Todoes", name: "IX_UpdatedFromId", newName: "IX_UpdatedFrom_Id");
            RenameIndex(table: "dbo.Todoes", name: "IX_CreatedFromId", newName: "IX_CreatedFrom_Id");
            RenameIndex(table: "dbo.Todoes", name: "IX_TodoToId", newName: "IX_TodoTo_Id");
            RenameIndex(table: "dbo.Todoes", name: "IX_TodoFromId", newName: "IX_TodoFrom_Id");
            RenameColumn(table: "dbo.Todoes", name: "UpdatedFromId", newName: "UpdatedFrom_Id");
            RenameColumn(table: "dbo.Todoes", name: "TodoToId", newName: "TodoTo_Id");
            RenameColumn(table: "dbo.Todoes", name: "TodoFromId", newName: "TodoFrom_Id");
            RenameColumn(table: "dbo.Todoes", name: "CreatedFromId", newName: "CreatedFrom_Id");
        }
    }
}
