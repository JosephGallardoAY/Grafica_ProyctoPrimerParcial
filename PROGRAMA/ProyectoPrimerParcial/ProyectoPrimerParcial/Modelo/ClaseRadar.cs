// ============================================================================
// Archivo: ClaseRadar.cs
// Descripción: Motor principal de la simulación de radar 2D.
//              Gestiona el barrido rotatorio, la detección de objetos aéreos,
//              el zoom y los anillos concéntricos del radar.
//
//              El barrido del radar funciona así:
//              1. Un ángulo de barrido avanza en cada tick (gira en el sentido
//                 de las manecillas del reloj, o en sentido antihorario).
//              2. Se calcula un cono de detección con la amplitud del haz.
//              3. Los objetos dentro del cono y dentro del rango máximo son
//                 marcados como "detectados".
//              4. Los objetos no detectados incrementan su contador de fading.
//
//              Usa internamente las matrices de transformación para:
//              - Rotar el punto final del brazo de barrido
//              - Aplicar zoom desde el centro del radar
//
// Proyecto: Simulador de Radar 2D - Computación Gráfica
// ============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;

namespace ProyectoPrimerParcial.Modelo
{
    /// <summary>
    /// Clase principal que representa el simulador de radar 2D.
    /// Controla el barrido, la detección y la gestión de objetos aéreos.
    /// </summary>
    public class ClaseRadar
    {
        // ==================================================================
        // Atributos del radar
        // ==================================================================

        /// <summary>Radio máximo de detección del radar (alcance en unidades del mundo).</summary>
        public double dblRadioMaximo { get; set; }

        /// <summary>Número de anillos concéntricos que se dibujan en la pantalla del radar.</summary>
        public int entNumeroAnillos { get; set; }

        /// <summary>
        /// Ángulo actual del brazo de barrido en grados sexagesimales [0, 360).
        /// Avanza en cada tick de simulación según la velocidad de barrido.
        /// </summary>
        public double dblAnguloBarrido { get; set; }

        /// <summary>
        /// Velocidad de rotación del barrido en grados por tick.
        /// Valores típicos: 2-10 grados por tick.
        /// </summary>
        public double dblVelocidadBarrido { get; set; }

        /// <summary>
        /// Amplitud angular del haz de barrido en grados.
        /// Define el ancho del "cono" de detección. Un objeto es detectado
        /// si su ángulo cae dentro del rango:
        ///   [ánguloBarrido - amplitud/2, ánguloBarrido + amplitud/2]
        /// Valor por defecto: 30 grados.
        /// </summary>
        public double dblAmplitudBarrido { get; set; }

        /// <summary>
        /// Factor de escala para el zoom del radar.
        /// 1.0 = sin zoom, > 1.0 = acercamiento, menor que 1.0 = alejamiento.
        /// Se aplica usando la matriz de escalamiento desde el centro.
        /// </summary>
        public double dblEscalaZoom { get; set; }

        /// <summary>Indica si el barrido gira en sentido horario (true, estándar de radar) o antihorario (false).</summary>
        public bool blnSentidoHorario { get; set; }

        /// <summary>Distancia umbral para activar alerta de colisión/proximidad entre dos aeronaves.</summary>
        public double dblDistanciaAlertaProximidad { get; set; }

        /// <summary>Pares de objetos aéreos en conflicto de proximidad con su distancia calculada.</summary>
        public List<Tuple<ClaseObjetoAereo, ClaseObjetoAereo, double>> lstAlertasColision { get; set; }

        /// <summary>Lista de todos los objetos aéreos rastreados por el radar.</summary>
        public List<ClaseObjetoAereo> lstObjetosAereos { get; set; }

        /// <summary>
        /// Posición del centro del radar en coordenadas del mundo.
        /// Normalmente es (0, 0) pero puede ser diferente si el radar se
        /// desplaza a otra ubicación.
        /// </summary>
        public ClaseVertice objCentro { get; set; }

        // ==================================================================
        // Generador de números aleatorios
        // ==================================================================

        /// <summary>Instancia del generador aleatorio para crear objetos.</summary>
        private Random objAleatorio;

