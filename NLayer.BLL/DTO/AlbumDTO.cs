using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.BLL.DTO
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int amountOfLikes { get; set; }

        public UserDTO User { get; set; }
        public IList<PhotoDTO> Photos { get; set; }

        public AlbumDTO()
        {
            Photos = new List<PhotoDTO>();
        }
    }
}
