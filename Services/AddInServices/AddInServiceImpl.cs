using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.enums;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.User;
using BisleriumCafeBackend.pojo.coffee;
using BisleriumCafeBackend.Repository.AddInRepo;
using BisleriumCafeBackend.Repository.TemporaryAttachmentsRepo;
using BisleriumCafeBackend.utils.GenericFile;
using static BisleriumCafeBackend.constants.FileNameEnum;

namespace BisleriumCafeBackend.Services.AddInServices
{
    public class AddInServiceImpl : IAddInService
    {
        private readonly IAddInRepo _addInRepo;
        private readonly GenericFileUtils genericFileUtils;
        private readonly ITemporaryAttachmentsRepo temporaryAttachmentsRepo;
        public AddInServiceImpl(IAddInRepo addInRepo, ITemporaryAttachmentsRepo temporaryAttachmentsRepo) {
            _addInRepo = addInRepo;
            this.temporaryAttachmentsRepo = temporaryAttachmentsRepo;
            genericFileUtils = new GenericFileUtils();
        }

        public void deleteAddInById(int id)
        {
            errorWhenAddInNotExist(id);
            _addInRepo.deleteAddin(id);
        }

        public void saveAddIn( CoffeeRequest addInRequest)
        {
            AddIn addIn;
            if (addInRequest.Id != null)
            {
                addIn = _addInRepo.findById((int)addInRequest.Id);
                addIn.Name = addInRequest.Name;
                addIn.Price = addInRequest.Price;
            }
            else
            {
                addIn = new AddIn
                {
                    Name = addInRequest.Name,
                    Price = addInRequest.Price,
                    Id = addInRequest.Id,
                };
            }

            errorWhenAddInNameAlreadyExist(addIn);
            List<AddIn> addInList = _addInRepo.getAll();
            if (addInRequest.fileId != null)
            {

                addIn.FilePath = genericFileUtils.CopyFileToServer(temporaryAttachmentsRepo.getById((int)addInRequest.fileId).Location, FilePathMapping.COFFEE, FilePathConstants.TempPath);
            }

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
