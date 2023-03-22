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
    public partial class frmItemEntry : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        public frmItemEntry()
        {
            InitializeComponent();
            CMBFILL();
            cmbGroupSearch.Focus();
            cmbGroupSearch.Select();
        }
        private void CMBFILL()
        {

            string sql = "select * from tbl_GenStoreItemList";
            DataTable dt = DPL.getDataTable(sql);
            cmbItemName.DataSource = dt;
            cmbItemName.DisplayMember = "ItemName";
            cmbItemName.ValueMember = "ItemListID";
            cmbItemName.SelectedValue = -1;


            string sqel = "select * from tbl_GenStoreItemgroup";
            DataTable dtt = DPL.getDataTable(sqel);
            cmbItemgroup.DataSource = dtt;
            cmbItemgroup.DisplayMember = "ItemGroupName";
            cmbItemgroup.ValueMember = "ItemgroupID";
            cmbItemgroup.SelectedValue = -1;

            string sqell = "select * from tbl_GenStoreItemgroup";
            DataTable dtd = DPL.getDataTable(sqell);
            cmbGroupSearch.DataSource = dtd;
            cmbGroupSearch.DisplayMember = "ItemGroupName";
            cmbGroupSearch.ValueMember = "ItemgroupID";
            cmbGroupSearch.SelectedValue = -1;
           
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbItemName.SelectedValue) > 0)
                {
                    cmbItemgroup.Focus();
                    cmbItemgroup.Select();
                }
                else
                {
                    MessageBox.Show("Please select item name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbItemName.Focus();
                    cmbItemName.Select();
                }
            }
        }

        private void cmbItemgroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbItemgroup.SelectedValue) > 0)
                {
                    btsave.Focus();
                    btsave.Select();
                }
                else
                {
                    MessageBox.Show("Please select item group", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbItemgroup.Focus();
                    cmbItemgroup.Select();
                }
            }
        }
        private void Update()
        {
            string UPDATE = "";
            try
            {
                if (cmbItemName.Text.Trim() != "" & cmbItemgroup.Text.Trim() != "")
                {
                    int ItemId = Convert.ToInt32(cmbItemName.SelectedValue);
                    int Itemgroup = Convert.ToInt32(cmbItemgroup.SelectedValue);
                    UPDATE = DAL.Update_Item_List(ItemId, Itemgroup);
                    if (UPDATE == "Succeeded")
                    {
                        MessageBox.Show("Succeeded");                        
                        cmbItemName.SelectedValue = -1;
                        //cmbItemgroup.SelectedValue = -1;
                        cmbItemName.Focus();
                        cmbItemName.Select();
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

        private void cmbGroupSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbGroupSearch.SelectedValue) > 0)
                {

                    int ItemGroupID = Convert.ToInt32(cmbGroupSearch.SelectedValue);

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
                    cmbGroupSearch.Focus();
                    cmbGroupSearch.Select();
                }
            }
        }
    }
}
