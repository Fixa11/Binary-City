using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Binary_City.Models
{
    [Table("Client")]
    public class Client
    {
        public Client()
        {
            ClientsContacts = new HashSet<ClientsContact>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(14, MinimumLength = 6, ErrorMessage = "Invalid Number")]
        public string ClientCode { get; set; }
        public virtual ICollection<ClientsContact> ClientsContacts { get; set; }


    }
}