using Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IClientService
    {
        Task<ClientEntity> Add(ClientEntity obj);        
        Task<bool> Delete(int id);        
        Task<List<ClientEntity>> Get();        
        Task<ClientEntity?> GetById(int id);       
        Task<ClientEntity> Update(ClientEntity obj);
    }
}
