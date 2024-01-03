using BisleriumCafeBackend.enums;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.User;
using BisleriumCafeBackend.Repository.AddInRepo;
using static BisleriumCafeBackend.constants.FileNameEnum;

namespace BisleriumCafeBackend.Services.AddInServices
{
    public class AddInServiceImpl : IAddInService
    {
        private readonly IAddInRepo _addInRepo;
        public AddInServiceImpl(IAddInRepo addInRepo) {
            _addInRepo = addInRepo;
        }
        public void saveAddIn( AddIn addIn)
        {
            List<AddIn> addInList = _addInRepo.getAll();


            if (addIn.Id == null)
            {
                if (addInList.Count() > 0)
                {
                    AddIn lastAddin = addInList.Last();
                    addIn.Id = lastAddin.Id + 1;
                }
                else
                {
                    addIn.Id = 1;
                }
                _addInRepo.saveAddin(addIn);
            }
            else
            {
                if (_addInRepo.findById(addIn.Id ?? 0) == null)
                {
                    throw new Exception("AddIn with that id doesn't exists");
                }
                _addInRepo.updateAddin(addIn);
            }
            
        }

        List<AddIn> IAddInService.getAllAddin()
        {
            return _addInRepo.getAll();
        }

        AddIn IAddInService.getSingleAddin(int id)
        {
            return _addInRepo.findById(id);
        }
    }
}
