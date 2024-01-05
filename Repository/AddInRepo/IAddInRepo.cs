using BisleriumCafeBackend.Model.AddIn;

namespace BisleriumCafeBackend.Repository.AddInRepo
{
    public interface IAddInRepo
    {
        List<AddIn> getAll();
        AddIn? findById(int id);
        AddIn? findByName(string name);

        void saveAddin(AddIn addIn);
        void updateAddin(AddIn addIn);

        void deleteAddin(int id);
    }
}
