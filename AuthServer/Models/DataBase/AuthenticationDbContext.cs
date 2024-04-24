using AuthServer.Models.Auth;
using AuthServer.Models.Contacts;
using AuthServer.Models.Content.Blog;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Content.Requests;
using AuthServer.Models.Content.Services;
using AuthServer.Models.Title;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Emit;

namespace AuthServer.Models.DataBase
{
    public class AuthenticationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FWTitle>().HasData(new FWTitle
            {
                Id = Guid.NewGuid(),
                MainTitle = "COSA NOSTRA",
                QuoteTitle = "Ничего личного, это просто бизнес",
                ButtonTitle = "Начать"                
            });
            builder.Entity<FWContact>().HasData( new FWContact
            {
                Id = Guid.NewGuid(),
                Address = "Мы сами вас найдем!",
                Email = "mail@example.com",
                Phone = "+7 (999) 123-45-67"
            });
            
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<CustomRole> CustomRoles { get; set; }
        public DbSet<FWProject> Projects { get; set; }
        public DbSet<FWService> Services { get; set; }
        public DbSet<FWBlogItem> Blog { get; set; }
        public DbSet<FWRequest> Requests { get; set; }
        public DbSet<FWTitle> Titles { get; set; }
        public DbSet<FWContact> Contacts { get; set; }
    }
}
