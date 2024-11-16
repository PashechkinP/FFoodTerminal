using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.DataAccessLayer.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly ApplicationDbContext _db;

        public BasketRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task Create(Basket entity)
        {
            await _db.BasketsDbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Basket> GetAll()
        {
            return _db.BasketsDbSet;
        }

        public async Task Delete(Basket entity)
        {
            _db.BasketsDbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Basket> Update(Basket entity)
        {
            _db.BasketsDbSet.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
