using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Woodz4U.Models
{
    public class SummaryModel
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string FileName { get; set; }
        public string CustomerName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public List<SummaryItemModel> Items { get; set; }
    }
}
