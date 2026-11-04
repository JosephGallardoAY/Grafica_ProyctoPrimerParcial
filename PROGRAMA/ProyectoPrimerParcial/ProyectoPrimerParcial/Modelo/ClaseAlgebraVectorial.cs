// ============================================================================
// Archivo: ClaseAlgebraVectorial.cs
// Descripción: Módulo matemático de Álgebra Vectorial 2D para Computación Gráfica.
//              Implementa las operaciones vectoriales fundamentales exigidas
//              en el Primer Parcial:
//              1. Magnitud / Norma Euclidiana: ||v|| = √(x² + y²)
//              2. Vector Unitario (Normalización): û = v / ||v||
//              3. Producto Punto (Escalar): a⃗ · b⃗ = ax·bx + ay·by = ||a||·||b||·cos(θ)
//              4. Producto Cruz 2D (Pseudovectorial / Determinante): a⃗ × b⃗ = ax·by - ay·bx
//              5. Ángulo entre vectores vía Producto Punto: θ = arccos((a⃗ · b⃗) / (||a||·||b||))
//              6. Proyección Ortogonal de un vector sobre otro: Proy_b(a) = ((a⃗ · b⃗) / ||b||²) · b⃗
//              7. Descomposición cartesiana en vectores canónicos: v⃗ = x·î + y·ĵ
//
// Asignatura: Computación Gráfica
// Carrera: Ingeniería en Tecnologías de la Información - ESPE
// ============================================================================

using System;
using System.Globalization;

namespace ProyectoPrimerParcial.Modelo
{
    /// <summary>
    /// Clase utilitaria estática con algoritmos puros de álgebra vectorial 2D.
    /// </summary>
    public static class ClaseAlgebraVectorial
    {
        // ==================================================================
        // 1. Magnitud / Norma Euclidiana (L2 Norm)
        // ==================================================================

        /// <summary>
        /// Calcula la norma o longitud euclidiana de un vector 2D.
        /// 
        /// FÓRMULA MATEMÁTICA:
        ///   ||v⃗|| = √(vx² + vy²)
        /// 
        /// Representa la distancia geométrica desde el origen hasta el punto (x, y).
        /// </summary>
        public static double Magnitud(double dblX, double dblY)
        {
            return Math.Sqrt(dblX * dblX + dblY * dblY);
        }

        public static double Magnitud(ClaseVertice objV)
        {
            return Magnitud(objV.X, objV.Y);
        }

        // ==================================================================
        // 2. Vector Unitario / Normalización
        // ==================================================================

        /// <summary>
        /// Calcula el vector unitario (vector de norma 1 que indica únicamente la dirección).
        /// 
        /// FÓRMULA MATEMÁTICA:
        ///   û = v⃗ / ||v⃗|| = (vx / ||v||, vy / ||v||)
        /// 
        /// Si la magnitud es cero, retorna el vector nulo (0, 0) para evitar división por cero.
        /// </summary>
        public static ClaseVertice Normalizar(double dblX, double dblY)
        {
            double dblMag = Magnitud(dblX, dblY);
            if (dblMag < 1e-9)
            {
                return new ClaseVertice(0.0, 0.0);
            }
            return new ClaseVertice(dblX / dblMag, dblY / dblMag);
        }

        public static ClaseVertice Normalizar(ClaseVertice objV)
        {
            return Normalizar(objV.X, objV.Y);
        }

        // ==================================================================
        // 3. Producto Punto / Producto Escalar (Dot Product)
        // ==================================================================

        /// <summary>
        /// Calcula el producto punto entre dos vectores bidimensionales.
        /// 
        /// FÓRMULA ALGEBRAICA:
        ///   a⃗ · b⃗ = ax · bx + ay · by
        /// 
        /// FÓRMULA GEOMÉTRICA:
        ///   a⃗ · b⃗ = ||a⃗|| · ||b⃗|| · cos(θ)
        /// 
        /// PROPIEDADES GEOMÉTRICAS CLAVE EN EL RADAR:
        /// - Si a⃗ · b⃗ > 0 : El ángulo entre ambos es agudo (θ < 90°).
        /// - Si a⃗ · b⃗ = 0 : Los vectores son ORTOGONALES / PERPENDICULARES (θ = 90°).
        /// - Si a⃗ · b⃗ < 0 : El ángulo es obtuso (θ > 90°).
        /// - Con vectores unitarios û_haz y û_obj: cos(θ) = û_haz · û_obj.
        ///   ¡Permite detectar si un objeto está dentro del haz del radar sin usar trigonometría inversa!
        /// </summary>
        public static double ProductoPunto(ClaseVertice objA, ClaseVertice objB)
        {
            return (objA.X * objB.X) + (objA.Y * objB.Y);
        }

