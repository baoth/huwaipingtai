using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit.Ext
{
   public static class CommonFun
    {
       public static string GetNewGuid() 
       {
           return Guid.NewGuid().ToString().Replace("-", "");
       }
    }
}
