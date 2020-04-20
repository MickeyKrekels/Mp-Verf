using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Entities
{
    public class ParadoxModalBase
    {
        public OleDbConnection connection;
        public DataSet connectionResult;
        //public int Id { get; set; }
    }
}
