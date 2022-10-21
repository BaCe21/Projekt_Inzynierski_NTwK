global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using System.Configuration;
global using Azure.Storage.Blobs;
global using Azure.Storage.Blobs.Models;
global using Microsoft.AspNetCore.Mvc;
global using System.ComponentModel.DataAnnotations;
global using TrikProjekt56.Models;
global using TrikProjekt56.Interfaces;
global using TrikProjekt56.Data;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.Extensions.Logging;
global using System.Diagnostics;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Migrations;
global using TrikProjekt56.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<ILocation, LocationRepository>();
builder.Services.AddScoped<IAge, AgeRepository>();
builder.Services.AddScoped<IHair, HairRepository>();
builder.Services.AddScoped<IGender, GenderRepository>();
builder.Services.AddScoped<IReligion, ReligionRepository>();
builder.Services.AddScoped<IEducation, EducationRepository>();
builder.Services.AddScoped<IHeight, HeightRepository>();
builder.Services.AddScoped<IWeight, WeightRepository>();
builder.Services.AddScoped<ICase, CaseRepository>();
builder.Services.AddDbContext<CaseContext>(options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:dbconn").Value));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<CaseContext>();
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
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
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
app.Run();