using FirstMVC_SQL.Data;
using FirstMVC_SQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVC_SQL.Controllers
{
    public class ContactsController : Controller
    {

        //inyección de dependencia
        private readonly IContactsRepository _contactsRepository;

        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }



        // GET: ContactsController
        public async Task<ActionResult> Index()
        {
            var contacts = await _contactsRepository.GetAll();

            return View(contacts);
        }

        // GET: ContactsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var contact= await _contactsRepository.GetDetails(id);
            return View(contact);
        }

        // GET: ContactsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var contact = new Contact()
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Phone = collection["Phone"],
                    Address = collection["Address"]
                };

                await _contactsRepository.Insert(contact);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var contact = await _contactsRepository.GetDetails(id);
            return View(contact);
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {

                var contact = new Contact()
                {
                    Id = int.Parse(collection["id"]),
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Phone = collection["Phone"],
                    Address = collection["Address"]
                };

                await _contactsRepository.Update(contact);
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var contact = await _contactsRepository.GetDetails(id);

            return View(contact);
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {

                await _contactsRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
