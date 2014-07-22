using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPOCOProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new PetaPoco.Database("ConnStr");

            // Show all articles    
            foreach (var item in db.Query<HeiMa>("SELECT * FROM heima"))
            {
                Console.WriteLine("{0} - {1}", item.Name, item.Age);
            }
            Console.ReadKey();
        }
    }
    public class HeiMa
    {
        public int  Age { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
