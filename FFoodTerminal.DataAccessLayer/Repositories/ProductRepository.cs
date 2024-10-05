using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<bool> Create(ProductEntity entity)
        {
            await _context.ProductEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(ProductEntity entity)
        {
            _context.ProductEntity.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductEntity> Get(int id)
        {
            return await _context.ProductEntity.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductEntity> GetByName(string name)
        {
            return await _context.ProductEntity.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<ProductEntity>> Select()
        {
            return await _context.ProductEntity.ToListAsync();
        }

        public async Task<ProductEntity> Update(ProductEntity entity)
        {
            _context.ProductEntity.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
