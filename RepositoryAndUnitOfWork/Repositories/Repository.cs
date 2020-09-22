using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryAndUnitOfWork.Contracts;
using RepositoryAndUnitOfWork.Data;
using static RepositoryAndUnitOfWork.Utilities.Utility;


namespace RepositoryAndUnitOfWork.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public virtual TEntity GetById(params object[] ids)
        {
            return _entities.Find(ids);
        }

        public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _entities.FindAsync(ids, cancellationToken);
        }
        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            NotNull(entity, nameof(entity));
            _entities.Add(entity);
            if (saveNow)
                _context.SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            NotNull(entity, nameof(entity));
            await _entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                try
                {
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            NotNull(entities, nameof(entities));
            _entities.AddRange(entities);
            if (saveNow)
                _context.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            NotNull(entities, nameof(entities));
            await _entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            NotNull(entity, nameof(entity));
            _entities.Remove(entity);
            if (saveNow)
                _context.SaveChanges();
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            NotNull(entity, nameof(entity));
            _entities.Remove(entity);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            NotNull(entities, nameof(entities));
            _entities.RemoveRange(entities);
            if (saveNow)
                _context.SaveChanges();
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            NotNull(entities, nameof(entities));
            _entities.RemoveRange(entities);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            NotNull(entity, nameof(entity));
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            NotNull(entity, nameof(entity));
            _entities.Update(entity);
            if (saveNow)
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            NotNull(entities, nameof(entities));
            _entities.UpdateRange(entities);
            if (saveNow)
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            NotNull(entities, nameof(entities));
            _entities.UpdateRange(entities);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }

        #region Attach & Detach
        public virtual void Attach(TEntity entity)
        {
            NotNull(entity, nameof(entity));
            if (_context.Entry(entity).State == EntityState.Detached)
                _entities.Attach(entity);
        }

        public virtual void Detach(TEntity entity)
        {
            NotNull(entity, nameof(entity));
            var entry = _context.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }
        #endregion

        #region Explicit Loading

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class
        {
            Attach(entity);
            var collection = _context.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class
        {
            Attach(entity);

            var collection = _context.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class
        {
            Attach(entity);
            var reference = _context.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class
        {
            Attach(entity);
            var reference = _context.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
        #endregion

    }
}