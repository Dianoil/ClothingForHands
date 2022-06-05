using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ClothingForHands.Model
{
    public partial class ClotherForHandsEntities : DbContext
    {
        public static ClotherForHandsEntities Context { get; set; } = new ClotherForHandsEntities();

        
    }
}
