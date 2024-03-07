using rare_crew_cs_zadatak.Models;

namespace rare_crew_cs_zadatak.Interfaces
{
	public interface IEmployeeService
	{
		Task<List<Employee>> GetEmployeesAsync();
	}
}
