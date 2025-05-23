using System.ComponentModel.DataAnnotations;

namespace testtoday.model
{
	public class Customerdto
	{
		[Required]
		public string name { get; set; }
		[Required]
		public string email { get; set; }
	}
}
