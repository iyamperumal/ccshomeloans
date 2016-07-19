namespace CcsWeb.DataContexts.LocalMigrations
{
    using CcsWeb.DataContexts;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CcsLocalDbContext>
    {
        public Configuration()
        {
            base.AutomaticMigrationsEnabled = false;
            base.MigrationsDirectory = @"DataContexts\LocalMigrations";
        }

        protected override void Seed(CcsLocalDbContext context)
        {
        }
    }
}