        // ==================================================================
        // Constructor
        // ==================================================================

        /// <summary>
        /// Crea una nueva instancia del simulador de radar con valores por defecto.
        /// </summary>
        /// <param name="dblRadioMaximo">Alcance máximo del radar (por defecto 200).</param>
        /// <param name="dblVelocidadBarrido">Velocidad de barrido en grados/tick (por defecto 3).</param>
        public ClaseRadar(double dblRadioMaximo = 200.0, double dblVelocidadBarrido = 3.0)
        {
            this.dblRadioMaximo = dblRadioMaximo;
            this.entNumeroAnillos = 5;
            this.dblAnguloBarrido = 0.0;
            this.dblVelocidadBarrido = dblVelocidadBarrido;
            this.dblAmplitudBarrido = 30.0;
            this.dblEscalaZoom = 1.0;
            this.blnSentidoHorario = true;
            this.dblDistanciaAlertaProximidad = 35.0;
            this.lstAlertasColision = new List<Tuple<ClaseObjetoAereo, ClaseObjetoAereo, double>>();
            this.lstObjetosAereos = new List<ClaseObjetoAereo>();
            this.objCentro = new ClaseVertice(0.0, 0.0);
            this.objAleatorio = new Random();
        }

        // ==================================================================
        // Métodos de barrido
        // ==================================================================

        /// <summary>
        /// Avanza el ángulo de barrido del radar en cada tick de simulación.
        /// 
        /// LÓGICA:
        ///   ánguloBarrido = (ánguloBarrido + velocidadBarrido) mod 360
        /// 
        /// Se usa la operación módulo para mantener el ángulo en [0, 360).
        /// Cuando el ángulo supera 360°, vuelve a empezar desde 0°.
        /// 
        /// Internamente, se utiliza la matriz de rotación para calcular
        /// el nuevo punto final del brazo de barrido, demostrando así
        /// la aplicación práctica de las transformaciones matriciales.
        /// </summary>
        public void ActualizarBarrido()
        {
            // Avanzar el ángulo según el sentido configurado (horario o antihorario)
            if (blnSentidoHorario)
            {
                dblAnguloBarrido -= dblVelocidadBarrido;
                if (dblAnguloBarrido < 0.0)
                {
                    dblAnguloBarrido += 360.0;
                }
            }
            else
            {
                dblAnguloBarrido += dblVelocidadBarrido;
                if (dblAnguloBarrido >= 360.0)
                {
                    dblAnguloBarrido -= 360.0;
                }
            }

            // DEMOSTRACIÓN DEL USO DE MATRICES:
            // Calcular el punto final del brazo de barrido usando la matriz
            // de rotación alrededor del centro del radar.
            ClaseMatriz3x3 objMatrizRotacion = ClaseTransformaciones.RotarAlrededorDePunto(
                blnSentidoHorario ? -dblVelocidadBarrido : dblVelocidadBarrido,
                objCentro.X, objCentro.Y);
        }

        // ==================================================================
        // Métodos de detección
        // ==================================================================

