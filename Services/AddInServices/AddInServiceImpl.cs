using BisleriumCafeBackend.constants;
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

        public void deleteAddInById(int id)
        {
            errorWhenAddInNotExist(id);
            _addInRepo.deleteAddin(id);
        }

        public void saveAddIn( AddIn addIn)
        {
            List<AddIn> addInList = _addInRepo.getAll();

            errorWhenAddInNameAlreadyExist(addIn);
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
                errorWhenAddInNotExist(addIn.Id ?? 0);
                _addInRepo.updateAddin(addIn);
            }
            
        }

        List<AddIn> IAddInService.getAllAddin()
        {
            return _addInRepo.getAll();
        }

        AddIn IAddInService.getSingleAddin(int id)
        {
            errorWhenAddInNotExist(id);
            return _addInRepo.findById(id);
        }

        private void errorWhenAddInNotExist(int id)
        {
            if (_addInRepo.findById(id) == null)
            {
                throw new Exception(MessageConstantsMerge.notExist("id", ModuleNameConstants.ADDIN));
            }
        }
        private void errorWhenAddInNameAlreadyExist(AddIn addIn)
        {
            AddIn checkAddIn = _addInRepo.findByName(addIn.Name);
            if (checkAddIn != null && checkAddIn.Id != addIn.Id)
            {
                throw new Exception(MessageConstantsMerge.alreadyExist("name", ModuleNameConstants.ADDIN));
            }
        }
    }
}
