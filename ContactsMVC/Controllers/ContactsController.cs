using System.Threading.Tasks;
using ContactsLibrary;
using Csla;
using Microsoft.AspNetCore.Mvc;

namespace ContactsMVC.Controllers
{
    public class ContactsController : Csla.Web.Mvc.Controller
    {

        public async Task<ActionResult> Index()
        {
            var list = await ContactList.GetContactListAsync();

            return View(list);
        }

        public async Task<ActionResult> Details(int id)
        {
            var obj = await ContactRO.GetContactROAsync(id);
            return View(obj);
        }

        public async Task<ActionResult> Create()
        {
            var obj = await DataPortal.CreateAsync<ContactEdit>();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactEdit contactEdit)
        {
            try
            {
                if (await SaveObjectAsync(contactEdit, false))
                    return RedirectToAction(nameof(Index));
                else
                    return View(contactEdit);
            }
            catch
            {
                return View(contactEdit);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var obj = await DataPortal.FetchAsync<ContactEdit>(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ContactEdit contactEdit)
        {
            try
            {
                LoadProperty(contactEdit, ContactEdit.IdProperty, id);
                if (await SaveObjectAsync(contactEdit, true))
                    return RedirectToAction(nameof(Index));
                else
                    return View(contactEdit);
            }
            catch
            {
                return View(contactEdit);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var obj = await DataPortal.FetchAsync<ContactRO>(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ContactRO contactRO)
        {
            try
            {
                await DataPortal.DeleteAsync<ContactEdit>(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(contactRO);
            }
        }
    }
}