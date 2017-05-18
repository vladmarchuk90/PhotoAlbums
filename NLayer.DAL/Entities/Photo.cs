using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NLayer.DAL.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AlbumId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateUploaded { get; set; }
        public int amountOfLikes { get; set; }
        public string PathToPhoto { get; set; }
        public string PathToThumbPhoto { get; set; }

        public virtual User User { get; set; }
        public virtual Album Album { get; set; }
    }
}
