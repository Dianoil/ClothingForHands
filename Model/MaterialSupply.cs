//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClothingForHands.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialSupply
    {
        public int id { get; set; }
        public int idMaterial { get; set; }
        public int idSupply { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Supply Supply { get; set; }
    }
}