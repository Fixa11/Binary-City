using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Binary_City.Models
{
    [Table("ClientsContact")]
    public class ClientsContact
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int Contactid { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }

        [ForeignKey("Contactid")]
        public virtual Contact Contacts { get; set; }

    }
}