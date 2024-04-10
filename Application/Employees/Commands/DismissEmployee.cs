using Application.Employees.Responses;
using Domain;
using MediatR;

namespace Application.Employees.Commands;
public record DismissEmployee(Guid employeeId) : IRequest<EmployeeDto>;
public class DismissEmployeeHandler : IRequestHandler<DismissEmployee, EmployeeDto>
{
    private readonly IRepository<Employee> _employeeRepository;

    public DismissEmployeeHandler(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Task<EmployeeDto> Handle(DismissEmployee request, CancellationToken cancellationToken)
    {
        Employee employeeToRemove = _employeeRepository.Get(request.employeeId);
        if (employeeToRemove == null)
        {
            throw new InvalidOperationException($"Employee with id'{request.employeeId}' was not found.");
        }
        var dismissedEmployee = _employeeRepository.Delete(request.employeeId);
        return Task.FromResult(EmployeeDto.FromEmployee(dismissedEmployee));
    }
}
