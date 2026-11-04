// ============================================================================
// Archivo: ClaseObjetoAereo.cs
// Descripción: Representa un objeto aéreo detectado por el radar.
//              Cada objeto tiene posición (cartesiana y polar), velocidad,
//              dirección de movimiento y estado de detección.
//
//              El movimiento se calcula con las ecuaciones:
//                x_nuevo = x_actual + velocidad · cos(dirección)
//                y_nuevo = y_actual + velocidad · sin(dirección)
//
//              Las coordenadas polares se recalculan desde las cartesianas:
//                radio = √(x² + y²)
//                ángulo = atan2(y, x)
//
// Proyecto: Simulador de Radar 2D - Computación Gráfica
// ============================================================================

using System;
using System.Drawing;

namespace ProyectoPrimerParcial.Modelo
{
    /// <summary>
    /// Clase que representa un objeto aéreo rastreado por el simulador de radar.
    /// Contiene información de posición, movimiento, tipo y estado de detección.
    /// </summary>
    public class ClaseObjetoAereo
    {
        // ==================================================================
        // Propiedades de identificación
        // ==================================================================

        /// <summary>Identificador único del objeto aéreo (ej: "OBJ-001").</summary>
        public string strIdentificador { get; set; }

        /// <summary>Tipo de aeronave (ej: "Avión", "Helicóptero", "OVNI", "Dron").</summary>
        public string strTipoAeronave { get; set; }

        // ==================================================================
        // Propiedades de posición
        // ==================================================================

        /// <summary>
        /// Posición actual del objeto en coordenadas cartesianas (x, y).
        /// Es la representación principal de la posición.
        /// </summary>
        public ClaseVertice objPosicionCartesiana { get; set; }

        /// <summary>
        /// Distancia del objeto al centro del radar (coordenada polar r).
        /// Se calcula automáticamente desde las coordenadas cartesianas.
        /// Fórmula: r = √(x² + y²)
        /// </summary>
        public double dblRadio { get; set; }

        /// <summary>
        /// Ángulo del objeto respecto al eje X positivo (coordenada polar θ).
        /// Se calcula automáticamente desde las coordenadas cartesianas.
        /// Fórmula: θ = atan2(y, x) convertido a grados [0, 360).
        /// </summary>
        public double dblAnguloGrados { get; set; }

        // ==================================================================
        // Propiedades de movimiento
        // ==================================================================

        /// <summary>
        /// Velocidad del objeto en unidades por tick de simulación.
        /// Determina cuánto se desplaza el objeto en cada actualización.
        /// </summary>
        public double dblVelocidad { get; set; }

        /// <summary>
        /// Dirección de movimiento del objeto en grados sexagesimales.
        /// 0° = derecha (este), 90° = arriba (norte), 180° = izquierda (oeste),
        /// 270° = abajo (sur).
        /// </summary>
        public double dblDireccion { get; set; }

        /// <summary>Altitud simulada del objeto (para información de visualización).</summary>
        public double dblAltitud { get; set; }

        // ==================================================================
        // Propiedades de estado de detección
        // ==================================================================

        /// <summary>
        /// Indica si el objeto se encuentra actualmente dentro de la zona
        /// de detección del barrido del radar.
        /// </summary>
        public bool blnDetectado { get; set; }

        /// <summary>
        /// Cantidad de ticks de simulación transcurridos desde la última detección.
        /// Se usa para implementar el efecto de desvanecimiento (fading) del eco
        /// del radar: mientras más tiempo pase sin ser detectado, más tenue
        /// aparece el objeto en la pantalla.
        /// </summary>
        public int entTiempoDesdeDeteccion { get; set; }

        /// <summary>Color de visualización del objeto en la pantalla del radar.</summary>
        public Color colColor { get; set; }

        /// <summary>Historial de las últimas posiciones recorridas para dibujar estela de trayectoria.</summary>
        public System.Collections.Generic.List<ClaseVertice> lstHistorialPosiciones { get; set; }

        // ==================================================================
        // Constructor
        // ==================================================================

        /// <summary>
        /// Crea un nuevo objeto aéreo con los parámetros especificados.
        /// Las coordenadas polares se calculan automáticamente desde la posición
        /// cartesiana proporcionada.
        /// </summary>
        /// <param name="strIdentificador">Identificador único del objeto.</param>
        /// <param name="strTipoAeronave">Tipo de aeronave.</param>
        /// <param name="dblX">Posición X inicial (coordenada cartesiana).</param>
        /// <param name="dblY">Posición Y inicial (coordenada cartesiana).</param>
        /// <param name="dblVelocidad">Velocidad en unidades/tick.</param>
        /// <param name="dblDireccion">Dirección de movimiento en grados.</param>
        /// <param name="dblAltitud">Altitud simulada.</param>
        public ClaseObjetoAereo(string strIdentificador, string strTipoAeronave,
                                 double dblX, double dblY,
                                 double dblVelocidad, double dblDireccion,
                                 double dblAltitud)
        {
            this.strIdentificador = strIdentificador;
            this.strTipoAeronave = strTipoAeronave;

            // Crear el vértice de posición cartesiana
            this.objPosicionCartesiana = new ClaseVertice(dblX, dblY);

            this.dblVelocidad = dblVelocidad;
            this.dblDireccion = dblDireccion;
            this.dblAltitud = dblAltitud;

            // Estado inicial: no detectado, sin tiempo de detección previo
            this.blnDetectado = false;
            this.entTiempoDesdeDeteccion = 0;

            // Color por defecto: verde (típico de pantalla de radar)
            this.colColor = Color.Lime;

            // Inicializar historial de trayectoria
            this.lstHistorialPosiciones = new System.Collections.Generic.List<ClaseVertice>();

            // Calcular las coordenadas polares iniciales a partir de las cartesianas
            ActualizarCoordenadasPolares();
        }

