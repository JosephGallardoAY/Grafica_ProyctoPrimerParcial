// ============================================================================
// Archivo: ClaseTransformaciones.cs
// Descripción: Fábrica estática de matrices de transformación geométrica 2D.
//              Provee métodos para crear matrices de traslación, rotación y
//              escalamiento, así como transformaciones compuestas alrededor
//              de puntos arbitrarios.
//
//              Transformaciones fundamentales en coordenadas homogéneas:
//
//              TRASLACIÓN (Tx, Ty):          ROTACIÓN (θ):
//              | 1  0  Tx |                  | cos(θ)  -sin(θ)  0 |
//              | 0  1  Ty |                  | sin(θ)   cos(θ)  0 |
//              | 0  0  1  |                  |   0        0     1 |
//
//              ESCALAMIENTO (Sx, Sy):
//              | Sx  0   0 |
//              | 0   Sy  0 |
//              | 0   0   1 |
//
// Proyecto: Simulador de Radar 2D - Computación Gráfica
// ============================================================================

using System;

namespace ProyectoPrimerParcial.Modelo
{
    /// <summary>
    /// Clase estática que provee métodos de fábrica para crear matrices de
    /// transformación geométrica 2D en coordenadas homogéneas 3x3.
    /// </summary>
    public static class ClaseTransformaciones
    {
        // ==================================================================
        // Transformaciones básicas
        // ==================================================================

        /// <summary>
        /// Crea una matriz de traslación 2D.
        /// 
        /// La traslación mueve un punto (x, y) a la posición (x + Tx, y + Ty).
        /// 
        /// Matriz de traslación:
        /// | 1  0  Tx |     | x |     | x + Tx |
        /// | 0  1  Ty |  ·  | y |  =  | y + Ty |
        /// | 0  0  1  |     | 1 |     |   1    |
        /// </summary>
        /// <param name="dblTx">Desplazamiento en el eje X.</param>
        /// <param name="dblTy">Desplazamiento en el eje Y.</param>
        /// <returns>Matriz de traslación 3x3.</returns>
        public static ClaseMatriz3x3 CrearTraslacion(double dblTx, double dblTy)
        {
            // Partir de la matriz identidad
            ClaseMatriz3x3 objMatriz = ClaseMatriz3x3.MatrizIdentidad();

            // Asignar los componentes de traslación en la tercera columna
            // M[0,2] = Tx (traslación en X)
            objMatriz.AsignarValor(0, 2, dblTx);
            // M[1,2] = Ty (traslación en Y)
            objMatriz.AsignarValor(1, 2, dblTy);

            return objMatriz;
        }

        /// <summary>
        /// Crea una matriz de rotación 2D alrededor del origen.
        /// 
        /// La rotación gira un punto alrededor del origen (0,0) un ángulo θ.
        /// 
        /// Fórmula de conversión: radianes = grados × (π / 180)
        /// 
        /// Matriz de rotación:
        /// | cos(θ)  -sin(θ)  0 |     | x |     | x·cos(θ) - y·sin(θ) |
        /// | sin(θ)   cos(θ)  0 |  ·  | y |  =  | x·sin(θ) + y·cos(θ) |
        /// |   0        0     1 |     | 1 |     |          1           |
        /// 
        /// Rotación positiva = sentido antihorario (convención matemática estándar).
        /// </summary>
        /// <param name="dblAnguloGrados">Ángulo de rotación en grados sexagesimales.</param>
        /// <returns>Matriz de rotación 3x3.</returns>
        public static ClaseMatriz3x3 CrearRotacion(double dblAnguloGrados)
        {
            // Convertir de grados a radianes: rad = grados × (π / 180)
            double dblAnguloRadianes = dblAnguloGrados * Math.PI / 180.0;

            // Calcular seno y coseno del ángulo
            double dblCoseno = Math.Cos(dblAnguloRadianes);
            double dblSeno = Math.Sin(dblAnguloRadianes);

            // Construir la matriz de rotación
            ClaseMatriz3x3 objMatriz = ClaseMatriz3x3.MatrizIdentidad();

            // Primera fila: [cos(θ), -sin(θ), 0]
            objMatriz.AsignarValor(0, 0, dblCoseno);
            objMatriz.AsignarValor(0, 1, -dblSeno);

            // Segunda fila: [sin(θ), cos(θ), 0]
            objMatriz.AsignarValor(1, 0, dblSeno);
            objMatriz.AsignarValor(1, 1, dblCoseno);

            return objMatriz;
        }

        /// <summary>
        /// Crea una matriz de escalamiento 2D respecto al origen.
        /// 
        /// El escalamiento multiplica las coordenadas por factores de escala.
        /// 
        /// Matriz de escalamiento:
        /// | Sx  0   0 |     | x |     | Sx · x |
        /// | 0   Sy  0 |  ·  | y |  =  | Sy · y |
        /// | 0   0   1 |     | 1 |     |   1    |
        /// 
        /// Si Sx = Sy → escalamiento uniforme (proporcional).
        /// Si Sx ≠ Sy → escalamiento no uniforme (distorsiona).
        /// Si Sx o Sy es negativo → reflexión en el eje correspondiente.
        /// </summary>
        /// <param name="dblSx">Factor de escala en el eje X.</param>
        /// <param name="dblSy">Factor de escala en el eje Y.</param>
        /// <returns>Matriz de escalamiento 3x3.</returns>
        public static ClaseMatriz3x3 CrearEscalamiento(double dblSx, double dblSy)
        {
            ClaseMatriz3x3 objMatriz = ClaseMatriz3x3.MatrizIdentidad();

            // M[0,0] = Sx (factor de escala en X)
            objMatriz.AsignarValor(0, 0, dblSx);
            // M[1,1] = Sy (factor de escala en Y)
            objMatriz.AsignarValor(1, 1, dblSy);

            return objMatriz;
        }

