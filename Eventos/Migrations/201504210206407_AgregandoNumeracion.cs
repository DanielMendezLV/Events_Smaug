namespace Eventos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoNumeracion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seccions", "Numeracion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Seccions", "Numeracion");
        }
    }
}
