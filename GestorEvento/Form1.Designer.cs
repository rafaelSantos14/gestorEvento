
namespace GestorEvento
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private MaterialSkin.Controls.MaterialButton btnTestarImpressao;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTestarImpressao = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // btnTestarImpressao
            // 
            this.btnTestarImpressao.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTestarImpressao.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnTestarImpressao.Depth = 0;
            this.btnTestarImpressao.HighEmphasis = true;
            this.btnTestarImpressao.Icon = null;
            this.btnTestarImpressao.Location = new System.Drawing.Point(241, 264);
            this.btnTestarImpressao.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnTestarImpressao.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnTestarImpressao.Name = "btnTestarImpressao";
            this.btnTestarImpressao.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnTestarImpressao.Size = new System.Drawing.Size(277, 36);
            this.btnTestarImpressao.TabIndex = 0;
            this.btnTestarImpressao.Text = "Testar Impressão - REFRIGERANTE";
            this.btnTestarImpressao.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnTestarImpressao.UseAccentColor = false;
            this.btnTestarImpressao.Click += new System.EventHandler(this.BtnTestarImpressao_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.btnTestarImpressao);
            this.Name = "Form1";
            this.Text = "GestorEvento - Teste de Conexão";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

