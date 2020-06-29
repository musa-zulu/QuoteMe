using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IClientService
    {
        List<Client> GetClients();
        bool CreateClient(Client client);
        Client GetClientById(Guid clientId);
        bool UpdateClient(Client clientToUpdate);
        bool DeleteClient(Client client);
    }
}