        /// <summary>
        /// Detecta qué objetos aéreos se encuentran dentro del haz de barrido
        /// utilizando ALGEBRA VECTORIAL PURA (Producto Punto y Norma Euclidiana).
        /// 
        /// FÓRMULAS MATEMÁTICAS APLICADAS:
        /// 1. Vector unitario del haz del radar:
        ///    û_haz = (cos(θ_haz), sin(θ_haz))
        /// 
        /// 2. Vector posición relativo del blanco:
        ///    r⃗ = (x_obj - x_centro, y_obj - y_centro)
        ///    Norma: ||r⃗|| = √(rx² + ry²)
        /// 
        /// 3. Vector unitario hacia el blanco:
        ///    û_blanco = r⃗ / ||r⃗||
        /// 
        /// 4. Producto Punto (Criterio angular del haz sin trigonometría inversa):
        ///    cos(Δθ) = û_haz · û_blanco
        ///    El objeto está en el haz si: cos(Δθ) >= cos(Amplitud / 2)
        /// 
        /// 5. Criterio de alcance:
        ///    ||r⃗|| <= RadioMaximo
        /// 
        /// Si el objeto es detectado:
        ///   - blnDetectado = true
        ///   - entTiempoDesdeDeteccion = 0 (se reinicia el contador de fading)
        /// Si no es detectado:
        ///   - blnDetectado = false
        ///   - entTiempoDesdeDeteccion++ (efecto fading de fósforo)
        /// </summary>
        public void DetectarObjetos()
        {
            // Vector unitario del haz de barrido actual
            double dblRadHaz = dblAnguloBarrido * Math.PI / 180.0;
            ClaseVertice objUHaz = new ClaseVertice(Math.Cos(dblRadHaz), Math.Sin(dblRadHaz));

            // Umbral del coseno para la mitad de la amplitud del haz
            double dblCosUmbral = Math.Cos((dblAmplitudBarrido / 2.0) * Math.PI / 180.0);

            foreach (ClaseObjetoAereo objAereo in lstObjetosAereos)
            {
                // Vector posición relativo al centro del radar
                ClaseVertice objPosRel = new ClaseVertice(
                    objAereo.objPosicionCartesiana.X - objCentro.X,
                    objAereo.objPosicionCartesiana.Y - objCentro.Y);

                // Criterio 1: Norma ||r⃗|| <= RadioMaximo (Rango Euclidiano)
                double dblDistancia = ClaseAlgebraVectorial.Magnitud(objPosRel);
                bool blnDentroDeRango = dblDistancia <= dblRadioMaximo;

                // Criterio 2: Producto Punto con vector unitario del blanco
                ClaseVertice objUBlanco = ClaseAlgebraVectorial.Normalizar(objPosRel);
                double dblProdPunto = ClaseAlgebraVectorial.ProductoPunto(objUHaz, objUBlanco);

                bool blnDentroDeHaz = dblProdPunto >= dblCosUmbral;

                // Aplicar resultado de detección
                if (blnDentroDeRango && blnDentroDeHaz)
                {
                    objAereo.blnDetectado = true;
                    objAereo.entTiempoDesdeDeteccion = 0;
                }
                else
                {
                    objAereo.blnDetectado = false;
                    objAereo.entTiempoDesdeDeteccion++;
                }
            }
        }

        // ==================================================================
        // Gestión de objetos aéreos
        // ==================================================================

        /// <summary>
        /// Agrega un nuevo objeto aéreo a la lista de rastreo del radar.
        /// </summary>
        /// <param name="objNuevoObjeto">El objeto aéreo a agregar.</param>
        public void AgregarObjeto(ClaseObjetoAereo objNuevoObjeto)
        {
            lstObjetosAereos.Add(objNuevoObjeto);
        }

        /// <summary>
        /// Elimina un objeto aéreo de la lista de rastreo por su identificador.
        /// </summary>
        /// <param name="strId">Identificador del objeto a eliminar.</param>
        public void EliminarObjeto(string strId)
        {
            // Buscar y remover el objeto con el identificador especificado
            lstObjetosAereos.RemoveAll(
                objAereo => objAereo.strIdentificador == strId);
        }

