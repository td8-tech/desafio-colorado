using Business.Repository.Interfaces;
using Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            
        }

        Task<ClientEntity> IClientService.Add(ClientEntity obj)
        {
            _clientRepository.Insert(obj);
            return Task.FromResult(obj);
        } 

        Task<bool> IClientService.Delete(int id)
        {
            _clientRepository.Delete(id);
            return Task.FromResult(true);
        }

        async Task<List<ClientEntity>> IClientService.Get()
        {
            var result = _clientRepository.SelectAll();
            return await result.ToListAsync();
        }

        Task<ClientEntity?> IClientService.GetById(int id)
        {
            return Task.FromResult(_clientRepository.Select(id));
        }

        Task<ClientEntity> IClientService.Update(ClientEntity obj)
        {
            _clientRepository.Update(obj);
            return Task.FromResult(obj);
        }
    }
}
