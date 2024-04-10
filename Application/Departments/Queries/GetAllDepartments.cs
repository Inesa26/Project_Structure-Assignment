using Application.Departments.Responses;
using Domain;
using MediatR;

namespace Application.Departments.Queries;
public record GetAllDepartments() : IRequest<List<DepartmentDto>>;
public class GetAllDepartmentstHandler : IRequestHandler<GetAllDepartments, List<DepartmentDto>>
{
    private readonly IRepository<Department> _departmentRepository;

    public GetAllDepartmentstHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public Task<List<DepartmentDto>> Handle(GetAllDepartments request, CancellationToken cancellationToken)
    {
        var departments = _departmentRepository.GetAll();
        return Task.FromResult(departments.Select(DepartmentDto.FromDepartment).ToList());
    }
}