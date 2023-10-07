using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bus_Objects
{
    public class ChatBO
    {

        public class HeadDatas
        {
            public string caption { get; set; }
            public string subcaption { get; set; }
            public string yaxisname { get; set; }

            public string numvisibleplot { get; set; }
            public string labeldisplay { get; set; }

            public string palettecolors { get; set; }

            public string theme { get; set; }
            public string formatnumberscale { get; set; }
             public string showValues { get; set; }
            public string rotateValues { get; set; }



            
        }

        public class MainDatas
        {
            public string label { get; set; }
            public string value { get; set; }
            public string displayValue { get; set; }
           
        }

    }
}
