using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LuanVan.Models
{
    [DataContract]
    public class PiePoints
    {
            public PiePoints()
            {

            }

            public PiePoints(double x, string legendText, string label)
            {
                this.X = x;
                this.legendText = legendText;
                this.Label = label;
            }

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> X = null;
            //Explicitly setting the name to be used while serializing to JSON. 

            [DataMember(Name = "legendText")]
            public string legendText = null;

            //Explicitly setting the name to be used while serializing to JSON. 
            [DataMember(Name = "label")]
            public string Label = null;

        
    }
}