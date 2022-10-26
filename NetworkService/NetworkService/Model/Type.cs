using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Type
    {
        private string name;
        private string img_src;

        public Type() { }

        public Type(string name, string img_src)
        {
            this.Name = name;
            this.Img_src = img_src;
        }

        public string Name 
        {
            get { return name; }
            set
            {
                if(name != value)
                {
                    name = value;
                }
            }
        }
        public string Img_src
        {
            get { return img_src; }
            set
            {
                if (img_src != value)
                {
                    img_src = value;
                }
            }
        }



    }
}
