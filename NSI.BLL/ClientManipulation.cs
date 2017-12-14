﻿using System;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.DC.ClientsRepository;

namespace NSI.BLL
{
    public partial class ClientManipulation:IClientManipulation
    {
        private readonly IClientRepository _clientRepository;

        public ClientManipulation(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ClientDTO CreateClient(ClientDTO clientDTO)
        {
           return _clientRepository.CreateClient(clientDTO);
        }

        public bool DeleteClientById(int clientId)
        {
            return _clientRepository.DeleteClientById(clientId);
        }

        public bool EditClient(int clientId, ClientDTO clientDTO)
        {
            return _clientRepository.EditClient(clientId, clientDTO); 
        }

        public ClientDTO GetClientById(int clientId)
        {
            return _clientRepository.GetClientById(clientId);
        }

        public ICollection<ClientDTO> GetClients()
        {
            return _clientRepository.GetClients();
        }

        //public ICollection<ClientDTO> SearchClient(ClientDTO searchCriteria)
        //{
        //    //return SearchClient(searchCriteria);
        //    throw new NotImplementedException();
        //}
    }
}
