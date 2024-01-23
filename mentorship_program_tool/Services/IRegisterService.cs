using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services
{
    public interface IRegisterService
    {
        public void PostRegister(RegisterModel register);
    }
}
