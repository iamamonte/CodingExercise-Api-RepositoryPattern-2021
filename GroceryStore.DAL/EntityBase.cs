using GroceryStore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.DAL
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
    }
}
