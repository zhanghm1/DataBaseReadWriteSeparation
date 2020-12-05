using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseChoose
{
    public interface IDataBaseConnectionFactory
    {
        public DatabaseChooseOptions options { get; set; }

        public string GetConnectionString();

        public void SetDatabaseChooseType(DatabaseChooseType chooseType);
    }
}