using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class ToDoModel
    {
        public int Id { get; set; }
        public string Task { get;set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Task}" +
                $" \n Start Date: {StartDate}" +
                $" \n End Date: {EndDate}";
        }
    }
}
