namespace ACA_GMW_2024
{
    partial class ShowPredecessor_Window
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
            splitContainer = new SplitContainer();
            PredecessorView = new PictureBox();
            PreviousStep = new Button();
            NextStep = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PredecessorView).BeginInit();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(PredecessorView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(PreviousStep);
            splitContainer.Panel2.Controls.Add(NextStep);
            splitContainer.Size = new Size(800, 450);
            splitContainer.SplitterDistance = 385;
            splitContainer.TabIndex = 1;
            // 
            // PredecessorView
            // 
            PredecessorView.Dock = DockStyle.Fill;
            PredecessorView.Location = new Point(0, 0);
            PredecessorView.Name = "PredecessorView";
            PredecessorView.Size = new Size(800, 385);
            PredecessorView.TabIndex = 1;
            PredecessorView.TabStop = false;
            // 
            // PreviousStep
            // 
            PreviousStep.Location = new Point(302, 26);
            PreviousStep.Name = "PreviousStep";
            PreviousStep.Size = new Size(75, 23);
            PreviousStep.TabIndex = 1;
            PreviousStep.Text = "<=";
            PreviousStep.UseVisualStyleBackColor = true;
            PreviousStep.Click += this.PreviousStep_Click;
            // 
            // NextStep
            // 
            NextStep.Location = new Point(441, 26);
            NextStep.Name = "NextStep";
            NextStep.Size = new Size(75, 23);
            NextStep.TabIndex = 0;
            NextStep.Text = "=>";
            NextStep.UseVisualStyleBackColor = true;
            NextStep.Click += NextStep_Click;
            // 
            // ShowPredecessor_Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            Name = "ShowPredecessor_Window";
            Text = "Form2";
            SizeChanged += WindowSizeChanged;
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PredecessorView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private PictureBox PredecessorView;
        private Button PreviousStep;
        private Button NextStep;
    }
}