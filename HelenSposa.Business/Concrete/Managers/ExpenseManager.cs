using AutoMapper;
using HelenSposa.Business.Abstract;
using HelenSposa.Business.Aspects.Autofac;
using HelenSposa.Business.Constant;
using HelenSposa.Core.Aspects.Autofac;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.DataAccess.Abstract;
using HelenSposa.Entities.Concrete;
using HelenSposa.Entities.Dtos.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Concrete.Managers
{
    public class ExpenseManager : IExpenseService
    {
        private IExpenseDal _expenseDal;
        private IMapper _mapper;

        public ExpenseManager(IExpenseDal expenseDal, IMapper mapper)
        {
            _expenseDal = expenseDal;
            _mapper = mapper;
        }

        [CacheRemoveAscpect("IExpenseService.Get")]
        public IResult Add(ExpenseAddDto addedExpense)
        {
            var mapExpense = _mapper.Map<Expense>(addedExpense);
            _expenseDal.Add(mapExpense);
            return new SuccessResult(Messages.ExpenseAdded);
        }

        public IResult Delete(int id)
        {
            _expenseDal.Delete(new Expense { Id=id});
            return new SuccessResult(Messages.ExpenseDeleted);
        }

        [CacheAspect(duration:5,Priority =2)]
        [SecuredOperation("admin",Priority =1)]
        public IDataResult<List<ExpenseShowDto>> GetAll()
        {
            var expenseList = _expenseDal.GetList();
            var mapExpenseList = _mapper.Map<List<ExpenseShowDto>>(expenseList);
            return new SuccessDataResult<List<ExpenseShowDto>>(mapExpenseList);
        }

        [CacheAspect(duration: 5)]
        public IDataResult<ExpenseShowDto> GetById(int id)
        {
            var expense = _expenseDal.Get(e => e.Id == id);
            var mapExpense = _mapper.Map<ExpenseShowDto>(expense);
            return new SuccessDataResult<ExpenseShowDto>(mapExpense);
        }

        public IResult Update(ExpenseUpdateDto updatedExpense)
        {
            var mapExpense = _mapper.Map<Expense>(updatedExpense);
            _expenseDal.Update(mapExpense);
            return new SuccessResult(Messages.ExpenseUpdated);
        }
    }
}
