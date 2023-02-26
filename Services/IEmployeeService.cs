using TaskManagerApi.Models;
namespace TaskManagerApi.Services;
public interface IEmployeeService
    {
        
        List<Employee> GetEmployeesList();

        Employee GetEmployeeDetailsById(int empId);

        ResponseModel SaveEmployee(Employee employeeModel);
        ResponseModel DeleteEmployee(int employeeId);
    }