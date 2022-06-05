using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingForHands.Model
{
    public partial class Material
    {
        public string Supplier
        {
            get
            {
                string result = String.Empty;
                foreach (var item in MaterialSupply)
                {
                    if (result != String.Empty)
                        result += ", ";
                    result += item.Supply.title;
                }
                if (result == String.Empty)
                    result = "Поставщиков нет";
                return result;
            }
        }

        public string ColorType
        {
            get
            {
                if (cost < minCost)
                    return "#f19292";

                else if (cost > minCost * 3)
                    return "#ffba01";

                else
                    return "White";


            }
        }

    /*    public string MinimalPrice
        {
            get
            {

                double result = 0;

                if (minCost > cost)
                {
                    var buff = Math.Ceiling((minCost - cost) / (double)quantityInPack);
                    result = buff * (double)cost;
                }

                return result == 0 ? "закупка не требуется" : result.ToString();
            }
        } */
    }
}
