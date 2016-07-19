namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class AddressesController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Address_Id,AddressDate,StreetAddress,UnitNumber,City,State,ZipCode,County,Zip4,ZipPlusZip4,CareOfName,CarrierRoute,Country,IsMailingAddress,IsMailBox,YearsAtThisAddress")] Address address)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Addresses.Add(address);
            }
            else
            {
                return this.View(address);
            }
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address model = await this.db.Addresses.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                result = this.View(model);
            }
            return result;
        }

        [ValidateAntiForgeryToken, ActionName("Delete"), HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Address entity = await this.db.Addresses.FindAsync(new object[] { id });
            this.db.Addresses.Remove(entity);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address model = await this.db.Addresses.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                result = this.View(model);
            }
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Address_Id,AddressDate,StreetAddress,UnitNumber,City,State,ZipCode,County,Zip4,ZipPlusZip4,CareOfName,CarrierRoute,Country,IsMailingAddress,IsMailBox,YearsAtThisAddress")] Address address)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry<Address>(address).State = EntityState.Modified;
            }
            else
            {
                return this.View(address);
            }
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address model = await this.db.Addresses.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                result = this.View(model);
            }
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Address address)
        {
            if ((address != null) && base.ModelState.IsValid)
            {
                this.db.Entry<Address>(address).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(new Address[] { address }.ToDataSourceResult(request, base.ModelState));
        }

        public ActionResult GetAddresses([DataSourceRequest] DataSourceRequest request)
        {
            List<Address> enumerable = this.db.Addresses.ToList<Address>();
            return base.Json(enumerable.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index() => 
            base.View();

        public async Task<ActionResult> Index2()
        {
            AddressesController controller2 = new AddressesController();
            List<Address> model = await this.db.Addresses.ToListAsync<Address>();
            return controller2.View(model);
        }







    }
}

