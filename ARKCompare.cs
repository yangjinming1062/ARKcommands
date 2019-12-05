using System.Collections.Generic;

namespace ARKcommands
{
    class ARKCompare : IComparer<ARKCommand>
    {
        public int Compare(ARKCommand x, ARKCommand y) => string.Compare(x.Name, y.Name);
    }
}
