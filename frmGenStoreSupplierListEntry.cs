using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoreInformationSystem.Data;
namespace StoreInformationSystem
{
    public partial class frmGenStoreSupplierListEntry : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        public frmGenStoreSupplierListEntry()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string SupplierName = txtSupplierName.Text.Trim();
            string sql = DAL.INSERT_tbl_GenStoreSupplierList(SupplierName);
            if (sql == "Succeeded")
            {
                MessageBox.Show("Succeeded");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Mainfrm.bGenStoreSupplierListEntry = true;
            this.Close();
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return") 
            {
                if (txtSupplierName.Text.ToString() != "")
                {
                    btnSave.Focus();
                    btnSave.Select();
                }
                else 
                {
                    MessageBox.Show("Please Write Supplier Name");
                    txtSupplierName.Focus();
                    txtSupplierName.Select();
                }
            }
        }

        private void txtSupplierName_Enter(object sender, EventArgs e)
        {
            txtSupplierName.BackColor = Color.Cyan;
            txtSupplierName.ForeColor = Color.Black;
        }

        private void txtSupplierName_Leave(object sender, EventArgs e)
        {
            txtSupplierName.BackColor = Color.White;
            txtSupplierName.ForeColor = Color.Black;
        }
    }
}
