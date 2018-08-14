using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander.Models
{
    public class SearchItemsVM
    {
        public string Repository { get; set; }
        public string SourceFile { get; set; }
        public int Line { get; set; }
        public string Content { get; set; }
    }
}
