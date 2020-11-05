namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class primera : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Contenido = c.String(nullable: false),
                        Categoria = c.String(),
                        FechaDeCreacion = c.DateTime(),
                        Imagen = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.IngresosGastosJMRs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IngresosGastosJMRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 60),
                        EsIngreso = c.Boolean(nullable: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Posts");
        }
    }
}
