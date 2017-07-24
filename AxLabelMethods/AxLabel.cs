using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxLabelHelper
{
    public class AxLabel
    {
        public string Comment { get; set; }

        public string Description { get; set; }

        public string FullLabel
        {
            get
            {
                return $"{ LabelId }{ Description }{ Environment.NewLine }{ Comment }";
            }
        }
        public string LabelId { get; set; }

        public string Line1
        {
            set
            {
                LabelId = value.Substring(0, value.IndexOf('='));
                Description = value.Substring(value.IndexOf('='));
            }
        }
    }
}
