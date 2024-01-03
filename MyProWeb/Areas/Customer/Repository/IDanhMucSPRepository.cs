using MyProWeb.Models;

namespace MyProWeb.Areas.Customer.Repository
{
    public interface IDanhMucSPRepository
    {
        public interface IDanhMucSPRepository
        {
            Category Add(Category loaiSp);
            Category Update(Category loaiSp);
            Category Delete(int id);
            Category GetLoaiSP(int loaiSp);

            IEnumerable<Category> GetAllLoaiSP();
        }
    }
}
