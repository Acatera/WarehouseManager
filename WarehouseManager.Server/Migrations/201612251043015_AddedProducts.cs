namespace WarehouseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            AddColumn("dbo.InvoiceDetails", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.InvoiceDetails", "Product_ProductId");
            AddForeignKey("dbo.InvoiceDetails", "Product_ProductId", "dbo.Products", "ProductId");
            DropColumn("dbo.InvoiceDetails", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceDetails", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.InvoiceDetails", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.InvoiceDetails", new[] { "Product_ProductId" });
            DropColumn("dbo.InvoiceDetails", "Product_ProductId");
            DropTable("dbo.Products");
        }
    }
}
