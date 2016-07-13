namespace TestFor3DGraphic
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.PanelForDrawing = new System.Windows.Forms.Panel();
            this.SCBVertical = new System.Windows.Forms.VScrollBar();
            this.SCBHorizon = new System.Windows.Forms.HScrollBar();
            this.RTBInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(12, 12);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 23);
            this.BtnRefresh.TabIndex = 0;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefreshClick);
            // 
            // PanelForDrawing
            // 
            this.PanelForDrawing.Location = new System.Drawing.Point(12, 53);
            this.PanelForDrawing.Name = "PanelForDrawing";
            this.PanelForDrawing.Size = new System.Drawing.Size(378, 292);
            this.PanelForDrawing.TabIndex = 1;
            // 
            // SCBVertical
            // 
            this.SCBVertical.Location = new System.Drawing.Point(416, 53);
            this.SCBVertical.Name = "SCBVertical";
            this.SCBVertical.Size = new System.Drawing.Size(24, 291);
            this.SCBVertical.TabIndex = 2;
            this.SCBVertical.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SCBVerticalScroll);
            // 
            // SCBHorizon
            // 
            this.SCBHorizon.Location = new System.Drawing.Point(12, 364);
            this.SCBHorizon.Name = "SCBHorizon";
            this.SCBHorizon.Size = new System.Drawing.Size(378, 27);
            this.SCBHorizon.TabIndex = 3;
            this.SCBHorizon.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SCBHorizonScroll);
            // 
            // RTBInfo
            // 
            this.RTBInfo.Location = new System.Drawing.Point(461, 53);
            this.RTBInfo.Name = "RTBInfo";
            this.RTBInfo.Size = new System.Drawing.Size(496, 291);
            this.RTBInfo.TabIndex = 4;
            this.RTBInfo.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 400);
            this.Controls.Add(this.RTBInfo);
            this.Controls.Add(this.SCBHorizon);
            this.Controls.Add(this.SCBVertical);
            this.Controls.Add(this.PanelForDrawing);
            this.Controls.Add(this.BtnRefresh);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Panel PanelForDrawing;
        private System.Windows.Forms.VScrollBar SCBVertical;
        private System.Windows.Forms.HScrollBar SCBHorizon;
        private System.Windows.Forms.RichTextBox RTBInfo;
    }
}

