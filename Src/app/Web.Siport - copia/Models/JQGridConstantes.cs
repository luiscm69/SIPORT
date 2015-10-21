using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace Web.Siport.Models
{
    public sealed class JQGridConstantes
    {
        public static Trirand.Web.Mvc.PagerSettings GetPagerSetting()
        {
            return new Trirand.Web.Mvc.PagerSettings { PageSize = Convert.ToInt32("100"), PageSizeOptions = "[25,50,100]" };
        }
        public static Trirand.Web.Mvc.PagerSettings GetPagerSettingTotal()
        {

            return new Trirand.Web.Mvc.PagerSettings { PageSize = 5000, PageSizeOptions = "[1000]" };

        }
        public static Trirand.Web.Mvc.ToolBarSettings GetToolBarSettingsTotal()
        {
            return new Trirand.Web.Mvc.ToolBarSettings
            {
                ToolBarPosition = ToolBarPosition.Hidden,
                ShowRefreshButton = false
            };
        }
        public static Trirand.Web.Mvc.ToolBarSettings GetToolBarSettingsBusquedaModal()
        {
            return new Trirand.Web.Mvc.ToolBarSettings
            {
                ToolBarPosition = ToolBarPosition.Bottom,
                ShowRefreshButton = true
            };
        }
        public static Trirand.Web.Mvc.PagerSettings GetPagerSettingBusquedaModal()
        {
            return new Trirand.Web.Mvc.PagerSettings { PageSize = 15, PageSizeOptions = "[15, 20, 25]" };
        }
        public static Trirand.Web.Mvc.NumberFormatter GetNumberFormatterEstandar()
        {
            return new NumberFormatter { DecimalSeparator = ".", ThousandsSeparator = ",", DecimalPlaces = 2 };
        }

        public static string GetFormatDate()
        {
            return "{0:yyyy/MM/dd}";
        }

    }
}