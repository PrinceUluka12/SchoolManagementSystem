using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using School.Data;
using School.Extensions;
using School.Services.CourseServices;
using School.Services.ExamEvaluationServices;
using School.Services.ExamSservices;
using School.Services.IdentityService;
using School.Services.LecturerServices;
using School.Services.QuestionServices;
using School.Services.StudentServices;
using School.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },new string[]{}
        }
    });

});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseServices , CoursesServices>();
builder.Services.AddScoped<IExamService,ExamService>();
builder.Services.AddScoped<ILecturerService, LecturerService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IExamEvaluationService, ExamEvaluationService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddHttpClient("Identity", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:IdentityAPI"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
builder.AddAppAuthentication();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
DatabaseManagement.MigrationIntilization(app);

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
