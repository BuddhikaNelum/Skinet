using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;
        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetProdcutByIdAsync(int id)
        {
            return await context.products
                                .Include(p => p.ProductType)
                                .Include(p => p.productBrand)
                                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await context.products
                                .Include(p => p.ProductType)
                                .Include(p => p.productBrand)
                                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}