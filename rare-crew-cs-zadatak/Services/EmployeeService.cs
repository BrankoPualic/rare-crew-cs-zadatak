using rare_crew_cs_zadatak.Interfaces;
using rare_crew_cs_zadatak.Models;
using System.Text.Json;

namespace rare_crew_cs_zadatak.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public EmployeeService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}

		public async Task<List<Employee>> GetEmployeesAsync()
		{
			string apiKey = _configuration["ApiKey"] ?? "";
			var response = await _httpClient.GetAsync($"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={apiKey}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};

			var employees = JsonSerializer.Deserialize<List<Employee>>(content, options);
			if (employees is null) throw new Exception("Employees variable is null. Please check the content you recieve from an api.");
			return employees;
		}
	}
}
