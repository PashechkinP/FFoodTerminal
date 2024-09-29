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

        public Task<bool> Create(ProductEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(ProductEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductEntity> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductEntity>> Select()
        {
            return await _context.ProductEntity.ToListAsync();
        }
    }
}
