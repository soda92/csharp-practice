﻿namespace contact_management_winforms
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
            lblName = new Label();
            txtName = new TextBox();
            lblPhoneNumber = new Label();
            txtPhoneNumber = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvContacts = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvContacts).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(22, 32);
            lblName.Margin = new Padding(6, 0, 6, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(69, 32);
            lblName.TabIndex = 0;
            lblName.Text = "名称:";
            // 
            // txtName
            // 
            txtName.Location = new Point(186, 26);
            txtName.Margin = new Padding(6, 6, 6, 6);
            txtName.Name = "txtName";
            txtName.Size = new Size(368, 39);
            txtName.TabIndex = 1;
            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new Point(22, 94);
            lblPhoneNumber.Margin = new Padding(6, 0, 6, 0);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(119, 32);
            lblPhoneNumber.TabIndex = 2;
            lblPhoneNumber.Text = "手机号码:";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(186, 87);
            txtPhoneNumber.Margin = new Padding(6, 6, 6, 6);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(368, 39);
            txtPhoneNumber.TabIndex = 2;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(22, 156);
            lblEmail.Margin = new Padding(6, 0, 6, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(76, 32);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(186, 149);
            txtEmail.Margin = new Padding(6, 6, 6, 6);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(368, 39);
            txtEmail.TabIndex = 3;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(22, 218);
            lblAddress.Margin = new Padding(6, 0, 6, 0);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(69, 32);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "住址:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(186, 211);
            txtAddress.Margin = new Padding(6, 6, 6, 6);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(368, 123);
            txtAddress.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(22, 363);
            btnAdd.Margin = new Padding(6, 6, 6, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(121, 49);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "添加";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(154, 363);
            btnUpdate.Margin = new Padding(6, 6, 6, 6);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(121, 49);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "修改";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(286, 363);
            btnDelete.Margin = new Padding(6, 6, 6, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(121, 49);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "删除";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(418, 363);
            btnClear.Margin = new Padding(6, 6, 6, 6);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(139, 49);
            btnClear.TabIndex = 8;
            btnClear.Text = "清空";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvContacts
            // 
            dgvContacts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvContacts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContacts.Location = new Point(594, 26);
            dgvContacts.Margin = new Padding(6, 6, 6, 6);
            dgvContacts.Name = "dgvContacts";
            dgvContacts.RowHeadersWidth = 82;
            dgvContacts.RowTemplate.Height = 25;
            dgvContacts.Size = new Size(869, 909);
            dgvContacts.TabIndex = 9;
            dgvContacts.SelectionChanged += dgvContacts_SelectionChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1486, 960);
            Controls.Add(dgvContacts);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtAddress);
            Controls.Add(lblAddress);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtPhoneNumber);
            Controls.Add(lblPhoneNumber);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Margin = new Padding(6, 6, 6, 6);
            Name = "Form1";
            Text = "通讯录管理";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvContacts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private Label lblPhoneNumber;
        private TextBox txtPhoneNumber;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblAddress;
        private TextBox txtAddress;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvContacts;
    }
}
