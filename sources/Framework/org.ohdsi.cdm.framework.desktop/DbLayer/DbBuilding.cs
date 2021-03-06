﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using org.ohdsi.cdm.framework.desktop.Helpers;

namespace org.ohdsi.cdm.framework.desktop.DbLayer
{
   public class DbBuilding
   {
      private readonly string _connectionString;

      public DbBuilding(string connectionString)
      {
         _connectionString = connectionString;
      }

      public void SetFieldToNowDate(int buildingId, string fieldName)
      {
         using (var conn = SqlConnectionHelper.OpenMssqlConnection(_connectionString))
         {
            var query = string.Format("UPDATE [dbo].[Building] SET {1} = '{2}' WHERE [Id] = {0}", buildingId, fieldName, DateTime.Now);
            using (var command = new SqlCommand(query, conn))
            {
               command.ExecuteNonQuery();
            }
         }
      }

      public void SetFieldTo(int buildingId, string fieldName, DateTime? value)
      {
         using (var conn = SqlConnectionHelper.OpenMssqlConnection(_connectionString))
         {
            var query = string.Format("UPDATE [dbo].[Building] SET {1} = @time WHERE [Id] = {0}", buildingId, fieldName);
            using (var command = new SqlCommand(query, conn))
            {
               command.Parameters.Add("@time", SqlDbType.DateTime);
               command.Parameters["@time"].Value = value ?? (object)DBNull.Value;
               command.ExecuteNonQuery();
            }
         }
      }

      public IEnumerable<IDataReader> Load(int buildingId)
      {
         const string query = "SELECT * FROM [dbo].[Building] where Id = {0}";

         using (var connection = SqlConnectionHelper.OpenMssqlConnection(_connectionString))
         {
            using (var cmd = new SqlCommand(string.Format(query, buildingId), connection))
            {
               cmd.CommandTimeout = 30000;
               using (var reader = cmd.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     yield return reader;
                  }
               }
            }
         }
      }

      public void Create(int buildingId)
      {
         const string query = "INSERT INTO [dbo].[Building] ([Id]) VALUES ({0})";

         using (var connection = SqlConnectionHelper.OpenMssqlConnection(_connectionString))
         {
            using (var cmd = new SqlCommand(string.Format(query, buildingId), connection))
            {
               cmd.CommandTimeout = 30000;
               cmd.ExecuteScalar();
            }
         }
      }
   }
}
