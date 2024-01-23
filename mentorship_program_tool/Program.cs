using mentorship_program_tool.Data;
using mentorship_program_tool.Repository;
using mentorship_program_tool.Repository.GetAllMenteeDetailsByIdRepository;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetMenteeDetailsById;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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
builder.Services.AddScoped< IGetAllActiveUnpairedMenteesService, GetAllActiveUnpairedMenteesService>();

builder.Services.AddScoped<IGetAllActiveMentorRepository, GetAllActiveMentorRepository>();
builder.Services.AddScoped<IGetAllActiveMentorService, GetAllActiveMentorService>();

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
