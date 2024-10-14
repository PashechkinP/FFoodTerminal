using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.DataAccessLayer.Interfaces
{
    public interface IBaseRepository<T> // T это дженерик, вместо него потом везде подставится ProductEntity
    {
        Task Create(T entity);

        IQueryable<T> GetAll(); // Чтобы Linq использовать при получении списка с условиями

        //Task<List<T>> Select();

        Task Delete(T entity);

        Task<T> Update(T entity);
    }
}
