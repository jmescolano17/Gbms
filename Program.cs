using Blazored.LocalStorage;
using Gbms.Class;
using Gbms.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using MudBlazor;
using System.Text;
using Gbms.Hubs;
using Gbms.Services;
using System.Security.Claims;
using Gbms.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<AppDb>();
builder.Services.AddTransient<UserServices>();
builder.Services.AddTransient<ClearanceServices>();
builder.Services.AddTransient<HouseholdServices>();
builder.Services.AddTransient<OfficialsServices>();
builder.Services.AddTransient<ResidentsServices>();
builder.Services.AddTransient<IndigencyServices>();
builder.Services.AddTransient<BusinessServices>();
builder.Services.AddTransient<BlotterServices>();
builder.Services.AddTransient<RevenueServices>();
builder.Services.AddScoped<Login>();
builder.Services.AddHttpClient();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<MudThemeProvider>();
//SignalR
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});


builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var appSettingsSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);


builder.Services.AddAuthorization(options => {
    options.AddPolicy("Admin", policy => {
        policy.RequireClaim(ClaimTypes.Role, "Admin");

    });
    options.AddPolicy("User", policy => {
        policy.RequireClaim(ClaimTypes.Role, "User");
    });

    options.AddPolicy("AdminUser", policy => {
        policy.RequireAssertion(context => context.User.HasClaim(ClaimTypes.Role, "User") ||
        context.User.HasClaim(ClaimTypes.Role, "Admin"));
    });
});



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        RequireExpirationTime = false
    };

});


var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseResponseCompression();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("NewPolicy");
app.UseAuthorization();
app.UseAuthentication();
// Map SignalR hub
app.MapHub<Hubs>("/hub");
app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();