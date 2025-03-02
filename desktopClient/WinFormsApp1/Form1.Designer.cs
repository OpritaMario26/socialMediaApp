namespace WinFormsApp1
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
            label1 = new Label();
            usersList = new ListBox();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            approveButton = new Button();
            removeButton = new Button();
            label3 = new Label();
            searchBox = new TextBox();
            searchButton = new Button();
            searchDataGridView = new DataGridView();
            label4 = new Label();
            emailGridView = new DataGridView();
            sendEmailButton = new Button();
            subjectTextBox = new TextBox();
            bodyTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)searchDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emailGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(95, 21);
            label1.TabIndex = 1;
            label1.Text = "Active Users";
            // 
            // usersList
            // 
            usersList.FormattingEnabled = true;
            usersList.ItemHeight = 15;
            usersList.Location = new Point(12, 48);
            usersList.Name = "usersList";
            usersList.Size = new Size(173, 199);
            usersList.TabIndex = 2;
            usersList.SelectedIndexChanged += usersList_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(248, 48);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(639, 148);
            dataGridView1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(248, 9);
            label2.Name = "label2";
            label2.Size = new Size(106, 21);
            label2.TabIndex = 4;
            label2.Text = "Pending Posts";
            // 
            // approveButton
            // 
            approveButton.Location = new Point(248, 214);
            approveButton.Name = "approveButton";
            approveButton.Size = new Size(119, 43);
            approveButton.TabIndex = 5;
            approveButton.Text = "APPROVE";
            approveButton.UseVisualStyleBackColor = true;
            approveButton.Click += approveButton_Click;
            // 
            // removeButton
            // 
            removeButton.Location = new Point(373, 214);
            removeButton.Name = "removeButton";
            removeButton.Size = new Size(119, 43);
            removeButton.TabIndex = 6;
            removeButton.Text = "REMOVE";
            removeButton.UseVisualStyleBackColor = true;
            removeButton.Click += removeButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 308);
            label3.Name = "label3";
            label3.Size = new Size(90, 21);
            label3.TabIndex = 7;
            label3.Text = "Search Post";
            // 
            // searchBox
            // 
            searchBox.Location = new Point(12, 344);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(148, 23);
            searchBox.TabIndex = 8;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(193, 344);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(75, 23);
            searchButton.TabIndex = 9;
            searchButton.Text = "SEARCH";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // searchDataGridView
            // 
            searchDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchDataGridView.Location = new Point(12, 397);
            searchDataGridView.Name = "searchDataGridView";
            searchDataGridView.RowTemplate.Height = 25;
            searchDataGridView.Size = new Size(639, 150);
            searchDataGridView.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(731, 214);
            label4.Name = "label4";
            label4.Size = new Size(91, 21);
            label4.TabIndex = 11;
            label4.Text = "Send Email ";
            // 
            // emailGridView
            // 
            emailGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            emailGridView.Location = new Point(731, 257);
            emailGridView.Name = "emailGridView";
            emailGridView.RowTemplate.Height = 25;
            emailGridView.Size = new Size(363, 150);
            emailGridView.TabIndex = 12;
            // 
            // sendEmailButton
            // 
            sendEmailButton.Location = new Point(1114, 285);
            sendEmailButton.Name = "sendEmailButton";
            sendEmailButton.Size = new Size(102, 95);
            sendEmailButton.TabIndex = 13;
            sendEmailButton.Text = "SEND";
            sendEmailButton.UseVisualStyleBackColor = true;
            sendEmailButton.Click += sendEmailButton_Click;
            // 
            // subjectTextBox
            // 
            subjectTextBox.Location = new Point(731, 440);
            subjectTextBox.Name = "subjectTextBox";
            subjectTextBox.Size = new Size(457, 23);
            subjectTextBox.TabIndex = 14;
            // 
            // bodyTextBox
            // 
            bodyTextBox.Location = new Point(731, 480);
            bodyTextBox.Multiline = true;
            bodyTextBox.Name = "bodyTextBox";
            bodyTextBox.Size = new Size(457, 126);
            bodyTextBox.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1228, 618);
            Controls.Add(bodyTextBox);
            Controls.Add(subjectTextBox);
            Controls.Add(sendEmailButton);
            Controls.Add(emailGridView);
            Controls.Add(label4);
            Controls.Add(searchDataGridView);
            Controls.Add(searchButton);
            Controls.Add(searchBox);
            Controls.Add(label3);
            Controls.Add(removeButton);
            Controls.Add(approveButton);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(usersList);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Social media app";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)searchDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)emailGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ListBox usersList;
        private DataGridView dataGridView1;
        private Label label2;
        private Button approveButton;
        private Button removeButton;
        private Label label3;
        private TextBox searchBox;
        private Button searchButton;
        private DataGridView searchDataGridView;
        private Label label4;
        private DataGridView emailGridView;
        private Button sendEmailButton;
        private TextBox subjectTextBox;
        private TextBox bodyTextBox;
    }
}