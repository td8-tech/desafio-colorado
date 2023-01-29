using Business.Repository.Interfaces;
using Data.Context;
using Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class ClientRepository : IClientRepository
    {
        protected Context _context;
        public ClientRepository(Context context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var result = _context.Set<ClientEntity>().Find(id);

            if (result != null)
            {
                _context.Set<ClientEntity>().Remove(result);
                _context.Entry(result).State = EntityState.Deleted;
            }

            return Convert.ToBoolean(_context.SaveChanges());
        }

        public bool Insert(ClientEntity obj)
        {
            _context.Set<ClientEntity>().Attach(obj);
            _context.Entry(obj).State = EntityState.Added;

            return Convert.ToBoolean(_context.SaveChanges());
        }

        public ClientEntity? Select(int id)
        {
            return _context.Set<ClientEntity>().Find(id);
        }

        public IQueryable<ClientEntity> SelectAll()
        {
            return _context.Set<ClientEntity>().AsQueryable();

        }

        public bool Update(ClientEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            return Convert.ToBoolean(_context.SaveChanges());
        }
    }
}
