﻿using Crud_Operation.CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operation.RepositoryLayer
{
    public class CrudOperationRL : ICrudOperationRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;

        public CrudOperationRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration[key: "ConnectionStrings:DBSettingConnection"]);
        }
        public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = new CreateRecordResponse();
            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                string SqlQuery = "Insert into CrudOperationTable (Username,Age) values (@UserName,@Age)";
                using (SqlCommand sqlCommand= new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 100;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Age", request.Age);
                    _sqlConnection.Open();
                    int Status= await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went Wrong";
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message =ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }
        public async Task<ReadRecordResponse> ReadRecord()
        {
            ReadRecordResponse response = new ReadRecordResponse();
            response.isSuccess = true;
            response.Message = "Successful";
                try
                {
                string SqlQuery = "Select UserName,Age from CrudOperationTable;";
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            response.readRecordData = new List<ReadRecordData>();
                            while(await sqlDataReader.ReadAsync())
                            {
                                ReadRecordData dbData = new ReadRecordData();
                                dbData.UserName=sqlDataReader[name:"UserName"] != DBNull.Value ? sqlDataReader[name:"UserName"].ToString():string.Empty;
                                dbData.Age = sqlDataReader[name: "Age"] != DBNull.Value ? Convert.ToInt32(sqlDataReader[name: "Age"]):0;
                                response.readRecordData.Add(dbData);
                            }
                        }
                    }
                }

                }
                catch (Exception ex)
                {
                    response.isSuccess = false;
                    response.Message = ex.Message;
                }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

        public  async Task<UpdateRecordResponse> UpdateRecord(UpdateRecordRequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = "Update CrudOperationTable Set UserName= @UserName,Age=@Age where Id= @Id";
                using (SqlCommand sqlCommand= new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Age", request.Age);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Id", request.Id);
                    _sqlConnection.Open();
                    int status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something went Wrong";
                    }
                }
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }
        public  async Task<DeleteRecordResponse> DeleteRecord(DeleteRecordRequest request)
        {
            DeleteRecordResponse response = new DeleteRecordResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = "Delete from CrudOperationTable where Id= @Id";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Id", request.Id);
                    _sqlConnection.Open();
                    int status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something went Wrong";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

    }
}
