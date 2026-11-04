namespace ProyectoPrimerParcial.Vista
{
    partial class FormAgregarObjeto
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.pnlCuerpo = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblModoCoord = new System.Windows.Forms.Label();
            this.rdoCartesianas = new System.Windows.Forms.RadioButton();
            this.rdoPolares = new System.Windows.Forms.RadioButton();
            this.lblCoord1 = new System.Windows.Forms.Label();
            this.txtCoord1 = new System.Windows.Forms.TextBox();
            this.lblCoord2 = new System.Windows.Forms.Label();
            this.txtCoord2 = new System.Windows.Forms.TextBox();
            this.lblVelocidad = new System.Windows.Forms.Label();
            this.txtVelocidad = new System.Windows.Forms.TextBox();
            this.lblRumbo = new System.Windows.Forms.Label();
            this.txtRumbo = new System.Windows.Forms.TextBox();
            this.lblAltitud = new System.Windows.Forms.Label();
            this.txtAltitud = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlEncabezado.SuspendLayout();
            this.pnlCuerpo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(460, 60);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(52, 211, 153);
            this.lblTitulo.Location = new System.Drawing.Point(14, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(325, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "➕ REGISTRO MANUAL DE AERONAVE 2D";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.lblSubtitulo.Location = new System.Drawing.Point(16, 33);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(398, 13);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Validación rigurosa de coordenadas cartesianas/polares y cinemática vectorial";
            // 
            // pnlCuerpo
            // 
            this.pnlCuerpo.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlCuerpo.Controls.Add(this.lblId);
            this.pnlCuerpo.Controls.Add(this.txtId);
            this.pnlCuerpo.Controls.Add(this.lblTipo);
            this.pnlCuerpo.Controls.Add(this.cmbTipo);
            this.pnlCuerpo.Controls.Add(this.lblModoCoord);
            this.pnlCuerpo.Controls.Add(this.rdoCartesianas);
            this.pnlCuerpo.Controls.Add(this.rdoPolares);
            this.pnlCuerpo.Controls.Add(this.lblCoord1);
            this.pnlCuerpo.Controls.Add(this.txtCoord1);
            this.pnlCuerpo.Controls.Add(this.lblCoord2);
            this.pnlCuerpo.Controls.Add(this.txtCoord2);
            this.pnlCuerpo.Controls.Add(this.lblVelocidad);
            this.pnlCuerpo.Controls.Add(this.txtVelocidad);
            this.pnlCuerpo.Controls.Add(this.lblRumbo);
            this.pnlCuerpo.Controls.Add(this.txtRumbo);
            this.pnlCuerpo.Controls.Add(this.lblAltitud);
            this.pnlCuerpo.Controls.Add(this.txtAltitud);
            this.pnlCuerpo.Controls.Add(this.lblColor);
            this.pnlCuerpo.Controls.Add(this.cmbColor);
            this.pnlCuerpo.Controls.Add(this.btnAceptar);
            this.pnlCuerpo.Controls.Add(this.btnCancelar);
            this.pnlCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCuerpo.Location = new System.Drawing.Point(0, 60);
            this.pnlCuerpo.Name = "pnlCuerpo";
            this.pnlCuerpo.Size = new System.Drawing.Size(460, 410);
            this.pnlCuerpo.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblId.Location = new System.Drawing.Point(18, 16);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(123, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Identificador (ID):";
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.White;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtId.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtId.Location = new System.Drawing.Point(180, 14);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(240, 22);
            this.txtId.TabIndex = 1;
            this.txtId.Text = "ALFA-01";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTipo.Location = new System.Drawing.Point(18, 48);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(111, 15);
            this.lblTipo.TabIndex = 2;
            this.lblTipo.Text = "Tipo de Aeronave:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.Color.White;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTipo.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "Avión",
            "Helicóptero",
            "OVNI",
            "Dron"});
            this.cmbTipo.Location = new System.Drawing.Point(180, 45);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(240, 23);
            this.cmbTipo.TabIndex = 3;
            // 
            // lblModoCoord
            // 
            this.lblModoCoord.AutoSize = true;
            this.lblModoCoord.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblModoCoord.ForeColor = System.Drawing.Color.FromArgb(2, 132, 199);
            this.lblModoCoord.Location = new System.Drawing.Point(18, 82);
            this.lblModoCoord.Name = "lblModoCoord";
            this.lblModoCoord.Size = new System.Drawing.Size(147, 15);
            this.lblModoCoord.TabIndex = 4;
            this.lblModoCoord.Text = "Sistema de Coordenadas:";
            // 
            // rdoCartesianas
            // 
            this.rdoCartesianas.AutoSize = true;
            this.rdoCartesianas.Checked = true;
            this.rdoCartesianas.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.rdoCartesianas.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoCartesianas.Location = new System.Drawing.Point(180, 80);
            this.rdoCartesianas.Name = "rdoCartesianas";
            this.rdoCartesianas.Size = new System.Drawing.Size(117, 19);
            this.rdoCartesianas.TabIndex = 5;
            this.rdoCartesianas.TabStop = true;
            this.rdoCartesianas.Text = "Cartesianas (X, Y)";
            this.rdoCartesianas.UseVisualStyleBackColor = true;
            this.rdoCartesianas.CheckedChanged += new System.EventHandler(this.RdoCoordenadas_CheckedChanged);
            // 
            // rdoPolares
            // 
            this.rdoPolares.AutoSize = true;
            this.rdoPolares.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.rdoPolares.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoPolares.Location = new System.Drawing.Point(310, 80);
            this.rdoPolares.Name = "rdoPolares";
            this.rdoPolares.Size = new System.Drawing.Size(95, 19);
            this.rdoPolares.TabIndex = 6;
            this.rdoPolares.Text = "Polares (r, θ)";
            this.rdoPolares.UseVisualStyleBackColor = true;
            this.rdoPolares.CheckedChanged += new System.EventHandler(this.RdoCoordenadas_CheckedChanged);
            // 
            // lblCoord1
            // 
            this.lblCoord1.AutoSize = true;
            this.lblCoord1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCoord1.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblCoord1.Location = new System.Drawing.Point(18, 115);
            this.lblCoord1.Name = "lblCoord1";
            this.lblCoord1.Size = new System.Drawing.Size(109, 15);
            this.lblCoord1.TabIndex = 7;
            this.lblCoord1.Text = "Posición X (unid):";
            // 
            // txtCoord1
            // 
            this.txtCoord1.BackColor = System.Drawing.Color.White;
            this.txtCoord1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCoord1.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtCoord1.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCoord1.Location = new System.Drawing.Point(180, 112);
            this.txtCoord1.Name = "txtCoord1";
            this.txtCoord1.Size = new System.Drawing.Size(240, 22);
            this.txtCoord1.TabIndex = 8;
            this.txtCoord1.Text = "80.0";
            // 
            // lblCoord2
            // 
            this.lblCoord2.AutoSize = true;
            this.lblCoord2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCoord2.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblCoord2.Location = new System.Drawing.Point(18, 147);
            this.lblCoord2.Name = "lblCoord2";
            this.lblCoord2.Size = new System.Drawing.Size(109, 15);
            this.lblCoord2.TabIndex = 9;
            this.lblCoord2.Text = "Posición Y (unid):";
            // 
            // txtCoord2
            // 
            this.txtCoord2.BackColor = System.Drawing.Color.White;
            this.txtCoord2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCoord2.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtCoord2.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtCoord2.Location = new System.Drawing.Point(180, 144);
            this.txtCoord2.Name = "txtCoord2";
            this.txtCoord2.Size = new System.Drawing.Size(240, 22);
            this.txtCoord2.TabIndex = 10;
            this.txtCoord2.Text = "60.0";
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblVelocidad.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblVelocidad.Location = new System.Drawing.Point(18, 179);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(117, 15);
            this.lblVelocidad.TabIndex = 11;
            this.lblVelocidad.Text = "Velocidad (u/tick):";
            // 
            // txtVelocidad
            // 
            this.txtVelocidad.BackColor = System.Drawing.Color.White;
            this.txtVelocidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVelocidad.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtVelocidad.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtVelocidad.Location = new System.Drawing.Point(180, 176);
            this.txtVelocidad.Name = "txtVelocidad";
            this.txtVelocidad.Size = new System.Drawing.Size(240, 22);
            this.txtVelocidad.TabIndex = 12;
            this.txtVelocidad.Text = "1.5";
            // 
            // lblRumbo
            // 
            this.lblRumbo.AutoSize = true;
            this.lblRumbo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRumbo.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblRumbo.Location = new System.Drawing.Point(18, 211);
            this.lblRumbo.Name = "lblRumbo";
            this.lblRumbo.Size = new System.Drawing.Size(133, 15);
            this.lblRumbo.TabIndex = 13;
            this.lblRumbo.Text = "Rumbo / Dirección (°):";
            // 
            // txtRumbo
            // 
            this.txtRumbo.BackColor = System.Drawing.Color.White;
            this.txtRumbo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRumbo.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtRumbo.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtRumbo.Location = new System.Drawing.Point(180, 208);
            this.txtRumbo.Name = "txtRumbo";
            this.txtRumbo.Size = new System.Drawing.Size(240, 22);
            this.txtRumbo.TabIndex = 14;
            this.txtRumbo.Text = "120.0";
            // 
            // lblAltitud
            // 
            this.lblAltitud.AutoSize = true;
            this.lblAltitud.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAltitud.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblAltitud.Location = new System.Drawing.Point(18, 243);
            this.lblAltitud.Name = "lblAltitud";
            this.lblAltitud.Size = new System.Drawing.Size(99, 15);
            this.lblAltitud.TabIndex = 15;
            this.lblAltitud.Text = "Altitud (metros):";
            // 
            // txtAltitud
            // 
            this.txtAltitud.BackColor = System.Drawing.Color.White;
            this.txtAltitud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAltitud.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtAltitud.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtAltitud.Location = new System.Drawing.Point(180, 240);
            this.txtAltitud.Name = "txtAltitud";
            this.txtAltitud.Size = new System.Drawing.Size(240, 22);
            this.txtAltitud.TabIndex = 16;
            this.txtAltitud.Text = "6500.0";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblColor.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblColor.Location = new System.Drawing.Point(18, 275);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(107, 15);
            this.lblColor.TabIndex = 17;
            this.lblColor.Text = "Color de Pantalla:";
            // 
            // cmbColor
            // 
            this.cmbColor.BackColor = System.Drawing.Color.White;
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbColor.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Items.AddRange(new object[] {
            "Verde Esmeralda (Amigable)",
            "Azul Cian (Civil / Enlace)",
            "Amarillo (Precaución)",
            "Naranja (Identificación Pendiente)",
            "Rojo (Hostil / Conflicto)",
            "Magenta (Experimental)"});
            this.cmbColor.Location = new System.Drawing.Point(180, 272);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(240, 23);
            this.cmbColor.TabIndex = 18;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(180, 320);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(130, 34);
            this.btnAceptar.TabIndex = 19;
            this.btnAceptar.Text = "✅ Registrar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(320, 320);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 34);
            this.btnCancelar.TabIndex = 20;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // FormAgregarObjeto
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(25)))));
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(460, 470);
            this.Controls.Add(this.pnlCuerpo);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAgregarObjeto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registro de Aeronave Táctica";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlCuerpo.ResumeLayout(false);
            this.pnlCuerpo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlCuerpo;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblModoCoord;
        private System.Windows.Forms.RadioButton rdoCartesianas;
        private System.Windows.Forms.RadioButton rdoPolares;
        private System.Windows.Forms.Label lblCoord1;
        private System.Windows.Forms.TextBox txtCoord1;
        private System.Windows.Forms.Label lblCoord2;
        private System.Windows.Forms.TextBox txtCoord2;
        private System.Windows.Forms.Label lblVelocidad;
        private System.Windows.Forms.TextBox txtVelocidad;
        private System.Windows.Forms.Label lblRumbo;
        private System.Windows.Forms.TextBox txtRumbo;
        private System.Windows.Forms.Label lblAltitud;
        private System.Windows.Forms.TextBox txtAltitud;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cmbColor;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
