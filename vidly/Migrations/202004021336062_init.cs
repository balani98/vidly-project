﻿namespace vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                        isSubsribedToNewsletter = c.Boolean(nullable: false),
                        membershipTypeId = c.Byte(nullable: false),
                        dateOfBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MembershipTypes", t => t.membershipTypeId, cascadeDelete: true)
                .Index(t => t.membershipTypeId);
            
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        id = c.Byte(nullable: false),
                        membershipTypeName = c.String(),
                        signUpFee = c.Short(nullable: false),
                        durationInMonths = c.Byte(nullable: false),
                        discountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        GenreName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                        genre_Id = c.Byte(nullable: false),
                        releaseDate = c.DateTime(nullable: false),
                        dateAdded = c.DateTime(nullable: false),
                        StockNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Genres", t => t.genre_Id, cascadeDelete: true)
                .Index(t => t.genre_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Customers", "membershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Movies", new[] { "genre_Id" });
            DropIndex("dbo.Customers", new[] { "membershipTypeId" });
            DropTable("dbo.Movies");
            DropTable("dbo.Genres");
            DropTable("dbo.MembershipTypes");
            DropTable("dbo.Customers");
        }
    }
}
