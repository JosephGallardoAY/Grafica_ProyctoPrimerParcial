// ============================================================================
// Archivo: ClaseRenderizadorRadar.cs
// Descripción: Motor de renderizado GDI+ para la pantalla del radar 2D.
//              Dibuja todos los elementos visuales del radar:
//              - Fondo oscuro militar / táctico con bisel exterior
//              - Graduación de brújula (360° en marcas de 10° y etiquetas cada 30°)
//              - Anillos concéntricos (rangos de distancia)
//              - Ejes cardinales (N, S, E, O)
//              - Línea de barrido con estela degradada (efecto fósforo P7)
//              - Estelas de trayectoria histórica (breadcrumbs)
//              - Vectores de velocidad y rumbo
//              - Figuras geométricas de aeronaves con fading
//              - Retícula de selección táctica (Target Lock)
//              - Alertas de proximidad y riesgo de colisión entre aeronaves
//              - Información HUD superpuesta
// ============================================================================

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ProyectoPrimerParcial.Modelo;

namespace ProyectoPrimerParcial.Vista
{
    /// <summary>
    /// Motor estático de renderizado GDI+ para la simulación visual del radar.
    /// </summary>
    public static class ClaseRenderizadorRadar
    {
        // ==================================================================
        // Paleta de colores tácticos del radar
        // ==================================================================

        private static readonly Color COL_FONDO_RADAR = Color.FromArgb(255, 6, 14, 10);
        private static readonly Color COL_BISEL_RADAR = Color.FromArgb(255, 20, 36, 26);
        private static readonly Color COL_ANILLO = Color.FromArgb(70, 0, 160, 60);
        private static readonly Color COL_EJE = Color.FromArgb(50, 0, 140, 50);
        private static readonly Color COL_BARRIDO = Color.FromArgb(255, 0, 255, 80);
        private static readonly Color COL_TEXTO_HUD = Color.FromArgb(255, 0, 230, 100);
        private static readonly Color COL_ETIQUETA_RANGO = Color.FromArgb(170, 0, 170, 80);
        private static readonly Color COL_BRUJULA = Color.FromArgb(120, 0, 200, 90);
        private static readonly Color COL_SELECCION = Color.FromArgb(255, 255, 220, 0);
        private static readonly Color COL_ALERTA_COLISION = Color.FromArgb(255, 255, 40, 40);

        private const int ENT_TAMANO_MARCADOR = 9;
        private const int ENT_SEGMENTOS_ESTELA = 45;
        private const float FLT_ARCO_ESTELA = 75.0f;

        // ==================================================================
        // Método principal de renderizado
        // ==================================================================

        public static void Renderizar(Graphics objGraficos, ClaseRadar objRadar,
                                       int entAncho, int entAlto,
                                       string strIdSeleccionado = null,
                                       bool blnVerVectores = true,
                                       bool blnVerEstelas = true,
                                       bool blnVerBrujula = true,
                                       bool blnVerAlertas = true)
        {
            objGraficos.SmoothingMode = SmoothingMode.AntiAlias;
            objGraficos.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Margen para la rosa de los vientos / brújula perimetral
            int entMargen = blnVerBrujula ? 46 : 28;
            float fltRadioDisponible = Math.Min(entAncho, entAlto) / 2.0f - entMargen;

            // Escala del mundo a pantalla
            float fltEscala = (float)(fltRadioDisponible / objRadar.dblRadioMaximo * objRadar.dblEscalaZoom);

            float fltCentroX = entAncho / 2.0f;
            float fltCentroY = entAlto / 2.0f;

            // Secuencia ordenada de capas de dibujo
            DibujarFondoYBisel(objGraficos, entAncho, entAlto, fltCentroX, fltCentroY, fltRadioDisponible);
            DibujarAnillos(objGraficos, objRadar, fltCentroX, fltCentroY, fltEscala);
            DibujarEjes(objGraficos, fltCentroX, fltCentroY, fltRadioDisponible);

            if (blnVerBrujula)
            {
                DibujarBrujulaPerimetral(objGraficos, fltCentroX, fltCentroY, fltRadioDisponible);
            }

            DibujarEstela(objGraficos, objRadar, fltCentroX, fltCentroY, fltEscala);
            DibujarLineaBarrido(objGraficos, objRadar, fltCentroX, fltCentroY, fltEscala);

            if (blnVerAlertas)
            {
                DibujarAlertasColision(objGraficos, objRadar, fltCentroX, fltCentroY, fltEscala);
            }

            DibujarObjetos(objGraficos, objRadar, fltCentroX, fltCentroY, fltEscala,
                           strIdSeleccionado, blnVerVectores, blnVerEstelas);

            DibujarHUD(objGraficos, objRadar, entAncho, entAlto);
        }

