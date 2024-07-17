namespace ACA_GMW_2024
{
    partial class InverseGameOfLife
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
            splitContainer = new SplitContainer();
            ResultField = new PictureBox();
            findPatterns = new Button();
            FP_Button = new Button();
            StepsBackward_Count = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ResultField).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StepsBackward_Count).BeginInit();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.BorderStyle = BorderStyle.Fixed3D;
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(ResultField);
            splitContainer.Panel1.RightToLeft = RightToLeft.No;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(findPatterns);
            splitContainer.Panel2.Controls.Add(FP_Button);
            splitContainer.Panel2.Controls.Add(StepsBackward_Count);
            splitContainer.Panel2.RightToLeft = RightToLeft.No;
            splitContainer.Size = new Size(967, 530);
            splitContainer.SplitterDistance = 418;
            splitContainer.TabIndex = 0;
            // 
            // ResultField
            // 
            ResultField.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ResultField.Location = new Point(0, 0);
            ResultField.Name = "ResultField";
            ResultField.Size = new Size(963, 414);
            ResultField.TabIndex = 0;
            ResultField.TabStop = false;
            ResultField.MouseDown += ResultField_MouseDown;
            // 
            // findPatterns
            // 
            findPatterns.Anchor = AnchorStyles.Bottom;
            findPatterns.Location = new Point(684, 57);
            findPatterns.Name = "findPatterns";
            findPatterns.Size = new Size(130, 23);
            findPatterns.TabIndex = 2;
            findPatterns.Text = "Find patterns";
            findPatterns.UseVisualStyleBackColor = true;
            findPatterns.Click += FindPatterns_Click;
            // 
            // FP_Button
            // 
            FP_Button.Anchor = AnchorStyles.Bottom;
            FP_Button.Location = new Point(500, 57);
            FP_Button.Name = "FP_Button";
            FP_Button.Size = new Size(135, 23);
            FP_Button.TabIndex = 1;
            FP_Button.Text = "Find precessors";
            FP_Button.UseVisualStyleBackColor = true;
            // 
            // StepsBackward_Count
            // 
            StepsBackward_Count.Anchor = AnchorStyles.Bottom;
            StepsBackward_Count.Location = new Point(203, 57);
            StepsBackward_Count.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            StepsBackward_Count.Name = "StepsBackward_Count";
            StepsBackward_Count.Size = new Size(120, 23);
            StepsBackward_Count.TabIndex = 0;
            StepsBackward_Count.Value = new decimal(new int[] { 1, 0, 0, 0 });
            StepsBackward_Count.ValueChanged += StepsBackward_Count_ValueChanged;
            // 
            // InverseGameOfLife
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(967, 530);
            Controls.Add(splitContainer);
            Name = "InverseGameOfLife";
            Text = "Constructive algorythm";
            SizeChanged += IGoL_Window_SizeChanged;
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ResultField).EndInit();
            ((System.ComponentModel.ISupportInitialize)StepsBackward_Count).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private Button FP_Button;
        private NumericUpDown StepsBackward_Count;
        private PictureBox ResultField;
        private Button findPatterns;
    }
}
