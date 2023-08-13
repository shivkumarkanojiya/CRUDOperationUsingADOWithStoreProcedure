using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDOperationUsingADOWithStoreProcedure.DAL;
using CRUDOperationUsingADOWithStoreProcedure.Models;

namespace CRUDOperationUsingADOWithStoreProcedure.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL pdal = new ProductDAL();
        // GET: Product
        public ActionResult Index(string searchBy, string searchValue)
        {
            try
            {
                var ProductList = pdal.GetAllProducts();
                if (ProductList.Count == 0)
                {
                    TempData["InsertMessage"] = "Products Not Availabe";
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InsertMessage"] = "Please provide search value.";
                        return View(ProductList);
                    }
                    else
                    {
                        if (searchBy == "ProductName")
                        {
                            var searchByProductname = ProductList.Where(p => p.productName.ToLower().Contains(searchValue.ToLower()));
                            return View(searchByProductname);
                        }
                        else if (searchBy == "Price")
                        {
                            var searchByProductname = ProductList.Where(p => p.price == decimal.Parse(searchValue));
                            return View(searchByProductname);
                        }
                        else if (searchBy == "Qty")
                        {
                            var searchByProductname = ProductList.Where(p => p.Qty == int.Parse(searchValue));
                            return View(searchByProductname);
                        }
                    }
                }
                return View(ProductList);
            }
            catch (Exception ex)
            {

                TempData["InsertMessage"] = ex.Message;
                return View();
            }

        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product p)
        {
            bool Isinserted = false;
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    Isinserted = pdal.SaveDataProductmast(p);
                    if (Isinserted)
                    {
                        TempData["InsertMessage"] = "Records Saved SuccessFully";
                    }
                    else
                    {
                        TempData["InsertMessage"] = "Records Not Saved Updated.";
                    }

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var ProductList = pdal.GetProductsbyid(id).FirstOrDefault();
                return View(ProductList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product p)
        {
            bool Isinserted = false;
            try
            {


                if (ModelState.IsValid)
                {
                    Isinserted = pdal.UpdateDataProductmast(p);
                    if (Isinserted)
                    {
                        TempData["InsertMessage"] = "Record Updated SuccessFully.";
                    }
                    else
                    {
                        TempData["InsertMessage"] = "Record Not Updated";
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["InsertMessage"] = ex.Message;
                return View();
            }
        }


        // POST: Product/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                var ProductList = pdal.DeleteDataProductmast(id);
                TempData["InsertMessage"] = "Record Deleted SuccessFully.";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
