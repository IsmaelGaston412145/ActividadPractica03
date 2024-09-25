

using BudgetBack.Domain;

namespace BudgetBack.Data
{
    public interface IBudgetRepository
    {
        bool Save(Budget oBudget);
        bool Update(Budget oBudget);

        List<Budget> GetAll();
        Budget? GetById(int id);

    }
}
