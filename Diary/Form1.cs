using System.Data;
using System.Xml.Linq;

namespace Diary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvData.SelectionChanged += dgvData_SelectionChanged;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DALDiary.CreateDataSQLite();
            DALDiary.CreateTableSQLite();
            ShowData();
            lblData.Text = DALDiary.path;
        }
        private void ShowData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DALDiary.GetContacts();
                dgvData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Contact contact = new Contact();
                contact.Id = Convert.ToInt32(txtId.Text);
                contact.Name = txtName.Text;
                contact.Email = txtEmail.Text;

                DALDiary.Add(contact);
                ShowData();

                txtId.Clear();
                txtName.Clear();
                txtEmail.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Contact contact = new Contact();
                contact.Id = Convert.ToInt32(txtId.Text);
                contact.Name = txtName.Text;
                contact.Email = txtEmail.Text;

                DALDiary.Update(contact);
                ShowData();

                txtId.Clear();
                txtName.Clear();
                txtEmail.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvData.SelectedRows[0].Cells["Id"].Value);
                    DALDiary.Delete(id);
                    ShowData();

                    txtId.Clear();
                    txtName.Clear();
                    txtEmail.Clear();
                }
                else
                {
                    MessageBox.Show("Selecione um registro para excluir.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (txtId.Text != "")
                {
                    int id = Convert.ToInt32(txtId.Text);
                    dt = DALDiary.GetContact(id);
                }
                else
                {
                    string name = txtName.Text;
                    dt = DALDiary.GetContacts(name);
                }
                dgvData.DataSource = dt;
                txtId.Clear();
                txtName.Clear();
                txtEmail.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvData.SelectedRows[0];
                txtId.Text = selectedRow.Cells["Id"].Value.ToString();
                txtName.Text = selectedRow.Cells["nome"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["email"].Value.ToString();
            }
        }
    }
}