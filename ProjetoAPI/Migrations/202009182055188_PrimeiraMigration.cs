namespace ProjetoAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiraMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Descricao = c.String(nullable: false, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        DataAlteracao = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        UsuarioAdm = c.Boolean(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
            DropTable("dbo.Produtos");
        }
    }
}
