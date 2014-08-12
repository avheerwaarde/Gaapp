using System.Linq;

namespace Gaapp.Repository
{
    public class GaapRepository
    {
        private static string _ConnectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ConnectionString))
                {
                    throw new GaappException("ConnectionString is not set");
                }
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        public static void CreateSqlStatements(GaappSqlInfo sqlInfo)
        {
            if (string.IsNullOrWhiteSpace(sqlInfo.SqlSelect) == false)
            {
                return;
            }
            sqlInfo.SqlSelect = "SELECT * FROM " + sqlInfo.SqlTableName + " WHERE Id = @Id AND IsDeleted = 0";
            sqlInfo.SqlDelete = "UPDATE " + sqlInfo.SqlTableName + " SET IsDeleted = 1, Version=@Version+1 WHERE Id = @Id AND Version = @Version AND IsDeleted=0";
            var updateFields = sqlInfo.SqlProperties.Select(p => ", [" + p + "]=@" + p);
            sqlInfo.SqlUpdate = "UPDATE " + sqlInfo.SqlTableName + " SET Version=@Version+1, Name=@Name " + string.Join("", updateFields) + " WHERE Id = @Id AND Version = @Version AND IsDeleted=0";
            var insertFields = sqlInfo.SqlProperties.Select(p => ",[" + p + "]");
            var insertValues = sqlInfo.SqlProperties.Select(p => ",@" + p);
            sqlInfo.SqlInsert = "INSERT INTO " + sqlInfo.SqlTableName + "([Version],[Name]" + string.Join("", insertFields) + ") VALUES( @Version, @Name" + string.Join("", insertValues) + "); SELECT CAST(SCOPE_IDENTITY() AS int)";
        }
    }
}
