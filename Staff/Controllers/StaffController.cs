using System.Threading.Tasks;
using Csla;
using Microsoft.AspNetCore.Mvc;
using StaffLibrary;

namespace Staff.Controllers
{
  public class StaffController : Csla.Web.Mvc.Controller
  {
    // GET: Staff
    public async Task<ActionResult> Index()
    {
      var list = await DataPortal.FetchAsync<StaffList>();
      return View(list);
    }

    // GET: Staff/Details/5
    public async Task<ActionResult> Details(int id)
    {
      var obj = await DataPortal.FetchAsync<StaffInfo>(id);
      return View(obj);
    }

    // GET: Staff/Create
    public async Task<ActionResult> Create()
    {
      var obj = await DataPortal.CreateAsync<StaffEdit>();
      return View(obj);
    }

    // POST: Staff/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(StaffEdit staff)
    {
      try
      {
        if (await SaveObjectAsync<StaffEdit>(staff, false))
          return RedirectToAction(nameof(Index));
        else
          return View(staff);
      }
      catch
      {
        return View(staff);
      }
    }

    // GET: Staff/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
      var obj = await DataPortal.FetchAsync<StaffEdit>(id);
      return View(obj);
    }

    // POST: Staff/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, StaffEdit staff)
    {
      try
      {
        LoadProperty(staff, StaffEdit.IdProperty, id);
        if (await SaveObjectAsync<StaffEdit>(staff, true))
          return RedirectToAction(nameof(Index));
        else
          return View(staff);
      }
      catch
      {
        return View(staff);
      }
    }

    // GET: Staff/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
      var obj = await DataPortal.FetchAsync<StaffInfo>(id);
      return View(obj);
    }

    // POST: Staff/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id, StaffInfo staff)
    {
      try
      {
        await DataPortal.DeleteAsync<StaffEdit>(id);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View(staff);
      }
    }
  }
}