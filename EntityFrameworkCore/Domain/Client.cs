using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain
{

    public class Client
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Cep { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
