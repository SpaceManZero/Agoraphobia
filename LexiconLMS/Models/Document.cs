using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
	public class Document
	{
		public int Id { get; set; }
		public virtual ApplicationUser User { get; set; }
		public virtual Course Course { get; set; }
		public virtual Module Module { get; set; }
		public virtual Activity Activity { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public DateTime UploadTime { get; set; }
		public DateTime DeadLine { get; set; }
	}
}