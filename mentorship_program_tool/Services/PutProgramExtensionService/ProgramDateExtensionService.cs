using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.MailService;
using mentorship_program_tool.Services.NotificationService;
using mentorship_program_tool.Services.PutProgramExtensionService;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace mentorship_program_tool.Services.PutProgramDateExtensionService
{

    public class ProgramDateExtensionService : IProgramDateExtensionService
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly ISignalNotificationService _notificationService;

        public ProgramDateExtensionService(AppDbContext context, IUnitOfWork unitOfWork, IMailService mailService, ISignalNotificationService notificationService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _notificationService = notificationService;
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

            var programdata = _context.Programs.FirstOrDefault(p => p.ProgramID == model.ProgramID);

            var mentorEmail = _unitOfWork.Employee.GetById(programdata.MentorID)?.EmailId;
            _mailService.SendExtensionApprovalEmailAsync(mentorEmail, programdata.ProgramName, programdata.EndDate);

            string mentorUser = programdata.MentorID.ToString();

            _notificationService.SendExtensionApprovalNotificationAsync(mentorUser).Wait();

            return true;
        }
    }

}
