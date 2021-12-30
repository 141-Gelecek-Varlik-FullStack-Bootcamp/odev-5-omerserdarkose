using AutoMapper;
using HelenSposa.Business.Abstract;
using HelenSposa.Business.Aspects.Autofac;
using HelenSposa.Business.Constant;
using HelenSposa.Business.ValidationRules.FluentValidation;
using HelenSposa.Core.Aspects.Autofac;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.DataAccess.Abstract;
using HelenSposa.Entities.Concrete;
using HelenSposa.Entities.Dtos.ExpenseType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Concrete.Managers
{
    public class ExpenseTypeManager : IExpenseTypeService
    {
        private IExpenseTypeDal _expenseTypeDal;
        private IMapper _mapper;

        public ExpenseTypeManager(IExpenseTypeDal expenseTypeDal, IMapper mapper)
        {
            _expenseTypeDal = expenseTypeDal;
            _mapper = mapper;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ExpenseTypeAddValidator))]
        //[CacheRemoveAscpect("IExpenseTypeService.Get")]
        public IResult Add(ExpenseTypeAddDto addedExpenseType)
        {
            var mapExpenseType = _mapper.Map<ExpenseType>(addedExpenseType);
            _expenseTypeDal.Add(mapExpenseType);
            return new SuccessResult(Messages.ExpenseTypeAdded);
        }

        [SecuredOperation("admin")]
        //[CacheRemoveAscpect("IExpenseTypeService.Get")]
        public IResult Delete(int id)
        {
            _expenseTypeDal.Delete(new ExpenseType { Id=id});
            return new SuccessResult(Messages.ExpenseTypeDeleted);
        }

        [SecuredOperation("admin,worker")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<ExpenseTypeShowDto>> GetAll()
        {
            var expenseTypeList = _expenseTypeDal.GetList();
            var mapExpenseTypeList = _mapper.Map<List<ExpenseTypeShowDto>>(expenseTypeList);
            return new SuccessDataResult<List<ExpenseTypeShowDto>>(mapExpenseTypeList);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ExpenseTypeUpdateValidator))]
        //[CacheRemoveAscpect("IExpenseTypeService.Get")]
        public IResult Update(ExpenseTypeUpdateDto updatedExpenseType)
        {
            var mapExpenseType = _mapper.Map<ExpenseType>(updatedExpenseType);
            _expenseTypeDal.Update(mapExpenseType);
            return new SuccessResult(Messages.ExpenseTypeUpdated);
        }
    }
}
