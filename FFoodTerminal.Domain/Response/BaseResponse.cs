using Azure.Core;
using FFoodTerminal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string DescriptionError { get; set; } // Название ошибок и их расшифровка

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; } // Результат запроса
    }

    public interface IBaseResponse<T>
    {
        StatusCode StatusCode { get; }
        T Data { get; set; }
    }
}