        // ==================================================================
        // Fondo y bisel circular
        // ==================================================================

        private static void DibujarFondoYBisel(Graphics objGraficos, int entAncho, int entAlto,
                                              float fltCentroX, float fltCentroY, float fltRadio)
        {
            // Fondo general del panel
            using (SolidBrush objBrochaFondo = new SolidBrush(COL_FONDO_RADAR))
            {
                objGraficos.FillRectangle(objBrochaFondo, 0, 0, entAncho, entAlto);
            }

            // Bisel exterior circular del radar
            float fltDiametro = fltRadio * 2.0f;
            using (Pen objLapizBisel = new Pen(COL_BISEL_RADAR, 3.0f))
            {
                objGraficos.DrawEllipse(objLapizBisel,
                    fltCentroX - fltRadio, fltCentroY - fltRadio,
                    fltDiametro, fltDiametro);
            }
        }

        // ==================================================================
        // Brújula perimetral / Grados sexagesimales (0° - 360°)
        // ==================================================================

        private static void DibujarBrujulaPerimetral(Graphics objGraficos,
                                                    float fltCentroX, float fltCentroY,
                                                    float fltRadio)
        {
            using (Pen objLapizMarcaLarga = new Pen(COL_BRUJULA, 1.5f))
            using (Pen objLapizMarcaCorta = new Pen(Color.FromArgb(60, 0, 160, 70), 1.0f))
            using (Font objFuenteGrados = new Font("Consolas", 7.0f, FontStyle.Regular))
            using (SolidBrush objBrochaTexto = new SolidBrush(COL_BRUJULA))
            {
                // Dibujar marcas cada 10 grados
                for (int entGrad = 0; entGrad < 360; entGrad += 10)
                {
                    double dblRad = entGrad * Math.PI / 180.0;
                    float fltCos = (float)Math.Cos(dblRad);
                    float fltSin = (float)Math.Sin(dblRad); // Y invertido en pantalla

                    bool blnEsMarcaMayor = (entGrad % 30 == 0);
                    float fltLargo = blnEsMarcaMayor ? 8.0f : 4.0f;

                    float fltX1 = fltCentroX + (fltRadio) * fltCos;
                    float fltY1 = fltCentroY - (fltRadio) * fltSin;
                    float fltX2 = fltCentroX + (fltRadio + fltLargo) * fltCos;
                    float fltY2 = fltCentroY - (fltRadio + fltLargo) * fltSin;

                    objGraficos.DrawLine(blnEsMarcaMayor ? objLapizMarcaLarga : objLapizMarcaCorta,
                        fltX1, fltY1, fltX2, fltY2);

                    // Etiquetas numéricas cada 30 grados
                    if (blnEsMarcaMayor)
                    {
                        float fltTextoRadio = fltRadio + 16.0f;
                        float fltTx = fltCentroX + fltTextoRadio * fltCos;
                        float fltTy = fltCentroY - fltTextoRadio * fltSin;

                        string strGrad = entGrad.ToString();
                        SizeF objTam = objGraficos.MeasureString(strGrad, objFuenteGrados);
                        objGraficos.DrawString(strGrad, objFuenteGrados, objBrochaTexto,
                            fltTx - objTam.Width / 2.0f,
                            fltTy - objTam.Height / 2.0f);
                    }
                }
            }
        }

        // ==================================================================
        // Anillos concéntricos de rango
        // ==================================================================

        private static void DibujarAnillos(Graphics objGraficos, ClaseRadar objRadar,
                                            float fltCentroX, float fltCentroY, float fltEscala)
        {
            double[] arrRadios = objRadar.ObtenerRadiosAnillos();

            using (Pen objLapizAnillo = new Pen(COL_ANILLO, 1.0f))
            using (Font objFuenteEtiqueta = new Font("Consolas", 8.0f, FontStyle.Bold))
            using (SolidBrush objBrochaEtiqueta = new SolidBrush(COL_ETIQUETA_RANGO))
            {
                for (int entI = 0; entI < arrRadios.Length; entI++)
                {
                    float fltRadioPx = (float)(arrRadios[entI] * fltEscala);
                    float fltX = fltCentroX - fltRadioPx;
                    float fltY = fltCentroY - fltRadioPx;
                    float fltDiametro = fltRadioPx * 2;

                    if (fltDiametro > 0)
                    {
                        objGraficos.DrawEllipse(objLapizAnillo, fltX, fltY, fltDiametro, fltDiametro);
                    }

                    // Etiqueta con distancia del rango
                    string strEtiqueta = string.Format("{0:F0} u", arrRadios[entI]);
                    objGraficos.DrawString(strEtiqueta, objFuenteEtiqueta, objBrochaEtiqueta,
                        fltCentroX + fltRadioPx + 3,
                        fltCentroY - 12);
                }
            }
        }

