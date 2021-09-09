namespace GenerateEXamlTool
{
    partial class GenerateEXamlTool
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
            this.listViewInfoOfPackages = new System.Windows.Forms.ListView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonChooseAssembly = new System.Windows.Forms.Button();
            this.buttonGenerateEXaml = new System.Windows.Forms.Button();
            this.textBoxAssemblyPath = new System.Windows.Forms.TextBox();
            this.textBoxXamlFilePath = new System.Windows.Forms.TextBox();
            this.buttonChooseXamlFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewInfoOfPackages
            // 
            this.listViewInfoOfPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewInfoOfPackages.HideSelection = false;
            this.listViewInfoOfPackages.Location = new System.Drawing.Point(29, 167);
            this.listViewInfoOfPackages.Name = "listViewInfoOfPackages";
            this.listViewInfoOfPackages.Size = new System.Drawing.Size(790, 443);
            this.listViewInfoOfPackages.TabIndex = 1;
            this.listViewInfoOfPackages.UseCompatibleStateImageBehavior = false;
            this.listViewInfoOfPackages.View = System.Windows.Forms.View.Details;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(246, 395);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 33);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Visible = false;
            // 
            // buttonChooseAssembly
            // 
            this.buttonChooseAssembly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChooseAssembly.Location = new System.Drawing.Point(29, 12);
            this.buttonChooseAssembly.Name = "buttonChooseAssembly";
            this.buttonChooseAssembly.Size = new System.Drawing.Size(163, 41);
            this.buttonChooseAssembly.TabIndex = 3;
            this.buttonChooseAssembly.Text = "Choose Assembly";
            this.buttonChooseAssembly.UseVisualStyleBackColor = true;
            this.buttonChooseAssembly.Click += new System.EventHandler(this.buttonChooseAssembly_Click);
            // 
            // buttonGenerateEXaml
            // 
            this.buttonGenerateEXaml.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenerateEXaml.Location = new System.Drawing.Point(891, 167);
            this.buttonGenerateEXaml.Name = "buttonGenerateEXaml";
            this.buttonGenerateEXaml.Size = new System.Drawing.Size(142, 41);
            this.buttonGenerateEXaml.TabIndex = 5;
            this.buttonGenerateEXaml.Text = "Generate";
            this.buttonGenerateEXaml.UseVisualStyleBackColor = true;
            this.buttonGenerateEXaml.Click += new System.EventHandler(this.buttonGenerateEXaml_Click);
            // 
            // textBoxAssemblyPath
            // 
            this.textBoxAssemblyPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAssemblyPath.Location = new System.Drawing.Point(224, 19);
            this.textBoxAssemblyPath.Name = "textBoxAssemblyPath";
            this.textBoxAssemblyPath.ReadOnly = true;
            this.textBoxAssemblyPath.Size = new System.Drawing.Size(809, 26);
            this.textBoxAssemblyPath.TabIndex = 6;
            // 
            // textBoxXamlFilePath
            // 
            this.textBoxXamlFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxXamlFilePath.Location = new System.Drawing.Point(224, 85);
            this.textBoxXamlFilePath.Name = "textBoxXamlFilePath";
            this.textBoxXamlFilePath.ReadOnly = true;
            this.textBoxXamlFilePath.Size = new System.Drawing.Size(809, 26);
            this.textBoxXamlFilePath.TabIndex = 8;
            // 
            // buttonChooseXamlFile
            // 
            this.buttonChooseXamlFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChooseXamlFile.Location = new System.Drawing.Point(29, 78);
            this.buttonChooseXamlFile.Name = "buttonChooseXamlFile";
            this.buttonChooseXamlFile.Size = new System.Drawing.Size(163, 41);
            this.buttonChooseXamlFile.TabIndex = 7;
            this.buttonChooseXamlFile.Text = "Choose Xaml File";
            this.buttonChooseXamlFile.UseVisualStyleBackColor = true;
            this.buttonChooseXamlFile.Click += new System.EventHandler(this.buttonChooseXamlFile_Click);
            // 
            // GenerateEXamlTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 622);
            this.Controls.Add(this.textBoxXamlFilePath);
            this.Controls.Add(this.buttonChooseXamlFile);
            this.Controls.Add(this.textBoxAssemblyPath);
            this.Controls.Add(this.buttonGenerateEXaml);
            this.Controls.Add(this.buttonChooseAssembly);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listViewInfoOfPackages);
            this.Name = "GenerateEXamlTool";
            this.Text = "GenerateEXaml Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewInfoOfPackages;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonChooseAssembly;
        private System.Windows.Forms.Button buttonGenerateEXaml;
        private System.Windows.Forms.TextBox textBoxAssemblyPath;
        private System.Windows.Forms.TextBox textBoxXamlFilePath;
        private System.Windows.Forms.Button buttonChooseXamlFile;
    }
}

