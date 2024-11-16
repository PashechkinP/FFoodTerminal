using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.DataAccessLayer.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Order entity)
        {
            await _db.OrdersDbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return _db.OrdersDbSet;
        }

        public async Task Delete(Order entity)
        {
            _db.OrdersDbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Order> Update(Order entity)
        {
            _db.OrdersDbSet.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
