using BudgetBack.Data;
using BudgetBack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBack.Services
{
    public class FacturaManager
    {
        private IBudgetRepository _repository;

        public FacturaManager()
        {
            _repository = new BudgetRepository();
        }

        public List<Budget> GetBudgets()
        {
            return _repository.GetAll();
        }

        public Budget? GetBudgetsById(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveBudget(Budget oBudget)
        {
            return _repository.Save(oBudget);
        }

        public bool UpdateBudget(Budget oBudget)
        {
            return _repository.Update(oBudget);
        }


    }
}
