using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Woodz4U.Models;

namespace Woodz4U.Controllers
{
    public class SummaryController : Controller
    {
        /// <summary>
        /// Get list of all summary reports
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var list = SummariesStorage.Summaries;

            return View(list);
        }

        /// <summary>
        /// Action to get view for creating summary report.
        /// For list of items, use partial view from ItemsController.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create summary report
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(SummaryModel model)
        {
            // Store summary report
            SummariesStorage.Summaries.Add(model);
            // Return to list of all summaries. Maybe redirect to summary report details? 
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get details for specific summary report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(long id)
        {
            var model = SummariesStorage.Summaries.FirstOrDefault(m => m.ID == id);

            return View(model);
        }

        /// <summary>
        /// Save summary report to file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IActionResult Save(long id, string filename = "")
        {
            var model = SummariesStorage.Summaries.FirstOrDefault(m => m.ID == id);

            if (string.IsNullOrEmpty(filename))
            {
                filename = $"{model.CustomerName}_{model.DueDate}_{model.ID}";
            }

            string json = JsonConvert.SerializeObject(model);

            System.IO.File.WriteAllText(@$"{filename}.json", json);

            model.FileName = filename;

            return View();
        }

        /// <summary>
        /// Confirm summary.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Confirm(long id)
        {
            // Get warehouse state
            ItemModel result = new ItemModel();
            using (StreamReader r = new StreamReader("warehouse.json"))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<ItemModel>(json);
            }

            var model = SummariesStorage.Summaries.FirstOrDefault(m => m.ID == id);

            // Check for each item in summary if quantity is bigger than ammount in warehouse. 
            model.Items.ForEach(m =>
            {
                var itemInWarehouse = result.Item.FirstOrDefault(x => x.Name == m.Item.Name);

                if (itemInWarehouse.Quantity < m.Item.Quantity)
                {
                    ModelState.AddModelError("ItemQuantity", $"There is not enough {m.Item.Name} in warehouse!");
                }
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.ConfirmedDate = DateTime.Now;

            // Update warehouse.json
            foreach(var item in model.Items)
            {
                result.Item.FirstOrDefault(m => m.Name == item.Item.Name).Quantity -= item.Item.Quantity;
            }

            string warehouseJson = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(@$"warehouse.json", warehouseJson);

            return View();
        }
    }
}