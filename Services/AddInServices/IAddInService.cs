using BisleriumCafeBackend.Model.AddIn;

namespace BisleriumCafeBackend.Services.AddInServices
{
    public interface IAddInService
    {
        void saveAddIn(AddIn addIn);

        AddIn getSingleAddin(int id);

        List<AddIn> getAllAddin();

        void deleteAddInById(int id);
    }
}
