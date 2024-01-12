using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace Framework.Security2023.Repositories
{
    class RepositoryPermission
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        internal RepositoryPermission()
        {
            this._sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        internal IEnumerable<Permission> GetPermission(Guid idRole)
        {

            string sql = "SELECT PermissionName, Id, RolId, PermissionDescription, Module, DateCreated, UserCreated,Active FROM Permission WHERE RolId = @rolId;";

            List<Permission> permissions = new List<Permission>();
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand(sql, this._sqlConnection);
                this._sqlConnection.Open();
                this._sqlCommand.Parameters.AddWithValue("rolId", idRole);
                this._sqlDataReader = this._sqlCommand.ExecuteReader();

                if (this._sqlDataReader.HasRows)
                {
                    while (this._sqlDataReader.Read())
                    {
                        this._sqlDataReader.Read();
                        permissions.Add(Permission.Create(
                            this._sqlDataReader.GetString(0),
                            this._sqlDataReader.GetGuid(1),
                            this._sqlDataReader.GetGuid(2),
                            this._sqlDataReader.GetString(3),
                            this._sqlDataReader.GetString(4), this._sqlDataReader.GetDateTime(5),
                            this._sqlDataReader.GetGuid(6), this._sqlDataReader.GetBoolean(7))
                            );

                    }

                }

            }

            return permissions;
        }

        internal bool InsertPermissions(List<Permission> permission)
        {
            DataTable dataTable = ConvertToDataTable<Permission>(permission);

            string procedure = @"SavePermissions";

            this._sqlCommand = new SqlCommand();

            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlConnection.Open();
                this._sqlCommand = new SqlCommand(procedure, this._sqlConnection);
                this._sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var param = this._sqlCommand.Parameters.AddWithValue("@Params", dataTable);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "tableOf_Permissions";
                return this._sqlCommand.ExecuteNonQuery() > 0;
            }
        }

        //private DataTable ConvertToDataTable(List<Permission> permission)
        //{
        //    DataTable dataTable = new DataTable();

        //    DataColumn[] dataColumns = new DataColumn[]
        //    {
        //        new DataColumn("Id"),
        //        new DataColumn("RolId"),
        //        new DataColumn("PermissionName"),
        //        new DataColumn("PermissionDescription"),
        //        new DataColumn("Module"),
        //        new DataColumn("DateCreated"),
        //        new DataColumn("UserCreated"),
        //        new DataColumn("Active")
        //    };
        //    dataTable.Columns.AddRange(dataColumns);


        //    for (int i = 0; i < permission.Count; i++)
        //    {
        //        DataRow dataRow = dataTable.NewRow();
        //        dataRow[i] = new object[]{ permission[i].Id,permission[i].RolId,permission[i].Permision,
        //            permission[i].Description, permission[i].Module, permission[i].DateCreated, permission[i].UserCreated,permission[i].Active};
        //        dataTable.Rows.Add(dataRow);
        //    }

        //    return dataTable;
        //}

        static DataTable ConvertToDataTable<T>(IEnumerable<T> collection)
        {
            DataTable dataTable = new DataTable();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var item in collection)
            {
                DataRow row = dataTable.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
