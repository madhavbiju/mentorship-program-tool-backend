﻿namespace mentorship_program_tool.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetWithOffsetLimitAsync(int offset, int limit);
    }
}
