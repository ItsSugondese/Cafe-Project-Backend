using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.pojo.coffee;

namespace BisleriumCafeBackend.Services.AddInServices
{
    public interface IAddInService
    {
        void saveAddIn(CoffeeRequest addInRequest);

        AddIn getSingleAddin(int id);

        List<AddIn> getAllAddin();

        void deleteAddInById(int id);
    }
}
