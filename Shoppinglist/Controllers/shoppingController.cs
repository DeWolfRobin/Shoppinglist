using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shoppinglist.Models;

namespace Shoppinglist.Controllers
{
    public class shoppingController : Controller
    {
        private List<shoppingItem> items = new List<shoppingItem>();
        public shoppingController() {
            items.Add(new shoppingItem("eten", 5));
            items.Add(new shoppingItem("meer eten", 1));
            items.Add(new shoppingItem("meeste eten", 9));
        }
        public ViewResult Index() {
            if (TempData.Peek("items") != null) {
                items = JsonConvert.DeserializeObject<List<shoppingItem>>(TempData["items"].ToString());
            }
            return View(items);
        }
        [HttpGet]
        public ViewResult Create() {
            return View(new shoppingItem {  });
        }

        [HttpGet]
        public ViewResult Edit(int id) {
            int i = 0;
            shoppingItem toedit = new shoppingItem();
            foreach (shoppingItem item in items) {
                if (i == id) {
                    toedit = item;
                    break;
                }
                i++;
            }
            return View(toedit);
        }

        [HttpPost]
        public IActionResult Edit(int id, shoppingItem item) {
            if (ModelState.IsValid) {
                items[id] = item;
                TempData["items"] = JsonConvert.SerializeObject(items);
                TempData.Keep();
                return RedirectToAction("Index");
            } else {
                return View(item);
            }
        }

        public IActionResult Delete(string naam) {
            foreach (shoppingItem item in items) {
                if (naam.Equals(item.Naam)) {
                    items.Remove(item);
                    TempData["items"] = JsonConvert.SerializeObject(items);
                    TempData.Keep();
                    break;
                }
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(shoppingItem item) {
            if (ModelState.IsValid) {
                items.Add(item);
                TempData["items"] = JsonConvert.SerializeObject(items);
                TempData.Keep();
                //return View("Finish", maaltijd);
                return RedirectToAction("Index");
            } else {
                return View(item);
            }
        }
    }
}