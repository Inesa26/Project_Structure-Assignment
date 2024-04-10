using Application.Departments.Responses;
using Domain;
using MediatR;

namespace Application.Departments.Commands;
public record RemoveDepartment(Guid Id) : IRequest<DepartmentDto>;
public class RemoveDepartmentHandler : IRequestHandler<RemoveDepartment, DepartmentDto>
{
    private readonly IRepository<Department> _departmentRepository;

    public RemoveDepartmentHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public Task<DepartmentDto> Handle(RemoveDepartment request, CancellationToken cancellationToken)
    {
        var existingDepartment = _departmentRepository.Get(request.Id);
        if (existingDepartment == null)
        {
            throw new InvalidOperationException($"Department with ID {request.Id} not found.");
        }

        var deletedDepartment = _departmentRepository.Delete(request.Id);
        return Task.FromResult(DepartmentDto.FromDepartment(deletedDepartment));
    }
}
