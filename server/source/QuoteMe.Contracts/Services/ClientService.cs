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

        public Client GetClientById(Guid clientID)
        {
            if (clientID == Guid.Empty)
                throw new ArgumentNullException(nameof(clientID));
            return _applicationDbContext.Clients.FirstOrDefault(x => x.ClientID == clientID);
        }

        public List<Client> GetClients()
        {
            return _applicationDbContext
                ?.Clients
                .ToList();
        }

        public bool UpdateClient(Client clientToUpdate)
        {
            if (clientToUpdate == null)
                throw new ArgumentNullException(nameof(clientToUpdate));
            var client = GetClientById(clientToUpdate.ClientID);

            if (client != null)
            {
                client.AddedBy = clientToUpdate.AddedBy;
                client.AdditionalContactInfo = clientToUpdate.AdditionalContactInfo;
                client.AddressID = clientToUpdate.AddressID;
                client.DateCreated = clientToUpdate.DateCreated;
                client.DateLastModified = clientToUpdate.DateLastModified;
                client.Demographics = clientToUpdate.Demographics;
                client.Email = clientToUpdate.Email;
                client.EmailPromotion = clientToUpdate.EmailPromotion;
                client.FirstName = clientToUpdate.FirstName;
                client.LastName = clientToUpdate.LastName;
                client.LastUpdatedBy = clientToUpdate.LastUpdatedBy;
                client.MiddleName = clientToUpdate.MiddleName;
                client.PersonTypeID = clientToUpdate.PersonTypeID;
                client.PhoneID = clientToUpdate.PhoneID;
                client.Suffix = clientToUpdate.Suffix;
                client.Title = clientToUpdate.Title;
            }
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
