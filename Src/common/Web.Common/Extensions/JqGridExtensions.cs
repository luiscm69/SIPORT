using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using Trirand.Web.Mvc;

namespace JqGridComponent.Extensions
{


    public static class JqGridExtensions
    {
        public static JsonResult DataBind(this JQGrid jqGrid, object dataSource, int total, Hashtable userData = null)
        {
            jqGrid.DataSource = dataSource;
            return FilterDataSource(jqGrid, total, HttpContext.Current.Request.QueryString, userData);
        }

        private static JsonResult FilterDataSource(JQGrid jqGrid, int total, NameValueCollection queryString, Hashtable userData = null)
        {
            //Definimos una variable que indica que se tiene el orden definido desde la grilla.  
            bool OrderGrid = true;
            IQueryable iqueryable = (jqGrid.DataSource as IQueryable);

            Guard.IsNotNull(iqueryable, "DataSource", "debe implementar la interfase IQueryable.");
            int num = Convert.ToInt32(queryString["page"]);
            int num2 = Convert.ToInt32(queryString["rows"]);
            string text = queryString["sidx"];
            string str = queryString["sord"];
            jqGrid.PagerSettings.CurrentPage = num;
            jqGrid.PagerSettings.PageSize = num2;

            int num3 = total;
            int totalPagesCount = (int)Math.Ceiling((float)num3 / (float)num2);
            if (string.IsNullOrEmpty(text))  //Esto indica que no se ha especificado un ordenamiento asc o desc desde la grilla.
            {
                if (jqGrid.Columns.Count == 0)
                {
                    throw new Exception("JQGrid debe tener al menos una columna definida.");
                }
                OrderGrid = false;  //Seteamos a false la variable para agarrar el ordenamiento inicial que viene desde la BD
                text = Util.GetPrimaryKeyField(jqGrid);
                str = "asc";
            }
            if (!string.IsNullOrEmpty(text))
            {
                string text5 = "";
                /*
                if (jqGrid.GroupSettings.GroupFields.Count > 0)
                {
                    string str2 = text.Split(new[] { ' ' })[0];
                    string str3 = text.Split(new[] { ' ' })[1].Split(new[] { ',' })[0];
                    text = text.Split(new[] { ',' })[1];
                    text5 = str2 + " " + str3;
                }*/
                if (text != null && text == " ")
                {
                    text = "";
                }
                if (!string.IsNullOrEmpty(text))
                {
                    if (jqGrid.GroupSettings.GroupFields.Count > 0 && !text5.EndsWith(","))
                    {
                        text5 += ",";
                    }
                    text5 = text5 + text + " " + str;
                }
                if (jqGrid.GroupSettings.GroupFields.Count > 0)
                {
                    text5 = "";
                    foreach (var item in jqGrid.GroupSettings.GroupFields)
                    {
                        text5 = text5 + "," + item.DataField + " " + item.GroupSortDirection.ToString();
                    }
                    text5 = text5.Substring(1);
                    //OrderGrid = false;
                }
                //Aplicamos el ordenamiento sólo cuando lo definimos desde la grilla si no se muestra tal como viene desde la BD
                if (OrderGrid == true)
                    iqueryable = iqueryable.OrderBy(text5, new object[0]);
            }
            DataTable dataTable = iqueryable.ToDataTable(jqGrid);
            Hashtable tempHash = Util.GetFooterInfo(jqGrid);
            if (userData != null)
            {
                foreach (DictionaryEntry elemento in userData)
                {
                    if (!tempHash.Contains(elemento.Key))
                        tempHash.Add(elemento.Key, elemento.Value);
                    else
                        tempHash[elemento.Key] = elemento.Value;
                }
            }
            JsonResponse response2 = new JsonResponse(num, totalPagesCount, num3, num2, dataTable.Rows.Count, tempHash);
            return Util.ConvertToJson(response2, jqGrid, dataTable);
        }


    }

    public static class IEnumerableExtensions
    {
        internal static DataTable ToDataTable(this IEnumerable en, JQGrid grid)
        {
            DataTable result = new DataTable();
            DataView dataView = en as DataView;
            if (dataView != null)
            {
                result = dataView.ToTable();
            }
            else
            {
                if (en != null)
                {
                    result = ObtainDataTableFromIEnumerable(en, grid);
                }
            }
            return result;
        }

