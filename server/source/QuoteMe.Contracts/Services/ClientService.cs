using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Services
{
    public class ClientService : IClientService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public ClientService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public bool CreateClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _applicationDbContext.Clients.Add(client);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool DeleteClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _applicationDbContext.Clients.Remove(client);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public Client GetClientById(Guid clientId)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentNullException(nameof(clientId));
            return _applicationDbContext.Clients.FirstOrDefault(x => x.ClientId == clientId);
        }

        public List<Client> GetClients()
        {
            return _applicationDbContext.Clients.ToList();
        }

        public bool UpdateClient(Client clientToUpdate)
        {
            if (clientToUpdate == null)
                throw new ArgumentNullException(nameof(clientToUpdate));
            _applicationDbContext.Clients.Update(clientToUpdate);
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
