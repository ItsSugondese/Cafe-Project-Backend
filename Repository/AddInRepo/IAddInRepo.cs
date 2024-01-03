using BisleriumCafeBackend.Model.AddIn;

namespace BisleriumCafeBackend.Repository.AddInRepo
{
    public interface IAddInRepo
    {
        List<AddIn> getAll();
        AddIn findById(int id);

        void saveAddin(AddIn addIn);
        void updateAddin(AddIn addIn);
    }
}
