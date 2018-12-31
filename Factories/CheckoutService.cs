using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using PhotoGallery.Entities;
using PhotoGallery.Helpers;
using Square.Connect.Model;

namespace PhotoGallery.Factories
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOptions<AppSettings> _appSettings;
        public string confirmCheckout { get; } = "confirmcheckout";
        public IEnvironment _env { get; }

        public CheckoutService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public CreateCheckoutRequest Create(Appointment appointment)
        {
            var redirectUrl = _appSettings.Value.Url.AbsoluteUri + confirmCheckout;
            var order = GenerateOrder(appointment);
            return new CreateCheckoutRequest(Guid.NewGuid().ToString(), order, RedirectUrl: redirectUrl, AskForShippingAddress: true, MerchantSupportEmail: "maczink15@outlook.com");
        }

        private CreateOrderRequest GenerateOrder(Appointment appointment)
        {
            var guid = Guid.NewGuid().ToString();
            return new CreateOrderRequest(LineItems: GenerateItems(appointment), IdempotencyKey: guid, ReferenceId: appointment.Id.ToString());
        }

        private List<CreateOrderRequestLineItem> GenerateItems(Appointment appointment)
        {
            return new List<CreateOrderRequestLineItem>()
            {
                new CreateOrderRequestLineItem(Name: "Appointment", Quantity: "1")
                {
                    BasePriceMoney = new Money(appointment.Price, Money.CurrencyEnum.USD),
                    Note = ConvertDisplay(appointment.Display),
                }
            };
        }

        private string ConvertDisplay(string appointmentDisplay)
        {
            DateTime.TryParse(appointmentDisplay, out DateTime displayDateTime);
            return displayDateTime.ToString("dddd, MMMM d, yyyy  hh:mm tt");
        }
    }
}
