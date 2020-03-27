using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CoronavirusTestWebsite
{
    public static class PageHelper
    {
        public static DataTable ToDataTable<T>(List<T> ourDataSet)
        // converts a generic list of like-typed objects to a data table that can be displayed
        {
            PropertyDescriptorCollection PropertyDescriptors =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            for (int i = 0; i < PropertyDescriptors.Count; i++)
            {
                PropertyDescriptor prop = PropertyDescriptors[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[PropertyDescriptors.Count];
            ourDataSet.ForEach(item =>
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = PropertyDescriptors[i].GetValue(item);
                }
                table.Rows.Add(values);
            });

            return table;
        }
    }
}