        /// <summary>
        /// Genera objetos aéreos aleatorios dentro del rango del radar para
        /// poblar la simulación con tráfico aéreo.
        /// 
        /// Cada objeto se genera con:
        /// - Posición aleatoria dentro del radio máximo (usando coordenadas polares
        ///   convertidas a cartesianas para distribución uniforme dentro del círculo)
        /// - Tipo de aeronave aleatorio
        /// - Velocidad aleatoria entre 0.5 y 3.0 unidades/tick
        /// - Dirección aleatoria entre 0° y 360°
        /// - Altitud aleatoria entre 1000 y 12000
        /// </summary>
        /// <param name="entCantidad">Número de objetos aleatorios a generar.</param>
        public void GenerarObjetosAleatorios(int entCantidad)
        {
            // Tipos de aeronave disponibles para asignación aleatoria
            string[] arrTipos = { "Avión", "Helicóptero", "OVNI", "Dron" };

            // Colores disponibles para diferenciación visual
            Color[] arrColores = { Color.Lime, Color.Cyan, Color.Yellow,
                                   Color.Orange, Color.Red, Color.Magenta };

            for (int entI = 0; entI < entCantidad; entI++)
            {
                // Generar posición aleatoria usando coordenadas polares
                // para lograr distribución uniforme dentro del círculo del radar.
                // Se usa raíz cuadrada del radio aleatorio para evitar
                // concentración de puntos cerca del centro:
                //   r = radioMax × √(aleatorio)  → distribución uniforme en área
                double dblRadioAleatorio = dblRadioMaximo * Math.Sqrt(objAleatorio.NextDouble());
                double dblAnguloAleatorio = objAleatorio.NextDouble() * 360.0;

                // Convertir de polares a cartesianas: x = r·cos(θ), y = r·sin(θ)
                var (dblX, dblY) = ClaseCoordenadas.PolaresACartesianas(
                    dblRadioAleatorio, dblAnguloAleatorio);

                // Generar propiedades aleatorias del objeto
                string strId = string.Format("OBJ-{0:D3}", entI + 1);
                string strTipo = arrTipos[objAleatorio.Next(arrTipos.Length)];
                double dblVelocidad = 0.5 + objAleatorio.NextDouble() * 2.5;  // Rango [0.5, 3.0]
                double dblDireccion = objAleatorio.NextDouble() * 360.0;       // Rango [0, 360)
                double dblAltitud = 1000 + objAleatorio.NextDouble() * 11000;  // Rango [1000, 12000]

                // Crear el objeto aéreo con las propiedades generadas
                ClaseObjetoAereo objNuevo = new ClaseObjetoAereo(
                    strId, strTipo, dblX, dblY, dblVelocidad, dblDireccion, dblAltitud);

                // Asignar un color aleatorio para diferenciación visual
                objNuevo.colColor = arrColores[objAleatorio.Next(arrColores.Length)];

                // Agregar a la lista de rastreo
                lstObjetosAereos.Add(objNuevo);
            }
        }

        // ==================================================================
        // Simulación
        // ==================================================================

        /// <summary>
        /// Ejecuta un tick completo de la simulación del radar.
        /// 
        /// SECUENCIA DE ACTUALIZACIÓN:
        /// 1. Avanzar el barrido del radar (rotar el brazo)
        /// 2. Actualizar la posición de cada objeto aéreo (movimiento)
        /// 3. Detectar objetos dentro del haz de barrido
        /// 
        /// Este método es llamado periódicamente por el Timer del formulario.
        /// </summary>
        public void ActualizarSimulacion()
        {
            // Paso 1: Avanzar el barrido del radar
            ActualizarBarrido();

            // Paso 2: Actualizar la posición de cada objeto aéreo
            foreach (ClaseObjetoAereo objAereo in lstObjetosAereos)
            {
                objAereo.ActualizarPosicion();
            }

            // Paso 3: Detectar objetos dentro del haz de barrido
            DetectarObjetos();

            // Paso 4: Evaluar distancias de proximidad para alerta de colisión
            VerificarColisiones();
        }

        /// <summary>
        /// Comprueba las distancias euclidianas entre todos los pares de aeronaves
        /// para alertar sobre posibles riesgos de colisión en el espacio aéreo.
        /// </summary>
        public void VerificarColisiones()
        {
            lstAlertasColision.Clear();
            for (int entI = 0; entI < lstObjetosAereos.Count; entI++)
            {
                for (int entJ = entI + 1; entJ < lstObjetosAereos.Count; entJ++)
                {
                    double dblDist = ClaseCoordenadas.CalcularDistancia(
                        lstObjetosAereos[entI].objPosicionCartesiana,
                        lstObjetosAereos[entJ].objPosicionCartesiana);

                    if (dblDist <= dblDistanciaAlertaProximidad)
                    {
                        lstAlertasColision.Add(Tuple.Create(
                            lstObjetosAereos[entI], lstObjetosAereos[entJ], dblDist));
                    }
                }
            }
        }

        // ==================================================================
        // Obtención de geometría para dibujo
        // ==================================================================

