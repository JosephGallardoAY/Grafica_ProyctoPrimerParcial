namespace ProyectoPrimerParcial
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Paneles estructurales
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlBannerAlerta = new System.Windows.Forms.Panel();
            this.lblBannerAlerta = new System.Windows.Forms.Label();
            this.btnCapturarPantalla = new System.Windows.Forms.Button();
            
            this.splitPrincipal = new System.Windows.Forms.SplitContainer();
            this.pnlRadarContenedor = new System.Windows.Forms.Panel();
            this.pnlRadar = new ProyectoPrimerParcial.Vista.PanelDobleBuffer();
            this.pnlConsolaLateral = new System.Windows.Forms.Panel();
            
            // Tarjeta 1: Control de Radar
            this.pnlCardControl = new System.Windows.Forms.Panel();
            this.lblTituloCardControl = new System.Windows.Forms.Label();
            this.btnIniciarPausar = new System.Windows.Forms.Button();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.chkSentidoHorario = new System.Windows.Forms.CheckBox();
            this.lblVelocidadBarrido = new System.Windows.Forms.Label();
            this.trbVelocidadBarrido = new System.Windows.Forms.TrackBar();
            this.lblVelocidadValor = new System.Windows.Forms.Label();
            this.lblZoom = new System.Windows.Forms.Label();
            this.btnZoomMenos = new System.Windows.Forms.Button();
            this.btnZoomReset = new System.Windows.Forms.Button();
            this.btnZoomMas = new System.Windows.Forms.Button();
            this.lblZoomValor = new System.Windows.Forms.Label();
            this.lblRangoRapido = new System.Windows.Forms.Label();
            this.btnRango50 = new System.Windows.Forms.Button();
            this.btnRango100 = new System.Windows.Forms.Button();
            this.btnRango200 = new System.Windows.Forms.Button();
            this.btnNuevoManual = new System.Windows.Forms.Button();
            this.btnGenerarAleatorios = new System.Windows.Forms.Button();
            this.btnEscenarioPatrulla = new System.Windows.Forms.Button();
            this.btnEscenarioInterceptacion = new System.Windows.Forms.Button();
            this.btnLimpiarObjetos = new System.Windows.Forms.Button();

            // Tarjeta 2: Inspector Vectorial (Target Lock)
            this.pnlCardInspector = new System.Windows.Forms.Panel();
            this.lblTituloCardInspector = new System.Windows.Forms.Label();
            this.lblEstadoLock = new System.Windows.Forms.Label();
            this.txtAnalisisVectorial = new System.Windows.Forms.TextBox();
            
            // Tarjeta 3: Demostrador de Matrices 3x3
            this.pnlCardMatrices = new System.Windows.Forms.Panel();
            this.lblTituloCardMatrices = new System.Windows.Forms.Label();
            this.cmbSeleccionarMatriz = new System.Windows.Forms.ComboBox();
            this.lblDescripcionMatriz = new System.Windows.Forms.Label();
            this.txtMatrizTransformacion = new System.Windows.Forms.TextBox();
            
            // Barra de estado
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusCursor = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusBarrido = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusObjetos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusSimulacion = new System.Windows.Forms.ToolStripStatusLabel();
            
            // Timer
            this.timerSimulacion = new System.Windows.Forms.Timer(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.splitPrincipal)).BeginInit();
            this.splitPrincipal.Panel1.SuspendLayout();
            this.splitPrincipal.Panel2.SuspendLayout();
            this.splitPrincipal.SuspendLayout();
            this.pnlSuperior.SuspendLayout();
            this.pnlBannerAlerta.SuspendLayout();
            this.pnlRadarContenedor.SuspendLayout();
            this.pnlConsolaLateral.SuspendLayout();
            this.pnlCardControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbVelocidadBarrido)).BeginInit();
            this.pnlCardInspector.SuspendLayout();
            this.pnlCardMatrices.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // -------------------------------------------------------------
            // pnlSuperior (Dark Slate Command Header)
            // -------------------------------------------------------------
            this.pnlSuperior.BackColor = System.Drawing.Color.FromArgb(30, 41, 59); // #1E293B
            this.pnlSuperior.Controls.Add(this.lblTitulo);
            this.pnlSuperior.Controls.Add(this.lblSubtitulo);
            this.pnlSuperior.Controls.Add(this.pnlBannerAlerta);
            this.pnlSuperior.Controls.Add(this.btnCapturarPantalla);
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlSuperior.Name = "pnlSuperior";
            this.pnlSuperior.Size = new System.Drawing.Size(1400, 56);
            this.pnlSuperior.TabIndex = 0;

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(52, 211, 153); // Emerald 400
            this.lblTitulo.Location = new System.Drawing.Point(14, 6);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(434, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📡 SISTEMA DE RADAR 2D | COMPUTACIÓN GRÁFICA";

            // lblSubtitulo
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225); // Slate 300
            this.lblSubtitulo.Location = new System.Drawing.Point(16, 32);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(550, 15);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Álgebra Vectorial (Magnitud, Vector Unitario, Producto Punto y Cruz) • Matrices Homogéneas 3x3 • GDI+";

            // pnlBannerAlerta
            this.pnlBannerAlerta.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.pnlBannerAlerta.Controls.Add(this.lblBannerAlerta);
            this.pnlBannerAlerta.Location = new System.Drawing.Point(620, 10);
            this.pnlBannerAlerta.Name = "pnlBannerAlerta";
            this.pnlBannerAlerta.Size = new System.Drawing.Size(320, 34);
            this.pnlBannerAlerta.TabIndex = 2;
            this.pnlBannerAlerta.Visible = false;

            // lblBannerAlerta
            this.lblBannerAlerta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBannerAlerta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblBannerAlerta.ForeColor = System.Drawing.Color.White;
            this.lblBannerAlerta.Location = new System.Drawing.Point(0, 0);
            this.lblBannerAlerta.Name = "lblBannerAlerta";
            this.lblBannerAlerta.Size = new System.Drawing.Size(320, 34);
            this.lblBannerAlerta.TabIndex = 0;
            this.lblBannerAlerta.Text = "⚠️ ALERTA: CONFLICTO DE PROXIMIDAD";
            this.lblBannerAlerta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnCapturarPantalla
            this.btnCapturarPantalla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapturarPantalla.BackColor = System.Drawing.Color.FromArgb(13, 148, 136); // Teal 600
            this.btnCapturarPantalla.FlatAppearance.BorderSize = 0;
            this.btnCapturarPantalla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapturarPantalla.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCapturarPantalla.ForeColor = System.Drawing.Color.White;
            this.btnCapturarPantalla.Location = new System.Drawing.Point(1225, 10);
            this.btnCapturarPantalla.Name = "btnCapturarPantalla";
            this.btnCapturarPantalla.Size = new System.Drawing.Size(160, 35);
            this.btnCapturarPantalla.TabIndex = 3;
            this.btnCapturarPantalla.Text = "📸 Capturar Radar";
            this.btnCapturarPantalla.UseVisualStyleBackColor = false;

            // -------------------------------------------------------------
            // splitPrincipal
            // -------------------------------------------------------------
            this.splitPrincipal.BackColor = System.Drawing.Color.FromArgb(203, 213, 225); // Slate 300 divider
            this.splitPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPrincipal.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitPrincipal.Location = new System.Drawing.Point(0, 56);
            this.splitPrincipal.Name = "splitPrincipal";
            // splitPrincipal.Panel1 (Radar Display Area)
            this.splitPrincipal.Panel1.BackColor = System.Drawing.Color.FromArgb(15, 23, 42); // Slate 900 frame
            this.splitPrincipal.Panel1.Controls.Add(this.pnlRadarContenedor);
            this.splitPrincipal.Panel1.Padding = new System.Windows.Forms.Padding(12);
            this.splitPrincipal.Panel1MinSize = 500;
            // splitPrincipal.Panel2 (Consola Táctica Clara)
            this.splitPrincipal.Panel2.BackColor = System.Drawing.Color.FromArgb(241, 245, 249); // Slate 100
            this.splitPrincipal.Panel2.Controls.Add(this.pnlConsolaLateral);
            this.splitPrincipal.Panel2.Padding = new System.Windows.Forms.Padding(8, 12, 12, 12);
            this.splitPrincipal.Panel2MinSize = 420;
            this.splitPrincipal.Size = new System.Drawing.Size(1400, 750);
            this.splitPrincipal.SplitterDistance = 940;
            this.splitPrincipal.SplitterWidth = 6;
            this.splitPrincipal.TabIndex = 1;

            // -------------------------------------------------------------
            // pnlRadarContenedor (Dark Bezel / CRT Screen)
            // -------------------------------------------------------------
            this.pnlRadarContenedor.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.pnlRadarContenedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRadarContenedor.Controls.Add(this.pnlRadar);
            this.pnlRadarContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRadarContenedor.Location = new System.Drawing.Point(12, 12);
            this.pnlRadarContenedor.Name = "pnlRadarContenedor";
            this.pnlRadarContenedor.Padding = new System.Windows.Forms.Padding(6);
            this.pnlRadarContenedor.Size = new System.Drawing.Size(916, 726);
            this.pnlRadarContenedor.TabIndex = 0;

            // pnlRadar (DoubleBuffered Canvas)
            this.pnlRadar.BackColor = System.Drawing.Color.FromArgb(6, 14, 10);
            this.pnlRadar.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pnlRadar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRadar.Location = new System.Drawing.Point(6, 6);
            this.pnlRadar.Name = "pnlRadar";
            this.pnlRadar.Size = new System.Drawing.Size(900, 710);
            this.pnlRadar.TabIndex = 0;

            // -------------------------------------------------------------
            // pnlConsolaLateral (Contenedor de 3 Tarjetas Tácticas Claras)
            // -------------------------------------------------------------
            this.pnlConsolaLateral.AutoScroll = true;
            this.pnlConsolaLateral.BackColor = System.Drawing.Color.FromArgb(241, 245, 249); // Slate 100
            this.pnlConsolaLateral.Controls.Add(this.pnlCardControl);
            this.pnlConsolaLateral.Controls.Add(this.pnlCardInspector);
            this.pnlConsolaLateral.Controls.Add(this.pnlCardMatrices);
            this.pnlConsolaLateral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConsolaLateral.Location = new System.Drawing.Point(8, 12);
            this.pnlConsolaLateral.Name = "pnlConsolaLateral";
            this.pnlConsolaLateral.Size = new System.Drawing.Size(434, 726);
            this.pnlConsolaLateral.TabIndex = 0;

            // -------------------------------------------------------------
            // Tarjeta 1: Control de Radar (Card 1)
            // -------------------------------------------------------------
            this.pnlCardControl.BackColor = System.Drawing.Color.White; // Pure White Card
            this.pnlCardControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardControl.Controls.Add(this.lblTituloCardControl);
            this.pnlCardControl.Controls.Add(this.btnIniciarPausar);
            this.pnlCardControl.Controls.Add(this.btnReiniciar);
            this.pnlCardControl.Controls.Add(this.chkSentidoHorario);
            this.pnlCardControl.Controls.Add(this.lblVelocidadBarrido);
            this.pnlCardControl.Controls.Add(this.trbVelocidadBarrido);
            this.pnlCardControl.Controls.Add(this.lblVelocidadValor);
            this.pnlCardControl.Controls.Add(this.lblZoom);
            this.pnlCardControl.Controls.Add(this.btnZoomMenos);
            this.pnlCardControl.Controls.Add(this.btnZoomReset);
            this.pnlCardControl.Controls.Add(this.btnZoomMas);
            this.pnlCardControl.Controls.Add(this.lblZoomValor);
            this.pnlCardControl.Controls.Add(this.lblRangoRapido);
            this.pnlCardControl.Controls.Add(this.btnRango50);
            this.pnlCardControl.Controls.Add(this.btnRango100);
            this.pnlCardControl.Controls.Add(this.btnRango200);
            this.pnlCardControl.Controls.Add(this.btnNuevoManual);
            this.pnlCardControl.Controls.Add(this.btnGenerarAleatorios);
            this.pnlCardControl.Controls.Add(this.btnEscenarioPatrulla);
            this.pnlCardControl.Controls.Add(this.btnEscenarioInterceptacion);
            this.pnlCardControl.Controls.Add(this.btnLimpiarObjetos);
            this.pnlCardControl.Location = new System.Drawing.Point(0, 0);
            this.pnlCardControl.Name = "pnlCardControl";
            this.pnlCardControl.Size = new System.Drawing.Size(418, 304);
            this.pnlCardControl.TabIndex = 0;

            // lblTituloCardControl
            this.lblTituloCardControl.AutoSize = true;
            this.lblTituloCardControl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTituloCardControl.ForeColor = System.Drawing.Color.FromArgb(4, 120, 87); // Emerald 700
            this.lblTituloCardControl.Location = new System.Drawing.Point(12, 10);
            this.lblTituloCardControl.Name = "lblTituloCardControl";
            this.lblTituloCardControl.Size = new System.Drawing.Size(202, 17);
            this.lblTituloCardControl.TabIndex = 0;
            this.lblTituloCardControl.Text = "🕹️ CONTROL DE SIMULACIÓN";

            // btnIniciarPausar
            this.btnIniciarPausar.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.btnIniciarPausar.FlatAppearance.BorderSize = 0;
            this.btnIniciarPausar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciarPausar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnIniciarPausar.ForeColor = System.Drawing.Color.White;
            this.btnIniciarPausar.Location = new System.Drawing.Point(12, 36);
            this.btnIniciarPausar.Name = "btnIniciarPausar";
            this.btnIniciarPausar.Size = new System.Drawing.Size(115, 32);
            this.btnIniciarPausar.TabIndex = 1;
            this.btnIniciarPausar.Text = "⏸ Pausar";
            this.btnIniciarPausar.UseVisualStyleBackColor = false;

            // btnReiniciar
            this.btnReiniciar.BackColor = System.Drawing.Color.FromArgb(71, 85, 105); // Slate 600
            this.btnReiniciar.FlatAppearance.BorderSize = 0;
            this.btnReiniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReiniciar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReiniciar.ForeColor = System.Drawing.Color.White;
            this.btnReiniciar.Location = new System.Drawing.Point(135, 36);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(105, 32);
            this.btnReiniciar.TabIndex = 2;
            this.btnReiniciar.Text = "🔄 Reiniciar 0°";
            this.btnReiniciar.UseVisualStyleBackColor = false;

            // chkSentidoHorario
            this.chkSentidoHorario.AutoSize = true;
            this.chkSentidoHorario.Checked = true;
            this.chkSentidoHorario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSentidoHorario.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkSentidoHorario.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); // Slate 800
            this.chkSentidoHorario.Location = new System.Drawing.Point(250, 42);
            this.chkSentidoHorario.Name = "chkSentidoHorario";
            this.chkSentidoHorario.Size = new System.Drawing.Size(113, 19);
            this.chkSentidoHorario.TabIndex = 3;
            this.chkSentidoHorario.Text = "Giro Horario ↻";
            this.chkSentidoHorario.UseVisualStyleBackColor = true;

            // lblVelocidadBarrido
            this.lblVelocidadBarrido.AutoSize = true;
            this.lblVelocidadBarrido.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblVelocidadBarrido.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); // Slate 600
            this.lblVelocidadBarrido.Location = new System.Drawing.Point(12, 82);
            this.lblVelocidadBarrido.Name = "lblVelocidadBarrido";
            this.lblVelocidadBarrido.Size = new System.Drawing.Size(106, 15);
            this.lblVelocidadBarrido.TabIndex = 4;
            this.lblVelocidadBarrido.Text = "Velocidad Barrido:";

            // trbVelocidadBarrido
            this.trbVelocidadBarrido.Location = new System.Drawing.Point(140, 74);
            this.trbVelocidadBarrido.Maximum = 12;
            this.trbVelocidadBarrido.Minimum = 1;
            this.trbVelocidadBarrido.Name = "trbVelocidadBarrido";
            this.trbVelocidadBarrido.Size = new System.Drawing.Size(195, 32);
            this.trbVelocidadBarrido.TabIndex = 5;
            this.trbVelocidadBarrido.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbVelocidadBarrido.Value = 3;

            // lblVelocidadValor
            this.lblVelocidadValor.AutoSize = true;
            this.lblVelocidadValor.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVelocidadValor.ForeColor = System.Drawing.Color.FromArgb(4, 120, 87); // Emerald 700
            this.lblVelocidadValor.Location = new System.Drawing.Point(345, 82);
            this.lblVelocidadValor.Name = "lblVelocidadValor";
            this.lblVelocidadValor.Size = new System.Drawing.Size(49, 15);
            this.lblVelocidadValor.TabIndex = 6;
            this.lblVelocidadValor.Text = "3.0°/t";

            // lblZoom
            this.lblZoom.AutoSize = true;
            this.lblZoom.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblZoom.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblZoom.Location = new System.Drawing.Point(12, 122);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(89, 15);
            this.lblZoom.TabIndex = 7;
            this.lblZoom.Text = "Zoom / Escala:";

            // btnZoomMenos
            this.btnZoomMenos.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnZoomMenos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnZoomMenos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomMenos.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnZoomMenos.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnZoomMenos.Location = new System.Drawing.Point(140, 116);
            this.btnZoomMenos.Name = "btnZoomMenos";
            this.btnZoomMenos.Size = new System.Drawing.Size(60, 28);
            this.btnZoomMenos.TabIndex = 8;
            this.btnZoomMenos.Text = "➖ -10%";
            this.btnZoomMenos.UseVisualStyleBackColor = false;

            // btnZoomReset
            this.btnZoomReset.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnZoomReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnZoomReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomReset.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnZoomReset.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnZoomReset.Location = new System.Drawing.Point(208, 116);
            this.btnZoomReset.Name = "btnZoomReset";
            this.btnZoomReset.Size = new System.Drawing.Size(62, 28);
            this.btnZoomReset.TabIndex = 9;
            this.btnZoomReset.Text = "🎯 100%";
            this.btnZoomReset.UseVisualStyleBackColor = false;

            // btnZoomMas
            this.btnZoomMas.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnZoomMas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnZoomMas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomMas.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnZoomMas.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnZoomMas.Location = new System.Drawing.Point(278, 116);
            this.btnZoomMas.Name = "btnZoomMas";
            this.btnZoomMas.Size = new System.Drawing.Size(60, 28);
            this.btnZoomMas.TabIndex = 10;
            this.btnZoomMas.Text = "➕ +10%";
            this.btnZoomMas.UseVisualStyleBackColor = false;

            // lblZoomValor
            this.lblZoomValor.AutoSize = true;
            this.lblZoomValor.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblZoomValor.ForeColor = System.Drawing.Color.FromArgb(2, 132, 199); // Sky 600
            this.lblZoomValor.Location = new System.Drawing.Point(346, 122);
            this.lblZoomValor.Name = "lblZoomValor";
            this.lblZoomValor.Size = new System.Drawing.Size(42, 15);
            this.lblZoomValor.TabIndex = 11;
            this.lblZoomValor.Text = "1.00x";

            // lblRangoRapido
            this.lblRangoRapido.AutoSize = true;
            this.lblRangoRapido.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRangoRapido.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblRangoRapido.Location = new System.Drawing.Point(12, 160);
            this.lblRangoRapido.Name = "lblRangoRapido";
            this.lblRangoRapido.Size = new System.Drawing.Size(90, 15);
            this.lblRangoRapido.TabIndex = 12;
            this.lblRangoRapido.Text = "Alcance Radar:";

            // btnRango50
            this.btnRango50.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnRango50.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnRango50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRango50.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRango50.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnRango50.Location = new System.Drawing.Point(140, 154);
            this.btnRango50.Name = "btnRango50";
            this.btnRango50.Size = new System.Drawing.Size(60, 26);
            this.btnRango50.TabIndex = 13;
            this.btnRango50.Text = "100 km";
            this.btnRango50.UseVisualStyleBackColor = false;

            // btnRango100
            this.btnRango100.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnRango100.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnRango100.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRango100.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRango100.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnRango100.Location = new System.Drawing.Point(208, 154);
            this.btnRango100.Name = "btnRango100";
            this.btnRango100.Size = new System.Drawing.Size(62, 26);
            this.btnRango100.TabIndex = 14;
            this.btnRango100.Text = "200 km";
            this.btnRango100.UseVisualStyleBackColor = false;

            // btnRango200
            this.btnRango200.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnRango200.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnRango200.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRango200.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRango200.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnRango200.Location = new System.Drawing.Point(278, 154);
            this.btnRango200.Name = "btnRango200";
            this.btnRango200.Size = new System.Drawing.Size(60, 26);
            this.btnRango200.TabIndex = 15;
            this.btnRango200.Text = "300 km";
            this.btnRango200.UseVisualStyleBackColor = false;

            // Botones de Registro y Escenarios Rápidos
            // btnNuevoManual
            this.btnNuevoManual.BackColor = System.Drawing.Color.FromArgb(13, 148, 136); // Teal 600
            this.btnNuevoManual.FlatAppearance.BorderSize = 0;
            this.btnNuevoManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoManual.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnNuevoManual.ForeColor = System.Drawing.Color.White;
            this.btnNuevoManual.Location = new System.Drawing.Point(12, 192);
            this.btnNuevoManual.Name = "btnNuevoManual";
            this.btnNuevoManual.Size = new System.Drawing.Size(188, 28);
            this.btnNuevoManual.TabIndex = 16;
            this.btnNuevoManual.Text = "➕ Nueva Aeronave";
            this.btnNuevoManual.UseVisualStyleBackColor = false;

            // btnGenerarAleatorios
            this.btnGenerarAleatorios.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); // Blue 600
            this.btnGenerarAleatorios.FlatAppearance.BorderSize = 0;
            this.btnGenerarAleatorios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarAleatorios.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerarAleatorios.ForeColor = System.Drawing.Color.White;
            this.btnGenerarAleatorios.Location = new System.Drawing.Point(208, 192);
            this.btnGenerarAleatorios.Name = "btnGenerarAleatorios";
            this.btnGenerarAleatorios.Size = new System.Drawing.Size(192, 28);
            this.btnGenerarAleatorios.TabIndex = 17;
            this.btnGenerarAleatorios.Text = "🎲 +3 Aleatorios";
            this.btnGenerarAleatorios.UseVisualStyleBackColor = false;

            // btnEscenarioPatrulla
            this.btnEscenarioPatrulla.BackColor = System.Drawing.Color.FromArgb(79, 70, 229); // Indigo 600
            this.btnEscenarioPatrulla.FlatAppearance.BorderSize = 0;
            this.btnEscenarioPatrulla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscenarioPatrulla.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnEscenarioPatrulla.ForeColor = System.Drawing.Color.White;
            this.btnEscenarioPatrulla.Location = new System.Drawing.Point(12, 228);
            this.btnEscenarioPatrulla.Name = "btnEscenarioPatrulla";
            this.btnEscenarioPatrulla.Size = new System.Drawing.Size(188, 28);
            this.btnEscenarioPatrulla.TabIndex = 18;
            this.btnEscenarioPatrulla.Text = "✈️ Demo: Patrulla";
            this.btnEscenarioPatrulla.UseVisualStyleBackColor = false;

            // btnEscenarioInterceptacion
            this.btnEscenarioInterceptacion.BackColor = System.Drawing.Color.FromArgb(124, 58, 237); // Purple 600
            this.btnEscenarioInterceptacion.FlatAppearance.BorderSize = 0;
            this.btnEscenarioInterceptacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscenarioInterceptacion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnEscenarioInterceptacion.ForeColor = System.Drawing.Color.White;
            this.btnEscenarioInterceptacion.Location = new System.Drawing.Point(208, 228);
            this.btnEscenarioInterceptacion.Name = "btnEscenarioInterceptacion";
            this.btnEscenarioInterceptacion.Size = new System.Drawing.Size(192, 28);
            this.btnEscenarioInterceptacion.TabIndex = 19;
            this.btnEscenarioInterceptacion.Text = "⚠️ Demo: Interceptación";
            this.btnEscenarioInterceptacion.UseVisualStyleBackColor = false;

            // btnLimpiarObjetos
            this.btnLimpiarObjetos.BackColor = System.Drawing.Color.FromArgb(220, 38, 38); // Red 600
            this.btnLimpiarObjetos.FlatAppearance.BorderSize = 0;
            this.btnLimpiarObjetos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarObjetos.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLimpiarObjetos.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarObjetos.Location = new System.Drawing.Point(12, 264);
            this.btnLimpiarObjetos.Name = "btnLimpiarObjetos";
            this.btnLimpiarObjetos.Size = new System.Drawing.Size(388, 26);
            this.btnLimpiarObjetos.TabIndex = 20;
            this.btnLimpiarObjetos.Text = "🧹 Limpiar Todos los Blancos";
            this.btnLimpiarObjetos.UseVisualStyleBackColor = false;

            // -------------------------------------------------------------
            // Tarjeta 2: Inspector Vectorial (Card 2)
            // -------------------------------------------------------------
            this.pnlCardInspector.BackColor = System.Drawing.Color.White; // Pure White Card
            this.pnlCardInspector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardInspector.Controls.Add(this.lblTituloCardInspector);
            this.pnlCardInspector.Controls.Add(this.lblEstadoLock);
            this.pnlCardInspector.Controls.Add(this.txtAnalisisVectorial);
            this.pnlCardInspector.Location = new System.Drawing.Point(0, 314);
            this.pnlCardInspector.Name = "pnlCardInspector";
            this.pnlCardInspector.Size = new System.Drawing.Size(418, 235);
            this.pnlCardInspector.TabIndex = 1;

            // lblTituloCardInspector
            this.lblTituloCardInspector.AutoSize = true;
            this.lblTituloCardInspector.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTituloCardInspector.ForeColor = System.Drawing.Color.FromArgb(2, 132, 199); // Sky 600
            this.lblTituloCardInspector.Location = new System.Drawing.Point(12, 8);
            this.lblTituloCardInspector.Name = "lblTituloCardInspector";
            this.lblTituloCardInspector.Size = new System.Drawing.Size(264, 17);
            this.lblTituloCardInspector.TabIndex = 0;
            this.lblTituloCardInspector.Text = "🎯 INSPECTOR VECTORIAL Y TELEMETRÍA";

            // lblEstadoLock
            this.lblEstadoLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstadoLock.AutoSize = true;
            this.lblEstadoLock.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold);
            this.lblEstadoLock.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6); // Amber 600
            this.lblEstadoLock.Location = new System.Drawing.Point(305, 10);
            this.lblEstadoLock.Name = "lblEstadoLock";
            this.lblEstadoLock.Size = new System.Drawing.Size(91, 13);
            this.lblEstadoLock.TabIndex = 1;
            this.lblEstadoLock.Text = "[CLIC EN BLANCO]";

            // txtAnalisisVectorial
            this.txtAnalisisVectorial.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAnalisisVectorial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAnalisisVectorial.Font = new System.Drawing.Font("Consolas", 8.8F);
            this.txtAnalisisVectorial.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); // Slate 900
            this.txtAnalisisVectorial.Location = new System.Drawing.Point(12, 30);
            this.txtAnalisisVectorial.Multiline = true;
            this.txtAnalisisVectorial.Name = "txtAnalisisVectorial";
            this.txtAnalisisVectorial.ReadOnly = true;
            this.txtAnalisisVectorial.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAnalisisVectorial.Size = new System.Drawing.Size(394, 192);
            this.txtAnalisisVectorial.TabIndex = 2;
            this.txtAnalisisVectorial.Text = "Haga clic sobre cualquier aeronave en el radar para fijar el objetivo y analizar sus vectores:";

            // -------------------------------------------------------------
            // Tarjeta 3: Demostrador de Matrices 3x3 (Card 3)
            // -------------------------------------------------------------
            this.pnlCardMatrices.BackColor = System.Drawing.Color.White; // Pure White Card
            this.pnlCardMatrices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardMatrices.Controls.Add(this.lblTituloCardMatrices);
            this.pnlCardMatrices.Controls.Add(this.cmbSeleccionarMatriz);
            this.pnlCardMatrices.Controls.Add(this.lblDescripcionMatriz);
            this.pnlCardMatrices.Controls.Add(this.txtMatrizTransformacion);
            this.pnlCardMatrices.Location = new System.Drawing.Point(0, 560);
            this.pnlCardMatrices.Name = "pnlCardMatrices";
            this.pnlCardMatrices.Size = new System.Drawing.Size(418, 245);
            this.pnlCardMatrices.TabIndex = 2;

            // lblTituloCardMatrices
            this.lblTituloCardMatrices.AutoSize = true;
            this.lblTituloCardMatrices.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTituloCardMatrices.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237); // Purple 600
            this.lblTituloCardMatrices.Location = new System.Drawing.Point(12, 8);
            this.lblTituloCardMatrices.Name = "lblTituloCardMatrices";
            this.lblTituloCardMatrices.Size = new System.Drawing.Size(266, 17);
            this.lblTituloCardMatrices.TabIndex = 0;
            this.lblTituloCardMatrices.Text = "📐 TRANSFORMACIONES MATRICIALES 3x3";

            // cmbSeleccionarMatriz
            this.cmbSeleccionarMatriz.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.cmbSeleccionarMatriz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeleccionarMatriz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSeleccionarMatriz.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.cmbSeleccionarMatriz.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.cmbSeleccionarMatriz.FormattingEnabled = true;
            this.cmbSeleccionarMatriz.Items.AddRange(new object[] {
            "Matriz de Rotación R(θ) — Barrido del Radar",
            "Matriz de Escalamiento S(sx, sy) — Zoom del Radar",
            "Matriz de Traslación T(tx, ty) — Movimiento de la Aeronave",
            "Composición Homogénea M = T · R · S"});
            this.cmbSeleccionarMatriz.Location = new System.Drawing.Point(12, 30);
            this.cmbSeleccionarMatriz.Name = "cmbSeleccionarMatriz";
            this.cmbSeleccionarMatriz.Size = new System.Drawing.Size(394, 21);
            this.cmbSeleccionarMatriz.TabIndex = 1;

            // lblDescripcionMatriz
            this.lblDescripcionMatriz.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblDescripcionMatriz.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); // Slate 600
            this.lblDescripcionMatriz.Location = new System.Drawing.Point(12, 56);
            this.lblDescripcionMatriz.Name = "lblDescripcionMatriz";
            this.lblDescripcionMatriz.Size = new System.Drawing.Size(394, 28);
            this.lblDescripcionMatriz.TabIndex = 2;
            this.lblDescripcionMatriz.Text = "R = [[cos θ, -sin θ, 0], [sin θ, cos θ, 0], [0, 0, 1]]";

            // txtMatrizTransformacion
            this.txtMatrizTransformacion.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtMatrizTransformacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatrizTransformacion.Font = new System.Drawing.Font("Consolas", 8.8F);
            this.txtMatrizTransformacion.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); // Slate 900
            this.txtMatrizTransformacion.Location = new System.Drawing.Point(12, 86);
            this.txtMatrizTransformacion.Multiline = true;
            this.txtMatrizTransformacion.Name = "txtMatrizTransformacion";
            this.txtMatrizTransformacion.ReadOnly = true;
            this.txtMatrizTransformacion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMatrizTransformacion.Size = new System.Drawing.Size(394, 146);
            this.txtMatrizTransformacion.TabIndex = 3;

            // -------------------------------------------------------------
            // statusStrip
            // -------------------------------------------------------------
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(226, 232, 240); // Slate 200
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusCursor,
            this.lblStatusBarrido,
            this.lblStatusObjetos,
            this.lblStatusZoom,
            this.lblStatusSimulacion});
            this.statusStrip.Location = new System.Drawing.Point(0, 806);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1400, 24);
            this.statusStrip.TabIndex = 2;

            // lblStatusCursor
            this.lblStatusCursor.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblStatusCursor.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); // Slate 800
            this.lblStatusCursor.Name = "lblStatusCursor";
            this.lblStatusCursor.Size = new System.Drawing.Size(329, 19);
            this.lblStatusCursor.Text = "📍 Cursor: X: 0.00, Y: 0.00 | r: 0.00 u, θ: 0.0°";

            // lblStatusBarrido
            this.lblStatusBarrido.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusBarrido.ForeColor = System.Drawing.Color.FromArgb(4, 120, 87); // Emerald 700
            this.lblStatusBarrido.Name = "lblStatusBarrido";
            this.lblStatusBarrido.Size = new System.Drawing.Size(161, 19);
            this.lblStatusBarrido.Text = "📡 Haz Barrido: 0.0°";

            // lblStatusObjetos
            this.lblStatusObjetos.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusObjetos.ForeColor = System.Drawing.Color.FromArgb(2, 132, 199); // Sky 600
            this.lblStatusObjetos.Name = "lblStatusObjetos";
            this.lblStatusObjetos.Size = new System.Drawing.Size(203, 19);
            this.lblStatusObjetos.Text = "🎯 Blancos: 0 (Detectados: 0)";

            // lblStatusZoom
            this.lblStatusZoom.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusZoom.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6); // Amber 600
            this.lblStatusZoom.Name = "lblStatusZoom";
            this.lblStatusZoom.Size = new System.Drawing.Size(91, 19);
            this.lblStatusZoom.Text = "🔍 Zoom: 1.0x";

            // lblStatusSimulacion
            this.lblStatusSimulacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusSimulacion.ForeColor = System.Drawing.Color.FromArgb(4, 120, 87); // Emerald 700
            this.lblStatusSimulacion.Name = "lblStatusSimulacion";
            this.lblStatusSimulacion.Size = new System.Drawing.Size(117, 19);
            this.lblStatusSimulacion.Text = "🟢 ESTADO: ACTIVO";

            // -------------------------------------------------------------
            // timerSimulacion
            // -------------------------------------------------------------
            this.timerSimulacion.Interval = 33;

            // -------------------------------------------------------------
            // Form1
            // -------------------------------------------------------------
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(11, 15, 25); // Deep Midnight
            this.ClientSize = new System.Drawing.Size(1400, 830);
            this.Controls.Add(this.splitPrincipal);
            this.Controls.Add(this.pnlSuperior);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1180, 750);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulador Táctico de Radar 2D | Álgebra Vectorial y Transformaciones 3x3";

            this.splitPrincipal.Panel1.ResumeLayout(false);
            this.splitPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPrincipal)).EndInit();
            this.splitPrincipal.ResumeLayout(false);
            this.pnlSuperior.ResumeLayout(false);
            this.pnlSuperior.PerformLayout();
            this.pnlBannerAlerta.ResumeLayout(false);
            this.pnlRadarContenedor.ResumeLayout(false);
            this.pnlConsolaLateral.ResumeLayout(false);
            this.pnlCardControl.ResumeLayout(false);
            this.pnlCardControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbVelocidadBarrido)).EndInit();
            this.pnlCardInspector.ResumeLayout(false);
            this.pnlCardInspector.PerformLayout();
            this.pnlCardMatrices.ResumeLayout(false);
            this.pnlCardMatrices.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlBannerAlerta;
        private System.Windows.Forms.Label lblBannerAlerta;
        private System.Windows.Forms.Button btnCapturarPantalla;
        private System.Windows.Forms.SplitContainer splitPrincipal;
        private System.Windows.Forms.Panel pnlRadarContenedor;
        private ProyectoPrimerParcial.Vista.PanelDobleBuffer pnlRadar;
        private System.Windows.Forms.Panel pnlConsolaLateral;
        
        // Tarjeta 1: Control
        private System.Windows.Forms.Panel pnlCardControl;
        private System.Windows.Forms.Label lblTituloCardControl;
        private System.Windows.Forms.Button btnIniciarPausar;
        private System.Windows.Forms.Button btnReiniciar;
        private System.Windows.Forms.CheckBox chkSentidoHorario;
        private System.Windows.Forms.Label lblVelocidadBarrido;
        private System.Windows.Forms.TrackBar trbVelocidadBarrido;
        private System.Windows.Forms.Label lblVelocidadValor;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Button btnZoomMenos;
        private System.Windows.Forms.Button btnZoomReset;
        private System.Windows.Forms.Button btnZoomMas;
        private System.Windows.Forms.Label lblZoomValor;
        private System.Windows.Forms.Label lblRangoRapido;
        private System.Windows.Forms.Button btnRango50;
        private System.Windows.Forms.Button btnRango100;
        private System.Windows.Forms.Button btnRango200;
        private System.Windows.Forms.Button btnNuevoManual;
        private System.Windows.Forms.Button btnGenerarAleatorios;
        private System.Windows.Forms.Button btnEscenarioPatrulla;
        private System.Windows.Forms.Button btnEscenarioInterceptacion;
        private System.Windows.Forms.Button btnLimpiarObjetos;

        // Tarjeta 2: Inspector Vectorial
        private System.Windows.Forms.Panel pnlCardInspector;
        private System.Windows.Forms.Label lblTituloCardInspector;
        private System.Windows.Forms.Label lblEstadoLock;
        private System.Windows.Forms.TextBox txtAnalisisVectorial;

        // Tarjeta 3: Demostrador de Matrices 3x3
        private System.Windows.Forms.Panel pnlCardMatrices;
        private System.Windows.Forms.Label lblTituloCardMatrices;
        private System.Windows.Forms.ComboBox cmbSeleccionarMatriz;
        private System.Windows.Forms.TextBox txtMatrizTransformacion;
        private System.Windows.Forms.Label lblDescripcionMatriz;

        // StatusStrip
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCursor;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBarrido;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusObjetos;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusZoom;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSimulacion;

        // Timer
        private System.Windows.Forms.Timer timerSimulacion;
    }
}
