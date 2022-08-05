using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DocumentoTramite
    {
        public int idDocumentoTramite { get; set; }
        public String nombreDocumento { get; set; }
        public String rutaDocumento { get; set; }
        public String numero { get; set; }
        public String descripcion { get; set; }
        public Funcionario funcionario { get; set; }
    }
}
