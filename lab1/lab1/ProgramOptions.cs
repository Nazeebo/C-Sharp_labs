using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace lab1 {
    public class Options {
        [Option('l', "length",
            Default = -1,
            Required = false,
            HelpText = "max legth of sequence diff bytes, after key comes numerical value")]
        public long Length { get; set; }

        [Option('a', "text",
            Default = false,
            Required = false,
            HelpText = "type text instead of character code if they're typable and '.' if they're not")]
        public bool Text { get; set; }

        [Option('y', "side_by_side",
            Default = false,
            Required = false,
            HelpText = "write diff in format b1|b2")]
        public bool Side_by_side { set; get; }

        [Option('q', "brief",
            Default = false,
            Required = false,
            HelpText = "write just the fact of diff or identity")]
        public bool Brief { set; get; }
    }
}
