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
		public virtual IEnumerable<Activity> Activities { get; set; }
	}
}