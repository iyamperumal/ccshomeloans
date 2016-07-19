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

    [Authorize(Roles="Admin")]
    public class Variables2Controller : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        private async void AddVariable(Variable V)
        {
            this.db.Variables.Add(V);
            await this.db.SaveChangesAsync();
        }

        public ActionResult Create() => 
            base.View();

        [ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> Create(Variable variable)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Variables.Add(variable);
            }
            else
            {
                return this.View(variable);
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
            Variable model = await this.db.Variables.FindAsync(new object[] { id });
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

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Variable entity = await this.db.Variables.FindAsync(new object[] { id });
            this.db.Variables.Remove(entity);
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
            Variable model = await this.db.Variables.FindAsync(new object[] { id });
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

        public async Task<ActionResult> Edit(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variable model = await this.db.Variables.FindAsync(new object[] { id });
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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Variable variable, string Btnasnew)
        {
            if (this.ModelState.IsValid)
            {
                if (Btnasnew != null)
                {
                    this.db.Variables.Add(variable);
                    await this.db.SaveChangesAsync();
                    return this.RedirectToAction("Index");
                }
                this.db.Entry<Variable>(variable).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }
            return this.View(variable);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Variable variable)
        {
            if ((variable != null) && base.ModelState.IsValid)
            {
                this.db.Entry<Variable>(variable).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(new Variable[] { variable }.ToDataSourceResult(request, base.ModelState));
        }

        public ActionResult GetVariableBackups([DataSourceRequest] DataSourceRequest request) => 
            base.Json(this.db.VariableBackups.ToList<VariableBackup>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        public ActionResult GetVariables([DataSourceRequest] DataSourceRequest request) => 
            base.Json(this.db.Variables.ToList<Variable>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        public async Task<ActionResult> Index()
        {
            Variables2Controller controller2 = new Variables2Controller();
            List<Variable> model = await this.db.Variables.ToListAsync<Variable>();
            return controller2.View(model);
        }

        public async Task<ActionResult> Index2()
        {
            Variables2Controller controller2 = new Variables2Controller();
            List<Variable> model = await this.db.Variables.ToListAsync<Variable>();
            return controller2.View(model);
        }

        public ActionResult SetVariableBackups([DataSourceRequest] DataSourceRequest request, VariableBackup variableBackup)
        {
            if ((variableBackup != null) && base.ModelState.IsValid)
            {
                this.db.Entry<VariableBackup>(variableBackup).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(new VariableBackup[] { variableBackup }.ToDataSourceResult(request, base.ModelState));
        }

        public ActionResult varbackup() => 
            base.View();









    }
}

