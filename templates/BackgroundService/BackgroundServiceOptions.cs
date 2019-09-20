using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundService
{
    public class BackgroundServiceOptions
    {
        public BackgroundServiceOptions()
        {
            Name = "Default Name";
        }

        public string Name { get; set; }
    }
}
