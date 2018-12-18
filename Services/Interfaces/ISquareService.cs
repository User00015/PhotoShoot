using System.Threading.Tasks;
using PhotoGallery.Entities;
using Square.Connect.Model;

namespace PhotoGallery.Services.Interfaces
{
    public interface ISquareService
    {
        Task<string> Checkout(Appointment appointment);
        Task<RetrieveTransactionResponse> RetrieveTransaction(string transactionId);
        ListLocationsResponse GetLocations();

    }
}