        private static DataTable ObtainDataTableFromIEnumerable(IEnumerable ien, JQGrid grid)
        {
            DataTable dataTable = new DataTable();
            foreach (object current in ien)
            {
                if (current is DbDataRecord)
                {
                    DbDataRecord dbDataRecord = current as DbDataRecord;
                    if (dataTable.Columns.Count == 0)
                    {
                        foreach (JQGridColumn current2 in grid.Columns)
                        {
                            dataTable.Columns.Add(current2.DataField);
                        }
                    }
                    DataRow dataRow = dataTable.NewRow();
                    foreach (JQGridColumn current3 in grid.Columns)
                    {
                        dataRow[current3.DataField] = dbDataRecord[current3.DataField];
                    }
                    dataTable.Rows.Add(dataRow);
                }
                else
                {
                    if (current is DataRow)
                    {
                        DataRow dataRow2 = current as DataRow;
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (JQGridColumn current4 in grid.Columns)
                            {
                                dataTable.Columns.Add(current4.DataField);
                            }
                        }
                        DataRow dataRow3 = dataTable.NewRow();
                        foreach (JQGridColumn current5 in grid.Columns)
                        {
                            dataRow3[current5.DataField] = dataRow2[current5.DataField];
                        }
                        dataTable.Rows.Add(dataRow3);
                    }
                    else
                    {
                        Type type = current.GetType();
                        PropertyInfo[] properties = type.GetProperties();
                        if (dataTable.Columns.Count == 0)
                        {
                            PropertyInfo[] array = properties;
                            for (int i = 0; i < array.Length; i++)
                            {
                                PropertyInfo propertyInfo = array[i];
                                Type type2 = propertyInfo.PropertyType;
                                if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    type2 = Nullable.GetUnderlyingType(type2);
                                }
                                dataTable.Columns.Add(propertyInfo.Name, type2);
                            }
                        }
                        DataRow dataRow4 = dataTable.NewRow();
                        PropertyInfo[] array2 = properties;
                        for (int j = 0; j < array2.Length; j++)
                        {
                            PropertyInfo propertyInfo2 = array2[j];
                            object value = propertyInfo2.GetValue(current, null);
                            if (value != null)
                            {
                                dataRow4[propertyInfo2.Name] = value;
                            }
                            else
                            {
                                dataRow4[propertyInfo2.Name] = DBNull.Value;
                            }
                        }
                        dataTable.Rows.Add(dataRow4);
                    }
                }
            }
            return dataTable;
        }
    }

    public static class JQGridColumnExtensions
    {
        public static string FormatDataValue(this JQGridColumn jqGridColumn, object dataValue, bool encode)
        {
            if (IsNull(dataValue))
            {
                return jqGridColumn.NullDisplayText;
            }
            string text = dataValue.ToString();
            string dataFormatString = jqGridColumn.DataFormatString;
            int length = text.Length;
            if (!jqGridColumn.HtmlEncodeFormatString)
            {
                if (length > 0 && encode)
                {
                    text = HttpUtility.HtmlEncode(text);
                }
                if (length == 0 && jqGridColumn.ConvertEmptyStringToNull)
                {
                    return jqGridColumn.NullDisplayText;
                }
                if (dataFormatString.Length == 0)
                {
                    return text;
                }
                if (encode)
                {
                    return string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[] { text });
                }
                return string.Format(CultureInfo.CurrentCulture, dataFormatString, new[] { dataValue });
            }
            else
            {
                if (length == 0 && jqGridColumn.ConvertEmptyStringToNull)
                {
                    return jqGridColumn.NullDisplayText;
                }
                if (!string.IsNullOrEmpty(dataFormatString))
                {
                    text = string.Format(CultureInfo.CurrentCulture, dataFormatString, new[] { dataValue });
                }
                if (!string.IsNullOrEmpty(text) && encode)
                {
                    text = HttpUtility.HtmlEncode(text);
                }
                return text;
            }
        }

        internal static bool IsNull(object value)
        {
            return value == null || Convert.IsDBNull(value);
        }

        public static bool EncodeValue { get; set; }
    }


    internal class Util
    {
        internal static JsonResult ConvertToJson(JsonResponse response, JQGrid grid, DataTable dt)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (response.records == 0)
            {
                jsonResult.Data = new object[0];
            }
            else
            {
                jsonResult.Data = PrepareJsonResponse(response, grid, dt);
            }
            return jsonResult;
        }

        internal static JsonResponse PrepareJsonResponse(JsonResponse response, JQGrid grid, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] array = new string[grid.Columns.Count];
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    JQGridColumn jQGridColumn = grid.Columns[j];
                    string text = "";
                    if (!String.IsNullOrEmpty(jQGridColumn.DataField))
                    {
                        int ordinal = dt.Columns[jQGridColumn.DataField].Ordinal;
                        text = (String.IsNullOrEmpty(jQGridColumn.DataFormatString) ? dt.Rows[i].ItemArray[ordinal].ToString() : jQGridColumn.FormatDataValue(dt.Rows[i].ItemArray[ordinal], jQGridColumn.HtmlEncode));
                    }
                    array[j] = text;
                }
                string id = array[GetPrimaryKeyIndex(grid)];
                JsonRow jsonRow = new JsonRow();
                jsonRow.id = id;
                jsonRow.cell = array;
                response.rows[i] = jsonRow;
            }
            return response;
        }

        public static string GetPrimaryKeyField(JQGrid grid)
        {
            int primaryKeyIndex = GetPrimaryKeyIndex(grid);
            return grid.Columns[primaryKeyIndex].DataField;
        }

        public static int GetPrimaryKeyIndex(JQGrid grid)
        {
            foreach (JQGridColumn current in grid.Columns)
            {
                if (current.PrimaryKey)
                {
                    return grid.Columns.IndexOf(current);
                }
            }
            return 0;
        }

        public static Hashtable GetFooterInfo(JQGrid grid)
        {
            Hashtable hashtable = new Hashtable();
            if (grid.AppearanceSettings.ShowFooter)
            {
                foreach (JQGridColumn current in grid.Columns)
                {
                    hashtable[current.DataField] = current.FooterValue;
                }
            }
            return hashtable;
        }
    }


    internal class JsonResponse
    {
        public int page { get; set; }
        public int total { get; set; }
        public int records { get; set; }
        public JsonRow[] rows { get; set; }
        public Hashtable userdata { get; set; }
        public JsonResponse(int currentPage, int totalPagesCount, int totalRowCount, int pageSize, int actualRows, Hashtable userData)
        {
            this.page = currentPage;
            this.total = totalPagesCount;
            this.records = totalRowCount;
            this.rows = new JsonRow[actualRows];
            this.userdata = userData;
        }
    }

    internal class JsonRow
    {
        public string id;
        public object[] cell { get; set; }
    }
}