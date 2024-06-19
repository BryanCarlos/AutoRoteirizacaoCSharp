namespace AutoRoteirizacao
{
    partial class frmAutoRoteirizacao
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutoRoteirizacao));
            btnSelectMaterialList = new Button();
            SuspendLayout();
            // 
            // btnSelectMaterialList
            // 
            btnSelectMaterialList.Location = new Point(220, 154);
            btnSelectMaterialList.Name = "btnSelectMaterialList";
            btnSelectMaterialList.Size = new Size(115, 53);
            btnSelectMaterialList.TabIndex = 0;
            btnSelectMaterialList.Text = "Inserir Lista";
            btnSelectMaterialList.UseVisualStyleBackColor = true;
            btnSelectMaterialList.Click += btnSelectMaterialList_Click;
            // 
            // frmAutoRoteirizacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(53, 53, 53);
            ClientSize = new Size(556, 386);
            Controls.Add(btnSelectMaterialList);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmAutoRoteirizacao";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Auto Roteirização";
            ResumeLayout(false);
        }

        #endregion
        private Button btnSelectMaterialList;
    }
}
