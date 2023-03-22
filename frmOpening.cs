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
    public partial class frmOpening : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        public frmOpening()
        {
            InitializeComponent();
            CMBFILL();
            //date();
        }
        string dated = "";
        private void date()
        {
            string sql = "SELECT GETDATE() AS GetDate";
            DataTable dt = DPL.getDataTable(sql);
            dated = dt.Rows[0]["GetDate"].ToString();
            //DateTime datet = Convert.ToDateTime(date);
            //dateCheck = datet.ToString("dd/MMM/yyyy");
            //dateTimePicker1.Value = Convert.ToDateTime(date);
            //dateTimePicker3.Value = Convert.ToDateTime(date);
            //dateTimePicker4.Value = Convert.ToDateTime(date);
        }
        private void CMBFILL()
        {
            string sql = "Select UnitName, UnitListID  From tblUnitList";
            DataTable dt = DPL.getDataTable(sql);
            cmbUnit.DataSource = dt;
            cmbUnit.DisplayMember = "UnitName";
            cmbUnit.ValueMember = "UnitListID";
            cmbUnit.SelectedValue = -1;

            string sql2 = "Select ItemGroupName, ItemgroupID  From tbl_GenStoreItemgroup";
            DataTable dt2 = DPL.getDataTable(sql2);
            cmbItemGroup.DataSource = dt2;
            cmbItemGroup.DisplayMember = "ItemGroupName";
            cmbItemGroup.ValueMember = "ItemgroupID";
            cmbItemGroup.SelectedValue = -1;
        }

        private void cmbUnit_Enter(object sender, EventArgs e)
        {
            cmbUnit.BackColor = Color.Aquamarine;
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbUnit.SelectedValue) > 0)
                {
                    cmbItemGroup.Focus();
                    cmbItemGroup.Select();
                }
                else
                {
                    MessageBox.Show("Please select unit name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbUnit.Focus();
                    cmbUnit.Select();
                }
            }
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            cmbUnit.BackColor = Color.White;
        }

        private void cmbItemGroup_Enter(object sender, EventArgs e)
        {
            cmbItemGroup.BackColor = Color.Aquamarine;
        }

        private void cmbItemGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbUnit.SelectedValue) > 0)
                {
                    if (Convert.ToInt32(cmbItemGroup.SelectedValue) > 0)
                    {
                        int ItemGroupID = Convert.ToInt32(cmbItemGroup.SelectedValue);
                        string ItemGroupName = cmbItemGroup.Text.Trim().ToString();
                        int unitID = Convert.ToInt32(cmbUnit.SelectedValue);

                        string sql = "SELECT ItemName, ItemListID FROM dbo.tbl_GenStoreItemList WHERE (ItemgroupID = " + ItemGroupID + ")";
                        DataTable dt = DPL.getDataTable(sql);
                        cmbItemName.DataSource = dt;
                        cmbItemName.DisplayMember = "ItemName";
                        cmbItemName.ValueMember = "ItemListID";
                        cmbItemName.SelectedValue = -1;


                        string sqel = @"SELECT TOP 100 PERCENT dbo.tbl_GenStoreItemList.SerialNo, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID, 
                                              dbo.tbl_GenStoreOpening.OpeningQty, dbo.tbl_GenStoreOpening.UnitListID
                                              FROM  dbo.tbl_GenStoreItemList INNER JOIN
                                              dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID LEFT OUTER JOIN
                                              dbo.tbl_GenStoreOpening ON dbo.tbl_GenStoreItemList.ItemListID = dbo.tbl_GenStoreOpening.ItemListID
                                              WHERE (dbo.tbl_GenStoreItemgroup.ItemGroupName = '" + ItemGroupName + "')AND (dbo.tbl_GenStoreOpening.UnitListID = " + unitID + ")" +
                          "ORDER BY dbo.tbl_GenStoreItemList.SerialNo";



//                        string sqel = @"SELECT TOP 100 PERCENT dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID, 
//                      dbo.tbl_GenStoreOpening.OpeningQty, dbo.tbl_GenStoreOpening.UnitListID
//                      FROM dbo.tbl_GenStoreItemList INNER JOIN
//                      dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID LEFT OUTER JOIN
//                      dbo.tbl_GenStoreOpening ON dbo.tbl_GenStoreItemList.ItemListID = dbo.tbl_GenStoreOpening.ItemListID
//                      WHERE (dbo.tbl_GenStoreItemgroup.ItemGroupName = '" + ItemGroupName + "') AND (dbo.tbl_GenStoreOpening.UnitListID = 1)" +
//                           "ORDER BY dbo.tbl_GenStoreItemList.ItemListID";

                        DataTable dtt = DPL.getDataTable(sqel);

                        dtgvOpening.DataSource = dtt;

                        //cmbItemName.Focus();
                        //cmbItemName.Select();
                        dtgvOpening.Select();
                        dtgvOpening.Rows[0].Cells["OpenQty"].Selected = true;
                    }
                    else
                    {
                        MessageBox.Show("Please select group name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbItemGroup.Focus();
                        cmbItemGroup.Select();
                    }
                }
                else
                {
                    MessageBox.Show("Please select unit", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbUnit.Focus();
                    cmbUnit.Select();
                    cmbItemGroup.SelectedValue = -1;
                }
            }
        }
        
        
        private void cmbItemGroup_Leave(object sender, EventArgs e)
        {
            cmbItemGroup.BackColor = Color.White;
        }

        private void INSERT()
        {
            date();
            string Save = "";
            if (cmbUnit.Text.Trim().ToString() != "" )
            {
                if (cmbItemGroup.Text.Trim().ToString() != "")
                {
                    int unitID = Convert.ToInt32(cmbUnit.SelectedValue);
                    for (int i = 0; i < dtgvOpening.Rows.Count; i++)
                    {
                        if (dtgvOpening.Rows[i].Cells["unitid"].Value != null && dtgvOpening.Rows[i].Cells["unitid"].Value.ToString() != "")
                        {
                            if (dtgvOpening.Rows[i].Cells["OpenQty"].Value != null && dtgvOpening.Rows[i].Cells["OpenQty"].Value.ToString() != "")
                            {
                                int ItemID = Convert.ToInt32(dtgvOpening.Rows[i].Cells["ItemNameID"].Value);
                                decimal OpeningQty = Convert.ToDecimal(dtgvOpening.Rows[i].Cells["OpenQty"].Value);
                                //decimal OpeningQty = 0;
                                //Save = DAL.INSERT_tbl_GenStoreOpening(ItemID, OpeningQty, unitID);
                                Save = DAL.UPDATE_tbl_GenStoreOpening(ItemID, OpeningQty, unitID);
                            }
                        }
                        else
                        {
                            if (dtgvOpening.Rows[i].Cells["OpenQty"].Value != null && dtgvOpening.Rows[i].Cells["OpenQty"].Value.ToString() != "")
                            {
                                int ItemID = Convert.ToInt32(dtgvOpening.Rows[i].Cells["ItemNameID"].Value);
                                decimal OpeningQty = Convert.ToDecimal(dtgvOpening.Rows[i].Cells["OpenQty"].Value);
                                Save = DAL.INSERT_tbl_GenStoreOpening(ItemID, OpeningQty, unitID, dated);
                            }                           
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select item group");
                    cmbItemGroup.Focus();
                    cmbItemGroup.Select();
                }
            }
            else
            {
                MessageBox.Show("Please select unit name");
                cmbUnit.Focus();
                cmbUnit.Select();
            }
//            int ItemGroupID = Convert.ToInt32(cmbItemGroup.SelectedValue);
//            string ItemGroupName = cmbItemGroup.Text.Trim().ToString();
//            string sqel = @"SELECT TOP 100 PERCENT dbo.tbl_GenStoreItemList.SerialNo, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID, 
//                                              dbo.tbl_GenStoreOpening.OpeningQty, dbo.tbl_GenStoreOpening.UnitListID
//                                              FROM  dbo.tbl_GenStoreItemList INNER JOIN
//                                              dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID LEFT OUTER JOIN
//                                              dbo.tbl_GenStoreOpening ON dbo.tbl_GenStoreItemList.ItemListID = dbo.tbl_GenStoreOpening.ItemListID
//                                              WHERE (dbo.tbl_GenStoreItemgroup.ItemGroupName = '" + ItemGroupName + "')" +
//                         "ORDER BY dbo.tbl_GenStoreItemList.SerialNo";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            INSERT();
            this.btnSave.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Mainfrm.bGenOpening = true;
            this.Close();
        }
    }
}
