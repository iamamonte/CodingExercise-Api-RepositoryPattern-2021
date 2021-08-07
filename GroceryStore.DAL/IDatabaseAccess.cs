using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.DAL
{
    public interface IDatabaseAccess
    {
        string Init();
        void Write(string rawData);
    }
}