        // ==================================================================
        // Ejes cardinales y centro
        // ==================================================================

        private static void DibujarEjes(Graphics objGraficos,
                                         float fltCentroX, float fltCentroY,
                                         float fltRadioDisponible)
        {
            using (Pen objLapizEje = new Pen(COL_EJE, 1.0f))
            using (Font objFuenteDir = new Font("Consolas", 10.0f, FontStyle.Bold))
            using (SolidBrush objBrochaDir = new SolidBrush(COL_TEXTO_HUD))
            {
                // Eje Este-Oeste
                objGraficos.DrawLine(objLapizEje,
                    fltCentroX - fltRadioDisponible, fltCentroY,
                    fltCentroX + fltRadioDisponible, fltCentroY);

                // Eje Norte-Sur
                objGraficos.DrawLine(objLapizEje,
                    fltCentroX, fltCentroY - fltRadioDisponible,
                    fltCentroX, fltCentroY + fltRadioDisponible);

                // Puntos cardinales con fondo sutil
                objGraficos.DrawString("N (90°)", objFuenteDir, objBrochaDir,
                    fltCentroX - 25, fltCentroY - fltRadioDisponible - 30);

                objGraficos.DrawString("S (270°)", objFuenteDir, objBrochaDir,
                    fltCentroX - 28, fltCentroY + fltRadioDisponible + 10);

                objGraficos.DrawString("E (0°)", objFuenteDir, objBrochaDir,
                    fltCentroX + fltRadioDisponible + 10, fltCentroY - 7);

                objGraficos.DrawString("O (180°)", objFuenteDir, objBrochaDir,
                    fltCentroX - fltRadioDisponible - 55, fltCentroY - 7);

                // Cruz del centro del radar
                float fltCruz = 5;
                using (Pen objLapizCentro = new Pen(COL_TEXTO_HUD, 1.5f))
                {
                    objGraficos.DrawLine(objLapizCentro, fltCentroX - fltCruz, fltCentroY, fltCentroX + fltCruz, fltCentroY);
                    objGraficos.DrawLine(objLapizCentro, fltCentroX, fltCentroY - fltCruz, fltCentroX, fltCentroY + fltCruz);
                }
            }
        }

        // ==================================================================
        // Estela del barrido (persistencia de fósforo)
        // ==================================================================

        private static void DibujarEstela(Graphics objGraficos, ClaseRadar objRadar,
                                           float fltCentroX, float fltCentroY,
                                           float fltEscala)
        {
            float fltRadioPx = (float)(objRadar.dblRadioMaximo * fltEscala);
            RectangleF objRectangulo = new RectangleF(
                fltCentroX - fltRadioPx, fltCentroY - fltRadioPx,
                fltRadioPx * 2, fltRadioPx * 2);

            if (fltRadioPx <= 0) return;

            float fltAnguloGDI = -(float)objRadar.dblAnguloBarrido;
            float fltArcoSegmento = FLT_ARCO_ESTELA / ENT_SEGMENTOS_ESTELA;

            // Sentido de la estela según la dirección de giro
            float fltSigno = objRadar.blnSentidoHorario ? -1.0f : 1.0f;

            for (int entI = 0; entI < ENT_SEGMENTOS_ESTELA; entI++)
            {
                float fltAnguloSegmento = fltAnguloGDI + (fltSigno * fltArcoSegmento * entI);

                double dblFactor = 1.0 - ((double)entI / ENT_SEGMENTOS_ESTELA);
                int entAlfa = (int)(90.0 * dblFactor * dblFactor);

                if (entAlfa > 0)
                {
                    using (SolidBrush objBrocha = new SolidBrush(Color.FromArgb(entAlfa, 0, 220, 70)))
                    {
                        objGraficos.FillPie(objBrocha, objRectangulo.X, objRectangulo.Y,
                            objRectangulo.Width, objRectangulo.Height,
                            fltAnguloSegmento, (fltSigno * fltArcoSegmento) + (fltSigno * 0.5f));
                    }
                }
            }
        }

