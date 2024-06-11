using IssueTracker.DAL;
using IssueTracker.DAL.Interfaces;
using IssueTracker.DAL.Repositories;
using IssueTracker.Domain.Entity;
using IssueTracker.Hubs;
using IssueTracker.Service.Implementations;
using IssueTracker.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddSignalR(options => { options.EnableDetailedErrors = true; });
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBaseRepository<UserEntity>, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBaseRepository<IssueEntity>, IssueRepository>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddDbContext<AppDbContext>(
options => {
    var connectionString = builder.Configuration.GetConnectionString("Postgres");
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()){
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
"action",
"{action=Index}",
new {controller = "Landing", action = "Index"});

app.MapControllerRoute(
"app",
"app",
new {controller = "App", action = "Issues"});

app.MapControllerRoute(
"appWithAction",
"app/{action}",
new {controller = "App", action = "Issues"});

app.MapControllerRoute(
"default",
// "{controller=Landing}/{action=CreateIssue}/{id?}");
"",
new {controller = "Landing", action = "Index"});

app.MapHub<CommentsHub>("/commentsHub");

app.Run();