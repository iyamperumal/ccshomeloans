namespace CcsWeb.DataContexts.RemoteMigrations
{
    using CcsWeb.DataContexts;
    using CcsWeb.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CcsRemoteDbContext>
    {
        public Configuration()
        {
            base.AutomaticMigrationsEnabled = true;
            base.AutomaticMigrationDataLossAllowed = true;
            base.MigrationsDirectory = @"DataContexts\RemoteMigrations";
        }

        protected override void Seed(CcsRemoteDbContext context)
        {
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
            if (!context.Roles.Any<IdentityRole>(r => (r.Name == "Admin")))
            {
                IdentityRole role = new IdentityRole {
                    Name = "Admin"
                };
                manager.Create<IdentityRole, string>(role);
            }
            if (!context.Roles.Any<IdentityRole>(r => (r.Name == "User")))
            {
                IdentityRole role2 = new IdentityRole {
                    Name = "User"
                };
                manager.Create<IdentityRole, string>(role2);
            }
            if (!context.Roles.Any<IdentityRole>(r => (r.Name == "Org")))
            {
                IdentityRole role3 = new IdentityRole {
                    Name = "Org"
                };
                manager.Create<IdentityRole, string>(role3);
            }
            if (!context.Roles.Any<IdentityRole>(r => (r.Name == "Lead")))
            {
                IdentityRole role4 = new IdentityRole {
                    Name = "Lead"
                };
                manager.Create<IdentityRole, string>(role4);
            }
            if (!context.Roles.Any<IdentityRole>(r => (r.Name == "Agent")))
            {
                IdentityRole role5 = new IdentityRole {
                    Name = "Agent"
                };
                manager.Create<IdentityRole, string>(role5);
            }
            if (!context.Roles.Any<IdentityRole>(r => (r.Name == "Real")))
            {
                IdentityRole role6 = new IdentityRole {
                    Name = "Real"
                };
                manager.Create<IdentityRole, string>(role6);
            }
            UserStore<ApplicationUser> store2 = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> manager2 = new UserManager<ApplicationUser>(store2);
            ApplicationUser user = new ApplicationUser {
                UserName = "jhammond"
            };
            if (!context.Users.Any<ApplicationUser>(u => (u.UserName == "jhammond")))
            {
                manager2.Create<ApplicationUser, string>(user, "waseasy");
                manager2.AddToRole<ApplicationUser, string>(user.Id, "Admin");
            }
            if (!context.Users.Any<ApplicationUser>(u => (u.UserName == "ouakil")))
            {
                user = new ApplicationUser {
                    UserName = "ouakil"
                };
                manager2.Create<ApplicationUser, string>(user, "youssef7");
                manager2.AddToRole<ApplicationUser, string>(user.Id, "Admin");
            }
            if (!context.Users.Any<ApplicationUser>(u => (u.UserName == "moustapha")))
            {
                user = new ApplicationUser {
                    UserName = "moustapha"
                };
                manager2.Create<ApplicationUser, string>(user, "youssef7");
                manager2.AddToRole<ApplicationUser, string>(user.Id, "Lead");
            }
        }
    }
}