        // ==================================================================
        // Brazo de barrido rotatorio
        // ==================================================================

        private static void DibujarLineaBarrido(Graphics objGraficos, ClaseRadar objRadar,
                                                  float fltCentroX, float fltCentroY,
                                                  float fltEscala)
        {
            ClaseVertice objExtremo = objRadar.ObtenerVerticeBarrido();

            float fltExtremoX = fltCentroX + (float)(objExtremo.X * fltEscala);
            float fltExtremoY = fltCentroY - (float)(objExtremo.Y * fltEscala);

            using (Pen objLapizBarrido = new Pen(COL_BARRIDO, 2.0f))
            {
                objGraficos.DrawLine(objLapizBarrido, fltCentroX, fltCentroY, fltExtremoX, fltExtremoY);
            }
        }

        // ==================================================================
        // Alertas de colisión / proximidad entre aeronaves
        // ==================================================================

        private static void DibujarAlertasColision(Graphics objGraficos, ClaseRadar objRadar,
                                                   float fltCentroX, float fltCentroY, float fltEscala)
        {
            if (objRadar.lstAlertasColision == null || objRadar.lstAlertasColision.Count == 0) return;

            using (Pen objLapizAlerta = new Pen(COL_ALERTA_COLISION, 1.8f))
            using (Font objFuenteAlerta = new Font("Consolas", 7.5f, FontStyle.Bold))
            using (SolidBrush objBrochaAlerta = new SolidBrush(COL_ALERTA_COLISION))
            {
                objLapizAlerta.DashStyle = DashStyle.Dash;

                foreach (var alerta in objRadar.lstAlertasColision)
                {
                    ClaseObjetoAereo obj1 = alerta.Item1;
                    ClaseObjetoAereo obj2 = alerta.Item2;
                    double dblDist = alerta.Item3;

                    float fltX1 = fltCentroX + (float)(obj1.objPosicionCartesiana.X * fltEscala);
                    float fltY1 = fltCentroY - (float)(obj1.objPosicionCartesiana.Y * fltEscala);
                    float fltX2 = fltCentroX + (float)(obj2.objPosicionCartesiana.X * fltEscala);
                    float fltY2 = fltCentroY - (float)(obj2.objPosicionCartesiana.Y * fltEscala);

                    // Línea discontinua roja de conflicto
                    objGraficos.DrawLine(objLapizAlerta, fltX1, fltY1, fltX2, fltY2);

                    // Etiqueta en el punto medio
                    float fltMidX = (fltX1 + fltX2) / 2.0f;
                    float fltMidY = (fltY1 + fltY2) / 2.0f;
                    string strTextoAlerta = string.Format("⚠️ CONFLICTO d={0:F1}u", dblDist);
                    objGraficos.DrawString(strTextoAlerta, objFuenteAlerta, objBrochaAlerta, fltMidX - 40, fltMidY - 14);
                }
            }
        }

        // ==================================================================
        // Objetos aéreos, estelas de trayectoria, vectores y Target Lock
        // ==================================================================

