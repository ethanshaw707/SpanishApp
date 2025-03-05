namespace ScrambleWordsUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblScrambled;
        private System.Windows.Forms.TextBox txtGuess;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnNewWord;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblScrambled = new System.Windows.Forms.Label();
            this.txtGuess = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnNewWord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblScrambled
            // 
            this.lblScrambled.AutoSize = true;
            this.lblScrambled.Location = new System.Drawing.Point(12, 20);
            this.lblScrambled.Name = "lblScrambled";
            this.lblScrambled.Size = new System.Drawing.Size(115, 13);
            this.lblScrambled.TabIndex = 0;
            this.lblScrambled.Text = "Unscramble this word:";
            // 
            // txtGuess
            // 
            this.txtGuess.Location = new System.Drawing.Point(15, 50);
            this.txtGuess.Name = "txtGuess";
            this.txtGuess.Size = new System.Drawing.Size(180, 20);
            this.txtGuess.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(210, 48);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnNewWord
            // 
            this.btnNewWord.Location = new System.Drawing.Point(15, 90);
            this.btnNewWord.Name = "btnNewWord";
            this.btnNewWord.Size = new System.Drawing.Size(270, 23);
            this.btnNewWord.TabIndex = 3;
            this.btnNewWord.Text = "New Word";
            this.btnNewWord.UseVisualStyleBackColor = true;
            this.btnNewWord.Click += new System.EventHandler(this.btnNewWord_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 130);
            this.Controls.Add(this.btnNewWord);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtGuess);
            this.Controls.Add(this.lblScrambled);
            this.Name = "Form1";
            this.Text = "Scramble Words Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
