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
    public partial class frmItemCategoryEntry : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        public frmItemCategoryEntry()
        {
            InitializeComponent();
            txtItemCategory.Focus();
            txtItemCategory.Select();
        }

        private void txtItemCategory_Enter(object sender, EventArgs e)
        {
            txtItemCategory.BackColor = Color.Cyan;
            txtItemCategory.ForeColor = Color.Black;
        }

        private void txtItemCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtItemCategory.Text.Trim().ToString() != "")
                {
                    btnSave.Focus();
                    btnSave.Select();
                }
                else
                {
                    MessageBox.Show("Please write Item Category");
                }
            }
        }

        private void txtCategorySerial_Enter(object sender, EventArgs e)
        {
            txtCategorySerial.BackColor = Color.Cyan;
            txtCategorySerial.ForeColor = Color.Black;
        }

        private void txtCategorySerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtCategorySerial.Text.Trim().ToString() != "")
                {
                    txtRemaks.Focus();
                    txtRemaks.Select(0, txtRemaks.TextLength);
                }
                else
                {
                    MessageBox.Show("Please write Category serial");
                }
            }
        }

        private void txtItemCategory_Leave(object sender, EventArgs e)
        {
            txtItemCategory.BackColor = Color.White;
            txtItemCategory.ForeColor = Color.Black;
        }

        private void txtCategorySerial_Leave(object sender, EventArgs e)
        {
            txtCategorySerial.BackColor = Color.White;
            txtCategorySerial.ForeColor = Color.Black;
        }

        private void txtRemaks_Enter(object sender, EventArgs e)
        {
            txtRemaks.BackColor = Color.Cyan;
            txtRemaks.ForeColor = Color.Black;
        }

        private void txtRemaks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtRemaks.Text.Trim().ToString() != "")
                {
                    btnSave.Focus();
                    btnSave.Select();
                }
                else
                {
                    MessageBox.Show("Please write Remarks");
                }

            }
        }

        private void txtRemaks_Leave(object sender, EventArgs e)
        {
            txtRemaks.BackColor = Color.White;
            txtRemaks.ForeColor = Color.Black;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Mainfrm.bItemCategoryEntry = true;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string itemcategory = txtItemCategory.Text.Trim();
            //string serial = txtCategorySerial.Text.Trim();
            //string Remarks=txtRemaks.Text;
            string sql = DAL.INSERT_tbl_GenStoreItemgroup(itemcategory);
            if (sql == "Succeeded")
            {
                MessageBox.Show("Succeeded");
            }
        }
    }
}
