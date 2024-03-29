﻿using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.MentorTaskRepository
{
    public class MentorTaskRepository : Repository<Models.EntityModel.Task>, IMentorTaskRepository
    {
        public MentorTaskRepository(AppDbContext context) : base(context) { }
    }
}
