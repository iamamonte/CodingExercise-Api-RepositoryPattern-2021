using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Infrastructure.DataAccess.Interface
{
    public interface IDatabaseAccess
    {
        string Init();
        void Write(string rawData);
    }
}
