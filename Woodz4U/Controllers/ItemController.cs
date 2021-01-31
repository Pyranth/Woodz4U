using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Woodz4U.Models;

namespace Woodz4U.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index(ItemSearchViewModel searchModel)
        {
            ItemModel result = new ItemModel();
            using (StreamReader r = new StreamReader("warehouse.json"))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<ItemModel>(json);
            }

            // TODO search
            //result.Item = result.Item.Where(m => m.Name.Contains(searchModel.Name) && m.Material.Contains(searchModel.Material)).ToList();

            return PartialView(result);

            //return View(result);
        }
    }
}