// ============================================================================
// Archivo: ClaseMatriz3x3.cs
// Descripción: Matriz de transformación homogénea 3x3 para operaciones de
//              transformación geométrica en 2D (traslación, rotación, escalado).
//
//              Estructura de la matriz homogénea 3x3:
//              | a  b  tx |     donde: a, b, c, d = componentes de rotación/escala
//              | c  d  ty |            tx, ty     = componentes de traslación
//              | 0  0  1  |            última fila = [0, 0, 1] para homogéneas
//
// Proyecto: Simulador de Radar 2D - Computación Gráfica
// ============================================================================

using System;
using System.Text;

namespace ProyectoPrimerParcial.Modelo
{
    /// <summary>
    /// Clase que representa una matriz de transformación homogénea 3x3.
    /// Se utiliza para aplicar transformaciones geométricas (traslación,
    /// rotación, escalamiento) a vértices en coordenadas homogéneas 2D.
    /// </summary>
    public class ClaseMatriz3x3
    {
        // ==================================================================
        // Atributos privados
        // ==================================================================

        /// <summary>
        /// Arreglo bidimensional que almacena los 9 valores de la matriz 3x3.
        /// Indexación: arrValores[fila, columna] donde fila ∈ [0,2], columna ∈ [0,2].
        /// </summary>
        private double[,] arrValores;

        // ==================================================================
        // Constructor
        // ==================================================================

        /// <summary>
        /// Constructor por defecto. Inicializa la matriz con todos los valores en 0.
        /// Para obtener una matriz identidad, usar el método estático MatrizIdentidad().
        /// </summary>
        public ClaseMatriz3x3()
        {
            // Crear arreglo 3x3 inicializado en ceros
            arrValores = new double[3, 3];
        }

        // ==================================================================
        // Métodos de fábrica estáticos
        // ==================================================================

        /// <summary>
        /// Crea y retorna una matriz identidad 3x3.
        /// La matriz identidad es el elemento neutro de la multiplicación:
        /// 
        /// | 1  0  0 |
        /// | 0  1  0 |   =>  I · M = M · I = M
        /// | 0  0  1 |
        /// 
        /// Multiplicar cualquier matriz o vértice por la identidad no produce cambio.
        /// </summary>
        /// <returns>Nueva instancia de ClaseMatriz3x3 configurada como identidad.</returns>
        public static ClaseMatriz3x3 MatrizIdentidad()
        {
            ClaseMatriz3x3 objMatriz = new ClaseMatriz3x3();

            // Asignar 1 en la diagonal principal: posiciones [0,0], [1,1], [2,2]
            objMatriz.arrValores[0, 0] = 1.0;
            objMatriz.arrValores[1, 1] = 1.0;
            objMatriz.arrValores[2, 2] = 1.0;

            return objMatriz;
        }

        // ==================================================================
        // Métodos de acceso a valores
        // ==================================================================

        /// <summary>
        /// Obtiene el valor almacenado en la posición [fila, columna] de la matriz.
        /// </summary>
        /// <param name="entFila">Índice de fila (0, 1 o 2).</param>
        /// <param name="entColumna">Índice de columna (0, 1 o 2).</param>
        /// <returns>El valor double en la posición especificada.</returns>
        public double ObtenerValor(int entFila, int entColumna)
        {
            return arrValores[entFila, entColumna];
        }

        /// <summary>
        /// Asigna un valor en la posición [fila, columna] de la matriz.
        /// </summary>
        /// <param name="entFila">Índice de fila (0, 1 o 2).</param>
        /// <param name="entColumna">Índice de columna (0, 1 o 2).</param>
        /// <param name="dblValor">Valor a asignar.</param>
        public void AsignarValor(int entFila, int entColumna, double dblValor)
        {
            arrValores[entFila, entColumna] = dblValor;
        }

        // ==================================================================
        // Métodos de multiplicación
        // ==================================================================

