using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.BLL.DTO
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UserDTO User { get; set; }
        public ICollection<PhotoDTO> Photos { get; set; }

        public AlbumDTO()
        {
            Photos = new List<PhotoDTO>();
        }
    }
}
