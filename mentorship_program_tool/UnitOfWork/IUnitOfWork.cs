﻿using mentorship_program_tool.Repository;

namespace mentorship_program_tool.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRoleRepository Role { get; }

        int Complete();
    }
}