        /// <summary>
        /// Multiplica esta matriz por otra matriz 3x3.
        /// 
        /// Fórmula de multiplicación de matrices:
        /// C[i,j] = Σ(k=0 hasta 2) A[i,k] · B[k,j]
        /// 
        /// Donde A = esta matriz, B = la otra matriz, C = resultado.
        /// 
        /// La multiplicación de matrices NO es conmutativa: A·B ≠ B·A
        /// El orden importa: las transformaciones se aplican de derecha a izquierda.
        /// </summary>
        /// <param name="objOtraMatriz">La matriz por la que se multiplica.</param>
        /// <returns>Nueva matriz resultado de la multiplicación.</returns>
        public ClaseMatriz3x3 Multiplicar(ClaseMatriz3x3 objOtraMatriz)
        {
            ClaseMatriz3x3 objResultado = new ClaseMatriz3x3();

            // Recorrer cada posición [i,j] de la matriz resultado
            for (int entFila = 0; entFila < 3; entFila++)
            {
                for (int entColumna = 0; entColumna < 3; entColumna++)
                {
                    // Calcular C[i,j] = A[i,0]·B[0,j] + A[i,1]·B[1,j] + A[i,2]·B[2,j]
                    double dblSuma = 0.0;
                    for (int entK = 0; entK < 3; entK++)
                    {
                        dblSuma += this.arrValores[entFila, entK] *
                                   objOtraMatriz.arrValores[entK, entColumna];
                    }
                    objResultado.arrValores[entFila, entColumna] = dblSuma;
                }
            }

            return objResultado;
        }

        /// <summary>
        /// Multiplica esta matriz por un vértice en coordenadas homogéneas.
        /// 
        /// Fórmula (multiplicación matriz × vector columna):
        /// | a  b  tx |   | x |     | a·x + b·y + tx·w |
        /// | c  d  ty | · | y |  =  | c·x + d·y + ty·w |
        /// | 0  0  1  |   | w |     | 0·x + 0·y + 1·w  |
        /// 
        /// Esto permite aplicar transformaciones geométricas al vértice.
        /// </summary>
        /// <param name="objVertice">El vértice a transformar.</param>
        /// <returns>Nuevo vértice con las coordenadas transformadas.</returns>
        public ClaseVertice Multiplicar(ClaseVertice objVertice)
        {
            // Extraer las componentes del vértice para mayor claridad
            double dblX = objVertice.X;
            double dblY = objVertice.Y;
            double dblW = objVertice.W;

            // Calcular cada componente del vértice resultado
            // X' = M[0,0]·X + M[0,1]·Y + M[0,2]·W
            double dblNuevoX = arrValores[0, 0] * dblX +
                               arrValores[0, 1] * dblY +
                               arrValores[0, 2] * dblW;

            // Y' = M[1,0]·X + M[1,1]·Y + M[1,2]·W
            double dblNuevoY = arrValores[1, 0] * dblX +
                               arrValores[1, 1] * dblY +
                               arrValores[1, 2] * dblW;

            // W' = M[2,0]·X + M[2,1]·Y + M[2,2]·W
            double dblNuevoW = arrValores[2, 0] * dblX +
                               arrValores[2, 1] * dblY +
                               arrValores[2, 2] * dblW;

            return new ClaseVertice(dblNuevoX, dblNuevoY, dblNuevoW);
        }

        // ==================================================================
        // Métodos de formato
        // ==================================================================

        /// <summary>
        /// Genera una representación textual legible de la matriz.
        /// Incluye el nombre de la matriz y sus valores formateados.
        /// </summary>
        /// <param name="strNombreMatriz">Nombre descriptivo de la matriz.</param>
        /// <returns>Cadena multilínea con la representación de la matriz.</returns>
        public string Formatear(string strNombreMatriz)
        {
            StringBuilder objTexto = new StringBuilder();

            objTexto.AppendLine(string.Format("=== {0} ===", strNombreMatriz));

            for (int entFila = 0; entFila < 3; entFila++)
            {
                objTexto.AppendLine(string.Format("| {0,10:F4}  {1,10:F4}  {2,10:F4} |",
                    arrValores[entFila, 0],
                    arrValores[entFila, 1],
                    arrValores[entFila, 2]));
            }

            return objTexto.ToString();
        }
    }
}
