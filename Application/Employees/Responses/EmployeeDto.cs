using Domain;

namespace Application.Employees.Responses
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public static EmployeeDto FromEmployee(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname
            };
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name ?? "N/A"}, Surname: {Surname ?? "N/A"}";
        }
    }
}
