// ============================================================================
// Archivo: FormAgregarObjeto.cs
// Descripción: Formulario modal con validación estricta para el ingreso
//              manual de nuevas aeronaves al radar táctico 2D.
//              Cumple el requisito de validación de datos de entrada:
//              - Validación de tipos numéricos (double.TryParse con InvariantCulture)
//              - Rangos físicos admisibles (radio <= alcance del radar, rumbo [0, 360))
//              - Conversión bidireccional entre Cartesianas y Polares
//
// Asignatura: Computación Gráfica
// Carrera: Ingeniería en Tecnologías de la Información - ESPE
// ============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ProyectoPrimerParcial.Modelo;

namespace ProyectoPrimerParcial.Vista
{
    public partial class FormAgregarObjeto : Form
    {
        public ClaseObjetoAereo ObjetoCreado { get; private set; }

        private double dblRadioMaximoRadar;
        private List<ClaseObjetoAereo> lstObjetosExistentes;

        public FormAgregarObjeto(double dblRadioMaximo, List<ClaseObjetoAereo> lstExistentes)
        {
            InitializeComponent();

            this.dblRadioMaximoRadar = dblRadioMaximo;
            this.lstObjetosExistentes = lstExistentes ?? new List<ClaseObjetoAereo>();

            this.cmbTipo.SelectedIndex = 0;   // Avión
            this.cmbColor.SelectedIndex = 0;  // Verde Esmeralda

            ActualizarTextosCoordenadas();
        }

        private void RdoCoordenadas_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTextosCoordenadas();
        }

        private void ActualizarTextosCoordenadas()
        {
            if (rdoCartesianas.Checked)
            {
                lblCoord1.Text = "Posición X (unid):";
                lblCoord2.Text = "Posición Y (unid):";
            }
            else
            {
                lblCoord1.Text = string.Format("Radio r (0 a {0:F0}u):", dblRadioMaximoRadar);
                lblCoord2.Text = "Ángulo θ (0 a 360°):";
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            bool blnValido = true;

            // 1. Validación del Identificador (ID)
            string strId = txtId.Text.Trim();
            if (string.IsNullOrEmpty(strId))
            {
                errorProvider.SetError(txtId, "El identificador no puede estar vacío.");
                blnValido = false;
            }
            else if (lstObjetosExistentes.Exists(o => string.Equals(o.strIdentificador, strId, StringComparison.OrdinalIgnoreCase)))
            {
                errorProvider.SetError(txtId, "Ya existe una aeronave registrada con este identificador.");
                blnValido = false;
            }

            // 2. Validación de Coordenadas
            double dblX = 0.0;
            double dblY = 0.0;

            if (rdoCartesianas.Checked)
            {
                if (!double.TryParse(txtCoord1.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out dblX))
                {
                    errorProvider.SetError(txtCoord1, "Ingrese un número decimal válido para X.");
                    blnValido = false;
                }

                if (!double.TryParse(txtCoord2.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out dblY))
                {
                    errorProvider.SetError(txtCoord2, "Ingrese un número decimal válido para Y.");
                    blnValido = false;
                }

                if (blnValido)
                {
                    double dblNorma = ClaseAlgebraVectorial.Magnitud(dblX, dblY);
                    if (dblNorma > dblRadioMaximoRadar)
                    {
                        errorProvider.SetError(txtCoord1, string.Format(
                            "La distancia ({0:F1}u) excede el alcance del radar ({1:F0}u).",
                            dblNorma, dblRadioMaximoRadar));
                        blnValido = false;
                    }
                }
            }
            else // Coordenadas Polares (r, θ)
            {
                double dblRadio = 0.0;
                double dblAngulo = 0.0;

                if (!double.TryParse(txtCoord1.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out dblRadio) || dblRadio < 0.0)
                {
                    errorProvider.SetError(txtCoord1, "El radio r debe ser un número mayor o igual a 0.");
                    blnValido = false;
                }
                else if (dblRadio > dblRadioMaximoRadar)
                {
                    errorProvider.SetError(txtCoord1, string.Format("El radio r no puede superar {0:F0}u.", dblRadioMaximoRadar));
                    blnValido = false;
                }

                if (!double.TryParse(txtCoord2.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out dblAngulo) || dblAngulo < 0.0 || dblAngulo >= 360.0)
                {
                    errorProvider.SetError(txtCoord2, "El ángulo θ debe estar en el intervalo [0°, 360°).");
                    blnValido = false;
                }

                if (blnValido)
                {
                    var cartesianas = ClaseCoordenadas.PolaresACartesianas(dblRadio, dblAngulo);
                    dblX = cartesianas.Item1;
                    dblY = cartesianas.Item2;
                }
            }

            // 3. Validación de Velocidad
            double dblVelocidad = 0.0;
            if (!double.TryParse(txtVelocidad.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out dblVelocidad) || dblVelocidad < 0.0 || dblVelocidad > 20.0)
            {
                errorProvider.SetError(txtVelocidad, "La velocidad debe ser un valor positivo entre 0.0 y 20.0 u/tick.");
                blnValido = false;
            }

            // 4. Validación de Rumbo / Dirección
            double dblRumbo = 0.0;
            if (!double.TryParse(txtRumbo.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out dblRumbo) || dblRumbo < 0.0 || dblRumbo >= 360.0)
            {
                errorProvider.SetError(txtRumbo, "El rumbo de vuelo debe estar en el intervalo [0°, 360°).");
                blnValido = false;
            }

            // 5. Validación de Altitud
            double dblAltitud = 0.0;
            if (!double.TryParse(txtAltitud.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out dblAltitud) || dblAltitud < 0.0 || dblAltitud > 30000.0)
            {
                errorProvider.SetError(txtAltitud, "La altitud debe ser un valor entre 0 y 30000 metros.");
                blnValido = false;
            }

            if (!blnValido)
            {
                return;
            }

            // Determinar color asignado
            Color colSeleccionado;
            switch (cmbColor.SelectedIndex)
            {
                case 0: colSeleccionado = Color.FromArgb(52, 211, 153); break; // Verde
                case 1: colSeleccionado = Color.FromArgb(56, 189, 248); break; // Cian
                case 2: colSeleccionado = Color.FromArgb(250, 204, 21); break; // Amarillo
                case 3: colSeleccionado = Color.FromArgb(251, 146, 60); break; // Naranja
                case 4: colSeleccionado = Color.FromArgb(244, 63, 94); break;  // Rojo
                case 5: colSeleccionado = Color.FromArgb(232, 121, 249); break; // Magenta
                default: colSeleccionado = Color.White; break;
            }

            string strTipo = cmbTipo.SelectedItem?.ToString() ?? "Avión";

            // Crear objeto y retornar OK
            this.ObjetoCreado = new ClaseObjetoAereo(
                strId, strTipo, dblX, dblY, dblVelocidad, dblRumbo, dblAltitud);
            this.ObjetoCreado.colColor = colSeleccionado;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
