using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.pojo.coffee;

namespace BisleriumCafeBackend.Services.CoffeeServices
{
    public interface ICoffeeService
    {
        void saveCoffee(CoffeeRequest coffee);

        Coffee getSingleCoffee(int id);

        List<Coffee> getAllCoffee();

        void deleteCoffeeById(int id);

       
    }
}
