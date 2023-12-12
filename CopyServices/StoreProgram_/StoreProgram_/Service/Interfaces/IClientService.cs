using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;

namespace StoreProgram_.Service.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponse>> GetAll();
        Task<ClientResponse> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ClientResponse>> UpdateAsync(int id, ClientRequest newClient);
        Task<ClientResponse> InsertAsync(ClientRequest updateClient);
    }
}
