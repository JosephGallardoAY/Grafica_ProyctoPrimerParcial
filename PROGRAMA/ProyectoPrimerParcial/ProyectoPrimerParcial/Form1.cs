// ============================================================================
// Archivo: Form1.cs
// Descripción: Consola Táctica de Simulación de Radar 2D.
//              Enfoque minimalista, académico y de alto impacto gráfico:
//              - Radar protagonista interactivo (70% del área de pantalla)
//              - Álgebra Vectorial completa (Magnitud, Vector Unitario, Producto Punto, Producto Cruz 2D)
//              - Transformaciones Homogéneas 3x3 (Rotación, Escalamiento, Traslación)
//              - Inspector Vectorial con Target Lock y descomposición visual sobre el radar
//              - Escenarios rápidos para evaluación académica
//
// Asignatura: Computación Gráfica
// Carrera: Ingeniería en Tecnologías de la Información - ESPE
// ============================================================================

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using ProyectoPrimerParcial.Modelo;
using ProyectoPrimerParcial.Vista;

namespace ProyectoPrimerParcial
{
    public partial class Form1 : Form
    {
        private ClaseRadar objRadar;
        private bool blnSimulacionActiva;
        private int entContadorTicks;
        private ClaseObjetoAereo objSeleccionado;
        private int entSecuenciaId;

        public Form1()
        {
            InitializeComponent();

            this.objRadar = new ClaseRadar(200.0, 3.0);
            this.blnSimulacionActiva = true;
            this.entContadorTicks = 0;
            this.entSecuenciaId = 1;

            ConfigurarControles();
            ConectarEventos();
            CargarEscenarioPatrulla();

            this.timerSimulacion.Start();
        }

        private void ConfigurarControles()
        {
            this.cmbSeleccionarMatriz.SelectedIndex = 0;
            this.chkSentidoHorario.Checked = objRadar.blnSentidoHorario;
            this.trbVelocidadBarrido.Value = (int)Math.Max(1, Math.Min(12, objRadar.dblVelocidadBarrido));
            this.lblVelocidadValor.Text = string.Format(CultureInfo.InvariantCulture, "{0:F1}°/t", objRadar.dblVelocidadBarrido);
            ActualizarIndicadorZoom();
        }