        private static void DibujarObjetos(Graphics objGraficos, ClaseRadar objRadar,
                                            float fltCentroX, float fltCentroY,
                                            float fltEscala, string strIdSeleccionado,
                                            bool blnVerVectores, bool blnVerEstelas)
        {
            using (Font objFuenteId = new Font("Consolas", 8.0f, FontStyle.Bold))
            using (Font objFuenteMini = new Font("Consolas", 6.5f))
            {
                foreach (ClaseObjetoAereo objAereo in objRadar.lstObjetosAereos)
                {
                    int entAlfa = Math.Max(0, 255 - objAereo.entTiempoDesdeDeteccion * 3);
                    if (entAlfa <= 0) continue;

                    float fltObjetoX = fltCentroX + (float)(objAereo.objPosicionCartesiana.X * fltEscala);
                    float fltObjetoY = fltCentroY - (float)(objAereo.objPosicionCartesiana.Y * fltEscala);

                    Color colConAlfa = Color.FromArgb(entAlfa, objAereo.colColor.R, objAereo.colColor.G, objAereo.colColor.B);

                    // 1. Estela histórica de trayectoria (breadcrumbs)
                    if (blnVerEstelas && objAereo.lstHistorialPosiciones != null)
                    {
                        for (int entP = 0; entP < objAereo.lstHistorialPosiciones.Count; entP++)
                        {
                            var pt = objAereo.lstHistorialPosiciones[entP];
                            float fltPx = fltCentroX + (float)(pt.X * fltEscala);
                            float fltPy = fltCentroY - (float)(pt.Y * fltEscala);

                            int entAlfaP = (int)(entAlfa * (entP + 1.0) / (objAereo.lstHistorialPosiciones.Count + 2) * 0.5);
                            if (entAlfaP > 0)
                            {
                                using (SolidBrush objBrochaPunto = new SolidBrush(Color.FromArgb(entAlfaP, objAereo.colColor)))
                                {
                                    objGraficos.FillEllipse(objBrochaPunto, fltPx - 1.5f, fltPy - 1.5f, 3f, 3f);
                                }
                            }
                        }
                    }

                    // 2. Vector de velocidad y rumbo
                    if (blnVerVectores)
                    {
                        double dblRadDir = objAereo.dblDireccion * Math.PI / 180.0;
                        float fltLargoVector = (float)(objAereo.dblVelocidad * fltEscala * 6.0);
                        float fltVecX = fltObjetoX + fltLargoVector * (float)Math.Cos(dblRadDir);
                        float fltVecY = fltObjetoY - fltLargoVector * (float)Math.Sin(dblRadDir);

                        using (Pen objLapizVector = new Pen(Color.FromArgb((int)(entAlfa * 0.7), objAereo.colColor), 1.0f))
                        {
                            objLapizVector.DashStyle = DashStyle.Dot;
                            objGraficos.DrawLine(objLapizVector, fltObjetoX, fltObjetoY, fltVecX, fltVecY);
                        }
                    }

                    // 3. Figura geométrica del objeto
                    using (SolidBrush objBrocha = new SolidBrush(colConAlfa))
                    using (Pen objLapiz = new Pen(colConAlfa, 1.8f))
                    {
                        DibujarFormaAeronave(objGraficos, objAereo.strTipoAeronave,
                            fltObjetoX, fltObjetoY, objAereo.dblDireccion,
                            objBrocha, objLapiz);

                        // Etiqueta de ID y Distancia
                        string strEtiqueta = string.Format("{0} [{1:F0}u]", objAereo.strIdentificador, objAereo.dblRadio);
                        objGraficos.DrawString(strEtiqueta, objFuenteId, objBrocha,
                            fltObjetoX + ENT_TAMANO_MARCADOR + 3,
                            fltObjetoY - ENT_TAMANO_MARCADOR);
                    }

                    // 4. Retícula táctica de selección (Target Lock) y Análisis Vectorial
                    if (strIdSeleccionado != null && objAereo.strIdentificador == strIdSeleccionado)
                    {
                        DibujarRetículaTargetLock(objGraficos, fltObjetoX, fltObjetoY);
                        DibujarAnalisisVectorialBlanco(objGraficos, objAereo, fltCentroX, fltCentroY, fltEscala, objRadar);
                    }
                }
            }
        }

        /// <summary>
        /// Dibuja una retícula de bloqueo de blanco (Target Lock) militar alrededor del objeto seleccionado.
        /// </summary>
        private static void DibujarRetículaTargetLock(Graphics objGraficos, float fltX, float fltY)
        {
            float fltBox = ENT_TAMANO_MARCADOR + 8;
            float fltCodo = 5;

            using (Pen objLapizLock = new Pen(COL_SELECCION, 1.8f))
            {
                // Esquina superior izquierda
                objGraficos.DrawLine(objLapizLock, fltX - fltBox, fltY - fltBox, fltX - fltBox + fltCodo, fltY - fltBox);
                objGraficos.DrawLine(objLapizLock, fltX - fltBox, fltY - fltBox, fltX - fltBox, fltY - fltBox + fltCodo);

                // Esquina superior derecha
                objGraficos.DrawLine(objLapizLock, fltX + fltBox, fltY - fltBox, fltX + fltBox - fltCodo, fltY - fltBox);
                objGraficos.DrawLine(objLapizLock, fltX + fltBox, fltY - fltBox, fltX + fltBox, fltY - fltBox + fltCodo);

                // Esquina inferior izquierda
                objGraficos.DrawLine(objLapizLock, fltX - fltBox, fltY + fltBox, fltX - fltBox + fltCodo, fltY + fltBox);
                objGraficos.DrawLine(objLapizLock, fltX - fltBox, fltY + fltBox, fltX - fltBox, fltY + fltBox - fltCodo);

                // Esquina inferior derecha
                objGraficos.DrawLine(objLapizLock, fltX + fltBox, fltY + fltBox, fltX + fltBox - fltCodo, fltY + fltBox);
                objGraficos.DrawLine(objLapizLock, fltX + fltBox, fltY + fltBox, fltX + fltBox, fltY + fltBox - fltCodo);
            }
        }

