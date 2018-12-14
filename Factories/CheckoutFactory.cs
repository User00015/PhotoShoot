using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoGallery.Entities;
using Square.Connect.Model;

namespace PhotoGallery.Factories
{
    public static class CheckoutFactory
    {
        public static string RedirectUrl { get; } = "https://localhost:4200";

        public static CreateCheckoutRequest Create(Appointment appointment)
        {
            var order = GenerateOrder(appointment);
            return new CreateCheckoutRequest(Guid.NewGuid().ToString(), order, RedirectUrl: RedirectUrl);
        }

        private static CreateOrderRequest GenerateOrder(Appointment appointment)
        {
            return new CreateOrderRequest(LineItems: GenerateItems(appointment));
        }

        private static List<CreateOrderRequestLineItem> GenerateItems(Appointment appointment)
        {
            return new List<CreateOrderRequestLineItem>()
            {
                new CreateOrderRequestLineItem(Name: "Appointment", Quantity: "1")
                {
                    BasePriceMoney = new Money(appointment.Price, Money.CurrencyEnum.USD),
                    Note = ConvertDisplay(appointment.Display)

                }
            };
        }

        private static string ConvertDisplay(string appointmentDisplay)
        {
            DateTime.TryParse(appointmentDisplay, out DateTime displayDateTime);
            return displayDateTime.ToString("dddd, MMMM d, yyyy  hh:mm tt");
        }
    }
}
