using Application.Departments.Responses;
using Domain;
using MediatR;

namespace Application.Departments.Commands;
public record CreateDepartment(string name) : IRequest<DepartmentDto>;
public class CreateDepartmentHandler : IRequestHandler<CreateDepartment, DepartmentDto>
{
    private readonly IRepository<Department> _departmentRepository;

    public CreateDepartmentHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public Task<DepartmentDto> Handle(CreateDepartment request, CancellationToken cancellationToken)
    {
        foreach (var existingDepartment in _departmentRepository.GetAll())
        {
            if (existingDepartment.Name == request.name)
            {
                throw new InvalidOperationException($"Department '{request.name}' already exists.");
            }
        }
        Department department = new Department { Name = request.name };
        var createdDepartment = _departmentRepository.Add(department);
        return Task.FromResult(DepartmentDto.FromDepartment(createdDepartment));
    }
}


