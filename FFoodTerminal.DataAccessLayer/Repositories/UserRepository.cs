using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.DataAccessLayer.Repositories
{
    public class UserRepository: IBaseRepository<UserEntity>
    {
        
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext db)
        {
            _context = db;
        }
        public IQueryable<UserEntity> GetAll()
        {
            return _context.UsersDbSet;
        }
        public async Task Delete(UserEntity entity)
        {
            _context.UsersDbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Create(UserEntity entity)
        {
            await _context.UsersDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<UserEntity> Update(UserEntity entity)
        {
            _context.UsersDbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }

}
