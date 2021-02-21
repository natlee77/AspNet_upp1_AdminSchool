using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Upp1_admin.Data;
using Upp1_admin.Models;

namespace Upp1_admin.Controllers
{
    public class SchoolClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly  UserManager<AppUser> _userManager; //++

        public SchoolClassesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager= userManager;    //++
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.SchoolClasses.ToListAsync());
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClasses
                .Include(s=>s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }




        // GET: SchoolClasses/Create---------------------------------
        public async Task<IActionResult> Create()  
        {
            ViewBag.Teachers = await _userManager.GetUsersInRoleAsync("Teacher"); //++
            //ViewBag.Managers = teachers;    //++

            return View();
        }  //nu,  har vi info  --i post måste spara

        // POST: SchoolClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Teacher")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {

                //++måste spara data from dropbox      ??? 
                //var Teacher = schoolClass.Teacher.GetDisplayName;//?????
                //Console.WriteLine(schoolClass.Teacher.Id);//kan kontrolera 

                _context.Add(schoolClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }



        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            //++ from editclassVM
            var viewModel = new EditClassViewModel()
            {
                CurrentClass = schoolClass,
                Teachers = _userManager.Users ,//.Include(x=>x.Id)
                // Teachers = (IEnumerable<AppUser>)_userManager.Users.Where(x => x.Id == id).FirstOrDefault()  //??
                //IsSelected = _userManager.Users.
                //(x => x.Id == id).FirstOrDefault()
                //    Any(x=>xid==id)?true:false
            };  //).ToList();
            return View(viewModel);  //skicka viewModel med Current class info---> nu måste hämta alla lärare 
        }//vald lärare --> spara (post)

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditClassViewModel model)
        { 
            if (id != model.CurrentClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(model); //det är editClassVM, måste ha gamla -SchoolClass
                    //++ new object
                    var SchoolClass = new SchoolClass()
                    {
                        Id = model.CurrentClass.Id,
                        //Teacher = (x=>x.IsSelected==true).Select()//select del ERROR
                        //Teacher = model.IsSelected.ToString()
                        //((x => x.Id == id).FirstOrDefault())


                    };


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassExists(model.CurrentClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            _context.SchoolClasses.Remove(schoolClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassExists(string id)
        {
            return _context.SchoolClasses.Any(e => e.Id == id);
        }
    }
}
