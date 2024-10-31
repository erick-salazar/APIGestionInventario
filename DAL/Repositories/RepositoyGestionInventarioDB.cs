using APIGestionInventario.Interfaces;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestionInventario.DAL.Repositories
{
    public class RepositoyGestionInventarioDB<T> : IRepositoyGestionInventarioDB<T> where T : class
    {
        private readonly GestionInventarioContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoyGestionInventarioDB(GestionInventarioContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
