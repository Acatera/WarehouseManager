namespace WarehouseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedInvoiceDetailDefinition : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceDetails", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceDetails", "Quantity", c => c.Single(nullable: false));
        }
    }
}
