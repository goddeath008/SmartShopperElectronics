﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Data;
using MyProWeb.Models;

namespace MyProWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("customer/Myproweb/filter/{action}")]
    public class FilterController : Controller
    {
        private readonly ThaimcqlGodContext _context;

        public FilterController(ThaimcqlGodContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Products
                .Include(a => a.IdbrandNavigation)
                .Include(a => a.IdcateNavigation)
                .ToList();
            return View(list);
        }
        public IActionResult SPTheoLoai(int loaiSp)
        {
            List<Product> lstSp = _context.Products
                .Include(x => x.IdcateNavigation)
                .Include(x => x.IdbrandNavigation)
                .Where(x => x.IdcateNavigation.Idcate == loaiSp)
                .OrderBy(x => x.NamePro).ToList();
            if (lstSp == null || lstSp.Count == 0)
            {
                ViewBag.Empty = "Empty Product!!!";
            }
            var aidi = _context.Categories.Where(x => x.Idcate == loaiSp).Select(x => x.NameCate).FirstOrDefault();

            ViewBag.TenDanhMuc = aidi;

            return View(lstSp);
        }
        public IActionResult SPTheoBrand(int loaiSp)
        {
            List<Product> lstSp = _context.Products
                .Include(x => x.IdcateNavigation)
                .Include(x => x.IdbrandNavigation)
                .Where(x => x.IdbrandNavigation.Idbrand == loaiSp)
                .OrderBy(x => x.NamePro).ToList();
            if (lstSp == null || lstSp.Count == 0)
            {
                ViewBag.Empty = "Empty Product!!!";
            }
            var aidi = _context.Brands.Where(x => x.Idbrand == loaiSp).Select(x => x.NameBrand).FirstOrDefault();

            ViewBag.TenDanhMuc = aidi;

            return View(lstSp);
        }
        public IActionResult Details(int id)
        {
            var list = _context.Products
                .Include(x => x.IdbrandNavigation)
                .Include(x => x.IdcateNavigation)
                .Where(x => x.Idpro == id)
                .ToList();
            return View(list);
        }

    }
}