        // ==================================================================
        // 4. Producto Cruz 2D / Producto Pseudovectorial (Determinante)
        // ==================================================================

        /// <summary>
        /// Calcula el producto cruz en dos dimensiones (componente k perpendicular al plano XY).
        /// 
        /// FÓRMULA MATEMÁTICA:
        ///   a⃗ × b⃗ = det | ax  bx | = ax · by - ay · bx
        ///               | ay  by |
        /// 
        /// FÓRMULA GEOMÉTRICA:
        ///   a⃗ × b⃗ = ||a⃗|| · ||b⃗|| · sin(θ)
        /// 
        /// PROPIEDADES EN EL RADAR:
        /// - Magnitud: Representa el área del paralelogramo delimitado por ambos vectores.
        /// - Signo: Determina la orientación relativa:
        ///   * Si a⃗ × b⃗ > 0 : b⃗ está a la IZQUIERDA de a⃗ (sentido antihorario).
        ///   * Si a⃗ × b⃗ < 0 : b⃗ está a la DERECHA de a⃗ (sentido horario).
        ///   * Si a⃗ × b⃗ = 0 : Los vectores son COLINEALES / PARALELOS.
        /// </summary>
        public static double ProductoCruz2D(ClaseVertice objA, ClaseVertice objB)
        {
            return (objA.X * objB.Y) - (objA.Y * objB.X);
        }

        // ==================================================================
        // 5. Ángulo entre Vectores
        // ==================================================================

        /// <summary>
        /// Obtiene el ángulo en grados sexagesimales entre dos vectores usando el producto punto.
        /// 
        /// FÓRMULA:
        ///   cos(θ) = (a⃗ · b⃗) / (||a⃗|| · ||b⃗||)
        ///   θ = arccos( clamp(cos(θ), -1.0, 1.0) )
        /// </summary>
        public static double AnguloEntreVectoresGrados(ClaseVertice objA, ClaseVertice objB)
        {
            double dblMagA = Magnitud(objA);
            double dblMagB = Magnitud(objB);

            if (dblMagA < 1e-9 || dblMagB < 1e-9)
            {
                return 0.0;
            }

            double dblCoseno = ProductoPunto(objA, objB) / (dblMagA * dblMagB);

            // Evitar imprecisiones numéricas de punto flotante fuera de [-1, 1]
            dblCoseno = Math.Max(-1.0, Math.Min(1.0, dblCoseno));

            double dblRadianes = Math.Acos(dblCoseno);
            return dblRadianes * 180.0 / Math.PI;
        }

        // ==================================================================
        // 6. Proyección Ortogonal
        // ==================================================================

        /// <summary>
        /// Proyecta ortogonalmente el vector a⃗ sobre la dirección del vector b⃗.
        /// 
        /// FÓRMULA:
        ///   Proy_b(a⃗) = ((a⃗ · b⃗) / ||b⃗||²) · b⃗
        /// </summary>
        public static ClaseVertice ProyeccionOrtogonal(ClaseVertice objA, ClaseVertice objB)
        {
            double dblMagBSq = (objB.X * objB.X) + (objB.Y * objB.Y);
            if (dblMagBSq < 1e-9)
            {
                return new ClaseVertice(0.0, 0.0);
            }

            double dblEscalar = ProductoPunto(objA, objB) / dblMagBSq;
            return new ClaseVertice(objB.X * dblEscalar, objB.Y * dblEscalar);
        }

        // ==================================================================
        // 7. Suma y Resta Vectorial
        // ==================================================================

        public static ClaseVertice Sumar(ClaseVertice objA, ClaseVertice objB)
        {
            return new ClaseVertice(objA.X + objB.X, objA.Y + objB.Y);
        }

        public static ClaseVertice Restar(ClaseVertice objA, ClaseVertice objB)
        {
            return new ClaseVertice(objA.X - objB.X, objA.Y - objB.Y);
        }
    }
}
