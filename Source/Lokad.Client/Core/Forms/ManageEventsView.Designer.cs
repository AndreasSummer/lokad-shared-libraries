namespace Lokad.Client.Forms
{
	partial class ManageEventsView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageEventsView));
			this._eventPoolView = new System.Windows.Forms.DataGridView();
			this._nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._startsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._durationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._knownSinceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._eventPoolSource = new System.Windows.Forms.BindingSource(this.components);
			this._cancel = new System.Windows.Forms.Button();
			this._ok = new System.Windows.Forms.Button();
			this._skuEventsView = new System.Windows.Forms.DataGridView();
			this._skuEventSource = new System.Windows.Forms.BindingSource(this.components);
			this._add = new System.Windows.Forms.Button();
			this._remove = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._assignedEventsTitle = new System.Windows.Forms.Label();
			this._create = new System.Windows.Forms.Button();
			this._edit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._eventPoolView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._eventPoolSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._skuEventsView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._skuEventSource)).BeginInit();
			this.SuspendLayout();
			// 
			// _eventPoolView
			// 
			this._eventPoolView.AllowUserToDeleteRows = false;
			this._eventPoolView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._eventPoolView.AutoGenerateColumns = false;
			this._eventPoolView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._eventPoolView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._nameColumn,
            this._startsColumn,
            this._durationColumn,
            this._knownSinceColumn});
			this._eventPoolView.DataSource = this._eventPoolSource;
			this._eventPoolView.Location = new System.Drawing.Point(12, 29);
			this._eventPoolView.Name = "_eventPoolView";
			this._eventPoolView.RowTemplate.Height = 24;
			this._eventPoolView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._eventPoolView.Size = new System.Drawing.Size(626, 210);
			this._eventPoolView.TabIndex = 0;
			this._eventPoolView.SelectionChanged += new System.EventHandler(this._poolView_SelectionChanged);
			// 
			// _nameColumn
			// 
			this._nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this._nameColumn.DataPropertyName = "Name";
			this._nameColumn.HeaderText = "Name";
			this._nameColumn.Name = "_nameColumn";
			this._nameColumn.ReadOnly = true;
			// 
			// _startsColumn
			// 
			this._startsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this._startsColumn.DataPropertyName = "Starts";
			this._startsColumn.HeaderText = "Starts";
			this._startsColumn.Name = "_startsColumn";
			this._startsColumn.ReadOnly = true;
			this._startsColumn.Width = 70;
			// 
			// _durationColumn
			// 
			this._durationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this._durationColumn.DataPropertyName = "DurationAsString";
			this._durationColumn.HeaderText = "Duration";
			this._durationColumn.Name = "_durationColumn";
			this._durationColumn.ReadOnly = true;
			this._durationColumn.Width = 87;
			// 
			// _knownSinceColumn
			// 
			this._knownSinceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this._knownSinceColumn.DataPropertyName = "KnownSince";
			this._knownSinceColumn.HeaderText = "Known Since";
			this._knownSinceColumn.Name = "_knownSinceColumn";
			this._knownSinceColumn.ReadOnly = true;
			this._knownSinceColumn.Width = 105;
			// 
			// _eventPoolSource
			// 
			this._eventPoolSource.AllowNew = false;
			// 
			// _cancel
			// 
			this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.Location = new System.Drawing.Point(563, 489);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 30);
			this._cancel.TabIndex = 7;
			this._cancel.Text = "Cancel";
			this._cancel.UseVisualStyleBackColor = true;
			// 
			// _ok
			// 
			this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._ok.Location = new System.Drawing.Point(482, 489);
			this._ok.Name = "_ok";
			this._ok.Size = new System.Drawing.Size(75, 30);
			this._ok.TabIndex = 6;
			this._ok.Text = "OK";
			this._ok.UseVisualStyleBackColor = true;
			// 
			// _skuEventsView
			// 
			this._skuEventsView.AllowUserToDeleteRows = false;
			this._skuEventsView.AllowUserToOrderColumns = true;
			this._skuEventsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._skuEventsView.AutoGenerateColumns = false;
			this._skuEventsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._skuEventsView.DataSource = this._skuEventSource;
			this._skuEventsView.Location = new System.Drawing.Point(12, 297);
			this._skuEventsView.Name = "_skuEventsView";
			this._skuEventsView.RowTemplate.Height = 24;
			this._skuEventsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._skuEventsView.Size = new System.Drawing.Size(626, 150);
			this._skuEventsView.TabIndex = 3;
			this._skuEventsView.DoubleClick += new System.EventHandler(this._edit_Click);
			this._skuEventsView.SelectionChanged += new System.EventHandler(this._skuEvents_SelectionChanged);
			// 
			// _skuEventSource
			// 
			this._skuEventSource.AllowNew = false;
			// 
			// _add
			// 
			this._add.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this._add.Location = new System.Drawing.Point(156, 245);
			this._add.Name = "_add";
			this._add.Size = new System.Drawing.Size(160, 29);
			this._add.TabIndex = 1;
			this._add.Text = "Add Events";
			this._add.UseVisualStyleBackColor = true;
			this._add.Click += new System.EventHandler(this._add_Click);
			// 
			// _remove
			// 
			this._remove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this._remove.Location = new System.Drawing.Point(322, 245);
			this._remove.Name = "_remove";
			this._remove.Size = new System.Drawing.Size(160, 29);
			this._remove.TabIndex = 2;
			this._remove.Text = "Remove Events";
			this._remove.UseVisualStyleBackColor = true;
			this._remove.Click += new System.EventHandler(this._remove_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 17);
			this.label1.TabIndex = 6;
			this.label1.Text = "Available Events";
			// 
			// _assignedEventsTitle
			// 
			this._assignedEventsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._assignedEventsTitle.AutoSize = true;
			this._assignedEventsTitle.Location = new System.Drawing.Point(12, 277);
			this._assignedEventsTitle.Name = "_assignedEventsTitle";
			this._assignedEventsTitle.Size = new System.Drawing.Size(81, 17);
			this._assignedEventsTitle.TabIndex = 7;
			this._assignedEventsTitle.Text = "Item Events";
			// 
			// _create
			// 
			this._create.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this._create.Location = new System.Drawing.Point(156, 453);
			this._create.Name = "_create";
			this._create.Size = new System.Drawing.Size(160, 30);
			this._create.TabIndex = 4;
			this._create.Text = "Add New Event...";
			this._create.UseVisualStyleBackColor = true;
			this._create.Click += new System.EventHandler(this._create_Click);
			// 
			// _edit
			// 
			this._edit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this._edit.Location = new System.Drawing.Point(322, 453);
			this._edit.Name = "_edit";
			this._edit.Size = new System.Drawing.Size(160, 30);
			this._edit.TabIndex = 5;
			this._edit.Text = "Edit Selected Events...";
			this._edit.UseVisualStyleBackColor = true;
			this._edit.Click += new System.EventHandler(this._edit_Click);
			// 
			// ManageEventsView
			// 
			this.AcceptButton = this._ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(650, 531);
			this.Controls.Add(this._edit);
			this.Controls.Add(this._create);
			this.Controls.Add(this._assignedEventsTitle);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._remove);
			this.Controls.Add(this._add);
			this.Controls.Add(this._skuEventsView);
			this.Controls.Add(this._ok);
			this.Controls.Add(this._cancel);
			this.Controls.Add(this._eventPoolView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(537, 511);
			this.Name = "ManageEventsView";
			this.ShowInTaskbar = false;
			this.Text = "ManageEventsView";
			this.Shown += new System.EventHandler(this.EventManagerView_Shown);
			((System.ComponentModel.ISupportInitialize)(this._eventPoolView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._eventPoolSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._skuEventsView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._skuEventSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView _eventPoolView;
		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.Button _ok;
		private System.Windows.Forms.DataGridView _skuEventsView;
		private System.Windows.Forms.Button _add;
		private System.Windows.Forms.Button _remove;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label _assignedEventsTitle;
		private System.Windows.Forms.BindingSource _eventPoolSource;
		private System.Windows.Forms.BindingSource _skuEventSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn _nameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _startsColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _durationColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _knownSinceColumn;
		private System.Windows.Forms.Button _create;
		private System.Windows.Forms.Button _edit;
	}
}