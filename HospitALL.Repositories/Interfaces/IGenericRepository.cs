﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HospitALL.Repositories.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");

        T GetById(object id);
        Task<T> GetByIdAsync(object id);

        void Add(T entity);
        Task AddAsync(T entity);

        void Update(T entity);
        Task UpdateAsync(T entity);

        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}
