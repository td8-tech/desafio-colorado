using Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.Interfaces
{
    public interface IClientRepository
    {
        bool Insert(ClientEntity obj);
        bool Update(ClientEntity obj);
        bool Delete(int id);
        IQueryable<ClientEntity> SelectAll();
        ClientEntity? Select(int id);
    }
}
