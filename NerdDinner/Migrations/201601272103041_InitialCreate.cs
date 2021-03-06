namespace NerdDinner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dinners",
                c => new
                    {
                        DinnerID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        EventDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 30),
                        HostedBy = c.String(nullable: false, maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.DinnerID);
            
            CreateTable(
                "dbo.RSVPs",
                c => new
                    {
                        RsvpID = c.Int(nullable: false, identity: true),
                        DinnerID = c.Int(nullable: false),
                        AttendeeEmail = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.RsvpID)
                .ForeignKey("dbo.Dinners", t => t.DinnerID, cascadeDelete: true)
                .Index(t => t.DinnerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RSVPs", "DinnerID", "dbo.Dinners");
            DropIndex("dbo.RSVPs", new[] { "DinnerID" });
            DropTable("dbo.RSVPs");
            DropTable("dbo.Dinners");
        }
    }
}
