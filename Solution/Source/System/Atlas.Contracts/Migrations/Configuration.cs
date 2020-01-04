using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Security;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework.NewDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Data Source=WILDWEST\\WILDWEST;Initial Catalog=Atlas88;User ID=sa;Password=kidwest12!";
        }

        protected override void Seed(CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework.NewDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }

    //internal sealed class Configuration<T> : DbMigrationsConfiguration<Implementation.Infrastructure.Data.EntityFramework.EntityFrameworkDbContext<T>>
    // where T : EntityBase
    //{
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = true;
    //        AutomaticMigrationDataLossAllowed = true;
    //        ContextKey = "Data Source=WILDWEST\\WILDWEST;Initial Catalog=Atlas50;User ID=sa;Password=kidwest12!";
    //    }

    //    protected override void Seed(CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework.EntityFrameworkDbContext<T> context)
    //    {
    //        //  This method will be called after migrating to the latest version.

    //        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
    //        //  to avoid creating duplicate seed data.
    //    }
    //}
}
