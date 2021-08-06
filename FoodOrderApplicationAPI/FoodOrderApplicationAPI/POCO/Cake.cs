using FoodOrderApplicationAPI.BaseClasses;
using FoodOrderApplicationAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApplicationAPI.POCO
{
    public class Cake : BaseUniqueID
    {
        string CakeTitle { get; set; }
        CakeEnums CakeEnums { get; set; }
        double Price { get; set; }
        DateTime CreationDate { get; set; }
    }
}
