using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services
{
    internal sealed class StorageService
    {
        private static int currentPageNumber = 0;

        public static int currNum
        {
            get
            {
                return currentPageNumber;
            }
        }

        public static void setCurrentPage(int p)
        {
            currentPageNumber = p;
        }
    }
}
