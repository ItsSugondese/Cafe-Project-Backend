using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;

namespace BisleriumCafeBackend.Services.CoffeeServices
{
    public interface ICoffeeService
    {
        void saveCoffee(Coffee coffee);

        AddIn getSingleCoffee(int id);

        List<AddIn> getAllCoffee();

        void deleteCoffeeById(int id);
    }
}
