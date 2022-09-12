using System;
using GoogleAnalyticsAPI.Jobs;
using GoogleAnalyticsAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("Privat", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://bank.gov.ua/");
});
builder.Services.AddHttpClient("GoogleAnalytics", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://www.google-analytics.com/");
});
builder.Services.AddTransient<IPrivatBankService, PrivateBankService>();
builder.Services.AddTransient<IGoogleAnalyticsService, GoogleAnalyticsService>();

builder.Services.AddQuartz(q =>  
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();

    // Create a "key" for the job
    var jobKey = new JobKey("IGoogleAnalyticsJob");

    // Register the job with the DI container
    q.AddJob<GaSendMetricsJob>(opts => opts.WithIdentity(jobKey));
                
    // Create a trigger for the job
    q.AddTrigger(opts => opts
        .ForJob(jobKey) 
        .WithIdentity("IGoogleAnalyticsJob-trigger") // give the trigger a unique name
        .WithCronSchedule("0 15 10 * * ?")); // at 10:15am every day

});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();