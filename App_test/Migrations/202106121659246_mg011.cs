namespace App_test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg011 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Barangs", "Brg_nama", c => c.String(nullable: false));
            AlterColumn("dbo.Perusahaans", "Comp_nama", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Perusahaans", "Comp_nama", c => c.String());
            AlterColumn("dbo.Barangs", "Brg_nama", c => c.String());
        }
    }
}
