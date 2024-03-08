using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01.Stream_Progress
{
    public class Picture : File
    {
        public Picture(string name, int length, int bytesSent) : base(name, length, bytesSent)
        {

        }
    }
}
