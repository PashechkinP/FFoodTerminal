using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.DataAccessLayer.Repositories
{
    public class ProductRepository : IBaseRepository<ProductEntity>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task Create(ProductEntity entity)
        {
            await _context.ProductsDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ProductEntity> GetAll()
        {
            return _context.ProductsDbSet;
        }

        public async Task Delete(ProductEntity entity)
        {
            _context.ProductsDbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductEntity> Update(ProductEntity entity)
        {
            _context.ProductsDbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
