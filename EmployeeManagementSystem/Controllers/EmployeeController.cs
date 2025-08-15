using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using System.Runtime.InteropServices;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var employees = await _context.Employee.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(n => n.Name.Contains(searchString) || n.Email.Contains(searchString)).ToList();
            }

            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateOfBirthSortParam"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["SalaryParam"] = sortOrder == "salary_asc" ? "salary_desc" : "salary_asc";


            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.Name).ToList();
                    break;
                case "date_asc":
                    employees = employees.OrderBy(d => d.DateOfBirth).ToList();
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(d => d.DateOfBirth).ToList();
                    break;
                case "salary_asc":
                    employees = employees.OrderBy(s => s.Salary).ToList();
                    break;
                case "salary_desc":
                    employees = employees.OrderByDescending(s => s.Salary).ToList();
                    break;
                default:
                    employees = employees.OrderBy(e => e.Name).ToList();
                    break;
            }

            return View(employees);
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
