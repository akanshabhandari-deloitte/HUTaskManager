using TaskManagerApi.Models;
namespace TaskManagerApi.Services;
public interface IProjectService
    {
        /// <summary>
        /// get list of all employees
        /// </summary>
        /// <returns></returns>
        List<Project> GetProjectList();

        /// <summary>
        /// get employee details by employee id
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        // Employee GetEmployeeDetailsById(int eId);

        /// <summary>
        ///  add edit employee
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        ResponseModel SaveProject(int id,Project projectModel);
    }