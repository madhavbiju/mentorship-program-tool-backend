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

        public IEnumerable<ProgramModel> GetProgram()
        {

            var program = _unitOfWork.Program.GetAll();
            return program;
        }

        public ProgramModel GetProgramById(int id)
        {
            var program = _unitOfWork.Program.GetById(id);
            return program;
        }

        public void CreateProgram(ProgramModel programDto)
        {

            _unitOfWork.Program.Add(programDto);
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