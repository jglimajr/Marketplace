using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.ComponentModel.DataAnnotations;
using InteliSystem.Utils.Dapper.Extensions.Attributes;

namespace InteliSystem.Utils.Dapper.Extensions
{
    public static class DapperExtension
    {
        public static Task<IEnumerable<T>> GetAllAsync<T>(this IDbConnection conn, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var scolumns = "";
            var comma = "";
            // var swhere = "";
            var columns = GetPropertyNames(typeof(T));
            columns.ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);
                scolumns += $"{comma}{colname}";
                comma = ", ";

            });

            var sSql = $"Select {scolumns} From {GetTableName(typeof(T))}";

            return conn.QueryAsync<T>(sSql);
        }

        public static Task<T> GetAsync<T>(this IDbConnection conn, T tobjct, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var scolumns = "";
            var comma = "";
            var swhere = "";
            var columns = GetPropertyNames(typeof(T));
            columns.ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);
                scolumns += $"{comma}{colname}";
                comma = ", ";
                // if (colname.ToUpper() == "ID" || colname.ToUpper() == $"ID{GetTableName(typeof(T))}" || colname.ToUpper() == $"{GetTableName(typeof(T))}ID" || IsPropertyKey(column))
                if (IsPropertyKey(column))
                {
                    swhere = $"{colname} = @{colname}";
                }

            });

            var sSql = $"Select {scolumns} From {GetTableName(typeof(T))} Where {swhere}";

            return conn.QueryFirstOrDefaultAsync<T>(sSql, tobjct);
        }
        public static Task<int> InsertAsync<T>(this IDbConnection conn, T tobject, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var scolumns = "";
            var svalues = "";
            var comma = "";
            var columns = GetPropertyNames(typeof(T));
            columns.Where(IsInsertable).ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);
                scolumns += $"{comma}{colname}";
                svalues += $"{comma}@{colname}";
                comma = ", ";
            });

            // var connClose = (conn.State == ConnectionState.Closed);

            var sSql = @$"Insert Into {GetTableName(typeof(T))} ({scolumns}) values ({svalues})";
            // if (connClose) conn.Close();
            return conn.ExecuteAsync(sSql, tobject, transaction, commandTimeout);
        }


        public static Task<int> UpdateAsync<T>(this IDbConnection conn, T tobject, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var scolumns = "";
            var comma = "";
            var swhere = "";
            var columns = GetPropertyNames(typeof(T));
            columns.Where(IsUpdateable).ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);
                // if (colname.ToUpper() == "ID" || colname.ToUpper() == $"ID{GetTableName(typeof(T))}" || colname.ToUpper() == $"{GetTableName(typeof(T))}ID" || IsPropertyKey(column))
                if (IsPropertyKey(column))
                {
                    swhere = $"{colname} = @{colname}";
                }
                else
                {
                    scolumns += $"{comma}{colname} = @{colname}";
                    comma = ", ";
                }
            });

            var sSql = $"Update {GetTableName(typeof(T))} Set {scolumns} Where {swhere}";

            return conn.ExecuteAsync(sSql, tobject, transaction, commandTimeout);

        }

        public static Task<int> DeleteAsync<T>(this IDbConnection conn, T tobject, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var swhere = "";
            var columns = GetPropertyNames(typeof(T));
            columns.ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);
                // if (colname.ToUpper() == "ID" || colname.ToUpper() == $"ID{GetTableName(typeof(T))}" || colname.ToUpper() == $"{GetTableName(typeof(T))}ID" || IsPropertyKey(column))
                if (IsPropertyKey(column))
                {
                    swhere = $"{colname} = @{colname}";
                }
            });

            var sSql = $"Delete From {GetTableName(typeof(T))}  Where {swhere}";

            return conn.ExecuteAsync(sSql, tobject, transaction, commandTimeout);

        }

        private static IEnumerable<PropertyInfo> GetPropertyNames(Type tclass)
        {
            // var tclass = oclass.GetType();

            var propertys = tclass.GetProperties(System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                            .Where(IsWriteable);

            return propertys;
        }


        private static string GetTableName(Type tclass)
        {
            // var tclass = oclass.GetType();

            string attname = tclass.GetCustomAttribute<TableAttribute>(false)?.Name ?? (tclass.GetCustomAttributes(false).FirstOrDefault(a => a.GetType().Name == "TableAttribute") as dynamic)?.Name;

            if (attname != null)
                return attname;

            return tclass.Name;
        }

        private static string GetColumnName(PropertyInfo tcolumn)
        {

            string attname = tcolumn.GetCustomAttribute<ColumnAttribute>(false)?.Name ?? (tcolumn.GetCustomAttributes(false).FirstOrDefault(a => a.GetType().Name == "ColumnAttribute") as dynamic)?.Name;
            if (attname != null)
                return attname;

            return tcolumn.Name;
        }

        private static bool IsPropertyKey(PropertyInfo tcolumn)
        {
            var attname = tcolumn.GetCustomAttribute<KeyAttribute>(false);

            return (attname != null);
        }

        private static bool IsWriteable(PropertyInfo inf)
        {
            var attributes = inf.GetCustomAttributes(typeof(WritePropertyAttribute), false).AsList();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WritePropertyAttribute)attributes[0];
            return writeAttribute.WriteProperty;
        }

        private static bool IsUpdateable(PropertyInfo inf)
        {
            var attibutes = inf.GetCustomAttributes(typeof(UpdatePropertyAttribute), false).AsList();
            if (attibutes.Count <= 0) return true;
            var updateAttribute = (UpdatePropertyAttribute)attibutes[0];

            return updateAttribute.UpdateProperty;
        }

        private static bool IsInsertable(PropertyInfo inf)
        {
            var attibutes = inf.GetCustomAttributes(typeof(InsertPropertyAttribute), false).AsList();
            if (attibutes.Count <= 0) return true;
            var insertAttibute = (InsertPropertyAttribute)attibutes[0];

            return insertAttibute.InsertProperty;
        }
    }
}