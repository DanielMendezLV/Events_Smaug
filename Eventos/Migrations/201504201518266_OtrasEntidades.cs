namespace Eventos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OtrasEntidades : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asientoes",
                c => new
                    {
                        IdAsiento = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false),
                        SeccionId = c.Int(nullable: false),
                        Fila = c.Int(nullable: false),
                        Columna = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        Seccion_IdSeccion = c.Int(),
                    })
                .PrimaryKey(t => t.IdAsiento)
                .ForeignKey("dbo.Seccions", t => t.Seccion_IdSeccion)
                .Index(t => t.Seccion_IdSeccion);
            
            CreateTable(
                "dbo.Entradas",
                c => new
                    {
                        IdEntrada = c.Int(nullable: false, identity: true),
                        Fecha = c.String(nullable: false),
                        AsientoId = c.Int(nullable: false),
                        SeccionId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        EventoId = c.Int(nullable: false),
                        Asiento_IdAsiento = c.Int(),
                        Evento_IdEvento = c.Int(),
                        Seccion_IdSeccion = c.Int(),
                        Usuario_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.IdEntrada)
                .ForeignKey("dbo.Asientoes", t => t.Asiento_IdAsiento)
                .ForeignKey("dbo.Eventoes", t => t.Evento_IdEvento)
                .ForeignKey("dbo.Seccions", t => t.Seccion_IdSeccion)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_IdUsuario)
                .Index(t => t.Asiento_IdAsiento)
                .Index(t => t.Evento_IdEvento)
                .Index(t => t.Seccion_IdSeccion)
                .Index(t => t.Usuario_IdUsuario);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Correo = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Nickname = c.String(nullable: false),
                        Telefono = c.Int(nullable: false),
                        RolId = c.Int(nullable: false),
                        Rol_IdRol = c.Int(),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .ForeignKey("dbo.Rols", t => t.Rol_IdRol)
                .Index(t => t.Rol_IdRol);
            
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        IdRol = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdRol);
            
            CreateTable(
                "dbo.Factura_Entrada",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacturaId = c.Int(nullable: false),
                        EntradaId = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Entrada_IdEntrada = c.Int(),
                        Factura_IdFactura = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entradas", t => t.Entrada_IdEntrada)
                .ForeignKey("dbo.Facturas", t => t.Factura_IdFactura)
                .Index(t => t.Entrada_IdEntrada)
                .Index(t => t.Factura_IdFactura);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        IdFactura = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        usuario_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.IdFactura)
                .ForeignKey("dbo.Usuarios", t => t.usuario_IdUsuario)
                .Index(t => t.usuario_IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Factura_Entrada", "Factura_IdFactura", "dbo.Facturas");
            DropForeignKey("dbo.Facturas", "usuario_IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Factura_Entrada", "Entrada_IdEntrada", "dbo.Entradas");
            DropForeignKey("dbo.Entradas", "Usuario_IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "Rol_IdRol", "dbo.Rols");
            DropForeignKey("dbo.Entradas", "Seccion_IdSeccion", "dbo.Seccions");
            DropForeignKey("dbo.Entradas", "Evento_IdEvento", "dbo.Eventoes");
            DropForeignKey("dbo.Entradas", "Asiento_IdAsiento", "dbo.Asientoes");
            DropForeignKey("dbo.Asientoes", "Seccion_IdSeccion", "dbo.Seccions");
            DropIndex("dbo.Factura_Entrada", new[] { "Factura_IdFactura" });
            DropIndex("dbo.Facturas", new[] { "usuario_IdUsuario" });
            DropIndex("dbo.Factura_Entrada", new[] { "Entrada_IdEntrada" });
            DropIndex("dbo.Entradas", new[] { "Usuario_IdUsuario" });
            DropIndex("dbo.Usuarios", new[] { "Rol_IdRol" });
            DropIndex("dbo.Entradas", new[] { "Seccion_IdSeccion" });
            DropIndex("dbo.Entradas", new[] { "Evento_IdEvento" });
            DropIndex("dbo.Entradas", new[] { "Asiento_IdAsiento" });
            DropIndex("dbo.Asientoes", new[] { "Seccion_IdSeccion" });
            DropTable("dbo.Facturas");
            DropTable("dbo.Factura_Entrada");
            DropTable("dbo.Rols");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Entradas");
            DropTable("dbo.Asientoes");
        }
    }
}
