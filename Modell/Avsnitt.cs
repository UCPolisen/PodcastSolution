﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace Modell
{
    public class Avsnitt
    {
        public string? Titel { get; set; }

        public virtual string? Beskrivning { get; set; }
    }
}
