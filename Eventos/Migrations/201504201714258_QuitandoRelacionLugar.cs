namespace Eventos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuitandoRelacionLugar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seccions", "Lugar_IdLugar", "dbo.Lugars");
            DropIndex("dbo.Seccions", new[] { "Lugar_IdLugar" });
            DropColumn("dbo.Seccions", "LugarId");
            DropColumn("dbo.Seccions", "Lugar_IdLugar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seccions", "Lugar_IdLugar", c => c.Int());
            AddColumn("dbo.Seccions", "LugarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Seccions", "Lugar_IdLugar");
            AddForeignKey("dbo.Seccions", "Lugar_IdLugar", "dbo.Lugars", "IdLugar");
        }
    }
}
