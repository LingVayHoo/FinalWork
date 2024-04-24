using AuthServer.Interfaces;
using FW_ASP.Net.Interfaces;
using FW_ASP.Net.Models;
using FW_ASP.Net.Models.Content.Blog;
using FW_ASP.Net.Models.Content.Projects;
using FW_ASP.Net.Models.Content.Requests;
using FW_ASP.Net.Models.Content.Services;
using FW_ASP.Net.Models.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

AuthenticationConfiguration authenticationConfiguration = new();
builder.Configuration.Bind("Authentication", authenticationConfiguration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    JwtBearerDefaults.AuthenticationScheme, o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
        ValidIssuer = authenticationConfiguration.Issuer,
        ValidAudience = authenticationConfiguration.Audience,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
    //o.Events = new JwtBearerEvents
    //{
    //    OnMessageReceived = context =>
    //    {
    //        context.Token = context.Request.Cookies["tasty-cookies"];
    //        return Task.CompletedTask;
    //    }
    //};
});
builder.Services.AddScoped<IAuth, AuthApiModel>();
builder.Services.AddScoped<IProjects, ProjectsApiModel>();
builder.Services.AddScoped<IServices, ServicesApiModel>();
builder.Services.AddScoped<IBlog, BlogApiModel>();
builder.Services.AddScoped<IRequest, RequestsApiModel>();
builder.Services.AddScoped<ITitle, TitleApiModel>();
builder.Services.AddScoped<IContacts, ContactsApiModel>();
builder.Services.AddScoped<IClaimsGetter, ClaimsGetter>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