        private void ConectarEventos()
        {
            // Panel de radar
            this.pnlRadar.Paint += new PaintEventHandler(PnlRadar_Paint);
            this.pnlRadar.MouseMove += new MouseEventHandler(PnlRadar_MouseMove);
            this.pnlRadar.MouseClick += new MouseEventHandler(PnlRadar_MouseClick);
            this.pnlRadar.MouseWheel += new MouseEventHandler(PnlRadar_MouseWheel);
            this.pnlRadar.Resize += (s, e) => this.pnlRadar.Invalidate();

            // Timer
            this.timerSimulacion.Tick += new EventHandler(TimerSimulacion_Tick);

            // Controles de simulación
            this.btnIniciarPausar.Click += new EventHandler(BtnIniciarPausar_Click);
            this.btnReiniciar.Click += (s, e) =>
            {
                this.objRadar.dblAnguloBarrido = 0.0;
                this.pnlRadar.Invalidate();
                ActualizarPanelMatrices();
            };
            this.chkSentidoHorario.CheckedChanged += (s, e) =>
            {
                this.objRadar.blnSentidoHorario = this.chkSentidoHorario.Checked;
                this.pnlRadar.Invalidate();
            };

            // Velocidad de barrido
            this.trbVelocidadBarrido.Scroll += (s, e) =>
            {
                this.objRadar.dblVelocidadBarrido = this.trbVelocidadBarrido.Value;
                this.lblVelocidadValor.Text = string.Format(CultureInfo.InvariantCulture, "{0:F1}°/t", this.objRadar.dblVelocidadBarrido);
            };

            // Zoom
            this.btnZoomMas.Click += (s, e) =>
            {
                this.objRadar.AplicarZoom(1.10);
                ActualizarIndicadorZoom();
                this.pnlRadar.Invalidate();
            };
            this.btnZoomMenos.Click += (s, e) =>
            {
                this.objRadar.AplicarZoom(0.90);
                ActualizarIndicadorZoom();
                this.pnlRadar.Invalidate();
            };
            this.btnZoomReset.Click += (s, e) =>
            {
                this.objRadar.dblEscalaZoom = 1.0;
                ActualizarIndicadorZoom();
                this.pnlRadar.Invalidate();
            };

            // Rango rápido (chips)
            this.btnRango50.Click += (s, e) => CambiarRango(100.0);
            this.btnRango100.Click += (s, e) => CambiarRango(200.0);
            this.btnRango200.Click += (s, e) => CambiarRango(300.0);

            // Registro manual con validación de entrada
            this.btnNuevoManual.Click += (s, e) =>
            {
                using (FormAgregarObjeto frm = new FormAgregarObjeto(objRadar.dblRadioMaximo, objRadar.lstObjetosAereos))
                {
                    if (frm.ShowDialog(this) == DialogResult.OK && frm.ObjetoCreado != null)
                    {
                        this.objRadar.AgregarObjeto(frm.ObjetoCreado);
                        this.objSeleccionado = frm.ObjetoCreado;
                        MostrarAnalisisVectorial(frm.ObjetoCreado);
                        ActualizarPanelMatrices();
                        this.pnlRadar.Invalidate();
                    }
                }
            };

            // Escenarios y gestión de objetos
            this.btnGenerarAleatorios.Click += (s, e) =>
            {
                this.objRadar.GenerarObjetosAleatorios(3);
                this.pnlRadar.Invalidate();
            };
            this.btnEscenarioPatrulla.Click += (s, e) => CargarEscenarioPatrulla();
            this.btnEscenarioInterceptacion.Click += (s, e) => CargarEscenarioInterceptacion();
            this.btnLimpiarObjetos.Click += (s, e) =>
            {
                this.objRadar.lstObjetosAereos.Clear();
                this.objSeleccionado = null;
                this.lblEstadoLock.Text = "[SIN BLANCO]";
                this.txtAnalisisVectorial.Text = "No hay aeronaves en seguimiento. Haga clic en 'Demo: Patrulla' o 'Tráfico' para iniciar.";
                this.pnlRadar.Invalidate();
            };

            // Captura de pantalla PNG
            this.btnCapturarPantalla.Click += new EventHandler(BtnCapturarPantalla_Click);

            // Selector de matriz
            this.cmbSeleccionarMatriz.SelectedIndexChanged += (s, e) => ActualizarPanelMatrices();
        }

        private void CambiarRango(double dblNuevoRango)
        {
            this.objRadar.dblRadioMaximo = dblNuevoRango;
            this.pnlRadar.Invalidate();
        }

        // ==================================================================
        // Escenarios Demostrativos para Evaluación
        // ==================================================================

        private void CargarEscenarioPatrulla()
        {
            objRadar.lstObjetosAereos.Clear();

            // Avión Alfa (Comercial / Patrulla)
            ClaseObjetoAereo a1 = new ClaseObjetoAereo("PATRULLA-01", "Avión", 85.0, 60.0, 1.2, 135.0, 7500.0);
            a1.colColor = Color.FromArgb(52, 211, 153); // Emerald
            objRadar.AgregarObjeto(a1);

            // Helicóptero Bravo (Rescate)
            ClaseObjetoAereo a2 = new ClaseObjetoAereo("RESCATE-02", "Helicóptero", -90.0, 70.0, 0.8, 45.0, 2200.0);
            a2.colColor = Color.FromArgb(56, 189, 248); // Sky
            objRadar.AgregarObjeto(a2);

            // Dron Charlie (Vigilancia)
            ClaseObjetoAereo a3 = new ClaseObjetoAereo("DRON-03", "Dron", -60.0, -80.0, 1.5, 310.0, 1200.0);
            a3.colColor = Color.FromArgb(250, 204, 21); // Yellow
            objRadar.AgregarObjeto(a3);

            // Seleccionar a1 por defecto para demostración inmediata
            objSeleccionado = a1;
            MostrarAnalisisVectorial(a1);
            pnlRadar.Invalidate();
        }

