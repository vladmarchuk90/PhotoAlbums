using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NLayer.DAL.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public User User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public Album()
        {
            Photos = new List<Photo>();
        }
    }
}