        // ==================================================================
        // Métodos
        // ==================================================================

        /// <summary>
        /// Actualiza la posición del objeto aéreo según su velocidad y dirección.
        /// 
        /// FÓRMULAS DE MOVIMIENTO:
        /// 
        ///   x_nuevo = x_actual + velocidad · cos(dirección)
        ///   y_nuevo = y_actual + velocidad · sin(dirección)
        /// 
        ///   Donde:
        ///   - velocidad es la magnitud del desplazamiento por tick
        ///   - dirección es el ángulo de movimiento (en radianes para cos/sin)
        ///   - cos(dirección) da la componente horizontal del movimiento
        ///   - sin(dirección) da la componente vertical del movimiento
        ///   
        ///   Este es esencialmente un vector de desplazamiento descompuesto
        ///   en sus componentes X e Y usando trigonometría básica.
        /// 
        /// Después de actualizar la posición cartesiana, se recalculan las
        /// coordenadas polares automáticamente.
        /// </summary>
        public void ActualizarPosicion()
        {
            // Convertir la dirección de grados a radianes para las funciones trigonométricas
            double dblDireccionRadianes = dblDireccion * Math.PI / 180.0;

            // Calcular el desplazamiento en X: Δx = velocidad · cos(dirección)
            double dblDesplazamientoX = dblVelocidad * Math.Cos(dblDireccionRadianes);

            // Calcular el desplazamiento en Y: Δy = velocidad · sin(dirección)
            double dblDesplazamientoY = dblVelocidad * Math.Sin(dblDireccionRadianes);

            // Guardar posición anterior en el historial para dibujar la estela
            lstHistorialPosiciones.Add(new ClaseVertice(objPosicionCartesiana.X, objPosicionCartesiana.Y));
            if (lstHistorialPosiciones.Count > 10)
            {
                lstHistorialPosiciones.RemoveAt(0);
            }

            // Actualizar la posición: posición_nueva = posición_actual + desplazamiento
            objPosicionCartesiana.X += dblDesplazamientoX;
            objPosicionCartesiana.Y += dblDesplazamientoY;

            // Recalcular las coordenadas polares con la nueva posición
            ActualizarCoordenadasPolares();
        }

        /// <summary>
        /// Recalcula las coordenadas polares (radio, ángulo) a partir de la
        /// posición cartesiana actual.
        /// 
        /// FÓRMULAS UTILIZADAS:
        /// 
        ///   radio = √(x² + y²)                   → distancia al origen
        ///   ángulo = atan2(y, x) × (180/π)       → ángulo en grados
        ///   
        ///   Si el ángulo resulta negativo (atan2 retorna [-π, π]),
        ///   se suma 360° para normalizarlo al rango [0, 360).
        /// </summary>
        public void ActualizarCoordenadasPolares()
        {
            // Usar el método utilitario de ClaseCoordenadas para la conversión
            var (dblRadioCalculado, dblAnguloCalculado) =
                ClaseCoordenadas.CartesianasAPolares(
                    objPosicionCartesiana.X,
                    objPosicionCartesiana.Y);

            dblRadio = dblRadioCalculado;
            dblAnguloGrados = dblAnguloCalculado;
        }

        /// <summary>
        /// Genera una cadena formateada con toda la información del objeto aéreo.
        /// Incluye identificación, posición (cartesiana y polar), movimiento y estado.
        /// </summary>
        /// <returns>Cadena multilínea con los datos completos del objeto.</returns>
        public string ObtenerInformacion()
        {
            return string.Format(
                "═══════════════════════════════════════\n" +
                " Objeto Aéreo: {0}\n" +
                " Tipo: {1}\n" +
                "───────────────────────────────────────\n" +
                " Posición Cartesiana: ({2:F2}, {3:F2})\n" +
                " Posición Polar: r={4:F2}, θ={5:F2}°\n" +
                " Velocidad: {6:F2} u/tick\n" +
                " Dirección: {7:F2}°\n" +
                " Altitud: {8:F2}\n" +
                " Detectado: {9}\n" +
                " Ticks desde detección: {10}\n" +
                "═══════════════════════════════════════",
                strIdentificador,
                strTipoAeronave,
                objPosicionCartesiana.X, objPosicionCartesiana.Y,
                dblRadio, dblAnguloGrados,
                dblVelocidad,
                dblDireccion,
                dblAltitud,
                blnDetectado ? "SÍ" : "NO",
                entTiempoDesdeDeteccion);
        }
    }
}
