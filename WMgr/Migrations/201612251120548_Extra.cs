namespace WarehouseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceDetails", "SellPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.InvoiceDetails", "SellPrice");
        }
    }
}
