using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;

namespace BisleriumCafeBackend.Repository.CoffeeRepo
{
    public interface ICoffeeRepo
    {
        List<Coffee> getAll();
        Coffee? findById(int id);
        Coffee? findByName(string name);

        void saveCoffee(Coffee coffee);
        void updateCoffee(Coffee coffee);

        void deleteCoffee(int id);
    }
}
