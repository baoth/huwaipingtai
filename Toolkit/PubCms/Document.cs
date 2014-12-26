using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit.PubCms
{
    public class Document : VTemplate.Engine.TemplateDocument
    {
        public Document(string templateContent)
            : base(templateContent)
        {
            //new VTemplate.Engine.TemplateDocument(templateContent);
        }

        public Document(string fileName, Encoding encod)
            : base(fileName, encod)
        {
            //new VTemplate.Engine.TemplateDocument(fileName, encod);
        }
        //public Document()
        //    : base(Guid.NewGuid().ToString(), Encoding.UTF8)
        //{
        //    //new VTemplate.Engine.TemplateDocument(Guid.NewGuid().ToString(), Encoding.UTF8);
        //}
    }
}
