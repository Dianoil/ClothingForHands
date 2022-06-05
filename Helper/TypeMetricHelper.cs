using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingForHands.Helper
{
    class TypeMetricHelper : ModelHelper
    {
        private MsgBoxHelper msgBoxHelper = new MsgBoxHelper();
        public List<Model.UnitType> GetTypeMetric()
        {
            List<Model.UnitType> lstMetricMaterial;
            try
            {
                lstMetricMaterial = context.UnitType.ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
                lstMetricMaterial = new List<Model.UnitType>();
            }
            catch (Exception ex)
            {
                msgBoxHelper.Error(ex);
                lstMetricMaterial = new List<Model.UnitType>();
            }
            return lstMetricMaterial;
        }
    }
}
