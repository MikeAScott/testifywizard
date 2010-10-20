using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.TreeSurgeon.Core;
using ThoughtWorks.TreeSurgeon.Core.Configuration;
using SQS.Testify.Core;

namespace WindowsUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class TestifyWindowsStartupForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox projectNameTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.TextBox messagesTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button exitButton;
		private Label label6;
		private ComboBox templateComboBox;
		private Button btnImport;
		private OpenFileDialog openFileDialog1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TestifyWindowsStartupForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			SetButtonEnabledState();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			templateComboBox.DataSource = new TemplateListSource();
            templateComboBox.ValueMember = "Name";
            templateComboBox.DisplayMember = "Description";
			templateComboBox.SelectedIndex = 0;

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestifyWindowsStartupForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.messagesTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.exitButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(144, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Just enter the name for your new project and hit \'Generate\' !";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(62, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(468, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "It is recommended that project names don\'t have spaces - use camel case LikeThis " +
                "instead.";
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.Location = new System.Drawing.Point(206, 144);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(256, 20);
            this.projectNameTextBox.TabIndex = 2;
            this.projectNameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(110, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Project Name";
            // 
            // generateButton
            // 
            this.generateButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.generateButton.Location = new System.Drawing.Point(390, 200);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(72, 23);
            this.generateButton.TabIndex = 4;
            this.generateButton.Text = "Generate";
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // messagesTextBox
            // 
            this.messagesTextBox.Location = new System.Drawing.Point(104, 240);
            this.messagesTextBox.Multiline = true;
            this.messagesTextBox.Name = "messagesTextBox";
            this.messagesTextBox.ReadOnly = true;
            this.messagesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.messagesTextBox.Size = new System.Drawing.Size(472, 128);
            this.messagesTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Messages:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(192, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "Welcome to";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(288, 16);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(108, 23);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Testify";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(256, 384);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Template: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // templateComboBox
            // 
            this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(206, 173);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(256, 21);
            this.templateComboBox.TabIndex = 13;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(502, 173);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 14;
            this.btnImport.Text = "Import ...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.zip";
            this.openFileDialog1.Filter = "\"Zip Files|*.zip\"";
            this.openFileDialog1.Title = "Import a template File";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // TestifyWindowsStartupForm
            // 
            this.AcceptButton = this.generateButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(592, 422);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.templateComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.messagesTextBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.projectNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestifyWindowsStartupForm";
            this.Text = "Testify";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new TestifyWindowsStartupForm());
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.sqs-uk.com");
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			SetButtonEnabledState();
		}

		private void SetButtonEnabledState()
		{
			if (projectNameTextBox.Text.Length > 0)
			{
				generateButton.Enabled = true;
			}
			else
			{
				generateButton.Enabled = false;
			}
		}

		private void generateButton_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			messagesTextBox.Text = string.Format("\r\nStarting Tree Generation for {0}", projectNameTextBox.Text);
			Application.DoEvents();

			string version = (String)templateComboBox.SelectedValue; 

			try
			{
				string outputPath = new TreeSurgeonFrontEnd(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), version).GenerateDevelopmentTree("Testify",projectNameTextBox.Text, version);
				messagesTextBox.Text += string.Format("\r\nTree Generation complete. Files can be found at {0}", outputPath);
			}
			catch (Exception exc)
			{
				messagesTextBox.Text += string.Format("\r\nUnhandled Exception thrown. Details follow:\r\n{0}\r\n{1}", exc.Message, exc.StackTrace);
			}
			Cursor.Current = Cursors.Default;
		}

		private void exitButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		class TemplateListSource : System.ComponentModel.IListSource {
			public bool ContainsListCollection {
				get { return false; }
			}

			public System.Collections.IList GetList() {
				return new ArrayList(TemplateSettings.Current.Templates);
			}
		}

		private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {

		}

		private void btnImport_Click(object sender, EventArgs e) {
			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				String wd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				TemplateInstaller installer = new TemplateInstaller(wd, Path.Combine(wd, "resources"));
                installer.Install(openFileDialog1.FileName);
                messagesTextBox.Text += string.Format("{0} template has been installed.  Please restart testify to use this template", openFileDialog1.FileName);
			}

		}

	}
}