        private void CargarEscenarioInterceptacion()
        {
            objRadar.lstObjetosAereos.Clear();

            // Dos aeronaves en ruta de convergencia para activar alerta de colisión matemática
            ClaseObjetoAereo a1 = new ClaseObjetoAereo("INTERCEPTOR", "Avión", -70.0, 20.0, 1.8, 10.0, 5000.0);
            a1.colColor = Color.FromArgb(56, 189, 248);
            objRadar.AgregarObjeto(a1);

            ClaseObjetoAereo a2 = new ClaseObjetoAereo("INTRUSO-X", "OVNI", 70.0, 25.0, 1.8, 190.0, 5100.0);
            a2.colColor = Color.FromArgb(244, 63, 94); // Rose
            objRadar.AgregarObjeto(a2);

            objSeleccionado = a1;
            MostrarAnalisisVectorial(a1);
            pnlRadar.Invalidate();
        }

        // ==================================================================
        // Bucle de Simulación
        // ==================================================================

        private void TimerSimulacion_Tick(object sender, EventArgs e)
        {
            if (blnSimulacionActiva)
            {
                objRadar.ActualizarSimulacion();
                pnlRadar.Invalidate();
                ActualizarPanelMatrices();

                // Banner de colisión
                if (objRadar.lstAlertasColision != null && objRadar.lstAlertasColision.Count > 0)
                {
                    pnlBannerAlerta.Visible = true;
                    lblBannerAlerta.Text = string.Format(
                        "⚠️ CONFLICTO DE PROXIMIDAD: {0} PAR(ES) A MENOS DE {1:F0}u",
                        objRadar.lstAlertasColision.Count, objRadar.dblDistanciaAlertaProximidad);
                }
                else
                {
                    pnlBannerAlerta.Visible = false;
                }

                // Barra de estado
                lblStatusBarrido.Text = string.Format(CultureInfo.InvariantCulture,
                    "📡 Haz: {0:F1}° ({1})",
                    objRadar.dblAnguloBarrido, objRadar.blnSentidoHorario ? "CW ↻" : "CCW ↺");

                int entDetectados = 0;
                foreach (var obj in objRadar.lstObjetosAereos)
                {
                    if (obj.blnDetectado) entDetectados++;
                }

                lblStatusObjetos.Text = string.Format("🎯 Blancos: {0} (Detectados: {1})",
                    objRadar.lstObjetosAereos.Count, entDetectados);

                // Actualizar telemetría vectorial periódicamente
                entContadorTicks++;
                if (entContadorTicks % 3 == 0 && objSeleccionado != null)
                {
                    MostrarAnalisisVectorial(objSeleccionado);
                }
            }
        }

        // ==================================================================
        // Renderizado del Radar (GDI+)
        // ==================================================================

        private void PnlRadar_Paint(object sender, PaintEventArgs e)
        {
            ClaseRenderizadorRadar.Renderizar(
                e.Graphics,
                objRadar,
                pnlRadar.ClientSize.Width,
                pnlRadar.ClientSize.Height,
                objSeleccionado?.strIdentificador,
                true,  // Ver vectores
                true,  // Ver estelas
                true,  // Ver brújula
                true   // Ver alertas
            );
        }

        // ==================================================================
        // Interacción con el Mouse sobre el Radar
        // ==================================================================

