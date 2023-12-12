using Microsoft.Data.SqlClient;
using StoreProgram_.Model;
using StoreProgram_.Repository.Interfaces;
using System.Data;

namespace StoreProgram_.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DataContext context) : base(context)
        {
        }
    }
}
