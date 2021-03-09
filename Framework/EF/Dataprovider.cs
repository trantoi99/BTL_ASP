using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.EF
{
   public class Dataprovider
    {
        private static BTL_Library bTL_Library = null;
        public static BTL_Library _bTL_Library
        {
            get
            {
                if(_bTL_Library == null)
                {
                    bTL_Library = new BTL_Library();
                }
                return _bTL_Library;
            }
            
        }
    }
}
