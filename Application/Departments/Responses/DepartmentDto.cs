using Domain;

namespace Application.Departments.Responses;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }


    public static DepartmentDto FromDepartment(Department department)
    {
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name
        };
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name ?? "N/A"}";
    }
}