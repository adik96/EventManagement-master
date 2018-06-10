namespace EventManagement.Migrations
{
    using EventsManagement.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventsManagement.Models.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EventsManagement.Models.AppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var states = new[]
            {
                new State() {Name = "oczekuj¹ce"},
                new State() {Name = "anulowane"},
                new State() {Name = "zrealizowane"},
                new State() {Name = "niezrealizowane"}
            };

            context.States.AddOrUpdate(
                s => s.Name,
                states
            );

            var organizationalUnits = new[]
            {
                new OrganizationalUnit() {Name = "Dzia³ IT"},
                new OrganizationalUnit() {Name = "Dzia³ Public Relations"},
                new OrganizationalUnit() {Name = "Dzia³ Finansów"},
                new OrganizationalUnit() {Name = "Dzia³ Ksiêgowoœci"},
                new OrganizationalUnit() {Name = "Dzia³ Prawny"},
                new OrganizationalUnit() {Name = "Dzia³ HR"},
                new OrganizationalUnit() {Name = "Dzia³ Kadr"},
                new OrganizationalUnit() {Name = "Dzia³ Logistyki"},
                new OrganizationalUnit() {Name = "Dzia³ Produkcji"},
                new OrganizationalUnit() {Name = "Dzia³ Handlowy"},
            };

            context.OrganizationalUnits.AddOrUpdate(
                o => o.Name,
                organizationalUnits
            );

            var employee = new IdentityRole("employee");
            var admin = new IdentityRole("admin");

            context.Roles.AddOrUpdate(
                r => r.Name,
                employee,
                admin
            );
            context.SaveChanges();

            var employeeMail = "pracownik@mail.com";
            var employee2Mail = "pracownik2@mail.com";
            var adminMail = "admin@mail.com";


            var userE = context.Users.SingleOrDefault(x => x.UserName == employeeMail);
            var userE2 = context.Users.SingleOrDefault(x => x.UserName == employee2Mail);
            var userA = context.Users.SingleOrDefault(x => x.UserName == adminMail);

            var hasher = new PasswordHasher();
            var pass = "qwerty";
            var passHash = hasher.HashPassword(pass);


            if (userE == null)
            {
                userE = new User
                {
                    Name = "Adrian",
                    Surname = "Rodzoñ",
                    Email = employeeMail,
                    EmailConfirmed = true,
                    PasswordHash = passHash,
                    SecurityStamp = new Guid().ToString(),
                    UserName = employeeMail,
                    OrganizationalUnitId = organizationalUnits[0].Id,

                };
                userE.Roles.Add(new IdentityUserRole() { RoleId = employee.Id, UserId = userE.Id });
                context.Users.Add(userE);
            }

            if (userE2 == null)
            {
                userE2 = new User
                {
                    Name = "Tomasz",
                    Surname = "Rzucidlak",
                    Email = employee2Mail,
                    EmailConfirmed = true,
                    PasswordHash = passHash,
                    SecurityStamp = new Guid().ToString(),
                    UserName = employee2Mail,
                    OrganizationalUnitId = organizationalUnits[1].Id,

                };
                userE2.Roles.Add(new IdentityUserRole() { RoleId = employee.Id, UserId = userE2.Id });
                context.Users.Add(userE2);
            }

            if (userA == null)
            {
                userA = new User
                {
                    Surname = "Admiñski",
                    Email = adminMail,
                    EmailConfirmed = true,
                    PasswordHash = passHash,
                    SecurityStamp = new Guid().ToString(),
                    Name = "Admin",
                    UserName = adminMail,
                    OrganizationalUnitId = organizationalUnits[0].Id,
                };
                userA.Roles.Add(new IdentityUserRole() { RoleId = admin.Id, UserId = userA.Id });
                //context.Users.Add(userA);
                context.Users.AddOrUpdate(
                r => r.UserName,
                userE, userE2, userA);
            }


            context.SaveChanges();

            var events = new Event[]
            {
                new Event
                {
                    Title = "Spotkanie organizacyjne",
                    Description = "Opis 1",
                    Place = "Pokój 109",
                    EventDate = DateTime.Now,
                    EventTime = DateTime.Now,
                    EventAdd = DateTime.Parse("2008-08-01"),
                    AuthorId = userE.Id,
                    StateId = states[0].Id
                },
                new Event
                {
                    Title = "Projekt NTI",
                    Description = "Opis 2",
                    Place = "Sala konferencyjna",
                    EventDate = DateTime.Now,
                    EventTime = DateTime.Now,
                    EventAdd = DateTime.Parse("2011-11-11"),
                    AuthorId = userE2.Id,
                    StateId = states[1].Id
                },
                new Event
                {
                    Title = "Rekrutacja",
                    Description = "Opis 3",
                    Place = "Pokój 110",
                    EventDate = DateTime.Now,
                    EventTime = DateTime.Now,
                    EventAdd = DateTime.Parse("2013-08-01"),
                    AuthorId = userE.Id,
                    StateId = states[2].Id
                },
                new Event
                {
                    Title = "Podsumowanie ostatniego miesi¹ca",
                    Description = "Opis 4",
                    Place = "Sala konferencyjna",
                    EventDate = DateTime.Now,
                    EventTime = DateTime.Now,
                    EventAdd = DateTime.Parse("2011-08-01"),
                    AuthorId = userE.Id,
                    StateId = states[3].Id
                },
            };

            context.SaveChanges();

            context.Events.AddOrUpdate(
                r => r.Title,
                events
            );

            context.SaveChanges();


        }
    }
}