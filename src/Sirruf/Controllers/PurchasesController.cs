using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sirruf.Data;
using Sirruf.Models;
using Microsoft.AspNetCore.Identity;

namespace Sirruf.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchasesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchString, int purchaseGrade)
        {
            IQueryable<int> genreQuery = from p in _context.Purchase
                                            orderby p.Grade
                                            select p.Grade;

            var purchases = from p in _context.Purchase
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                purchases = purchases.Where(s => s.Name.Contains(searchString));
            }

            if (purchaseGrade >= 1 && purchaseGrade <= 10)
            {
                purchases = purchases.Where(s => s.Grade == purchaseGrade);
            }

            var purchaseGradeVM = new PurchaseGradeViewModel();
            purchaseGradeVM.grades = new SelectList(await genreQuery.Distinct().ToListAsync());
            purchaseGradeVM.purchases = await purchases.ToListAsync();

            return View(purchaseGradeVM);
        }

        public async Task<IActionResult> PublicPurchases(string searchString, int purchaseGrade)
        {
            IQueryable<int> genreQuery = from p in _context.Purchase
                                         orderby p.Grade
                                         where p.IsPublic == true
                                         select p.Grade;

            var purchases = from p in _context.Purchase
                            where p.IsPublic == true
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                purchases = purchases.Where(s => s.Name.Contains(searchString));
            }

            if (purchaseGrade >= 1 && purchaseGrade <= 10)
            {
                purchases = purchases.Where(s => s.Grade == purchaseGrade);
            }

            var purchaseGradeVM = new PurchaseGradeViewModel();
            purchaseGradeVM.grades = new SelectList(await genreQuery.Distinct().ToListAsync());
            purchaseGradeVM.purchases = await purchases.ToListAsync();

            return View(purchaseGradeVM);
        }

        public async Task<IActionResult> UserPurchases(string searchString, int purchaseGrade)
        {
            IQueryable<int> genreQuery = from p in _context.Purchase
                                         orderby p.Grade
                                         select p.Grade;
    
            var purchases = from p in _context.Purchase
                            where p.ApplicationUserID == _userManager.GetUserId(User)
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                purchases = purchases.Where(s => s.Name.Contains(searchString));
            }

            if (purchaseGrade >= 1 && purchaseGrade <= 10)
            {
                purchases = purchases.Where(s => s.Grade == purchaseGrade);
            }

            var purchaseGradeVM = new PurchaseGradeViewModel();
            purchaseGradeVM.grades = new SelectList(await genreQuery.Distinct().ToListAsync());
            purchaseGradeVM.purchases = await purchases.ToListAsync();

            return View(purchaseGradeVM);
        }


        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.ID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, Grade, BrandID, ShopID, CategoryID, Name, Price, PurchaseDate")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.ID == id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, Grade, BrandID, ShopID, CategoryID, Name, Price, PurchaseDate")] Purchase purchase)
        {
            if (id != purchase.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.ID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.ID == id);
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.ID == id);
        }
    }
}
