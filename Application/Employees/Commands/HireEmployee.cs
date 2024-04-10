using Application.Employees.Responses;
using Domain;
using MediatR;

namespace Application.Employees.Commands;
public record HireEmployee(string name, string surname, DateTime date, string email, Guid departmentId) : IRequest<EmployeeDto>;
public class HireEmployeeHandler : IRequestHandler<HireEmployee, EmployeeDto>
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly IRepository<Employee> _employeeRepository;

    public HireEmployeeHandler(IRepository<Department> departmentRepository, IRepository<Employee> employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
    }

    public Task<EmployeeDto> Handle(HireEmployee request, CancellationToken cancellationToken)
    {
        Department department = _departmentRepository.Get(request.departmentId);
        if (department == null)
        {
            throw new InvalidOperationException($"Department with id'{request.departmentId}' was not found.");
        }
        Employee employee = new Employee
        {
            Name = request.name,
            Surname = request.surname,
            Date = request.date,
            Email = request.email,
            DepartmentId = department.Id
        };
        var hiredEmployee = _employeeRepository.Add(employee);
        return Task.FromResult(EmployeeDto.FromEmployee(hiredEmployee));
    }
}

