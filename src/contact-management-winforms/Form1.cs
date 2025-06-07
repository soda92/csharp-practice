using Microsoft.EntityFrameworkCore;
using System.Data;

namespace contact_management_winforms
{
    public partial class Form1 : Form
    {
        private ContactDbContext _dbContext;
        private Contact? _selectedContact;

        public Form1()
        {
            InitializeComponent();
            this.FormClosed += Form1_FormClosed; // Subscribe to the FormClosed event
            _dbContext = new ContactDbContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Ensure the database is created
                _dbContext.Database.EnsureCreated();
                ConfigureDataGridView();
                LoadContacts();
                ClearInputFields(); // Ensure fields are clear and buttons are in correct state initially
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing application: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvContacts.AutoGenerateColumns = false;
            dgvContacts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContacts.MultiSelect = false;
            dgvContacts.ReadOnly = true;
            dgvContacts.AllowUserToAddRows = false;

            dgvContacts.Columns.Clear(); // Clear existing columns if any

            dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Visible = false });
            dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Name", Width = 150 });
            dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PhoneNumber", HeaderText = "Phone Number", Width = 120 });
            dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 150 });
            dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Address", HeaderText = "Address", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private void LoadContacts()
        {
            try
            {
                dgvContacts.DataSource = _dbContext.Contacts.AsNoTracking().ToList();
                dgvContacts.ClearSelection(); // Important after refresh
                ClearInputFields(); // Reset fields and button states
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading contacts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtName.Clear();
            txtPhoneNumber.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            _selectedContact = null;
            dgvContacts.ClearSelection();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = true;
            txtName.Focus();
        }

        private void dgvContacts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count > 0)
            {
                var selectedRow = dgvContacts.SelectedRows[0];
                if (selectedRow.DataBoundItem is Contact contact)
                {
                    _selectedContact = contact; // Keep the original tracked entity if possible, or re-fetch
                    // For simplicity, we'll re-fetch if we need to update, or use the DataBoundItem directly if it's safe
                    // For this example, we'll populate fields from the selected contact for viewing
                    // and re-fetch or use _selectedContact.Id for DB operations.

                    txtName.Text = contact.Name;
                    txtPhoneNumber.Text = contact.PhoneNumber;
                    txtEmail.Text = contact.Email;
                    txtAddress.Text = contact.Address;

                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = false; // Disable add when an item is selected for editing
                }
            }
            else
            {
                // If selection is cleared programmatically or by user, reset.
                if (_selectedContact != null) // only clear if there was a selected contact
                {
                    ClearInputFields();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newContact = new Contact
            {
                Name = txtName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text
            };

            _dbContext.Contacts.Add(newContact);
            _dbContext.SaveChanges();
            LoadContacts(); // Refresh and clear fields
            MessageBox.Show("Contact added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedContact == null || dgvContacts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a contact to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fetch the entity from the context to ensure it's tracked
            var contactToUpdate = _dbContext.Contacts.Find(_selectedContact.Id);
            if (contactToUpdate == null)
            {
                MessageBox.Show("Contact not found in database. It might have been deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadContacts();
                return;
            }

            contactToUpdate.Name = txtName.Text;
            contactToUpdate.PhoneNumber = txtPhoneNumber.Text;
            contactToUpdate.Email = txtEmail.Text;
            contactToUpdate.Address = txtAddress.Text;

            _dbContext.SaveChanges();
            LoadContacts(); // Refresh and clear fields
            MessageBox.Show("Contact updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedContact == null || dgvContacts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a contact to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{_selectedContact.Name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var contactToDelete = _dbContext.Contacts.Find(_selectedContact.Id);
                if (contactToDelete != null)
                {
                    _dbContext.Contacts.Remove(contactToDelete);
                    _dbContext.SaveChanges();
                }
                else
                {
                     MessageBox.Show("Contact not found in database. It might have already been deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadContacts(); // Refresh and clear fields
                MessageBox.Show("Contact deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        // Handles the FormClosed event to dispose of the DbContext.
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _dbContext?.Dispose();
        }
    }
}
