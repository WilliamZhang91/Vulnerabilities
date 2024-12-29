using Azure.Core;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vulnerabilities.Data;
using Vulnerabilities.Repositories.CommentRepository;
using Vulnerabilities.Repositories.CreditCardRepository;
using Vulnerabilities.Repositories.ProfileRepository;
using Vulnerabilities.Repositories.UserRepository;
using Vulnerabilities.Services.CommentService;
using Vulnerabilities.Services.CreditCardService;
using Vulnerabilities.Services.EncryptionProvider;
using Vulnerabilities.Services.ProfileService;
using Vulnerabilities.Services.UserService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<IEncryptionProvider, CustomEncryptionProvider>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Session_Cookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.None;
    });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "__RequestVerificationToken";
    options.HeaderName = "RequestVerificationToken";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<EncryptionOptions>(builder.Configuration.GetSection("Encryption"));

builder.Services.AddControllersWithViews();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
    options
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

var antiforgery = app.Services.GetRequiredService<IAntiforgery>();

//app.Use(async (context, next) =>
//{
//    if (context.Request.Method == HttpMethods.Post &&
//        !context.Request.Path.StartsWithSegments("/api/auth/login"))
//    {
//        var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

//        try
//        {
//            await antiforgery.ValidateRequestAsync(context);

//        }
//        catch (AntiforgeryValidationException ex)
//        {
//            context.Response.StatusCode = StatusCodes.Status403Forbidden;
//            var userClaims = context.User?.Claims.Select(c => new { c.Type, c.Value }).ToList();
//            logger.LogError("Antiforgery token validation failed. User Claims: {Claims}", userClaims);
//            logger.LogError(ex.ToString());
//            context.Response.StatusCode = StatusCodes.Status403Forbidden;
//            await context.Response.WriteAsync("Invalid antiforgery token.");
//            var errorMessage = new
//            {
//                Error = "Antiforgery token validation failed.",
//                InnerException = ex.InnerException?.Message
//            };

//            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorMessage);

//            await context.Response.WriteAsync(jsonResponse);
//            return;
//        }

//        await next();
//    }
//});

using (var scope = app.Services.CreateScope())
{
    //replace DataContext with your Db Context name
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/auth/login"))
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var tokens = antiforgery.GetAndStoreTokens(context);
        context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions
        {
            HttpOnly = false,
            SameSite = SameSiteMode.None,
            Secure = true
        });
    }
    await next();
});

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    var tokenFromHeader = context.Request.Headers["RequestVerificationToken"].FirstOrDefault();
    logger.LogInformation("Token from Header: {TokenFromHeader}", tokenFromHeader);
    var tokenFromCookie = context.Request.Cookies["__RequestVerificationToken"];
    logger.LogInformation("Token from Cookie: {TokenFromCookie}", tokenFromCookie);
    await next();
});

app.MapControllers();

app.Run();
