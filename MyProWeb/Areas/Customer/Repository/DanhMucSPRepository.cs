using MyProWeb.Data;
using MyProWeb.Models;

namespace MyProWeb.Areas.Customer.Repository
{
    public class DanhMucSPRepository :IDanhMucSPRepository
    {
        private readonly ThaimcqlGodContext _context;

        public DanhMucSPRepository(ThaimcqlGodContext context) 
        {
            _context = context;
        }
        public Category Add(Category loaiSp)
        {
            _context.Categories.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        

        public IEnumerable<Category> GetAllLoaiSP()
        {
            return _context.Categories;
        }

        public Category GetLoaiSP(int id)
        {
            return _context.Categories.Find(id);
        }



        public Category Update(Category loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        


    }
}
