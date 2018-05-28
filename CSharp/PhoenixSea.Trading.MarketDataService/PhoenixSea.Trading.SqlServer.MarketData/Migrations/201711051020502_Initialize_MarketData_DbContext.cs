namespace PhoenixSea.Trading.SqlServer.MarketData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize_MarketData_DbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsTreasury",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LongTermRealAverageRate = c.Double(),
                        AssetProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetProduct", t => t.AssetProductId, cascadeDelete: true)
                .Index(t => t.AssetProductId, name: "IX_AssetProduct_Id");
            
            CreateTable(
                "dbo.AssetProduct",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 1000, unicode: false),
                        AssetClassId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetClass", t => t.AssetClassId, cascadeDelete: true)
                .Index(t => t.AssetClassId, name: "IX_AssetClass_Id");
            
            CreateTable(
                "dbo.AssetClass",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsTreasury", "AssetProductId", "dbo.AssetProduct");
            DropForeignKey("dbo.AssetProduct", "AssetClassId", "dbo.AssetClass");
            DropIndex("dbo.AssetProduct", "IX_AssetClass_Id");
            DropIndex("dbo.UsTreasury", "IX_AssetProduct_Id");
            DropTable("dbo.AssetClass");
            DropTable("dbo.AssetProduct");
            DropTable("dbo.UsTreasury");
        }
    }
}