        /// <summary>
        /// Representación gráfica de la descomposición vectorial del blanco seleccionado:
        /// 1. Vector posición r⃗ desde el origen (0, 0).
        /// 2. Proyecciones ortogonales punteadas sobre los ejes cartesianos X e Y.
        /// 3. Vector unitario û_r = r⃗ / ||r⃗|| en el origen.
        /// 4. Vector unitario del haz û_haz en el origen.
        /// 5. Arco angular polar θ.
        /// 6. Tarjeta flotante con valores de álgebra vectorial en pantalla.
        /// </summary>
        private static void DibujarAnalisisVectorialBlanco(Graphics objGraficos, ClaseObjetoAereo objAereo,
                                                           float fltCentroX, float fltCentroY, float fltEscala,
                                                           ClaseRadar objRadar)
        {
            float fltTargetX = fltCentroX + (float)(objAereo.objPosicionCartesiana.X * fltEscala);
            float fltTargetY = fltCentroY - (float)(objAereo.objPosicionCartesiana.Y * fltEscala);

            using (Pen objLapizVectorPos = new Pen(Color.FromArgb(220, 56, 189, 248), 1.8f)) // Cyan Vector r
            using (Pen objLapizProyeccion = new Pen(Color.FromArgb(140, 148, 163, 184), 1.0f)) // Slate dashed
            using (Pen objLapizUnitario = new Pen(Color.FromArgb(255, 234, 179, 8), 2.2f)) // Yellow Unit Vector u_r
            using (Pen objLapizHazUnitario = new Pen(Color.FromArgb(255, 0, 255, 120), 2.0f)) // Green Unit Vector u_haz
            using (Font objFuenteVector = new Font("Consolas", 7.5f, FontStyle.Bold))
            using (SolidBrush objBrochaCyan = new SolidBrush(Color.FromArgb(56, 189, 248)))
            using (SolidBrush objBrochaAmarilla = new SolidBrush(Color.FromArgb(234, 179, 8)))
            using (SolidBrush objBrochaVerde = new SolidBrush(Color.FromArgb(0, 255, 120)))
            {
                objLapizProyeccion.DashStyle = DashStyle.Dash;

                // 1. Vector Posición r⃗ desde el centro (0, 0) hasta el objetivo
                objLapizVectorPos.CustomEndCap = new AdjustableArrowCap(4, 5);
                objGraficos.DrawLine(objLapizVectorPos, fltCentroX, fltCentroY, fltTargetX, fltTargetY);

                // 2. Proyecciones ortogonales a los ejes X e Y
                objGraficos.DrawLine(objLapizProyeccion, fltTargetX, fltTargetY, fltTargetX, fltCentroY);
                objGraficos.DrawLine(objLapizProyeccion, fltTargetX, fltTargetY, fltCentroX, fltTargetY);

                // Marcas y cotas en los ejes
                objGraficos.FillEllipse(objBrochaCyan, fltTargetX - 2.5f, fltCentroY - 2.5f, 5, 5);
                objGraficos.FillEllipse(objBrochaCyan, fltCentroX - 2.5f, fltTargetY - 2.5f, 5, 5);
                objGraficos.DrawString(string.Format("x={0:F0}", objAereo.objPosicionCartesiana.X), objFuenteVector, objBrochaCyan, fltTargetX - 15, fltCentroY + 4);
                objGraficos.DrawString(string.Format("y={0:F0}", objAereo.objPosicionCartesiana.Y), objFuenteVector, objBrochaCyan, fltCentroX + 4, fltTargetY - 7);

                // 3. Vector Unitario û_r = r⃗ / ||r⃗||
                ClaseVertice objUnitario = ClaseAlgebraVectorial.Normalizar(
                    objAereo.objPosicionCartesiana.X, objAereo.objPosicionCartesiana.Y);

                float fltLargoUnitarioPx = 42.0f; // Longitud visual
                float fltUx = fltCentroX + (float)(objUnitario.X * fltLargoUnitarioPx);
                float fltUy = fltCentroY - (float)(objUnitario.Y * fltLargoUnitarioPx);

                objLapizUnitario.CustomEndCap = new AdjustableArrowCap(3, 4);
                objGraficos.DrawLine(objLapizUnitario, fltCentroX, fltCentroY, fltUx, fltUy);
                objGraficos.DrawString("û_r", objFuenteVector, objBrochaAmarilla, fltUx + 2, fltUy - 8);

                // 4. Vector Unitario del Haz û_haz
                double dblRadHaz = objRadar.dblAnguloBarrido * Math.PI / 180.0;
                float fltUhx = fltCentroX + (float)(Math.Cos(dblRadHaz) * fltLargoUnitarioPx);
                float fltUhy = fltCentroY - (float)(Math.Sin(dblRadHaz) * fltLargoUnitarioPx);

                objLapizHazUnitario.CustomEndCap = new AdjustableArrowCap(3, 4);
                objGraficos.DrawLine(objLapizHazUnitario, fltCentroX, fltCentroY, fltUhx, fltUhy);
                objGraficos.DrawString("û_h", objFuenteVector, objBrochaVerde, fltUhx + 2, fltUhy - 8);

                // 5. Arco angular θ desde el eje X positivo
                float fltRadioArco = 28.0f;
                float fltAnguloGrados = (float)objAereo.dblAnguloGrados;
                objGraficos.DrawArc(objLapizUnitario,
                    fltCentroX - fltRadioArco, fltCentroY - fltRadioArco,
                    fltRadioArco * 2, fltRadioArco * 2,
                    -fltAnguloGrados, fltAnguloGrados);

                // 6. Tarjeta flotante con valores matemáticos
                float fltCardX = fltTargetX + 16.0f;
                float fltCardY = fltTargetY + 10.0f;
                float fltCardW = 150.0f;
                float fltCardH = 58.0f;

                if (fltCardX + fltCardW > fltCentroX * 2 - 10) fltCardX = fltTargetX - fltCardW - 16.0f;
                if (fltCardY + fltCardH > fltCentroY * 2 - 10) fltCardY = fltTargetY - fltCardH - 10.0f;

                using (SolidBrush objBrochaFondoCard = new SolidBrush(Color.FromArgb(215, 11, 15, 25)))
                using (Pen objBordeCard = new Pen(Color.FromArgb(160, 56, 189, 248), 1.0f))
                using (Font objFuenteCard = new Font("Consolas", 6.8f))
                {
                    objGraficos.FillRectangle(objBrochaFondoCard, fltCardX, fltCardY, fltCardW, fltCardH);
                    objGraficos.DrawRectangle(objBordeCard, fltCardX, fltCardY, fltCardW, fltCardH);

                    objGraficos.DrawString(string.Format("r⃗ = ({0:F1})î + ({1:F1})ĵ", objAereo.objPosicionCartesiana.X, objAereo.objPosicionCartesiana.Y),
                        objFuenteCard, objBrochaCyan, fltCardX + 4, fltCardY + 4);

                    objGraficos.DrawString(string.Format("||r⃗|| = {0:F1} u | θ = {1:F1}°", objAereo.dblRadio, objAereo.dblAnguloGrados),
                        objFuenteCard, objBrochaAmarilla, fltCardX + 4, fltCardY + 17);

                    ClaseVertice vHaz = new ClaseVertice(Math.Cos(dblRadHaz), Math.Sin(dblRadHaz));
                    double dblProdPunto = ClaseAlgebraVectorial.ProductoPunto(vHaz, objUnitario);
                    double dblProdCruz = ClaseAlgebraVectorial.ProductoCruz2D(vHaz, objUnitario);

                    objGraficos.DrawString(string.Format("û_h · û_b = {0:F3} (cos)", dblProdPunto),
                        objFuenteCard, objBrochaVerde, fltCardX + 4, fltCardY + 30);

                    objGraficos.DrawString(string.Format("û_h × û_b = {0:F3} (sin)", dblProdCruz),
                        objFuenteCard, objBrochaCyan, fltCardX + 4, fltCardY + 43);
                }
            }
        }

