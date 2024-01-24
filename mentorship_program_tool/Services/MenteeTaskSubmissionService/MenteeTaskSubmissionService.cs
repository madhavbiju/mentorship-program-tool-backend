﻿using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.MentorTaskRepository;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.MenteeTaskSubmissionService
{
    public class MenteeTaskSubmissionService : IMenteeTaskSubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenteeTaskSubmissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SubmitTask(int id, MenteeTaskSubmissionAPIModel menteetasksubmissionapimodel)
        {
            var existingTask = _unitOfWork.menteeTaskSubmissionRepository.GetById(id);

            if (existingTask == null)
            {
                return;
            }

            // Update properties based on adminapi model
            existingTask.filepath = menteetasksubmissionapimodel.filepath;
            existingTask.submissiontime = DateTime.Now;


            _unitOfWork.Complete();
        }
    }
}
