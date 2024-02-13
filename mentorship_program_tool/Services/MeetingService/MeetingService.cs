using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mentorship_program_tool.Services.MeetingService
{
    public class MeetingService : IMeetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public MeetingService(IUnitOfWork unitOfWork, AppDbContext context)
        {

            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IEnumerable<MeetingSchedule> GetMeetings()
        {
            return _unitOfWork.MeetingSchedule.GetAll();
        }

        public IEnumerable<MeetingSchedule> GetMeetingByEmployeeId(int id,int role)
        {
            var programIds = new List<int>();
            // Find program IDs where the given id matches either mentorID or menteeID
            if (role==2)
            {
                programIds = _context.Programs
                                     .Where(p => p.MentorID == id)
                                     .Select(p => p.ProgramID)
                                     .ToList();
            }
            if (role == 3)
            {
                programIds = _context.Programs
                                     .Where(p => p.MenteeID == id)
                                     .Select(p => p.ProgramID)
                                     .ToList();
            }

            // Retrieve meetings associated with the found program IDs
            var meetings = _context.MeetingSchedules
                                .Where(m => programIds.Contains(m.ProgramID))
                                .ToList(); // Execute the query and materialize results

            return meetings;
        }

        public MeetingSchedule GetMeetingById(int id)
        {
            return _unitOfWork.MeetingSchedule.GetById(id);
        }

        public void CreateMeeting(MeetingSchedule meeting)
        {
            _unitOfWork.MeetingSchedule.Add(meeting);
            _unitOfWork.Complete();
        }

        public void DeleteMeeting(int id)
        {
            var meeting = _unitOfWork.MeetingSchedule.GetById(id);

            if (meeting == null)
            {
                return;
            }

            _unitOfWork.MeetingSchedule.Delete(meeting);
            _unitOfWork.Complete();
        }
    }
}