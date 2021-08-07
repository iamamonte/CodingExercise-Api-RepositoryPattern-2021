using GroceryStore.DAL;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Helpers
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _dbPath;
        public DatabaseAccess(IWebHostEnvironment hostingEnvironment) 
        {
            _hostingEnvironment = hostingEnvironment;
            _dbPath = $"{_hostingEnvironment.ContentRootPath}/Helpers/database.json";
        }
        
        public string Init()
        {
            return File.ReadAllText(_dbPath);
            
        }

        public void Write(string rawData)
        {
            File.WriteAllText(_dbPath, rawData);
        }
    }
}
