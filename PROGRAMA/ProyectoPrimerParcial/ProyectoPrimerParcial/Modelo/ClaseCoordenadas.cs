// ============================================================================
// Archivo: ClaseCoordenadas.cs
// Descripción: Utilidad estática para conversión entre sistemas de coordenadas:
//              - Cartesianas ↔ Polares
//              - Mundo ↔ Pantalla
//              - Cálculos de distancia y ángulo
//
//              Sistemas de coordenadas:
//              ┌─────────────────────────────────────────┐
//              │ CARTESIANAS: (x, y)                     │
//              │   • Eje X horizontal, Eje Y vertical    │
//              │   • Origen en (0, 0)                    │
//              │                                         │
//              │ POLARES: (r, θ)                         │
//              │   • r = distancia al origen (radio)     │
//              │   • θ = ángulo desde el eje X positivo  │
//              │   • θ positivo = sentido antihorario    │
//              │                                         │
//              │ PANTALLA: (px, py)                      │
//              │   • Origen en esquina superior izquierda│
//              │   • Eje Y invertido (crece hacia abajo) │
//              └─────────────────────────────────────────┘
//
// Proyecto: Simulador de Radar 2D - Computación Gráfica
// ============================================================================

using System;

namespace ProyectoPrimerParcial.Modelo
{
    /// <summary>
    /// Clase estática que provee métodos de conversión entre sistemas de
    /// coordenadas y cálculos geométricos auxiliares para el simulador de radar.
    /// </summary>
    public static class ClaseCoordenadas
    {
        // ==================================================================
        // Conversión entre Cartesianas y Polares
        // ==================================================================

        /// <summary>
        /// Convierte coordenadas cartesianas (x, y) a polares (r, θ).
        /// 
        /// FÓRMULAS MATEMÁTICAS:
        /// 
        ///   Radio (distancia al origen):
        ///     r = √(x² + y²)
        ///   
        ///   Ángulo (dirección desde el eje X positivo):
        ///     θ = atan2(y, x)
        ///   
        ///   Se usa atan2(y,x) en lugar de atan(y/x) porque:
        ///   - atan2 maneja correctamente los 4 cuadrantes (rango: -π a π)
        ///   - atan2 evita la división por cero cuando x = 0
        ///   - atan solo retorna valores en el rango (-π/2, π/2)
        ///   
        ///   Conversión a grados: θ_grados = θ_radianes × (180 / π)
        /// </summary>
        /// <param name="dblX">Coordenada X cartesiana.</param>
        /// <param name="dblY">Coordenada Y cartesiana.</param>
        /// <returns>
        /// Tupla (dblRadio, dblAnguloGrados) con la coordenada polar equivalente.
        /// El ángulo está en grados sexagesimales en el rango [0, 360).
        /// </returns>
        public static (double dblRadio, double dblAnguloGrados) CartesianasAPolares(
            double dblX, double dblY)
        {
            // Calcular el radio: r = √(x² + y²)
            double dblRadio = Math.Sqrt(dblX * dblX + dblY * dblY);

            // Calcular el ángulo con atan2(y, x), que retorna radianes en [-π, π]
            double dblAnguloRadianes = Math.Atan2(dblY, dblX);

            // Convertir de radianes a grados: grados = radianes × (180 / π)
            double dblAnguloGrados = dblAnguloRadianes * 180.0 / Math.PI;

            // Normalizar el ángulo al rango [0, 360) para evitar valores negativos
            if (dblAnguloGrados < 0)
            {
                dblAnguloGrados += 360.0;
            }

            return (dblRadio, dblAnguloGrados);
        }

