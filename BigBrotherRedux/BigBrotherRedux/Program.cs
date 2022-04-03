using BigBrotherRedux.Migrations;
using BigBrotherRedux.Services;
using BigBrotherRedux.Services.Interfaces;
using BigBrotherRedux.Services.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BigBrotherReduxContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repo and Interface scoping.
builder.Services.AddScoped<IUserIPDataRepo, UserIPDataRepo>();
builder.Services.AddScoped<IUserInteractionRepo, UserInteractionRepo>();
builder.Services.AddScoped<IPageReferenceRepo, PageReferenceRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseHttpsRedirection();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors();
app.MapControllers();

app.Run();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=UserIpData}");
    endpoints.MapControllerRoute(
       name: "default2",
       pattern: "{controller=Session}");
     endpoints.MapControllerRoute(
       name: "default3",
       pattern: "{controller=PageReference}");
    endpoints.MapControllerRoute(
       name: "default4",
       pattern: "{controller=UserInteraction}");
});