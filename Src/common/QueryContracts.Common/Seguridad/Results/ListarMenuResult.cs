using System.Collections.Generic;
namespace QueryContracts.Common.Seguridad.Results
{
    public class ListarMenuResult : QueryResult
    {
        public IEnumerable<ListarMenuDto> Hits { get; set; }
    }
    public class ListarMenuDto
    {
        private string url; 

        public double IdMenuPerfil { get; set; }
        public double IdMenu { get; set; }
        public double? IdMenuPadre { get; set; }
        public int Nivel 
        {
            get; 
            set;
        }
        public string Nombre { get; set; }
        public string Url 
        {
            get
            {
                return Tipo == "I" && string.IsNullOrEmpty(url) ? "#" : url;
            }
            set
            {
                url = value;
            }
        }

        public string Tipo { get; set; }
    }
}
