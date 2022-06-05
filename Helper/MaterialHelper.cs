using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClothingForHands.Helper
{
    public class MaterialHelper : ModelHelper
    {
        MsgBoxHelper msgBoxHelper = new MsgBoxHelper();
        #region Get
        public List<Model.Material> GetMaterial()
        {
            List<Model.Material> lstMaterial;
            try
            {
                lstMaterial = context.Material.ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
                lstMaterial = new List<Model.Material>();
            }
            catch (Exception ex)
            {
                msgBoxHelper.Error(ex);
                lstMaterial = new List<Model.Material>();
            }
            return lstMaterial;
        }

        public List<Model.Material> GetMaterial(string find)
        {
            List<Model.Material> lstMaterial;
            try
            {
                lstMaterial = context.Material
                    .Where(i => i.title.Contains(find)).ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
                lstMaterial = new List<Model.Material>();
            }
            catch (Exception ex)
            {
                msgBoxHelper.Error(ex);
                lstMaterial = new List<Model.Material>();
            }
            return lstMaterial;
        }

        public List<Model.Material> GetMaterial(int typeMaterialId)
        {
            List<Model.Material> lstMaterial;
            try
            {
                lstMaterial = context.Material
                    .Where(i => i.TypeMaterial1.id == typeMaterialId).ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
                lstMaterial = new List<Model.Material>();
            }
            catch (Exception ex)
            {
                msgBoxHelper.Error(ex);
                lstMaterial = new List<Model.Material>();
            }
            return lstMaterial;
        }

        public List<Model.Material> GetMaterial(string find, int typeMaterialId)
        {
            List<Model.Material> lstMaterial;
            try
            {
                lstMaterial = context.Material
                    .Where(i => (i.title.Contains(find)) &&
                    i.TypeMaterial1.id == typeMaterialId).ToList();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                msgBoxHelper.Error("Ошибка подключения к базе данных");
                lstMaterial = new List<Model.Material>();
            }
            catch (Exception ex)
            {
                msgBoxHelper.Error(ex);
                lstMaterial = new List<Model.Material>();
            }
            return lstMaterial;
        }

        public List<Model.Supply> GetSuppliers(object material)
        {
            List<Model.Supply> lstSupplier = new List<Model.Supply>();
            try
            {
                var buff = ((Model.Material)material).MaterialSupply;
                foreach (var item in buff)
                {
                    lstSupplier.Add(item.Supply);

                }
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
        #endregion


        #region Sort
        public void SortPriceAsc(List<Model.Material> materials)
        {
            materials.Sort((x, y) => x.cost.CompareTo(y.cost));
        }

        public void SortPriceDesc(List<Model.Material> materials)
        {
            materials.Sort((x, y) => y.cost.CompareTo(x.cost));
        }

        public void SortNameAsc(List<Model.Material> materials)
        {
            materials.Sort((x, y) => x.title.CompareTo(y.title));
        }

        public void SortNameDesc(List<Model.Material> materials)
        {
            materials.Sort((x, y) => y.title.CompareTo(x.title));
        }

        public void SortCountAsc(List<Model.Material> materials)
        {
            materials.Sort((x, y) => x.quantityInPack.CompareTo(y.quantityInPack));
        }

        public void SortCountDesc(List<Model.Material> materials)
        {
            materials.Sort((x, y) => y.quantityInPack.CompareTo(x.quantityInPack));
        }

        #endregion

        #region MInNumber
        public int MaxMinNumber(IList materials)
        {
            var buff = new List<Model.Material>();
            foreach (Model.Material item in materials)
            {
                buff.Add(item);
            }
            if (buff.Count > 0)
                return buff.Max(i => i.minCost);
            return 0;
        }
        public bool ChangeMinNumber(IList materials, int minNumber)
        {
            var buff = new List<Model.Material>();
            foreach (Model.Material item in materials)
            {
                item.minCost = minNumber;
            }
            return Save();
        }

        #endregion

        public List<Model.Material> Listing(List<Model.Material> materials, int startNumber, int count = 15)
        {
            List<Model.Material> buff = new List<Model.Material>();
            for (int i = 0; startNumber < materials.Count; startNumber++)
            {

                if (i >= count)
                    break;

                buff.Add(materials[startNumber]);
                i++;
            }
            return buff;
        }

        public bool DeleteMaterial(Model.Material material)
        {
            context.Material.Remove(material);
            return Save();
        }

        public bool DeleteMaterial(object material)
        {
            context.Material.Remove((Model.Material)material);
            return Save();
        }


        //public string NewImage(OpenFileDialog openFileDialog)
        //{
        //    //changesMaterial.image = File.ReadAllBytes(openFileDialog.FileName);
        //    ////string path = Assembly.GetExecutingAssembly().Location + @"materials\" + System.IO.Path.GetFileName(openFileDialog.FileName);
        //    ////File.Copy(openFileDialog.FileName, @"123");
        //    ////changesMaterial.image = path;
        //    //return openFileDialog.FileName;

        //    //Сделать имаге
        //}

        public bool SaveMaterial(object material)
        {
            if (material is Model.Material currentMaterial)
            {
                if (currentMaterial.id == 0)
                    context.Material.Add(currentMaterial);
            }
            return Save();
        }

        public bool SaveMaterial(object material, IEnumerable supplier)
        {
            if (material is Model.Material curentMaterial && supplier is List<Model.Supply> currentSupplier)
            {
                context.MaterialSupply.RemoveRange(curentMaterial.MaterialSupply);
                foreach (var item in currentSupplier)
                {
                    Model.Supply buffSupplier = context.Supply.Where(i => i.id == item.id).FirstOrDefault();
                    if (curentMaterial.MaterialSupply.Where(i => i.idSupply == item.id).Count() > 1)
                    {
                        msgBoxHelper.Error("Есть дублирующиеся возможные поставщики.");
                        return false;
                    }
                    curentMaterial.MaterialSupply.Add(new Model.MaterialSupply()
                    {
                        idMaterial = curentMaterial.id,
                        Material = curentMaterial,
                        idSupply = item.id,
                    });
                }
                return SaveMaterial(material);
            }
            return false;
        }
        public List<Model.Material> Materials;

        private Model.Material changesMaterial = new Model.Material();
        public object ChangesMaterial
        {
            get
            {
                return changesMaterial;
            }
            set
            {
                changesMaterial = (Model.Material)value;
            }
        }



    }
}
