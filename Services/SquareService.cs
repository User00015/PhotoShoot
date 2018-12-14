using System;
using PhotoGallery.Services.Interfaces;
using Square.Connect.Api;
using Square.Connect.Client;
using Square.Connect.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoGallery.Entities;
using PhotoGallery.Factories;
using RestSharp.Extensions;

namespace PhotoGallery.Services
{
    public class SquareService : ISquareService
    {
        private readonly string _accessToken = "sandbox-sq0atb-7a6pX3VZ40QWQyZwSI9xfA"; //TODO THIS IS A SECRET -- CHANGE BEFORE DEPLOYMENT
        private readonly CheckoutApi _checkout; 

        public SquareService()
        {
            _checkout = new CheckoutApi();
            Configuration.Default.AccessToken = _accessToken;
        }

        public ListLocationsResponse GetLocations()
        {
            var locations = new LocationsApi();
            return locations.ListLocations();
        }

        public async Task<string> Checkout(Appointment appointment)
        {
            var locationId = GetLocations().Locations.FirstOrDefault()?.Id;
            var checkoutRequest = CheckoutFactory.Create(appointment);

            var response = await _checkout.CreateCheckoutAsync(locationId, checkoutRequest);
            return response?.Checkout?.ToJson();
        }
    }
}