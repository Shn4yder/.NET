using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_15
{
    internal class RowOfData
    { 
        public object id_ord { get; set; }
        public object service { get; set; }
        public object cost { get; set; }
        public object FIO { get; set; }
        public object sale { get; set; }
        public object phone { get; set; }

        public RowOfData() { }
        public RowOfData(object _id_ord, object _FIO, object _sale, object _cost, object _service, object _phone)
        {
            id_ord = _id_ord;
            FIO = _FIO;
            sale = _sale;
            cost = _cost;
            service = _service;
            phone = _phone;
        }

        public void DataChange(object _id_ord, object _FIO, object _sale, object _cost, object _service, object _phone)
        {
            id_ord = _id_ord;
            FIO = _FIO;
            sale = _sale;
            cost = _cost;
            service = _service;
            phone = _phone;
        }

    }
}
