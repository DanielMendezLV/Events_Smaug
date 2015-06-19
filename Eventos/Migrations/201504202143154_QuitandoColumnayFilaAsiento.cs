namespace Eventos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuitandoColumnayFilaAsiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seccions", "Fila", c => c.Int(nullable: false));
            AddColumn("dbo.Seccions", "Columna", c => c.Int(nullable: false));
            DropColumn("dbo.Asientoes", "Fila");
            DropColumn("dbo.Asientoes", "Columna");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Asientoes", "Columna", c => c.Int(nullable: false));
            AddColumn("dbo.Asientoes", "Fila", c => c.Int(nullable: false));
            DropColumn("dbo.Seccions", "Columna");
            DropColumn("dbo.Seccions", "Fila");
        }
    }
}
