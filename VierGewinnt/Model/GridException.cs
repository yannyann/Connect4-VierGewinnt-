using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class GridException : Exception
    {
        public GridException()
            : base()
        {

        }

        public GridException(String message)
            : base(message)
        {

        }
    }

    public class ColumnOutOfGridException : GridException
    {
        public ColumnOutOfGridException()
            : base()
        {

        }

        public ColumnOutOfGridException(String message)
            : base(message)
        {

        }
    }

    public class FullColumnGridException : GridException
    {
        public FullColumnGridException()
            : base()
        {

        }

        public FullColumnGridException(String message)
            : base(message)
        {

        }
    }
}
