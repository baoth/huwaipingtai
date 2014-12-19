using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QSmart.Core.Object;

namespace DataModel
{
    public class SecitonOperator : QSmartEntity
    {

      public int Id { get; set; }
      public int SectionId { get; set; }
      public string LoginName { get; set; }
      public string Password { get; set; }

    }
}
