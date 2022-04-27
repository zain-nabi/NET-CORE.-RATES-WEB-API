﻿using System;
using System.Threading.Tasks;
using Triton.Model.LeaveManagement.Custom;
using Triton.Model.LeaveManagement.Tables;
using Triton.Service.Utils;

namespace Triton.Service.Data
{
    public static class EmployeeService
    {
        /// <summary>
        /// Returns <see cref="Employees"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentEmployeeCode"></param>
        /// <returns>Employees</returns>
        public static  async Task<Employees> GetEmployee(string currentEmployeeCode)
        {
            return await RestApiHelper.GetAsync<Employees>(new Uri(UrlHelper.Api.TritonApi, $"{UrlHelper.Controller.Employee}{currentEmployeeCode}"));
        }


        /// <summary>
        /// Returns employee by CostcentreID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="costCentreId"></param>
        /// <returns>EmployeeUserMapModel</returns>
        public static  async Task<EmployeeUserMapModel>GetBranchManager(int costCentreId)
        {
            return await RestApiHelper.GetAsync<EmployeeUserMapModel>(new Uri(UrlHelper.Api.TritonApi, $"{UrlHelper.Controller.Employee}{costCentreId}"));
        }

        /// <summary>
        /// Returns employee by TritonSecurityUserID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tritonSecurityUserId"></param>
        /// <returns></returns>
        public static  async Task<Employees>GetEmployeeByOldUserId(int tritonSecurityUserId)
        {
            return await RestApiHelper.GetAsync<Employees>(new Uri(UrlHelper.Api.TritonApi, $"{UrlHelper.Controller.Employee}{tritonSecurityUserId}"));
        }

        public static async Task<bool> IsEmployeeCodeExists(string employeeCode)
        {
            return await RestApiHelper.GetAsync<bool>(new Uri(UrlHelper.Api.RecruitmentApi, $"Employees/Employees?employeeCode={employeeCode}"));
        }

        /// <summary>
        /// Get Employee details and other objects
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public static async Task<EmployeesModels> GetEmployeeModel(int employeeId)
        {
            return await RestApiHelper.GetAsync<EmployeesModels>(new Uri(UrlHelper.Api.RecruitmentApi, $"Employees/GetEmployeeInfo?employeeId={employeeId}"));
        }

        /// <summary>
        /// Update an Employees information
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static async Task<bool> Put(Employees employee)
        {
            return await RestApiHelper.PutAsync(new Uri(UrlHelper.Api.RecruitmentApi, "Employees/UpdateAsync"), employee);
        }
    }
}
