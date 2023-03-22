using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoreInformationSystem.Data;
using System.Linq;
using System.Collections;


namespace StoreInformationSystem
{
    public partial class EditItemForm : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        public EditItemForm()
        {
            InitializeComponent();
            cmbItemGroup.Focus();
            cmbItemGroup.Select();

            string sql2 = "Select ItemGroupName, ItemgroupID  From tbl_GenStoreItemgroup";
            DataTable dt2 = DPL.getDataTable(sql2);
            cmbItemGroup.DataSource = dt2;
            cmbItemGroup.DisplayMember = "ItemGroupName";
            cmbItemGroup.ValueMember = "ItemgroupID";
            cmbItemGroup.SelectedValue = -1;



            //string sql = "select * from tbl_GenStoreItemList";
            //DataTable dt = DPL.getDataTable(sql);
            //cmbItemName.DataSource = dt;
            //cmbItemName.DisplayMember = "ItemName";
            //cmbItemName.ValueMember = "ItemListID";
            //cmbItemName.SelectedValue = -1;
        }

        private void cmbItemGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbItemGroup.SelectedValue) > 0)
                {

                    int ItemGroupID = Convert.ToInt32(cmbItemGroup.SelectedValue);

                    string sql = "SELECT ItemName, ItemListID FROM dbo.tbl_GenStoreItemList WHERE (ItemgroupID = " + ItemGroupID + ")";
                    DataTable dt = DPL.getDataTable(sql);
                    cmbItemName.DataSource = dt;
                    cmbItemName.DisplayMember = "ItemName";
                    cmbItemName.ValueMember = "ItemListID";
                    cmbItemName.SelectedValue = -1;

                    cmbItemName.Focus();
                    cmbItemName.Select();
                }
                else
                {
                    MessageBox.Show("Please select search group", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbItemGroup.Focus();
                    cmbItemGroup.Select();
                }
            }
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbItemName.SelectedValue) > 0)
                {

                    string ItemName = cmbItemName.Text.ToString();
                    txtChangeName.Text = ItemName;
                    txtChangeName.Focus();                   
                }
                else
                {
                    MessageBox.Show("Please select item name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbItemName.Focus();
                    cmbItemName.Select();
                }
            }
        }
        private void Update()
        {
            string UPDATE = "";
            try
            {
                if (Convert.ToInt32(cmbItemGroup.SelectedValue) > 0 && Convert.ToInt32(cmbItemName.SelectedValue) > 0 && txtChangeName.Text.Trim().ToString() != "")
                {
                    DialogResult dr = MessageBox.Show("Are you sure want to change item name?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        string ChangeItemName = txtChangeName.Text.Trim().ToString().Replace("'", "''");
                        int ItemID = Convert.ToInt32(cmbItemName.SelectedValue);
                        UPDATE = DAL.Update_ItemName(ChangeItemName, ItemID);
                        if (UPDATE == "Succeeded")
                        {
                            MessageBox.Show("Succeeded");
                            cmbItemName.SelectedValue = -1;
                            txtChangeName.Text = "";




                            int ItemGroupID = Convert.ToInt32(cmbItemGroup.SelectedValue);

                            string sql = "SELECT ItemName, ItemListID FROM dbo.tbl_GenStoreItemList WHERE (ItemgroupID = " + ItemGroupID + ")";
                            DataTable dt = DPL.getDataTable(sql);
                            cmbItemName.DataSource = dt;
                            cmbItemName.DisplayMember = "ItemName";
                            cmbItemName.ValueMember = "ItemListID";
                            cmbItemName.SelectedValue = -1;

                            cmbItemName.Focus();
                            cmbItemName.Select();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Some essential value must be added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btsave_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void txtChangeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtChangeName.Text.Trim()!="")
                {                    
                    btsave.Focus();
                }
                else
                {
                    MessageBox.Show("Please Write correct item name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtChangeName.Focus();
                    txtChangeName.Select();
                }
            }
        }
    }
}
    

