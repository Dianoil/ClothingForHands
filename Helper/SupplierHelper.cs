using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingForHands.Helper
{
    class SupplierHelper : ModelHelper
    {
        MsgBoxHelper msgBoxHelper = new MsgBoxHelper();
        public List<Model.Supply> GetSuppliers()
        {
            List<Model.Supply> lstSupplier;
            try
            {
                lstSupplier = context.Supply.ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
                lstSupplier = new List<Model.Supply>();
            }
            catch (Exception ex)
            {
                lstSupplier = new List<Model.Supply>();
                msgBoxHelper.Error(ex);
            }
            return lstSupplier;
        }

    }
}
