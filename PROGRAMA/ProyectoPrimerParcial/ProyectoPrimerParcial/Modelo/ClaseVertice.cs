// ============================================================================
// Archivo: ClaseVertice.cs
// Descripción: Representa un vértice en coordenadas homogéneas 2D (x, y, w).
//              En coordenadas homogéneas, un punto 2D se representa como un
//              vector de 3 componentes (x, y, w) donde las coordenadas
//              cartesianas reales son (x/w, y/w). Normalmente w = 1.
// Proyecto: Simulador de Radar 2D - Computación Gráfica
// ============================================================================

using System;

namespace ProyectoPrimerParcial.Modelo
{
    /// <summary>
    /// Clase que representa un vértice en coordenadas homogéneas 2D.
    /// Un punto (x, y) se representa como (x, y, w) donde w = 1 por defecto.
    /// Las coordenadas cartesianas reales son (X/W, Y/W).
    /// </summary>
    public class ClaseVertice
    {
        // ==================================================================
        // Propiedades
        // ==================================================================

        /// <summary>Componente X del vértice en coordenadas homogéneas.</summary>
        public double X { get; set; }

        /// <summary>Componente Y del vértice en coordenadas homogéneas.</summary>
        public double Y { get; set; }

        /// <summary>
        /// Componente W (factor homogéneo). Normalmente vale 1.0.
        /// Si W ≠ 1, las coordenadas cartesianas reales son (X/W, Y/W).
        /// </summary>
        public double W { get; set; }

        // ==================================================================
        // Constructores
        // ==================================================================

        /// <summary>
        /// Constructor principal. Crea un vértice con coordenadas (x, y, w).
        /// Por defecto, w = 1.0 para representar un punto en el plano 2D.
        /// </summary>
        /// <param name="dblX">Coordenada X del vértice.</param>
        /// <param name="dblY">Coordenada Y del vértice.</param>
        /// <param name="dblW">Factor homogéneo (por defecto 1.0).</param>
        public ClaseVertice(double dblX = 0.0, double dblY = 0.0, double dblW = 1.0)
        {
            X = dblX;
            Y = dblY;
            W = dblW;
        }

        // ==================================================================
        // Métodos
        // ==================================================================

        /// <summary>
        /// Crea una copia independiente (clon) de este vértice.
        /// Esto evita problemas de referencia compartida al manipular vértices.
        /// </summary>
        /// <returns>Un nuevo ClaseVertice con los mismos valores X, Y, W.</returns>
        public ClaseVertice Clonar()
        {
            return new ClaseVertice(X, Y, W);
        }

        /// <summary>
        /// Representación textual del vértice, formateada a 4 decimales.
        /// Formato: (X, Y, W)
        /// </summary>
        /// <returns>Cadena con las coordenadas formateadas.</returns>
        public override string ToString()
        {
            return string.Format("({0:F4}, {1:F4}, {2:F4})", X, Y, W);
        }
    }
}
