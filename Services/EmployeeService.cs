using TaskManagerApi.Models;

namespace TaskManagerApi.Services;
public class EmployeeService:IEmployeeService
{
    private TaskManagerContext _context;
    public EmployeeService(TaskManagerContext context) {
        _context = context;
    }

    public ResponseModel DeleteEmployee(int employeeId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Employee _temp = GetEmployeeDetailsById(employeeId);
            if (_temp != null) {
                _context.Remove < Employee > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Employee Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Employee Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public Employee GetEmployeeDetailsById(int empId)
    {
        Employee emp;
        try {
            emp = _context.Find < Employee > (empId);
        } catch (Exception) {
            throw;
        }
        return emp;
    }

    public List<Employee> GetEmployeesList()
    { 
         List < Employee > empList;
        try {
            empList = _context.Set < Employee > ().ToList();
            Console.WriteLine(empList);
        } catch (Exception) {
            throw;
        }
        return empList;
    }

    public ResponseModel SaveEmployee(Employee employeeModel)
    {
        ResponseModel model = new ResponseModel();
      
                _context.Add < Employee > (employeeModel);
                model.Messsage = "Employee Inserted Successfully";
           // }
            _context.SaveChanges();
            model.IsSuccess = true;
        
        return model;
    }
}