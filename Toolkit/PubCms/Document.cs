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
            new VTemplate.Engine.TemplateDocument(templateContent);
        }

        public Document(string fileName, Encoding encod)
            : base(fileName, encod)
        {
            new VTemplate.Engine.TemplateDocument(fileName, encod);
        }
    }
}
