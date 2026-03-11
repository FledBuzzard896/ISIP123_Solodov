using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Item
    {
        public string name;
        public string description;
        public string imgUrl;

        public Item(string name, string description, string imgUrl)
        {

            this.description = description;
            this.name = name;
            this.imgUrl = imgUrl;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine(description);
        }
    }
}
