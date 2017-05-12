using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public virtual ICollection<Album> Albums { get; set; }
    }
}
