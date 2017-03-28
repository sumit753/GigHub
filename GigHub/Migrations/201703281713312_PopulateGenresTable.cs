namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres (Id,Name) Values (1,'Jazz')");
            Sql("Insert into Genres (Id,Name) Values (2,'Rock')");
            Sql("Insert into Genres (Id,Name) Values (3,'Indie')");
            Sql("Insert into Genres (Id,Name) Values (4,'Pop')");



        }

        public override void Down()
        {
            Sql("Delete FROM Genres WHERE Id IN (1,2,3,4)");
        }
    }
}