        /// <summary>
        /// Calcula y retorna el vértice del punto final del brazo de barrido.
        /// 
        /// FÓRMULA:
        ///   x = centroX + radioMaximo · cos(ánguloBarrido)
        ///   y = centroY + radioMaximo · sin(ánguloBarrido)
        /// 
        /// Primero se calcula el punto en coordenadas polares (r, θ) y luego
        /// se convierte a cartesianas. El punto resultante está sobre el
        /// perímetro del círculo del radar, a la posición del ángulo de barrido.
        /// </summary>
        /// <returns>Vértice con la posición del extremo del brazo de barrido.</returns>
        public ClaseVertice ObtenerVerticeBarrido()
        {
            // Convertir la posición polar del brazo a coordenadas cartesianas
            // usando las fórmulas: x = r·cos(θ), y = r·sin(θ)
            var (dblX, dblY) = ClaseCoordenadas.PolaresACartesianas(
                dblRadioMaximo, dblAnguloBarrido);

            // Sumar la posición del centro del radar (traslación)
            return new ClaseVertice(objCentro.X + dblX, objCentro.Y + dblY);
        }

        /// <summary>
        /// Aplica un factor de zoom al radar usando la matriz de escalamiento
        /// desde el centro del radar.
        /// 
        /// La escala se multiplica por el factor proporcionado:
        ///   nuevaEscala = escalaActual × factorZoom
        /// 
        /// Internamente se demuestra el uso de la matriz de escalamiento desde
        /// un centro, que es una transformación compuesta:
        ///   M = T(Cx,Cy) · S(factor,factor) · T(-Cx,-Cy)
        /// 
        /// Se limita el zoom a un rango razonable [0.1, 10.0] para evitar
        /// escalas extremas que dificulten la visualización.
        /// </summary>
        /// <param name="dblFactorZoom">Factor multiplicativo de zoom (ej: 1.1 para +10%).</param>
        public void AplicarZoom(double dblFactorZoom)
        {
            // Calcular la nueva escala
            double dblNuevaEscala = dblEscalaZoom * dblFactorZoom;

            // Limitar el zoom a un rango razonable [0.1, 10.0]
            if (dblNuevaEscala < 0.1) dblNuevaEscala = 0.1;
            if (dblNuevaEscala > 10.0) dblNuevaEscala = 10.0;

            dblEscalaZoom = dblNuevaEscala;

            // DEMOSTRACIÓN DEL USO DE MATRICES DE ESCALAMIENTO:
            // Crear la matriz de escalamiento desde el centro del radar.
            // Esta matriz puede ser utilizada por la capa de vista para
            // transformar todos los puntos de la pantalla del radar.
            ClaseMatriz3x3 objMatrizZoom = ClaseTransformaciones.EscalarDesdeCentro(
                dblFactorZoom, dblFactorZoom,
                objCentro.X, objCentro.Y);

            // La matriz objMatrizZoom está disponible si se necesita transformar
            // múltiples vértices con la misma operación de zoom.
        }

        /// <summary>
        /// Calcula y retorna los radios de los anillos concéntricos del radar.
        /// 
        /// Los anillos están distribuidos uniformemente entre 0 y el radio máximo:
        ///   radio_i = radioMaximo × (i + 1) / numAnillos
        /// 
        /// Ejemplo con 5 anillos y radioMaximo = 200:
        ///   Anillo 1: 200 × 1/5 = 40
        ///   Anillo 2: 200 × 2/5 = 80
        ///   Anillo 3: 200 × 3/5 = 120
        ///   Anillo 4: 200 × 4/5 = 160
        ///   Anillo 5: 200 × 5/5 = 200 (borde exterior)
        /// </summary>
        /// <returns>Arreglo de doubles con los radios de cada anillo.</returns>
        public double[] ObtenerRadiosAnillos()
        {
            double[] arrRadios = new double[entNumeroAnillos];

            for (int entI = 0; entI < entNumeroAnillos; entI++)
            {
                // Calcular el radio del anillo i (distribución uniforme)
                // radio_i = radioMaximo × (i + 1) / numAnillos
                arrRadios[entI] = dblRadioMaximo * (entI + 1.0) / entNumeroAnillos;
            }

            return arrRadios;
        }
    }
}
