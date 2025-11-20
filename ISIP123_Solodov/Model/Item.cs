using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov.Model
{
    internal class Item
    {
        public string name;
        public string description;

        public Item(string name, string description)
        {

            this.description = description;
            this.name = name;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine(description);
        }
    }
}
