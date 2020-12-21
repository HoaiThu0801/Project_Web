using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Project_Web.Models
{
	[DataContract]
	public class DataPoints
	{
        public DataPoints(string label, double y)
		{
			this.Label = label;
			this.Y = y;
		}
		public DataPoints(double y, string label)
		{
			this.Label = label;
			this.Y = y;
		}
		//Explicitly setting the name to be used while serializing to JSON. 
		[DataMember(Name = "label")]
		public string Label = null;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}