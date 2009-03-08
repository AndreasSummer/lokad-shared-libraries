namespace Lokad.Client.Forms
{
	partial class DisplayEventsView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayEventsView));
			this._cancel = new System.Windows.Forms.Button();
			this._grid = new System.Windows.Forms.DataGridView();
			this._name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._start = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._knownSince = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
			this.SuspendLayout();
			// 
			// _cancel
			// 
			this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.Location = new System.Drawing.Point(461, 299);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 30);
			this._cancel.TabIndex = 1;
			this._cancel.Text = "Close";
			this._cancel.UseVisualStyleBackColor = true;
			// 
			// _grid
			// 
			this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._name,
            this._start,
            this._duration,
            this._knownSince});
			this._grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this._grid.Location = new System.Drawing.Point(12, 12);
			this._grid.Name = "_grid";
			this._grid.RowTemplate.Height = 24;
			this._grid.Size = new System.Drawing.Size(524, 281);
			this._grid.TabIndex = 0;
			// 
			// _name
			// 
			this._name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this._name.DataPropertyName = "Name";
			this._name.HeaderText = "Name";
			this._name.Name = "_name";
			this._name.ReadOnly = true;
			// 
			// _start
			// 
			this._start.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this._start.DataPropertyName = "Starts";
			this._start.HeaderText = "Start";
			this._start.Name = "_start";
			this._start.ReadOnly = true;
			this._start.Width = 63;
			// 
			// _duration
			// 
			this._duration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this._duration.DataPropertyName = "DurationAsString";
			this._duration.HeaderText = "Duration";
			this._duration.Name = "_duration";
			this._duration.ReadOnly = true;
			this._duration.Width = 87;
			// 
			// _knownSince
			// 
			this._knownSince.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this._knownSince.DataPropertyName = "KnownSince";
			this._knownSince.HeaderText = "Known Since";
			this._knownSince.Name = "_knownSince";
			this._knownSince.ReadOnly = true;
			this._knownSince.Width = 105;
			// 
			// DisplayEventsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(548, 341);
			this.Controls.Add(this._grid);
			this.Controls.Add(this._cancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(457, 275);
			this.Name = "DisplayEventsView";
			this.ShowInTaskbar = false;
			this.Text = "DisplayEventsView";
			((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.DataGridView _grid;
		private System.Windows.Forms.DataGridViewTextBoxColumn _name;
		private System.Windows.Forms.DataGridViewTextBoxColumn _start;
		private System.Windows.Forms.DataGridViewTextBoxColumn _duration;
		private System.Windows.Forms.DataGridViewTextBoxColumn _knownSince;
	}
}