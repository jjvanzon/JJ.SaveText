﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JJ.Framework.Xml.Tests.Mocks
{
    internal class Element_WithChildElement_WithExplicitAnnotation
    {
        [XmlElement]
        public int Element_WithExplicitAnnotation { get; set; }
    }
}
