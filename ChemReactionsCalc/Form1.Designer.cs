namespace ChemReactionsCalc
{
    partial class Form1
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
            this.StartReactionButton = new System.Windows.Forms.Button();
            this.AnionListBox1 = new System.Windows.Forms.ListBox();
            this.CompoundLabel1 = new System.Windows.Forms.Label();
            this.CationListBox1 = new System.Windows.Forms.ListBox();
            this.AnionListBox2 = new System.Windows.Forms.ListBox();
            this.CationListBox2 = new System.Windows.Forms.ListBox();
            this.CompoundLabel2 = new System.Windows.Forms.Label();
            this.SolubilityLabel1 = new System.Windows.Forms.Label();
            this.SolubilityLabel2 = new System.Windows.Forms.Label();
            this.ReactionLabel = new System.Windows.Forms.Label();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.QuestionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartReactionButton
            // 
            this.StartReactionButton.Location = new System.Drawing.Point(11, 15);
            this.StartReactionButton.Name = "StartReactionButton";
            this.StartReactionButton.Size = new System.Drawing.Size(65, 37);
            this.StartReactionButton.TabIndex = 0;
            this.StartReactionButton.Text = "->";
            this.StartReactionButton.UseVisualStyleBackColor = true;
            this.StartReactionButton.Click += new System.EventHandler(this.StartReactionButton_Click);
            // 
            // AnionListBox1
            // 
            this.AnionListBox1.FormattingEnabled = true;
            this.AnionListBox1.ItemHeight = 20;
            this.AnionListBox1.Location = new System.Drawing.Point(163, 213);
            this.AnionListBox1.Name = "AnionListBox1";
            this.AnionListBox1.Size = new System.Drawing.Size(94, 324);
            this.AnionListBox1.TabIndex = 1;
            this.AnionListBox1.SelectedIndexChanged += new System.EventHandler(this.AnionListBox1_SelectedIndexChanged);
            // 
            // CompoundLabel1
            // 
            this.CompoundLabel1.AutoSize = true;
            this.CompoundLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompoundLabel1.Location = new System.Drawing.Point(67, 159);
            this.CompoundLabel1.Name = "CompoundLabel1";
            this.CompoundLabel1.Size = new System.Drawing.Size(139, 18);
            this.CompoundLabel1.TabIndex = 3;
            this.CompoundLabel1.Text = "CompoundLabel1";
            // 
            // CationListBox1
            // 
            this.CationListBox1.FormattingEnabled = true;
            this.CationListBox1.ItemHeight = 20;
            this.CationListBox1.Location = new System.Drawing.Point(67, 213);
            this.CationListBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CationListBox1.Name = "CationListBox1";
            this.CationListBox1.Size = new System.Drawing.Size(89, 324);
            this.CationListBox1.TabIndex = 4;
            this.CationListBox1.SelectedIndexChanged += new System.EventHandler(this.CationListBox1_SelectedIndexChanged);
            // 
            // AnionListBox2
            // 
            this.AnionListBox2.FormattingEnabled = true;
            this.AnionListBox2.ItemHeight = 20;
            this.AnionListBox2.Location = new System.Drawing.Point(404, 213);
            this.AnionListBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AnionListBox2.Name = "AnionListBox2";
            this.AnionListBox2.Size = new System.Drawing.Size(89, 324);
            this.AnionListBox2.TabIndex = 7;
            this.AnionListBox2.SelectedIndexChanged += new System.EventHandler(this.AnionListBox2_SelectedIndexChanged);
            // 
            // CationListBox2
            // 
            this.CationListBox2.FormattingEnabled = true;
            this.CationListBox2.ItemHeight = 20;
            this.CationListBox2.Location = new System.Drawing.Point(305, 213);
            this.CationListBox2.Name = "CationListBox2";
            this.CationListBox2.Size = new System.Drawing.Size(94, 324);
            this.CationListBox2.TabIndex = 5;
            this.CationListBox2.SelectedIndexChanged += new System.EventHandler(this.CationListBox2_SelectedIndexChanged);
            // 
            // CompoundLabel2
            // 
            this.CompoundLabel2.AutoSize = true;
            this.CompoundLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompoundLabel2.Location = new System.Drawing.Point(305, 159);
            this.CompoundLabel2.Name = "CompoundLabel2";
            this.CompoundLabel2.Size = new System.Drawing.Size(139, 18);
            this.CompoundLabel2.TabIndex = 8;
            this.CompoundLabel2.Text = "CompoundLabel2";
            // 
            // SolubilityLabel1
            // 
            this.SolubilityLabel1.AutoSize = true;
            this.SolubilityLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SolubilityLabel1.Location = new System.Drawing.Point(67, 185);
            this.SolubilityLabel1.Name = "SolubilityLabel1";
            this.SolubilityLabel1.Size = new System.Drawing.Size(109, 18);
            this.SolubilityLabel1.TabIndex = 14;
            this.SolubilityLabel1.Text = "SolubilityLabel1";
            // 
            // SolubilityLabel2
            // 
            this.SolubilityLabel2.AutoSize = true;
            this.SolubilityLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SolubilityLabel2.Location = new System.Drawing.Point(305, 185);
            this.SolubilityLabel2.Name = "SolubilityLabel2";
            this.SolubilityLabel2.Size = new System.Drawing.Size(109, 18);
            this.SolubilityLabel2.TabIndex = 15;
            this.SolubilityLabel2.Text = "SolubilityLabel2";
            // 
            // ReactionLabel
            // 
            this.ReactionLabel.AutoSize = true;
            this.ReactionLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReactionLabel.Location = new System.Drawing.Point(83, 20);
            this.ReactionLabel.Name = "ReactionLabel";
            this.ReactionLabel.Size = new System.Drawing.Size(133, 28);
            this.ReactionLabel.TabIndex = 16;
            this.ReactionLabel.Text = "ReactionLabel";
            this.ReactionLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ReactionLabel_MouseDown);
            this.ReactionLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ReactionLabel_MouseUp);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.MessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessageTextBox.Location = new System.Drawing.Point(11, 61);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(553, 89);
            this.MessageTextBox.TabIndex = 17;
            this.MessageTextBox.Text = "MessageTextBox";
            // 
            // QuestionButton
            // 
            this.QuestionButton.FlatAppearance.BorderSize = 0;
            this.QuestionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QuestionButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.QuestionButton.ForeColor = System.Drawing.Color.Navy;
            this.QuestionButton.Location = new System.Drawing.Point(538, 4);
            this.QuestionButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.QuestionButton.Name = "QuestionButton";
            this.QuestionButton.Size = new System.Drawing.Size(26, 31);
            this.QuestionButton.TabIndex = 18;
            this.QuestionButton.TabStop = false;
            this.QuestionButton.Text = "?";
            this.QuestionButton.UseVisualStyleBackColor = true;
            this.QuestionButton.Click += new System.EventHandler(this.QuestionButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(574, 567);
            this.Controls.Add(this.QuestionButton);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.ReactionLabel);
            this.Controls.Add(this.SolubilityLabel2);
            this.Controls.Add(this.SolubilityLabel1);
            this.Controls.Add(this.CompoundLabel2);
            this.Controls.Add(this.AnionListBox2);
            this.Controls.Add(this.CationListBox2);
            this.Controls.Add(this.CationListBox1);
            this.Controls.Add(this.CompoundLabel1);
            this.Controls.Add(this.AnionListBox1);
            this.Controls.Add(this.StartReactionButton);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button StartReactionButton;
        private ListBox AnionListBox1;
        private Label CompoundLabel1;
        private ListBox CationListBox1;
        private ListBox AnionListBox2;
        private ListBox CationListBox2;
        private Label CompoundLabel2;
        private Label SolubilityLabel1;
        private Label SolubilityLabel2;
        private Label ReactionLabel;
        private TextBox MessageTextBox;
        private Button QuestionButton;
    }
}