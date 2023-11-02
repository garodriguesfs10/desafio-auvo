using DesafioAuvo.Domain.Entities;

namespace DesafioAuvo.Application.Services.Interfaces
{
    public interface ICsvService
    {
        Task<List<Resultado>> ProcesseCsvAsync(string path);
    }
}
