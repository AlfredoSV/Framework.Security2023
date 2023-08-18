
using Framework.Security2023.Entities;
using System;
using System.Data.SqlClient;

namespace Framework.Security2023.Repositories
{
    public class RepositoryRole
    {
        private SqlConnection _sqlConnection;
        private SqlDataReader _sqlDataReader;
        private SqlCommand _sqlCommand;
        private readonly string _sqlTextConnection;

        public RepositoryRole(){
            this._sqlTextConnection = SlqConnectionStr.Instance.SqlConnectionString;
        }

        public Role GetRol(Guid userId)
        {
            string sql = "SELECT ro.Id, ro.RolName, ro.Active from Rol ro inner join Users us on ro.Id = us.RolId where us.Id = @userId";

            Role role = null;
            this._sqlCommand = new SqlCommand();
            using (this._sqlConnection = new SqlConnection(this._sqlTextConnection))
            {
                this._sqlCommand = new SqlCommand(sql,this._sqlConnection);
                this._sqlConnection.Open();
                this._sqlCommand.Parameters.AddWithValue("userId", userId);
                this._sqlDataReader = this._sqlCommand.ExecuteReader();

                if (this._sqlDataReader.HasRows)
                {
                    this._sqlDataReader.Read();
                    role = Role.Create(
                        this._sqlDataReader.GetGuid(0),
                        this._sqlDataReader.GetString(1));
                }

            }

            return role;
        }
    }
}
