using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        [Display(Name = "Avatar")]
        public string PathToAvatar { get; set; }

        //public IList<AlbumDTO> Albums { get; set; }
    }
}
