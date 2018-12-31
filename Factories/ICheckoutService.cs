using PhotoGallery.Entities;
using Square.Connect.Model;

namespace PhotoGallery.Factories
{
    public interface ICheckoutService
    {
        CreateCheckoutRequest Create(Appointment appointment);
    }
}