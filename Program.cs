using ChatAppApi.Data;
using ChatAppApi.Hubs; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// 1. CẤU HÌNH DỊCH VỤ
builder.Services.AddControllers();
builder.Services.AddSignalR(); 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 2. CẤU HÌNH PIPELINE
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();               
app.MapHub<ChatHub>("/chatHub");    

// 3. CẤU HÌNH FILE TĨNH
app.UseStaticFiles();

var loginPath = Path.Combine(builder.Environment.WebRootPath, "Login-form");
if (Directory.Exists(loginPath))
{
    app.UseDefaultFiles(new DefaultFilesOptions
    {
        FileProvider = new PhysicalFileProvider(loginPath),
        RequestPath = "", 
        DefaultFileNames = new List<string> { "index.html" }
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(loginPath),
        RequestPath = "" 
    });
}

var htmlPath = Path.Combine(builder.Environment.WebRootPath, "html");
if (Directory.Exists(htmlPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(htmlPath),
        RequestPath = "/html"
    });
}

app.Run();
