using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.ProgramService;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.ProgramService
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProgramDetailsResponseAPIModel GetProgram(int status, int pageNumber, int pageSize)
        {
            var programs = _unitOfWork.Program
                             .GetAll()
                             .Where(p => p.ProgramStatus == status);

            // Get total count of programs
            int totalCount = programs.Count();

            // Implement pagination
            var paginatedPrograms = programs
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToList();

            // Create result object
            var result = new ProgramDetailsResponseAPIModel
            {
                Programs = paginatedPrograms,
                TotalCount = totalCount
            };

            return result;
        }


        public Models.EntityModel.Program GetProgramById(int id)
        {
            var program = _unitOfWork.Program.GetById(id);
            return program;
        }

        public void CreateProgram(Models.EntityModel.Program programDto)
        {

            _unitOfWork.Program.Add(programDto);
            _unitOfWork.Complete();
        }

        public void UpdateProgram(int id, Models.EntityModel.Program programDto)
        {
            var existingProgram = _unitOfWork.Program.GetById(id);

            if (existingProgram == null)
            {
                // Handle not found scenario
                return;
            }

            existingProgram.MentorID = programDto.MentorID;
            existingProgram.ModifiedBy = programDto.ModifiedBy;
            existingProgram.ModifiedTime = programDto.ModifiedTime;
            existingProgram.EndDate = programDto.EndDate;


            _unitOfWork.Complete();
        }

        public void DeleteProgram(int id)
        {
            var program = _unitOfWork.Program.GetById(id);

            if (program == null)
            {
                // Handle not found scenario
                return;
            }

            _unitOfWork.Program.Delete(program);
            _unitOfWork.Complete();
        }


    }
}