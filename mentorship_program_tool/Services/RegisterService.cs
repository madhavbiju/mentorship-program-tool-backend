using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using NuGet.Protocol.Plugins;
using System;
using System.Linq;

namespace mentorship_program_tool.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void PostRegister(RegisterModel register)
        {
            var employee = new EmployeeModel
            {
                outlookemployeeid = "",
                firstname = register.firstname,
                lastname = register.lastname,
                emailid = register.email,
                createddate = DateTime.Now,
                accountstatus = "active"
            };
            _unitOfWork.Employee.Add(employee);
            _unitOfWork.Complete();
            var employeeId = employee.employeeid;

            if (employeeId > 0)
            {

                // Add user to login table
                var login = new LoginModel
                {
                    employeeid = employeeId,
                    email = register.email,
                    password = register.password
                };
                _unitOfWork.Login.Add(login);
                _unitOfWork.Complete();

            }
            _unitOfWork.Register.Add(register);
            _unitOfWork.Complete();
        }
    }
}
