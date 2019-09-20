using System;
using System.Collections.Generic;
using System.Text;

namespace InjectedLibrary
{
    public class InjectedLibraryOptions
    {
        public string Name { get; set; }

        public InjectedLibraryOptions()
        {
            Name = "DefaultName";
        }
    }
}
