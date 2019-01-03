using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using PhotoGallery.Entities;
using PhotoGallery.Factories;
using PhotoGallery.Helpers;
using PhotoGallery.Services.Interfaces;
using Square.Connect.Api;
using Square.Connect.Client;
using Square.Connect.Model;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Services
{
    public class SquareService : ISquareService
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IHostingEnvironment _env;
        private readonly CheckoutApi _checkout;


        public SquareService(ICheckoutService checkoutService, IOptions<SquareSecretKey> squareSecretKey, IHostingEnvironment env)
        {
            _checkoutService = checkoutService;
            _env = env;
            _checkout = new CheckoutApi();

            Configuration.Default.AccessToken = env.IsProduction() ? squareSecretKey?.Value?.Secret : "sandbox-sq0atb-CiMknPZajSaOmSBVRx6ifQ";
        }

        public ListLocationsResponse GetLocations()
        {
            var locations = new LocationsApi();
            return locations.ListLocations();
        }

        public async Task<string> Checkout(Appointment appointment)
        {
            var locationId = GetLocations().Locations.FirstOrDefault()?.Id;
            var checkoutRequest = _checkoutService.Create(appointment);

            var response = await _checkout.CreateCheckoutAsync(locationId, checkoutRequest);
            return response?.Checkout?.ToJson();
        }

        public async Task<RetrieveTransactionResponse> RetrieveTransaction(string transactionId)
        {
            var locationId = GetLocations().Locations.FirstOrDefault()?.Id;
            return await new TransactionsApi().RetrieveTransactionAsync(locationId, transactionId);
        }
    }
}