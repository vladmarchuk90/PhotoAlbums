using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.BLL.DTO
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateUploaded { get; set; }
        public string PathToPhoto { get; set; }

        //public UserDTO User { get; set; }
        //public AlbumDTO Album { get; set; }
    }
}
