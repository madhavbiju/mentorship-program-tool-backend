// ProgramExtensionService.cs
using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.GetProgramExtensionService;

public class ProgramExtensionService : IProgramExtensionService
{
    private readonly AppDbContext _context;

    public ProgramExtensionService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<ProgramExtensionRequestResponseAPIModel> GetAllProgramExtensionRequestsAsync(int status, int pageNumber, int pageSize)
    {
        var query = from pe in _context.ProgramExtensions
                    join p in _context.Programs on pe.ProgramID equals p.ProgramID
                    join mentee in _context.Employees on p.MenteeID equals mentee.EmployeeID
                    join mentor in _context.Employees on p.MentorID equals mentor.EmployeeID // Join to get mentor info
                    where pe.RequestStatusID == status
                    select new ProgramExtensionRequestAPIModel
                    {
                        ProgramExtensionID = pe.ProgramExtensionID,
                        ProgramID = p.ProgramID,
                        MenteeID = p.MenteeID,
                        MentorID = p.MentorID, // Added
                        ProgramName = p.ProgramName,
                        NewEndDate = pe.NewEndDate,
                        Reason = pe.Reason,
                        MenteeName = $"{mentee.FirstName} {mentee.LastName}",
                        MentorName = $"{mentor.FirstName} {mentor.LastName}", // Added
                        CurrentEndDate = p.EndDate,
                        RequestStatusID = pe.RequestStatusID
                    };

        var totalCount = await query.CountAsync();
        var requests = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new ProgramExtensionRequestResponseAPIModel
        {
            Requests = requests,
            TotalCount = totalCount
        };
    }

}
