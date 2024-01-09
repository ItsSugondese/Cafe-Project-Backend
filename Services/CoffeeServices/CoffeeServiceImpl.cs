using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.pojo.coffee;
using BisleriumCafeBackend.Repository.AddInRepo;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.TemporaryAttachmentsRepo;
using BisleriumCafeBackend.utils.GenericFile;
using OfficeOpenXml.Packaging.Ionic.Zip;

namespace BisleriumCafeBackend.Services.CoffeeServices
{
    public class CoffeeServiceImpl : ICoffeeService
    {
        private readonly ICoffeeRepo _coffeeRepo;
        private readonly GenericFileUtils genericFileUtils;
        private readonly ITemporaryAttachmentsRepo temporaryAttachmentsRepo;


        public CoffeeServiceImpl(ICoffeeRepo coffeeRepo, ITemporaryAttachmentsRepo temporaryAttachmentsRepo)
        {
            _coffeeRepo = coffeeRepo;
            this.temporaryAttachmentsRepo = temporaryAttachmentsRepo;
            genericFileUtils = new GenericFileUtils();
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

        public void saveCoffee(CoffeeRequest coffeeRequest)
        {
            Coffee coffee;
            if (coffeeRequest.Id != null)
            {
                coffee = _coffeeRepo.findById((int)coffeeRequest.Id);
                coffee.Name = coffeeRequest.Name;
                coffee.Price = coffeeRequest.Price;
            }
            else
            {
                coffee = new Coffee
                {
                    Name = coffeeRequest.Name,
                    Price = coffeeRequest.Price,
                    Id = coffeeRequest.Id,
                };
            }
            errorWhenCoffeeNameAlreadyExist(coffee);
            List<Coffee> coffeeList = _coffeeRepo.getAll();

            if (coffeeRequest.fileId != null)
            {

                coffee.FilePath = genericFileUtils.CopyFileToServer(temporaryAttachmentsRepo.getById((int)coffeeRequest.fileId).Location, FilePathMapping.COFFEE, FilePathConstants.TempPath);
            }
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
                errorWhenCoffeeNotExist(coffeeRequest.Id ?? 0);
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
            if (checkCoffee != null && checkCoffee.Id != coffee.Id)
            {
                throw new Exception(MessageConstantsMerge.alreadyExist("name", ModuleNameConstants.COFFEE));
            }
        }

        
    }
}
