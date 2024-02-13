using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

        public GetAllMeetingsResponseAPIModel GetAllMeetings(int pageNumber, string sortBy)
        {
            int pageSize = 5; // Assuming 5 records per page

            IQueryable<GetAllMeetingsAPIModel> query = (from ms in _context.MeetingSchedules
                                                        join p in _context.Programs on ms.ProgramID equals p.ProgramID
                                                        join mentor in _context.Employees on p.MentorID equals mentor.EmployeeID
                                                        join mentee in _context.Employees on p.MenteeID equals mentee.EmployeeID
                                                        join status in _context.Statuses on ms.MeetingStatus equals status.StatusID
                                                        select new GetAllMeetingsAPIModel
                                                        {
                                                            meetingID = ms.MeetingID, // Include meetingID
                                                            menteeName = mentee.FirstName + " " + mentee.LastName,
                                                            scheduledDate = ms.ScheduleDate,
                                                            scheduledTime = ms.StartTime,
                                                            meetingName = ms.Title,
                                                            Status = status.StatusValue
                                                        });

            // Apply sorting based on the sortBy parameter
            switch (sortBy)
            {
                case "date":
                    query = query.OrderBy(ms => ms.scheduledDate);
                    break;
                case "menteeName":
                    query = query.OrderBy(ms => ms.menteeName);
                    break;
                default:
                    // Default sorting by date if sortBy parameter is not recognized
                    query = query.OrderByDescending(ms => ms.scheduledDate);
                    break;
            }
            int totalCount = query.Count();

            // Apply pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new GetAllMeetingsResponseAPIModel { Meetings = query.ToList(), TotalCount = totalCount };
        }





        public IEnumerable<MeetingSchedule> GetMeetingByEmployeeId(int id, int role)
        {
            var programIds = new List<int>();
            // Find program IDs where the given id matches either mentorID or menteeID
            if (role == 2)
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




        //to get upcoming 3meetings of a mentor or mentee
        public IEnumerable<MeetingSchedule> GetSoonMeetingByEmployeeId(int id)
        {
            var mentorProgramIds = _context.Programs
                                            .Where(p => p.MentorID == id)
                                            .Select(p => p.ProgramID)
                                            .ToList();

            var menteeProgramIds = _context.Programs
                                            .Where(p => p.MenteeID == id)
                                            .Select(p => p.ProgramID)
                                            .ToList();

            var programIds = mentorProgramIds.Concat(menteeProgramIds).ToList();

            var meetings = _context.MeetingSchedules
                                 .Where(m => programIds.Contains(m.ProgramID) && m.ScheduleDate >= DateTime.Now)
                                 .OrderBy(m => m.ScheduleDate)
                                 .Take(3)
                                 .ToList();

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