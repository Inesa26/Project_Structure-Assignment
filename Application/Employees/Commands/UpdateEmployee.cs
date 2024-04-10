using Application.Employees.Responses;
using Domain;
using MediatR;

namespace Application.Employees.Commands;
public record UpdateEmployee(Guid employeeId, string name, string surname, DateTime date, string email, Guid departmentId) : IRequest<EmployeeDto>;
public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, EmployeeDto>
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly IRepository<Employee> _employeeRepository;

    public UpdateEmployeeHandler(IRepository<Department> departmentRepository, IRepository<Employee> employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
    }

    public Task<EmployeeDto> Handle(UpdateEmployee request, CancellationToken cancellationToken)
    {
        Employee employeeToUpdate = _employeeRepository.Get(request.employeeId);
        if (employeeToUpdate == null)
        {
            throw new InvalidOperationException($"Employee with id'{request.employeeId}' was not found.");
        }
        Department department = _departmentRepository.Get(request.departmentId);
        if (department == null)
        {
            throw new InvalidOperationException($"Department with ID '{request.departmentId}' was not found.");
        }
        employeeToUpdate.Name = request.name;
        employeeToUpdate.Surname = request.surname;
        employeeToUpdate.Email = request.email;
        employeeToUpdate.Date = request.date;
        employeeToUpdate.DepartmentId = request.departmentId;

        _employeeRepository.Update(request.employeeId, employeeToUpdate);
        return Task.FromResult(EmployeeDto.FromEmployee(employeeToUpdate));

    }
}
