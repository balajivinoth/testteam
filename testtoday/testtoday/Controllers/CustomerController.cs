using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using testtoday.model;

namespace testtoday.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly string connectionstring;
		public CustomerController(IConfiguration configuration) {
			connectionstring = configuration.GetConnectionString("DefaultConnection")!;
		}
		[HttpPost]
		public IActionResult Create(Customerdto customerdto)
		{
			try{
				using(var connection = new SqlConnection(connectionstring))
				{
					Console.WriteLine("db connected");
					connection.Open();
					string sql = "Insert into Users(name,email)" +
						"output Inserted.*"+
						"values(@name,@email)";
					var customer = new Customer()
					{
						name = customerdto.name,
						email = customerdto.email
					};
					var newCustomer = connection.QuerySingleOrDefault<Customer>(sql,customer);
					if(newCustomer != null)
					{
						return Ok(newCustomer);
					}
				}
				


			}catch (Exception ex) { 
				Console.WriteLine("we have an exception"+ex.Message);
			}	
			return BadRequest();
		}
	}
}
