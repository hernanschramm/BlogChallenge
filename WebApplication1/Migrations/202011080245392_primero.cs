namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class primero : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Estado", c => c.Boolean(nullable: false));
            DropColumn("dbo.Posts", "Activo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Activo", c => c.Boolean(nullable: false));
            DropColumn("dbo.Posts", "Estado");
        }
    }
}
