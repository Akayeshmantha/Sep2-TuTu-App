namespace Tu_Tu.Migrations
{
    using Model.Persistence;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tu_Tu_Request_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled =false;
        }

        protected override void Seed(Tu_Tu_Request_Context context)
        {
            if (context.UserTu_Tu.Count() > 0)
            {
                return;
            }


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
