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

var app = builder.Build();

app.UseCors();

// 1. Status Check Route
app.MapGet("/api/status", () => new { message = "Backend is up and running, Shanzay!" });

// 2. Student Form Submission Route
app.MapPost("/api/students", (Student student) => 
{
    // Print all incoming data to your terminal to confirm it works!
    Console.WriteLine($"[Backend Received] Name: {student.Name}, Grade: {student.Grade}, Age: {student.Age}, Gender: {student.Gender}, Contact: {student.PhoneNumber}");
    
    // Send a success response back to the browser frontend
    return Results.Ok(new { success = true, message = $"Successfully saved biodata for {student.Name}!" });
});

app.Run();

// Updated blueprint explaining what a Student looks like to our C# program
public class Student
{
    public string Name { get; set; } = string.Empty;
    public int Grade { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}