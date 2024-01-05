using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Repository.AddInRepo;
using BisleriumCafeBackend.Repository.CoffeeRepo;

namespace BisleriumCafeBackend.Services.CoffeeServices
{
    public class CoffeeServiceImpl : ICoffeeService
    {
        private readonly ICoffeeRepo _coffeeRepo;
        public CoffeeServiceImpl(ICoffeeRepo coffeeRepo)
        {
            _coffeeRepo = coffeeRepo;
        }
        public void deleteCoffeeById(int id)
        {

            errorWhenCoffeeNotExist(id);
            _coffeeRepo.deleteCoffee(id);
        }

        public List<Coffee> getAllCoffee()
        {
            return _coffeeRepo.getAll();
        }

        public Coffee getSingleCoffee(int id)
        {
            return _coffeeRepo.findById(id);
        }

        public void saveCoffee(Coffee coffee)
        {
            List<Coffee> coffeeList = _coffeeRepo.getAll();

            errorWhenCoffeeNameAlreadyExist(coffee);
            if (coffee.Id == null)
            {
                if (coffeeList.Count() > 0)
                {
                    Coffee lastCoffee = coffeeList.Last();
                    coffee.Id = lastCoffee.Id + 1;
                }
                else
                {
                    coffee.Id = 1;
                }
                    _coffeeRepo.saveCoffee(coffee);
            }
            else
            {
                errorWhenCoffeeNotExist(coffee.Id ?? 0);
                _coffeeRepo.updateCoffee(coffee);
            }
        }

        private void errorWhenCoffeeNotExist(int id)
        {
            if (_coffeeRepo.findById(id) == null)
            {
                throw new Exception(MessageConstantsMerge.notExist("id", ModuleNameConstants.COFFEE));
            }
        }
        private void errorWhenCoffeeNameAlreadyExist(Coffee coffee)
        {
            Coffee checkCoffee = _coffeeRepo.findByName(coffee.Name);
            if (checkCoffee != null && checkCoffee.Id != checkCoffee.Id)
            {
                throw new Exception(MessageConstantsMerge.alreadyExist("name", ModuleNameConstants.COFFEE));
            }
        }
    }
}
