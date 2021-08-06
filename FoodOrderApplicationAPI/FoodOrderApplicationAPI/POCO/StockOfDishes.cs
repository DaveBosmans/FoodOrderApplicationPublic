using FoodOrderApplicationAPI.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApplicationAPI.POCO
{
    public class StockOfDishes : BaseUniqueID
    {

        //As much as possible we try to match the dish name with the enum.

        public string NameOfDish { get; set; }
        public int NumberInStock { get; set; }
    }
}
