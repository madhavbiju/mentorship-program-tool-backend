using mentorship_program_tool.Data;
using mentorship_program_tool.Repository;
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
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddScoped<IGetMenteeDetailsByIdRepository, GetMenteeDetailsByIdRepository>();
builder.Services.AddScoped<IGetMenteeDetailsByIdService, GetMenteeDetailsByIdService>();

builder.Services.AddScoped<IGetAllProgramsRepository, GetAllProgramsRepository>();
builder.Services.AddScoped<IGetAllProgramsService, GetAllProgramsService>();

builder.Services.AddScoped<IGetAllActiveUnpairedMenteesRepository, GetAllActiveUnpairedMenteesRepository>();
builder.Services.AddScoped<IGetAllActiveUnpairedMenteesService, GetAllActiveUnpairedMenteesService>();

builder.Services.AddScoped<IGetAllActiveMentorRepository, GetAllActiveMentorRepository>();
builder.Services.AddScoped<IGetAllActiveMentorService, GetAllActiveMentorService>();

builder.Services.AddScoped<IGetAllMenteesOfMentorService, GetAllMenteesOfMentorService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
