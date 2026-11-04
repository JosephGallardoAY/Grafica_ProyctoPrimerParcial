// ============================================================================
// Archivo: PanelDobleBuffer.cs
// Descripción: Panel personalizado con doble buffer activado para evitar
//              el parpadeo (flickering) durante la animación del radar.
//
//              En Windows Forms, el control Panel no tiene DoubleBuffered
//              activado por defecto, lo que causa parpadeo al redibujar
//              el contenido continuamente (como en una animación).
//
//              El doble buffer funciona así:
//              1. Se dibuja todo el contenido en un búfer en memoria (off-screen).
//              2. Una vez completo, se copia el búfer completo a la pantalla.
//              3. Esto elimina el parpadeo porque el usuario nunca ve el
//                 dibujo parcial.
//
// Proyecto: Simulador de Radar 2D - Computación Gráfica
// ============================================================================

using System.Windows.Forms;

namespace ProyectoPrimerParcial.Vista
{
    /// <summary>
    /// Panel con doble buffer para renderizado suave sin parpadeo.
    /// Hereda de Panel y activa las opciones de renderizado optimizado.
    /// </summary>
    public class PanelDobleBuffer : Panel
    {
        /// <summary>
        /// Constructor que activa el doble buffer y los estilos de pintura optimizados.
        /// </summary>
        public PanelDobleBuffer()
        {
            // Activar doble buffer para evitar parpadeo
            this.DoubleBuffered = true;

            // Configurar estilos de pintura optimizados:
            // - AllPaintingInWmPaint: todo el dibujo se hace en WM_PAINT (evita WM_ERASEBKGND)
            // - UserPaint: el control se dibuja a sí mismo (no delega al sistema)
            // - OptimizedDoubleBuffer: usa un búfer intermedio para pintura suave
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);

            // Aplicar los cambios de estilo
            this.UpdateStyles();
        }
    }
}
