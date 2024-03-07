namespace rare_crew_cs_zadatak.Models
{
	public class Employee
	{
		public string? EmployeeName { get; set; }
		public DateTime StarTimeUtc { get; set; }
		public DateTime EndTimeUtc { get; set; }
		public int? MonthlyHours { get; set; }
	}
}
