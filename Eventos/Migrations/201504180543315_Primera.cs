namespace Eventos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primera : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evento_Seccion",
                c => new
                    {
                        EventoId = c.Int(nullable: false),
                        SeccionId = c.Int(nullable: false),
                        Evento_IdEvento = c.Int(),
                        Seccion_IdSeccion = c.Int(),
                    })
                .PrimaryKey(t => new { t.EventoId, t.SeccionId })
                .ForeignKey("dbo.Eventoes", t => t.Evento_IdEvento)
                .ForeignKey("dbo.Seccions", t => t.Seccion_IdSeccion)
                .Index(t => t.Evento_IdEvento)
                .Index(t => t.Seccion_IdSeccion);
            
            CreateTable(
                "dbo.Eventoes",
                c => new
                    {
                        IdEvento = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        EntradasDisponibles = c.Int(nullable: false),
                        Foto = c.String(nullable: false),
                        LugarId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        Lugar_IdLugar = c.Int(),
                        Tipo_IdTipo = c.Int(),
                    })
                .PrimaryKey(t => t.IdEvento)
                .ForeignKey("dbo.Lugars", t => t.Lugar_IdLugar)
                .ForeignKey("dbo.Tipoes", t => t.Tipo_IdTipo)
                .Index(t => t.Lugar_IdLugar)
                .Index(t => t.Tipo_IdTipo);
            
            CreateTable(
                "dbo.Lugars",
                c => new
                    {
                        IdLugar = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        NoEntradas = c.Int(nullable: false),
                        Foto = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdLugar);
            
            CreateTable(
                "dbo.Tipoes",
                c => new
                    {
                        IdTipo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdTipo);
            
            CreateTable(
                "dbo.Seccions",
                c => new
                    {
                        IdSeccion = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        NoAsientos = c.Int(nullable: false),
                        Precio = c.Int(nullable: false),
                        LugarId = c.Int(nullable: false),
                        Lugar_IdLugar = c.Int(),
                    })
                .PrimaryKey(t => t.IdSeccion)
                .ForeignKey("dbo.Lugars", t => t.Lugar_IdLugar)
                .Index(t => t.Lugar_IdLugar);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seccions", "Lugar_IdLugar", "dbo.Lugars");
            DropForeignKey("dbo.Evento_Seccion", "Seccion_IdSeccion", "dbo.Seccions");
            DropForeignKey("dbo.Eventoes", "Tipo_IdTipo", "dbo.Tipoes");
            DropForeignKey("dbo.Eventoes", "Lugar_IdLugar", "dbo.Lugars");
            DropForeignKey("dbo.Evento_Seccion", "Evento_IdEvento", "dbo.Eventoes");
            DropIndex("dbo.Seccions", new[] { "Lugar_IdLugar" });
            DropIndex("dbo.Evento_Seccion", new[] { "Seccion_IdSeccion" });
            DropIndex("dbo.Eventoes", new[] { "Tipo_IdTipo" });
            DropIndex("dbo.Eventoes", new[] { "Lugar_IdLugar" });
            DropIndex("dbo.Evento_Seccion", new[] { "Evento_IdEvento" });
            DropTable("dbo.Seccions");
            DropTable("dbo.Tipoes");
            DropTable("dbo.Lugars");
            DropTable("dbo.Eventoes");
            DropTable("dbo.Evento_Seccion");
        }
    }
}
