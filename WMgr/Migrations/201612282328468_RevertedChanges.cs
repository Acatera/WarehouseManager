namespace WMgr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertedChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prices", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Prices", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InvoiceDetails", "SellPrice_PriceId", "dbo.Prices");
            DropIndex("dbo.InvoiceDetails", new[] { "SellPrice_PriceId" });
            DropIndex("dbo.Prices", new[] { "ProductId" });
            DropIndex("dbo.Prices", new[] { "CurrencyId" });
            AddColumn("dbo.InvoiceDetails", "SellPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.InvoiceDetails", "SellPrice_PriceId");
            DropTable("dbo.Currencies");
            DropTable("dbo.Prices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PriceId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        CultureId = c.Int(nullable: false),
                        CurrencyFormat = c.String(),
                        CurrencySymbol = c.String(),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            AddColumn("dbo.InvoiceDetails", "SellPrice_PriceId", c => c.Int());
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.InvoiceDetails", "SellPrice");
            CreateIndex("dbo.Prices", "CurrencyId");
            CreateIndex("dbo.Prices", "ProductId");
            CreateIndex("dbo.InvoiceDetails", "SellPrice_PriceId");
            AddForeignKey("dbo.InvoiceDetails", "SellPrice_PriceId", "dbo.Prices", "PriceId");
            AddForeignKey("dbo.Prices", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Prices", "CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
        }
    }
}
