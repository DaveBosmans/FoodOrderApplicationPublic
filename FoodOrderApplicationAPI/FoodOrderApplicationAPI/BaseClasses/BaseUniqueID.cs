using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApplicationAPI.BaseClasses
{

    //Not used anywhere, but other classes will derrive from this class.

    //TODO: How to make this class impossible to create objects.
    public class BaseUniqueID
    {
        public string UniqueString { get; set; }
    }
}
