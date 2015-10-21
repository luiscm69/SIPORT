namespace Seguridad.Common
{
    using System.Collections.Generic;

    public class Menu
    {
        public double IdMenu { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public string Url { get; set; }
        public List<Menu> Items { get; set; }

    }
}
