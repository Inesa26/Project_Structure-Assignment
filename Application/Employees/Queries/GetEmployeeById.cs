using Application.Employees.Responses;
using Domain;
using MediatR;

namespace Application.Employees.Queries;
public record GetEmployeeById(Guid Id) : IRequest<EmployeeDto>;
public class GetEmployeeByIdtHandler : IRequestHandler<GetEmployeeById, EmployeeDto>
{
    private readonly IRepository<Employee> _employeeRepository;

    public GetEmployeeByIdtHandler(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Task<EmployeeDto> Handle(GetEmployeeById request, CancellationToken cancellationToken)
    {
        var employee = _employeeRepository.Get(request.Id);
        return Task.FromResult(EmployeeDto.FromEmployee(employee));
    }
}