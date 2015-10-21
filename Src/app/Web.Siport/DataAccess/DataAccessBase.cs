using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.Siport.DataAccess
{
    public static class DataAccessBase
    {

        private static Exception Unwrap(Exception ex)
        {
            while (null != ex.InnerException)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

        public static  void SetLogError(Exception e)
        {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Error(e.Message, e);
            e = Unwrap(e);
            log.Error(e.Message, e);
        }
    }
}