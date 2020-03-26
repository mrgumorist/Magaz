using System;
using System.Collections.Generic;
using System.Linq;
namespace MyPrint
{
    public class ReportModel
    {
        public DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string Sum { get; set; } = "200.50";
    }
    public class OrderItem
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
       
    }
}