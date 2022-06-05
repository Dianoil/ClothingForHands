using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingForHands.Model;

namespace ClothingForHands.Helper
{
    public class ModelHelper
    {
        private MsgBoxHelper msgBoxHelper = new MsgBoxHelper();
        protected static ClotherForHandsEntities context = new ClotherForHandsEntities();

        public bool Save()
        {
            try
            {
                context.SaveChanges();
                msgBoxHelper.Information("Информация сохранена");
                return true;
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
            }
            catch (Exception ex)
            {
                msgBoxHelper.Error(ex);
            }
            return false;
        }

        public bool RollBack()
        {
            try
            {
                context.Dispose();
                context = new ClotherForHandsEntities();
                return true;
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
            }
            catch (Exception ex)
            {
                msgBoxHelper.Error(ex);
            }
            return false;
        }
    }
}
