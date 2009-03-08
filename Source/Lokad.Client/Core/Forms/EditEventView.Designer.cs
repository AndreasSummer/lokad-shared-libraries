namespace Lokad.Client.Forms
{
	partial class EditEventView
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
			this.components = new System.ComponentModel.Container();
			this._cancel = new System.Windows.Forms.Button();
			this._knownSinceCheck = new System.Windows.Forms.CheckBox();
			this._knownSinceDate = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._duration = new System.Windows.Forms.NumericUpDown();
			this._name = new System.Windows.Forms.TextBox();
			this._starts = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._ok = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._duration)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// _cancel
			// 
			this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.Location = new System.Drawing.Point(319, 242);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 30);
			this._cancel.TabIndex = 5;
			this._cancel.Text = "Cancel";
			this._cancel.UseVisualStyleBackColor = true;
			// 
			// _knownSinceCheck
			// 
			this._knownSinceCheck.AutoSize = true;
			this._knownSinceCheck.Location = new System.Drawing.Point(9, 123);
			this._knownSinceCheck.Name = "_knownSinceCheck";
			this._knownSinceCheck.Size = new System.Drawing.Size(111, 21);
			this._knownSinceCheck.TabIndex = 7;
			this._knownSinceCheck.Text = "Known Since";
			this._knownSinceCheck.UseVisualStyleBackColor = true;
			this._knownSinceCheck.CheckedChanged += new System.EventHandler(this._knownSinceCheck_CheckedChanged);
			// 
			// _knownSinceDate
			// 
			this._knownSinceDate.Enabled = false;
			this._knownSinceDate.Location = new System.Drawing.Point(29, 150);
			this._knownSinceDate.Name = "_knownSinceDate";
			this._knownSinceDate.Size = new System.Drawing.Size(200, 22);
			this._knownSinceDate.TabIndex = 3;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this._knownSinceCheck);
			this.groupBox1.Controls.Add(this._knownSinceDate);
			this.groupBox1.Controls.Add(this._duration);
			this.groupBox1.Controls.Add(this._name);
			this.groupBox1.Controls.Add(this._starts);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(382, 224);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Event Details";
			// 
			// _duration
			// 
			this._duration.DecimalPlaces = 2;
			this._duration.Location = new System.Drawing.Point(236, 95);
			this._duration.Maximum = new decimal(new int[] {
            365000,
            0,
            0,
            0});
			this._duration.Name = "_duration";
			this._duration.Size = new System.Drawing.Size(112, 22);
			this._duration.TabIndex = 2;
			// 
			// _name
			// 
			this._name.Location = new System.Drawing.Point(9, 50);
			this._name.Name = "_name";
			this._name.Size = new System.Drawing.Size(339, 22);
			this._name.TabIndex = 0;
			// 
			// _starts
			// 
			this._starts.Location = new System.Drawing.Point(9, 95);
			this._starts.Name = "_starts";
			this._starts.Size = new System.Drawing.Size(197, 22);
			this._starts.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Name:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(233, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(115, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Duration in days:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Starts:";
			// 
			// _ok
			// 
			this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._ok.Location = new System.Drawing.Point(238, 242);
			this._ok.Name = "_ok";
			this._ok.Size = new System.Drawing.Size(75, 30);
			this._ok.TabIndex = 4;
			this._ok.Text = "OK";
			this._ok.UseVisualStyleBackColor = true;
			this._ok.Click += new System.EventHandler(this._ok_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// EditEventView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(406, 284);
			this.Controls.Add(this._cancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._ok);
			this.MinimumSize = new System.Drawing.Size(424, 327);
			this.Name = "EditEventView";
			this.Text = "EditEventView";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._duration)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.CheckBox _knownSinceCheck;
		private System.Windows.Forms.DateTimePicker _knownSinceDate;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown _duration;
		private System.Windows.Forms.TextBox _name;
		private System.Windows.Forms.DateTimePicker _starts;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _ok;
		private System.Windows.Forms.ErrorProvider errorProvider1;
	}
}