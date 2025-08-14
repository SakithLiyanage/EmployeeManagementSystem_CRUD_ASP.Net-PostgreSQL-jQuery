using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employee = await _context.Employee
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return _context.Employee != null
                ? View(employee)
                : Problem("Entity set 'ApplicationDbContext.Employee' is null.");
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Employee == null)
                return NotFound();

            var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
                return NotFound();

            return PartialView("_Details", employee);  
        }


        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Email, Phone, JobTitle, DateOfBirth, Salary, Department, HireDate, ManagerId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(employee);
            }
                return View(employee); 
        }



        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Employee == null)
                return NotFound();

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
                return NotFound();

            return PartialView("_Edit", employee);  
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,JobTitle,DateOfBirth,Salary,Department,HireDate,ManagerId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }
        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _Categories = await _context.Employee.FindAsync(id);

                if (_Categories != null)
                {
                    _context.Employee.Remove(_Categories);
                }
                await _context.SaveChangesAsync();
                return new JsonResult(_Categories);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private bool EmployeeExists(long id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