        /// <summary>
        /// Convierte coordenadas polares (r, θ) a cartesianas (x, y).
        /// 
        /// FÓRMULAS MATEMÁTICAS:
        /// 
        ///   x = r · cos(θ)    → componente horizontal
        ///   y = r · sin(θ)    → componente vertical
        /// 
        ///   Donde θ debe estar en radianes para las funciones Math.Cos/Sin.
        ///   Conversión: θ_radianes = θ_grados × (π / 180)
        ///   
        ///   Ejemplo: Para r=5, θ=45°:
        ///     x = 5 · cos(45°) = 5 · 0.7071 ≈ 3.5355
        ///     y = 5 · sin(45°) = 5 · 0.7071 ≈ 3.5355
        /// </summary>
        /// <param name="dblRadio">Distancia al origen (r ≥ 0).</param>
        /// <param name="dblAnguloGrados">Ángulo en grados sexagesimales.</param>
        /// <returns>
        /// Tupla (dblX, dblY) con la coordenada cartesiana equivalente.
        /// </returns>
        public static (double dblX, double dblY) PolaresACartesianas(
            double dblRadio, double dblAnguloGrados)
        {
            // Convertir de grados a radianes: rad = grados × (π / 180)
            double dblAnguloRadianes = dblAnguloGrados * Math.PI / 180.0;

            // Calcular componente X: x = r · cos(θ)
            double dblX = dblRadio * Math.Cos(dblAnguloRadianes);

            // Calcular componente Y: y = r · sin(θ)
            double dblY = dblRadio * Math.Sin(dblAnguloRadianes);

            return (dblX, dblY);
        }

        // ==================================================================
        // Cálculos geométricos
        // ==================================================================

        /// <summary>
        /// Calcula la distancia euclidiana entre dos vértices.
        /// 
        /// FÓRMULA MATEMÁTICA (distancia euclidiana en 2D):
        /// 
        ///   d = √((x₂ - x₁)² + (y₂ - y₁)²)
        /// 
        ///   Derivada del Teorema de Pitágoras:
        ///   Si Δx = x₂ - x₁ y Δy = y₂ - y₁, entonces d = √(Δx² + Δy²)
        ///   
        ///   La distancia siempre es ≥ 0.
        ///   La distancia de un punto a sí mismo es 0.
        /// </summary>
        /// <param name="objVertice1">Primer vértice (punto de origen).</param>
        /// <param name="objVertice2">Segundo vértice (punto de destino).</param>
        /// <returns>La distancia euclidiana entre los dos vértices.</returns>
        public static double CalcularDistancia(ClaseVertice objVertice1, ClaseVertice objVertice2)
        {
            // Calcular las diferencias en cada eje
            double dblDeltaX = objVertice2.X - objVertice1.X;
            double dblDeltaY = objVertice2.Y - objVertice1.Y;

            // Aplicar la fórmula: d = √(Δx² + Δy²)
            return Math.Sqrt(dblDeltaX * dblDeltaX + dblDeltaY * dblDeltaY);
        }

        /// <summary>
        /// Calcula el ángulo en grados desde el vértice 1 hacia el vértice 2.
        /// 
        /// FÓRMULA MATEMÁTICA:
        /// 
        ///   θ = atan2(y₂ - y₁, x₂ - x₁)
        /// 
        ///   El ángulo se mide desde el eje X positivo en sentido antihorario.
        ///   Se convierte a grados y se normaliza al rango [0, 360).
        ///   
        ///   Ejemplo: Si v1=(0,0) y v2=(1,1), el ángulo es 45°.
        /// </summary>
        /// <param name="objVertice1">Vértice de referencia (origen del ángulo).</param>
        /// <param name="objVertice2">Vértice de destino.</param>
        /// <returns>Ángulo en grados sexagesimales en el rango [0, 360).</returns>
        public static double CalcularAngulo(ClaseVertice objVertice1, ClaseVertice objVertice2)
        {
            // Calcular las diferencias en cada eje
            double dblDeltaX = objVertice2.X - objVertice1.X;
            double dblDeltaY = objVertice2.Y - objVertice1.Y;

            // Calcular el ángulo con atan2 (retorna radianes en [-π, π])
            double dblAnguloRadianes = Math.Atan2(dblDeltaY, dblDeltaX);

            // Convertir a grados: grados = radianes × (180 / π)
            double dblAnguloGrados = dblAnguloRadianes * 180.0 / Math.PI;

            // Normalizar al rango [0, 360)
            if (dblAnguloGrados < 0)
            {
                dblAnguloGrados += 360.0;
            }

            return dblAnguloGrados;
        }

