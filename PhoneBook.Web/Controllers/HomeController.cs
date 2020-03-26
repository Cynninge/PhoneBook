using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Repository.Interfaces;
using PhoneBook.Repository.Tables;
using PhoneBook.Web.Models;
using System;
using System.Diagnostics;

namespace PhoneBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonRepository _personRepository;

        public HomeController(ILogger<HomeController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            ViewData["Success"] = "";
            IndexModel indexModel = new IndexModel();
            indexModel.Init(_personRepository);
            return View(indexModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Error"] = "";
            ViewData["Success"] = "";
            
            try
            {
                var person = _personRepository.GetbyId(id);
                return View(person);
            }
            catch { }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Edit(int id, Person model)
        {
            ViewData["Error"] = "";
            ViewData["Success"] = "";
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = id;
                    _personRepository.Save(model);
                    return Redirect($"/home/index/{id}?message=Zapisano zmiany");
                }
                else
                {
                    throw new Exception("Błędy w formularzu!!!");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Ups coś poszło nie tak.";
            }
            return View(model);           
        }

        public IActionResult Remove(int id)
        {
            ViewData["Error"] = "";
            ViewData["Success"] = "";
            try
            {
                _personRepository.Remove(id);
                return RedirectToAction("Index");
            }
            catch { }
            return NoContent();
        }

        public IActionResult Add()
        {
            ViewData["Error"] = "";
            ViewData["Success"] = "";
            
            try
            {
                Person person = new Person();
                return View(person);
            }
            catch { }
            return NoContent();
        }
        [HttpPost]
        public IActionResult Add(Person model)
        {
            ViewData["Error"] = "";
            ViewData["Success"] = "";
            
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _personRepository.Add(model);
                    return Redirect($"/home/index/{id}?message=Dodano wiersz");
                }
                else
                {
                    throw new Exception("Błędy w formularzu!!!");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Ups coś poszło nie tak.";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Search(Person model)
        {
            return View();
        }
    }
    
}
