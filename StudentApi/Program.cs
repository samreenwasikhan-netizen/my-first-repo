using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Enable CORS so our frontend HTML page can talk to this backend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Connect to our SQL Server database using our connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.UseCors();

// 1. Status Check Route
app.MapGet("/api/status", () => new { message = "Backend is up and running, Shanzay!" });

// 2. Student Form Submission Route
// 2. Student Form Submission Route (Saves to SQL Database)
app.MapPost("/api/students", async (Student student, AppDbContext db) =>
{
    // Print to your terminal to confirm receipt
    Console.WriteLine($"[Backend Received] Name: {student.Name}, Grade: {student.Grade}, Age: {student.Age}");

    // Add the student data into your SQL database context
    db.Students.Add(student);

    // Save changes permanently to the database
    await db.SaveChangesAsync();

    return Results.Ok(new { success = true, message = $"Successfully saved biodata for {student.Name} to the SQL database!" });
});

// 3. Fetch All Students Route
app.MapGet("/api/students", async (AppDbContext db) =>
{
    var students = await db.Students.ToListAsync();
    return Results.Ok(students);
});

app.Run();

// Updated blueprint explaining what a Student looks like to our C# program
public class Student
{
    public int Id { get; set; }     // The database will handle this automatically!
    public string Name { get; set; } = string.Empty;
    public int Grade { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}