        // ==================================================================
        // Conversión Mundo ↔ Pantalla
        // ==================================================================

        /// <summary>
        /// Convierte coordenadas del mundo (sistema cartesiano) a coordenadas
        /// de pantalla (sistema de píxeles).
        /// 
        /// TRANSFORMACIÓN:
        /// 
        ///   pantallaX = centroX + (mundoX × escala)
        ///   pantallaY = centroY - (mundoY × escala)    ← NOTA: Y se invierte
        /// 
        ///   El eje Y se invierte porque:
        ///   - En el mundo matemático, Y crece hacia ARRIBA.
        ///   - En la pantalla (GDI+), Y crece hacia ABAJO.
        ///   
        ///   El centroX/Y es el punto de la pantalla donde se ubica el origen
        ///   del sistema del mundo (normalmente el centro del PictureBox).
        ///   
        ///   La escala permite hacer zoom: escala > 1 amplía, escala menor que 1 reduce.
        /// </summary>
        /// <param name="dblMundoX">Coordenada X en el mundo.</param>
        /// <param name="dblMundoY">Coordenada Y en el mundo.</param>
        /// <param name="dblCentroX">X del centro de la pantalla (en píxeles).</param>
        /// <param name="dblCentroY">Y del centro de la pantalla (en píxeles).</param>
        /// <param name="dblEscala">Factor de escala (zoom).</param>
        /// <returns>
        /// Tupla (dblPantallaX, dblPantallaY) con las coordenadas en píxeles.
        /// </returns>
        public static (double dblPantallaX, double dblPantallaY) MundoAPantalla(
            double dblMundoX, double dblMundoY,
            double dblCentroX, double dblCentroY, double dblEscala)
        {
            // X en pantalla: desplazar desde el centro + escalar la coordenada mundo
            double dblPantallaX = dblCentroX + (dblMundoX * dblEscala);

            // Y en pantalla: desplazar desde el centro - escalar (eje Y invertido)
            double dblPantallaY = dblCentroY - (dblMundoY * dblEscala);

            return (dblPantallaX, dblPantallaY);
        }

        /// <summary>
        /// Convierte coordenadas de pantalla (píxeles) a coordenadas del mundo
        /// (sistema cartesiano).
        /// 
        /// TRANSFORMACIÓN INVERSA:
        /// 
        ///   mundoX = (pantallaX - centroX) / escala
        ///   mundoY = (centroY - pantallaY) / escala    ← NOTA: Y se invierte
        /// 
        ///   Esta es la operación inversa de MundoAPantalla().
        ///   Es útil para convertir clics del mouse a coordenadas del mundo.
        /// </summary>
        /// <param name="dblPantallaX">Coordenada X en pantalla (píxeles).</param>
        /// <param name="dblPantallaY">Coordenada Y en pantalla (píxeles).</param>
        /// <param name="dblCentroX">X del centro de la pantalla (en píxeles).</param>
        /// <param name="dblCentroY">Y del centro de la pantalla (en píxeles).</param>
        /// <param name="dblEscala">Factor de escala (zoom).</param>
        /// <returns>
        /// Tupla (dblMundoX, dblMundoY) con las coordenadas en el mundo.
        /// </returns>
        public static (double dblMundoX, double dblMundoY) PantallaAMundo(
            double dblPantallaX, double dblPantallaY,
            double dblCentroX, double dblCentroY, double dblEscala)
        {
            // X en mundo: invertir el desplazamiento y la escala
            double dblMundoX = (dblPantallaX - dblCentroX) / dblEscala;

            // Y en mundo: invertir desplazamiento y la dirección del eje Y
            double dblMundoY = (dblCentroY - dblPantallaY) / dblEscala;

            return (dblMundoX, dblMundoY);
        }
    }
}