        private void PnlRadar_MouseMove(object sender, MouseEventArgs e)
        {
            int entAncho = pnlRadar.ClientSize.Width;
            int entAlto = pnlRadar.ClientSize.Height;
            int entMargen = 46;
            float fltRadioDisp = Math.Min(entAncho, entAlto) / 2.0f - entMargen;
            double dblEscala = (fltRadioDisp / objRadar.dblRadioMaximo) * objRadar.dblEscalaZoom;

            double dblCentroX = entAncho / 2.0;
            double dblCentroY = entAlto / 2.0;

            var (dblMundoX, dblMundoY) = ClaseCoordenadas.PantallaAMundo(
                e.X, e.Y, dblCentroX, dblCentroY, dblEscala);

            var (dblRadio, dblAngulo) = ClaseCoordenadas.CartesianasAPolares(dblMundoX, dblMundoY);

            lblStatusCursor.Text = string.Format(CultureInfo.InvariantCulture,
                "📍 Cursor: X: {0:F1}, Y: {1:F1} | r: {2:F1} u, θ: {3:F1}°",
                dblMundoX, dblMundoY, dblRadio, dblAngulo);
        }

        private void PnlRadar_MouseClick(object sender, MouseEventArgs e)
        {
            pnlRadar.Focus();

            int entAncho = pnlRadar.ClientSize.Width;
            int entAlto = pnlRadar.ClientSize.Height;
            int entMargen = 46;
            float fltRadioDisp = Math.Min(entAncho, entAlto) / 2.0f - entMargen;
            double dblEscala = (fltRadioDisp / objRadar.dblRadioMaximo) * objRadar.dblEscalaZoom;
            double dblCentroX = entAncho / 2.0;
            double dblCentroY = entAlto / 2.0;

            var (dblMundoX, dblMundoY) = ClaseCoordenadas.PantallaAMundo(
                e.X, e.Y, dblCentroX, dblCentroY, dblEscala);

            ClaseVertice objClic = new ClaseVertice(dblMundoX, dblMundoY);

            // Búsqueda del objeto más cercano
            double dblTolerancia = 20.0 / objRadar.dblEscalaZoom;
            ClaseObjetoAereo objEncontrado = null;
            double dblMenorDistancia = double.MaxValue;

            foreach (var obj in objRadar.lstObjetosAereos)
            {
                double dblDist = ClaseCoordenadas.CalcularDistancia(objClic, obj.objPosicionCartesiana);
                if (dblDist <= dblTolerancia && dblDist < dblMenorDistancia)
                {
                    dblMenorDistancia = dblDist;
                    objEncontrado = obj;
                }
            }

            if (objEncontrado != null)
            {
                // Activar Target Lock
                objSeleccionado = objEncontrado;
                MostrarAnalisisVectorial(objEncontrado);
                ActualizarPanelMatrices();
                pnlRadar.Invalidate();
            }
            else
            {
                // Clic en espacio vacío: agregar un nuevo objeto en ese punto exacto
                entSecuenciaId++;
                string strNuevoId = string.Format("BLANCO-{0:D2}", entSecuenciaId);
                double dblVel = 1.0 + new Random().NextDouble() * 1.5;
                double dblRumbo = new Random().Next(0, 360);
                ClaseObjetoAereo objNuevo = new ClaseObjetoAereo(
                    strNuevoId, "Avión", dblMundoX, dblMundoY, dblVel, dblRumbo, 4000.0);
                objNuevo.colColor = Color.FromArgb(56, 189, 248);

                objRadar.AgregarObjeto(objNuevo);
                objSeleccionado = objNuevo;
                MostrarAnalisisVectorial(objNuevo);
                pnlRadar.Invalidate();
            }
        }

        private void PnlRadar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                objRadar.AplicarZoom(1.08);
            }
            else if (e.Delta < 0)
            {
                objRadar.AplicarZoom(0.92);
            }

