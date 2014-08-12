using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Gaapp.Models;

namespace Gaapp.Repository
{
    public class TestRepository 
    {
        private static readonly GaappSqlInfo _GaappSqlInfo = new GaappSqlInfo
        {
            SqlTableName = "TestBase",
            SqlProperties = new List<string> {"Test1", "Test2"}
        };

        static TestRepository() 
        {
            GaapRepository.CreateSqlStatements(_GaappSqlInfo);    
        }

        public static TestEntity GetEntity(int id)
        {
            try
            {
                using (var connection = new SqlConnection(GaapRepository.ConnectionString))
                {
                    connection.Open();
                    return connection.Query<TestEntity>(_GaappSqlInfo.SqlSelect, new { Id = id }).Single();
                }
            }
            catch (InvalidOperationException)
            {
                throw new GaappDoesNotExistException("Instance of Test with Id:" + id + " does not exist");
            }
        }

        public static int SaveEntity( TestEntity entity )
        {
            using (var connection = new SqlConnection(GaapRepository.ConnectionString))
            {
                connection.Open();
                if (entity.Id == 0)
                {
                    entity.Version = 1;
                    entity.Id = connection.Query<int>(_GaappSqlInfo.SqlInsert, entity).Single();
                }
                else
                {
                    var result = connection.Execute(_GaappSqlInfo.SqlUpdate, entity);
                    if (result == 0)
                    {
                        throw new GaappDoesNotExistException("Instance of Test with Id:" + entity.Id + " does not exist");
                    }
                    entity.Version += 1;
                }
                return entity.Id;
            }
        }

        public static void DeleteEntity( TestEntity entity )
        {
            using (var connection = new SqlConnection(GaapRepository.ConnectionString))
            {
                connection.Open();
                var result = connection.Execute(_GaappSqlInfo.SqlDelete, entity);
                if (result == 0)
                {
                    throw new GaappDoesNotExistException("Instance of Test with Id:" + entity.Id + " does not exist");
                }
            }
        }

    }

}
