using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Project_Web.Models
{
    [DataContract]
    public class DataPoint_Name_y
    {

		public DataPoint_Name_y(string name, double y)
		{
			this.Name = name;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "name")]
		public string Name = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}