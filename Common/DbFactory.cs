using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.DataBase;
using QSmart.Core.Object;
using System.Data;
using System.Reflection;

namespace Common
{
    public class DbFactory
    {

        public static DbSession CreateDbSession() { return new DbSessionInstance(); }
    }
  
}