        // ==================================================================
        // Formas geométricas vectoriales
        // ==================================================================

        private static void DibujarFormaAeronave(Graphics objGraficos, string strTipo,
                                                   float fltX, float fltY, double dblDireccion,
                                                   SolidBrush objBrocha, Pen objLapiz)
        {
            int entT = ENT_TAMANO_MARCADOR;

            switch (strTipo)
            {
                case "Avión":
                    // Triángulo rotado en la dirección de vuelo
                    double dblRad = dblDireccion * Math.PI / 180.0;
                    PointF[] arrVerticesBase = new PointF[]
                    {
                        new PointF(0, -entT * 1.2f),
                        new PointF(-entT * 0.7f, entT * 0.9f),
                        new PointF(entT * 0.7f, entT * 0.9f)
                    };

                    PointF[] arrVerticesRotados = new PointF[3];
                    float fltCos = (float)Math.Cos(-dblRad);
                    float fltSin = (float)Math.Sin(-dblRad);

                    for (int entI = 0; entI < 3; entI++)
                    {
                        float fltVx = arrVerticesBase[entI].X;
                        float fltVy = arrVerticesBase[entI].Y;
                        arrVerticesRotados[entI] = new PointF(
                            fltX + fltVx * fltCos - fltVy * fltSin,
                            fltY + fltVx * fltSin + fltVy * fltCos);
                    }
                    objGraficos.FillPolygon(objBrocha, arrVerticesRotados);
                    break;

                case "Helicóptero":
                    // Circunferencia con cruz de aspas
                    objGraficos.DrawEllipse(objLapiz, fltX - entT, fltY - entT, entT * 2, entT * 2);
                    objGraficos.DrawLine(objLapiz, fltX - entT, fltY, fltX + entT, fltY);
                    objGraficos.DrawLine(objLapiz, fltX, fltY - entT, fltX, fltY + entT);
                    break;

                case "OVNI":
                    // Diamante / Rombo simétrico
                    PointF[] arrDiamante = new PointF[]
                    {
                        new PointF(fltX, fltY - entT * 1.1f),
                        new PointF(fltX + entT * 1.1f, fltY),
                        new PointF(fltX, fltY + entT * 1.1f),
                        new PointF(fltX - entT * 1.1f, fltY)
                    };
                    objGraficos.FillPolygon(objBrocha, arrDiamante);
                    break;

                case "Dron":
                    // Cuadrado concéntrico con punto central
                    objGraficos.FillRectangle(objBrocha, fltX - entT * 0.7f, fltY - entT * 0.7f, entT * 1.4f, entT * 1.4f);
                    using (SolidBrush objBrochaCentro = new SolidBrush(Color.Black))
                    {
                        objGraficos.FillEllipse(objBrochaCentro, fltX - 2, fltY - 2, 4, 4);
                    }
                    break;

                default:
                    objGraficos.FillEllipse(objBrocha, fltX - entT * 0.5f, fltY - entT * 0.5f, entT, entT);
                    break;
            }
        }

