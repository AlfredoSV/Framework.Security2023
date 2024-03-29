using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
            _sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        internal async Task<IEnumerable<Permission>> GetPermission(Guid idRole)
        {

            string sql = "SELECT PermissionName, Id, RolId, PermissionDescription, Module, DateCreated, UserCreated,Active FROM Permission WHERE RolId = @rolId;";

            List<Permission> permissions = new List<Permission>();
            _sqlCommand = new SqlCommand();
            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlCommand = new SqlCommand(sql, _sqlConnection);
                _sqlConnection.Open();
                _sqlCommand.Parameters.AddWithValue("rolId", idRole);
                _sqlDataReader = await _sqlCommand.ExecuteReaderAsync();

                if (_sqlDataReader.HasRows)
                {
                    while (_sqlDataReader.Read())
                    {
                        
                        permissions.Add(Permission.Create(
                            _sqlDataReader.GetString(0),
                            _sqlDataReader.GetGuid(1),
                            _sqlDataReader.GetGuid(2),
                            _sqlDataReader.GetString(3),
                            _sqlDataReader.GetString(4), _sqlDataReader.GetDateTime(5),
                            _sqlDataReader.GetGuid(6), _sqlDataReader.GetBoolean(7))
                            );

                    }

                }

            }

            return permissions;
        }

        internal async Task<bool> InsertPermissions(IEnumerable<Permission> permission)
        {
            DataTable dataTable = ConvertToDataTable(permission);

            string procedure = @"SavePermissions";

            _sqlCommand = new SqlCommand();

            using (_sqlConnection = new SqlConnection(_sqlTextConnection))
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand(procedure, _sqlConnection);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                var param = _sqlCommand.Parameters.AddWithValue("@Params", dataTable);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "tableOf_Permissions";
                return await _sqlCommand.ExecuteNonQueryAsync() > 0;
            }
        }

        private static DataTable ConvertToDataTable<T>(IEnumerable<T> collection)
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
