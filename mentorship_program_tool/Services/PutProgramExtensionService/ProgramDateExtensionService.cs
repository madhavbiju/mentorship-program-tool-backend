using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.PutProgramExtensionService;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace mentorship_program_tool.Services.PutProgramDateExtensionService
{
   
    public class ProgramDateExtensionService : IProgramDateExtensionService
    {
        private readonly AppDbContext _context;

        public ProgramDateExtensionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateProgramDateAsync(ProgramDateUpdateAPIModel model)
        {
            var program = _context.Programs.FirstOrDefault(p => p.ProgramID == model.ProgramID);

            if (program == null) return false;

            program.ModifiedBy = model.ModifiedBy;
            program.ModifiedTime = model.ModifiedTime;
            program.EndDate = model.EndDate;

            _context.Programs.Update(program);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
