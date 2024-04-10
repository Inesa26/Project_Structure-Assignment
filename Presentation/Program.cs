using Application;
using Application.Departments.Commands;
using Application.Departments.Queries;
using Application.Employees.Commands;
using Application.Employees.Queries;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var diContainer = new ServiceCollection()
    .AddSingleton<IRepository<Department>, RepositoryImpl<Department>>()
    .AddSingleton<IRepository<Employee>, RepositoryImpl<Employee>>()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IRepository<Employee>).Assembly))
    .BuildServiceProvider();


var mediator = diContainer.GetRequiredService<IMediator>();

// Adding a new department
var department1 = await mediator.Send(new CreateDepartment("Department 1"));
var department2 = await mediator.Send(new CreateDepartment("Department 2"));

//Get list of Departments
var departments = await mediator.Send(new GetAllDepartments());

//Hiring a new employee
var employee1 = await mediator.Send(new HireEmployee("Vlad", "Godorozea", new DateTime(1990, 7, 17), "vlad@gmail.com", department2.Id));

//Get Employee 
employee1 = await mediator.Send(new GetEmployeeById(employee1.Id));
Console.WriteLine(employee1);

//Update Employee
employee1 = await mediator.Send(new UpdateEmployee(employee1.Id, "Alex", "Godorozea", new DateTime(1993, 1, 28), "alex@gmail.com", department1.Id));
Console.WriteLine(employee1);