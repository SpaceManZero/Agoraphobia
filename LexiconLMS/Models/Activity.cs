using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
	public class ActivityType
	{
		public int Id { get; set; }
		public string Type { get; set; }
	}

	public class Activity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public virtual ActivityType Type { get; set; }
		public virtual Module Module { get; set; }
	}
}