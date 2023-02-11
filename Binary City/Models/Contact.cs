using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Binary_City.Models
{
   
    [Table("Contact")]
  
    public class Contact
    {
      
        public Contact()
        {
            ClientsContacts = new HashSet<ClientsContact>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid Email address")]
        public string Email { get; set; }
        public virtual ICollection<ClientsContact> ClientsContacts { get; set; }
    }
}