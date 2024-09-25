using BudgetBack.Data;
using BudgetBack.Domain;

namespace BudgetBack.Services
{
    public class ProductManager
    {
        private IProductRepository _repositorio;

        public ProductManager()
        {
            _repositorio = new ProductRepositoryADO();
        }

        public List<Product> GetProducts()
        {
            return _repositorio.GetAll();
        }

        public Product GetProductByCodigo(int cod)
        {
            return _repositorio.GetById(cod);
        }

        public bool SaveProduct(Product oProduct)
        {
            return _repositorio.Save(oProduct);
        }
        public bool DeleteProduct(int cod)
        {
            return _repositorio.Delete(cod);
        }
    }
}