        // ==================================================================
        // Transformaciones compuestas
        // ==================================================================

        /// <summary>
        /// Crea una matriz compuesta para rotar alrededor de un punto arbitrario.
        /// 
        /// ALGORITMO (3 pasos combinados en una sola matriz):
        /// 1. Trasladar el centro de rotación al origen: T(-Cx, -Cy)
        /// 2. Rotar el ángulo deseado: R(θ)
        /// 3. Trasladar de vuelta a la posición original: T(+Cx, +Cy)
        /// 
        /// Matriz compuesta: M = T(Cx, Cy) · R(θ) · T(-Cx, -Cy)
        /// 
        /// IMPORTANTE: Las matrices se multiplican de derecha a izquierda,
        /// pero se aplican en el orden 1→2→3 al transformar un vértice.
        /// 
        /// Este método es CLAVE para el barrido del radar: el brazo del radar
        /// rota alrededor del centro del radar, que no necesariamente está
        /// en el origen del sistema de coordenadas.
        /// </summary>
        /// <param name="dblAnguloGrados">Ángulo de rotación en grados.</param>
        /// <param name="dblCentroX">Coordenada X del centro de rotación.</param>
        /// <param name="dblCentroY">Coordenada Y del centro de rotación.</param>
        /// <returns>Matriz compuesta de rotación alrededor de un punto.</returns>
        public static ClaseMatriz3x3 RotarAlrededorDePunto(double dblAnguloGrados,
                                                            double dblCentroX,
                                                            double dblCentroY)
        {
            // Paso 1: Crear matriz de traslación al origen T(-Cx, -Cy)
            ClaseMatriz3x3 objTraslacionAlOrigen = CrearTraslacion(-dblCentroX, -dblCentroY);

            // Paso 2: Crear matriz de rotación R(θ)
            ClaseMatriz3x3 objRotacion = CrearRotacion(dblAnguloGrados);

            // Paso 3: Crear matriz de traslación de regreso T(+Cx, +Cy)
            ClaseMatriz3x3 objTraslacionDeRegreso = CrearTraslacion(dblCentroX, dblCentroY);

            // Componer las matrices: M = T(Cx,Cy) · R(θ) · T(-Cx,-Cy)
            // Primero: R(θ) · T(-Cx,-Cy)
            ClaseMatriz3x3 objParcial = objRotacion.Multiplicar(objTraslacionAlOrigen);
            // Luego: T(Cx,Cy) · [R(θ) · T(-Cx,-Cy)]
            ClaseMatriz3x3 objResultado = objTraslacionDeRegreso.Multiplicar(objParcial);

            return objResultado;
        }

        /// <summary>
        /// Crea una matriz compuesta para escalar desde un centro arbitrario.
        /// 
        /// ALGORITMO (3 pasos combinados en una sola matriz):
        /// 1. Trasladar el centro de escalamiento al origen: T(-Cx, -Cy)
        /// 2. Escalar con los factores dados: S(Sx, Sy)
        /// 3. Trasladar de vuelta a la posición original: T(+Cx, +Cy)
        /// 
        /// Matriz compuesta: M = T(Cx, Cy) · S(Sx, Sy) · T(-Cx, -Cy)
        /// 
        /// Este método es CLAVE para el zoom del radar: permite ampliar o
        /// reducir la visualización manteniendo el centro del radar fijo.
        /// </summary>
        /// <param name="dblSx">Factor de escala en X.</param>
        /// <param name="dblSy">Factor de escala en Y.</param>
        /// <param name="dblCentroX">Coordenada X del centro de escalamiento.</param>
        /// <param name="dblCentroY">Coordenada Y del centro de escalamiento.</param>
        /// <returns>Matriz compuesta de escalamiento desde un punto.</returns>
        public static ClaseMatriz3x3 EscalarDesdeCentro(double dblSx, double dblSy,
                                                         double dblCentroX, double dblCentroY)
        {
            // Paso 1: Crear matriz de traslación al origen T(-Cx, -Cy)
            ClaseMatriz3x3 objTraslacionAlOrigen = CrearTraslacion(-dblCentroX, -dblCentroY);

            // Paso 2: Crear matriz de escalamiento S(Sx, Sy)
            ClaseMatriz3x3 objEscalamiento = CrearEscalamiento(dblSx, dblSy);

            // Paso 3: Crear matriz de traslación de regreso T(+Cx, +Cy)
            ClaseMatriz3x3 objTraslacionDeRegreso = CrearTraslacion(dblCentroX, dblCentroY);

            // Componer las matrices: M = T(Cx,Cy) · S(Sx,Sy) · T(-Cx,-Cy)
            // Primero: S(Sx,Sy) · T(-Cx,-Cy)
            ClaseMatriz3x3 objParcial = objEscalamiento.Multiplicar(objTraslacionAlOrigen);
            // Luego: T(Cx,Cy) · [S(Sx,Sy) · T(-Cx,-Cy)]
            ClaseMatriz3x3 objResultado = objTraslacionDeRegreso.Multiplicar(objParcial);

            return objResultado;
        }
    }
}
