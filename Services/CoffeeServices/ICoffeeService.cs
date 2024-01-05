using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;

namespace BisleriumCafeBackend.Services.CoffeeServices
{
    public interface ICoffeeService
    {
        void saveCoffee(Coffee coffee);

        Coffee getSingleCoffee(int id);

        List<Coffee> getAllCoffee();

        void deleteCoffeeById(int id);
    }
}
