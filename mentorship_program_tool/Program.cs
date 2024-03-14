using mentorship_program_tool.Data;
using mentorship_program_tool.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using mentorship_program_tool.Repository.AdminApprovalRequestRepository;
using mentorship_program_tool.Repository.AdminDashboardCountRepository;
using mentorship_program_tool.Repository.EmployeeRepository;
using mentorship_program_tool.Repository.GetAllMenteeDetailsByIdRepository;
using mentorship_program_tool.Repository.GetAllProgramsRepository;
using mentorship_program_tool.Repository.MentorRequestRepository;
using mentorship_program_tool.Repository.ReportTypeRepository;
using mentorship_program_tool.Repository.RoleRepository;
using mentorship_program_tool.Repository.StatusRepository;
using mentorship_program_tool.Repository.MenteeTaskSubmissionRepository;
using mentorship_program_tool.Repository.MentorTaskRepository;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.AdminApprovalRequestService;
using mentorship_program_tool.Services.AdminDashboardCountService;
using mentorship_program_tool.Services.EmployeeService;
using mentorship_program_tool.Services.GetAllProgramService;
using mentorship_program_tool.Services.GetMenteeDetailsById;
using mentorship_program_tool.Services.MentorRequestService;
using mentorship_program_tool.Services.ReportTypeService;
using mentorship_program_tool.Services.RoleService;
using mentorship_program_tool.Services.StatusService;
using mentorship_program_tool.Services.MenteeTaskSubmissionService;
using mentorship_program_tool.Services.MentorTaskRepository;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using mentorship_program_tool.Repository.ProgramRepository;
using mentorship_program_tool.Services.ProgramService;
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;
using mentorship_program_tool.Repository.GetActiveTasksRepository;
using mentorship_program_tool.Services.GetActiveTasks;
using mentorship_program_tool.Repository.GetTasksByEmployeeIdRepository;
using mentorship_program_tool.Services.GetTasksbyEmployeeIdService;
using Microsoft.OpenApi.Models;
using System.Reflection;
using mentorship_program_tool.Repository.GetUserDetailsRepository;
using mentorship_program_tool.Services.GetUserDetailsService;
using mentorship_program_tool.Services.MentorDashboardCountService;
using mentorship_program_tool.Middleware;
using mentorship_program_tool.Services.GraphAPIService;
using mentorship_program_tool.Repository.EmployeeRoleRepository;
using mentorship_program_tool.Services.EmployeeRoleService;
using System.Text;
using mentorship_program_tool.Models.GraphModel;
using mentorship_program_tool.Services.MenteesOfMentorListService;
using mentorship_program_tool.Repository.MeetingScheduleRepository;
using mentorship_program_tool.Repository.MeetingScheduleReposixtory;
using mentorship_program_tool.Services.MeetingService;
using mentorship_program_tool.Services.JwtService;
using mentorship_program_tool.Services.StatusUpdaterService;
using mentorship_program_tool.Services.GetProgramExtensionService;
using mentorship_program_tool.Services.PutProgramDateExtensionService;
using mentorship_program_tool.Services.PutProgramExtensionService;
using mentorship_program_tool.Services.MentorsOfMenteesListService;
using mentorship_program_tool.Services.MailService;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.Services.GetAllMenteesListService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSection);

// Configure JWT Authentication
var jwtSettings = jwtSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience
    };
});
builder.Services.AddAuthorization(options =>
{
    // Define a policy for the Admin role
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
    // Define a policy for the Mentor role
    options.AddPolicy("RequireMentorRole", policy => policy.RequireRole("mentor"));
    // Define a policy for the Mentee role
    options.AddPolicy("RequireMenteeRole", policy => policy.RequireRole("mentee"));
});

builder.Services.AddScoped<JwtService>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IMentorRequestRepository, MentorRequestRepository>();
builder.Services.AddScoped<IMentorRequestService, MentorRequestService>();

builder.Services.AddScoped<IAdminApprovalRequestRepository, AdminApprovalRequestRepository>();
builder.Services.AddScoped<IAdminApprovalRequestService, AdminApprovalRequestService>();

builder.Services.AddScoped<IAdminDashboardCountRepository, AdminDashboardCountRepository>();
builder.Services.AddScoped<IAdminDashboardCountService, AdminDashboardCountService>();

builder.Services.AddScoped<IMentorTaskRepository, MentorTaskRepository>();
builder.Services.AddScoped<IMentorTaskService, MentorTaskService>();

builder.Services.AddScoped<IMenteeTaskSubmissionRepository, MenteeTaskSubmissionRepository>();
builder.Services.AddScoped<IMenteeTaskSubmissionService, MenteeTaskSubmissionService>();

builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusService, StatusService>();

builder.Services.AddScoped<IReportTypeRepository, ReportTypeRepository>();
builder.Services.AddScoped<IReportTypeService, ReportTypeService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IMeetingScheduleRepository, MeetingScheduleRepository>();
builder.Services.AddScoped<IMeetingService, MeetingService>();

builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<IProgramService, ProgramService>();

builder.Services.AddScoped<IGetMenteeDetailsByIdRepository, GetMenteeDetailsByIdRepository>();
builder.Services.AddScoped<IGetMenteeDetailsByIdService, GetMenteeDetailsByIdService>();

builder.Services.AddScoped<IGetAllProgramsRepository, GetAllProgramsRepository>();
builder.Services.AddScoped<IGetAllProgramsService, GetAllProgramsService>();

builder.Services.AddScoped<IGetTasksByProgramIdRepository, GetTasksByProgramIdRepository>();
builder.Services.AddScoped<IGetTasksByProgramIdService, GetTasksByProgramIdService>();

builder.Services.AddScoped<IGetTasksbyEmployeeIdRepository, GetTasksbyEmployeeIdRepository>();
builder.Services.AddScoped<IGetTasksbyEmployeeIdService, GetTasksByEmployeeIdService>();

builder.Services.AddScoped<IGetAllMenteesListService, GetAllMenteesListService>();


builder.Services.AddScoped<IGetAllActiveUnpairedMenteesRepository, GetAllActiveUnpairedMenteesRepository>();
builder.Services.AddScoped<IGetAllActiveUnpairedMenteesService, GetAllActiveUnpairedMenteesService>();

builder.Services.AddScoped<IGetAllActiveMentorRepository, GetAllActiveMentorRepository>();
builder.Services.AddScoped<IGetAllActiveMentorService, GetAllActiveMentorService>();

builder.Services.AddScoped<IGetAllMenteesOfMentorService, GetAllMenteesOfMentorService>();

builder.Services.AddScoped<IGetUserDetailsRepository, GetUserDetailsRepository>();
builder.Services.AddScoped<IGetUserDetailsService, GetUserDetailsService>();

builder.Services.AddScoped<IMentorDashboardCountService, MentorDashboardCountService>();
builder.Services.AddHttpClient<GraphApiService>();

builder.Services.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();
builder.Services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();

builder.Services.AddScoped<IMenteesOfMentorListService, MenteesOfMentorListService>();
builder.Services.AddScoped<IMentorsOfMenteesListService, MentorsOfMenteesListService>();


builder.Services.AddScoped<IProgramExtensionService, ProgramExtensionService>();
builder.Services.AddScoped<IProgramDateExtensionService, ProgramDateExtensionService>();

builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddHostedService<ProgramStatusUpdater>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Mentorship Program Tool",
        Description = "An ASP.NET Core Web API for managing Mentee-Mentor Pairing"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseTokenDecodingMiddleware();
app.UseCors();

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
