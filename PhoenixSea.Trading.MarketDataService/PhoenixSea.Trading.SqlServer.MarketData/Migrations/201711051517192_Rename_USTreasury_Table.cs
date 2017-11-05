namespace PhoenixSea.Trading.SqlServer.MarketData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_USTreasury_Table : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsTreasury", newName: "UsTreasuryData");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UsTreasuryData", newName: "UsTreasury");
        }
    }
}