        // ==================================================================
        // Información superpuesta (HUD)
        // ==================================================================

        private static void DibujarHUD(Graphics objGraficos, ClaseRadar objRadar,
                                        int entAncho, int entAlto)
        {
            using (Font objFuenteHUD = new Font("Consolas", 9.0f, FontStyle.Bold))
            using (SolidBrush objBrochaHUD = new SolidBrush(COL_TEXTO_HUD))
            {
                // Esquina superior izquierda
                string strAngulo = string.Format("HAZ: {0:F1}° ({1})",
                    objRadar.dblAnguloBarrido, objRadar.blnSentidoHorario ? "CW ↻" : "CCW ↺");
                objGraficos.DrawString(strAngulo, objFuenteHUD, objBrochaHUD, 12, 12);

                // Esquina superior derecha
                string strZoom = string.Format("ZOOM: {0:F2}x", objRadar.dblEscalaZoom);
                SizeF objTamZoom = objGraficos.MeasureString(strZoom, objFuenteHUD);
                objGraficos.DrawString(strZoom, objFuenteHUD, objBrochaHUD,
                    entAncho - objTamZoom.Width - 12, 12);

                // Esquina inferior izquierda
                string strObjetos = string.Format("OBJETOS: {0}", objRadar.lstObjetosAereos.Count);
                SizeF objTamObj = objGraficos.MeasureString(strObjetos, objFuenteHUD);
                objGraficos.DrawString(strObjetos, objFuenteHUD, objBrochaHUD,
                    12, entAlto - objTamObj.Height - 12);

                // Esquina inferior derecha
                string strRango = string.Format("R_MAX: {0:F0} u", objRadar.dblRadioMaximo);
                SizeF objTamRango = objGraficos.MeasureString(strRango, objFuenteHUD);
                objGraficos.DrawString(strRango, objFuenteHUD, objBrochaHUD,
                    entAncho - objTamRango.Width - 12,
                    entAlto - objTamRango.Height - 12);
            }
        }
    }
}
