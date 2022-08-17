﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Interface
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}