namespace WMgr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExtraPropsToInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoices", "Validated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Validated");
            DropColumn("dbo.Invoices", "Date");
            DropColumn("dbo.Invoices", "Number");
        }
    }
}
