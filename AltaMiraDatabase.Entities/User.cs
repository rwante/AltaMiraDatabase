using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltaMiraDatabase.Entities
{
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Username { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public Address Address { get; set; }
        [StringLength(11)]
        public string Phone { get; set; }
        public string Website { get; set; }
        public Company Company { get; set; }
        public string Pass { get; set; }
    }
}