            ActualizarIndicadorZoom();
            pnlRadar.Invalidate();
        }

        // ==================================================================
        // Tarjeta 2: Sustentación Matemática de Álgebra Vectorial
        // ==================================================================

        private void MostrarAnalisisVectorial(ClaseObjetoAereo obj)
        {
            lblEstadoLock.Text = string.Format("[LOCKED: {0}]", obj.strIdentificador);

            // Cálculos vectoriales formales del Primer Parcial
            ClaseVertice vPos = obj.objPosicionCartesiana;
            double dblNorma = ClaseAlgebraVectorial.Magnitud(vPos);
            ClaseVertice vUnitario = ClaseAlgebraVectorial.Normalizar(vPos);

            // Vector unitario del haz del radar en este instante
            double dblRadHaz = objRadar.dblAnguloBarrido * Math.PI / 180.0;
            ClaseVertice vHaz = new ClaseVertice(Math.Cos(dblRadHaz), Math.Sin(dblRadHaz));

            // Producto Punto: û_haz · û_obj = cos(α)
            double dblProdPunto = ClaseAlgebraVectorial.ProductoPunto(vHaz, vUnitario);
            double dblAnguloConHaz = Math.Acos(Math.Max(-1.0, Math.Min(1.0, dblProdPunto))) * 180.0 / Math.PI;

            // Producto Cruz 2D (Determinante): û_haz × û_obj = sin(α)
            double dblProdCruz = ClaseAlgebraVectorial.ProductoCruz2D(vHaz, vUnitario);

            // Descomposición del vector velocidad: v⃗ = (vx, vy)
            double dblRadRumbo = obj.dblDireccion * Math.PI / 180.0;
            double dblVx = obj.dblVelocidad * Math.Cos(dblRadRumbo);
            double dblVy = obj.dblVelocidad * Math.Sin(dblRadRumbo);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("BLANCO: {0} | Tipo: {1}", obj.strIdentificador, obj.strTipoAeronave));
            sb.AppendLine("──────────────────────────────────────────────────");
            sb.AppendLine("1. VECTOR POSICIÓN (r⃗) Y NORMA:");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   r⃗ = ({0:F2})î + ({1:F2})ĵ", vPos.X, vPos.Y));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   ||r⃗|| = √(x² + y²) = {0:F2} unidades", dblNorma));
            sb.AppendLine();
            sb.AppendLine("2. VECTOR UNITARIO DE DIRECCIÓN (û):");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   û = r⃗ / ||r⃗|| = ({0:F3})î + ({1:F3})ĵ", vUnitario.X, vUnitario.Y));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   Coordenadas Polares: r = {0:F1} u,  θ = {1:F1}°", obj.dblRadio, obj.dblAnguloGrados));
            sb.AppendLine();
            sb.AppendLine("3. PRODUCTO PUNTO Y DETECCIÓN DEL HAZ:");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   û_haz · û_blanco = {0:F3}  →  cos(Δθ)", dblProdPunto));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   Diferencia angular: Δθ = {0:F1}° (Amplitud Haz = {1:F0}°)", dblAnguloConHaz, objRadar.dblAmplitudBarrido));
            sb.AppendLine(string.Format("   Estado en el haz: {0}",
                obj.blnDetectado ? "¡BLANCO EN EL HAZ (DETECTADO)!" : "Fuera del haz (Fading persistente)"));
            sb.AppendLine();
            sb.AppendLine("4. PRODUCTO CRUZ 2D (ORIENTACIÓN):");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   û_haz × û_blanco = {0:F3}", dblProdCruz));
            sb.AppendLine(string.Format("   Lado relativo: {0}",
                dblProdCruz >= 0 ? "Izquierda (Antihorario)" : "Derecha (Horario)"));
            sb.AppendLine();
            sb.AppendLine("5. VECTOR VELOCIDAD Y DESCOMPOSICIÓN:");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   v⃗ = ({0:F2})î + ({1:F2})ĵ | Magnitud: {2:F2} u/tick", dblVx, dblVy, obj.dblVelocidad));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "   Rumbo: {0:F1}° | Altitud: {1:F0} m", obj.dblDireccion, obj.dblAltitud));

            txtAnalisisVectorial.Text = sb.ToString();
        }

        // ==================================================================
        // Tarjeta 3: Sustentación de Transformaciones 3x3
        // ==================================================================

        private void ActualizarPanelMatrices()
        {
            int entSeleccion = cmbSeleccionarMatriz.SelectedIndex;
            StringBuilder sb = new StringBuilder();

            switch (entSeleccion)
            {
                case 0: // Rotación
                    lblDescripcionMatriz.Text =
                        "R(θ) = [[cos θ, -sin θ, 0], [sin θ, cos θ, 0], [0, 0, 1]]\r\n" +
                        "Rota el brazo de barrido del radar alrededor del origen (0, 0).";

                    ClaseMatriz3x3 matRotacion = ClaseTransformaciones.CrearRotacion(objRadar.dblAnguloBarrido);
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "Ángulo de Barrido θ = {0:F2}° ({1:F4} rad)",
                        objRadar.dblAnguloBarrido, objRadar.dblAnguloBarrido * Math.PI / 180.0));
                    sb.AppendLine(matRotacion.Formatear("Matriz R(θ)"));
                    sb.AppendLine("Ecuaciones de Rotación Homogénea:");
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "  x' = x · cos({0:F1}°) - y · sin({0:F1}°)", objRadar.dblAnguloBarrido));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "  y' = x · sin({0:F1}°) + y · cos({0:F1}°)", objRadar.dblAnguloBarrido));
                    sb.AppendLine("  w' = 1");
                    break;

                case 1: // Escalamiento
                    lblDescripcionMatriz.Text =
                        "S(sx, sy) = [[sx, 0, 0], [0, sy, 0], [0, 0, 1]]\r\n" +
                        "Aplica zoom geométrico sobre las coordenadas del mundo.";

                    double dblEscala = objRadar.dblEscalaZoom;
                    ClaseMatriz3x3 matEscalamiento = ClaseTransformaciones.CrearEscalamiento(dblEscala, dblEscala);
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "Factor de Escala sx = sy = {0:F2}", dblEscala));
                    sb.AppendLine(matEscalamiento.Formatear("Matriz S(sx, sy)"));
                    sb.AppendLine("Ecuaciones de Escalamiento:");
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "  x' = {0:F2} · x", dblEscala));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "  y' = {0:F2} · y", dblEscala));
                    sb.AppendLine("  w' = 1");
                    break;

                case 2: // Traslación
                    lblDescripcionMatriz.Text =
                        "T(tx, ty) = [[1, 0, tx], [0, 1, ty], [0, 0, 1]]\r\n" +
                        "Desplaza las coordenadas de la aeronave en el plano cartesiano.";

                    double dblTx = (objSeleccionado != null) ? objSeleccionado.objPosicionCartesiana.X : 0.0;
                    double dblTy = (objSeleccionado != null) ? objSeleccionado.objPosicionCartesiana.Y : 0.0;
                    ClaseMatriz3x3 matTraslacion = ClaseTransformaciones.CrearTraslacion(dblTx, dblTy);
                    string strNombreObj = (objSeleccionado != null) ? objSeleccionado.strIdentificador : "(Ninguno)";

                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "Blanco: {0} | Tx = {1:F2}, Ty = {2:F2}", strNombreObj, dblTx, dblTy));
                    sb.AppendLine(matTraslacion.Formatear("Matriz T(tx, ty)"));
                    sb.AppendLine("Ecuaciones de Traslación:");
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "  x' = 1·x + 0·y + {0:F2}", dblTx));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "  y' = 0·x + 1·y + {0:F2}", dblTy));
                    sb.AppendLine("  w' = 1");
                    break;

                case 3: // Composición
                    lblDescripcionMatriz.Text =
                        "M = T(tx, ty) · R(θ) · S(sx, sy)\r\n" +
                        "Composición asociativa de las tres transformaciones obligatorias.";

                    double dblTxC = (objSeleccionado != null) ? objSeleccionado.objPosicionCartesiana.X : 50.0;
                    double dblTyC = (objSeleccionado != null) ? objSeleccionado.objPosicionCartesiana.Y : 50.0;
                    ClaseMatriz3x3 mT = ClaseTransformaciones.CrearTraslacion(dblTxC, dblTyC);
                    ClaseMatriz3x3 mR = ClaseTransformaciones.CrearRotacion(objRadar.dblAnguloBarrido);
                    ClaseMatriz3x3 mS = ClaseTransformaciones.CrearEscalamiento(objRadar.dblEscalaZoom, objRadar.dblEscalaZoom);
                    ClaseMatriz3x3 mComp = mT.Multiplicar(mR).Multiplicar(mS);

                    sb.AppendLine("Matriz Compuesta Resultante M = T · R · S:");
                    sb.AppendLine(mComp.Formatear("Matriz M (3x3)"));
                    sb.AppendLine("Demostración: La multiplicación matricial permite componer");
                    sb.AppendLine("escalamiento, rotación y traslación en una sola operación.");
                    break;
            }

            txtMatrizTransformacion.Text = sb.ToString();
        }

        // ==================================================================
        // Controles de Simulación & Captura
        // ==================================================================

        private void BtnIniciarPausar_Click(object sender, EventArgs e)
        {
            blnSimulacionActiva = !blnSimulacionActiva;
            if (blnSimulacionActiva)
            {
                btnIniciarPausar.Text = "⏸ Pausar";
                btnIniciarPausar.BackColor = Color.FromArgb(5, 150, 105);
                lblStatusSimulacion.Text = "🟢 ESTADO: ACTIVO";
                lblStatusSimulacion.ForeColor = Color.FromArgb(4, 120, 87);
            }
            else
            {
                btnIniciarPausar.Text = "▶ Iniciar";
                btnIniciarPausar.BackColor = Color.FromArgb(217, 119, 6);
                lblStatusSimulacion.Text = "🟡 ESTADO: PAUSADO";
                lblStatusSimulacion.ForeColor = Color.FromArgb(180, 83, 9);
            }
        }

        private void ActualizarIndicadorZoom()
        {
            lblZoomValor.Text = string.Format(CultureInfo.InvariantCulture, "{0:F2}x", objRadar.dblEscalaZoom);
            lblStatusZoom.Text = string.Format(CultureInfo.InvariantCulture, "🔍 Zoom: {0:F1}x", objRadar.dblEscalaZoom);
        }

        private void BtnCapturarPantalla_Click(object sender, EventArgs e)
        {
            try
            {
                using (Bitmap bmpCaptura = new Bitmap(pnlRadar.ClientSize.Width, pnlRadar.ClientSize.Height))
                {
                    using (Graphics g = Graphics.FromImage(bmpCaptura))
                    {
                        ClaseRenderizadorRadar.Renderizar(
                            g,
                            objRadar,
                            bmpCaptura.Width,
                            bmpCaptura.Height,
                            objSeleccionado?.strIdentificador,
                            true, true, true, true
                        );
                    }

                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Imagen PNG (*.png)|*.png|Imagen JPEG (*.jpg)|*.jpg";
                        sfd.FileName = string.Format("Radar_Captura_{0:yyyyMMdd_HHmmss}.png", DateTime.Now);
                        sfd.Title = "Guardar Captura de Radar para Informe Técnico";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            bmpCaptura.Save(sfd.FileName, ImageFormat.Png);
                            MessageBox.Show("Captura guardada exitosamente:\n" + sfd.FileName,
                                "Captura Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al capturar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
