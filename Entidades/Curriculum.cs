using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Curriculum
    {
        public int idCurriculum { get; set; }
        public String nombre { get; set; }
        public String ruta { get; set; }
        public String descripcion { get; set; }
        public Funcionario funcionario { get; set; }
    }
}
