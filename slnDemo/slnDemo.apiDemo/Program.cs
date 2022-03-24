using Microsoft.EntityFrameworkCore;
using slnDemo.apiDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DBRobot"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//Get All Students
app.MapGet("/minimalapi/Students", (AppDbContext db) =>
{
    return db.Students.ToList();
});
//Get All students By Id
app.MapGet("/minimalApi/StudentsById", (AppDbContext db, int id) =>
{
    var students = db.Students.Find(id);
    return Results.Ok(students);
});
//Add students
app.MapPost("/minimalApi/Addstudents", (AppDbContext db, Student stud) =>
{
    db.Students.Add(stud);
    db.SaveChanges();
    return Results.Created($"/minimalApi/StudentsById/{ stud.Id}", stud);
});
//Update students
app.MapPut("/minimalApi/Updatestudents/", (AppDbContext db, Student stud) =>
{
    var students = db.Students.FirstOrDefault(x => x.Id == stud.Id);
    students.Name = stud.Name;
    students.Age = stud.Age;
    db.Students.Update(students);
    db.SaveChanges();
    return Results.NoContent();
});
//Delete students
app.MapDelete("/minimalApi/Deltestudents/", (AppDbContext db, int id) =>
{
    var students = db.Students.Find(id);
    db.Students.Remove(students);
    db.SaveChanges();
    return Results.NoContent();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.Run();