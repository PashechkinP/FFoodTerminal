using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FFoodTerminal.DataAccessLayer.Repositories
{
    public class ProfileRepository: IBaseRepository<ProfileEntity>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(ProfileEntity entity)
        {
            await _dbContext.ProfilesDbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<ProfileEntity> GetAll()
        {
            return _dbContext.ProfilesDbSet;
        }

        public async Task Delete(ProfileEntity entity)
        {
            _dbContext.ProfilesDbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProfileEntity> Update(ProfileEntity entity)
        {
            _dbContext.ProfilesDbSet.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
