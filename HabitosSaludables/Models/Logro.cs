using System;
using System.Collections.Generic;
using System.Text;

namespace HabitosSaludables.Models
{
    public class Logro
    {
        /// <summary>Nombre corto del logro (mostrado en el badge).</summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>Emoji representativo del logro.</summary>
        public string Emoji { get; set; } = string.Empty;

        /// <summary>Descripción del criterio para desbloquearlo.</summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Indica si el usuario ya cumplió el criterio.
        /// Cuando es false el badge se muestra opaco.
        /// </summary>
        public bool Desbloqueado { get; set; }
    }
}
