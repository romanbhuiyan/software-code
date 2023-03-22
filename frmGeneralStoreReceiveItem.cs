using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using StoreInformationSystem.Data;

namespace StoreInformationSystem
{
    public partial class frmGeneralStoreReceiveItem : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        string UserName = UserLoginForm.UserName;
        public frmGeneralStoreReceiveItem()
        {
            InitializeComponent();
            CMBFILL();
            cmbReceiveUnit.Focus();
            cmbReceiveUnit.Select();
            cmbReceiveUnit.BackColor = Color.Aquamarine;
            date();
        }
        private void CMBFILL()
        {
            string sql = "Select UnitName, UnitListID  From tblUnitList";
            DataTable dt = DPL.getDataTable(sql);
            cmbReceiveUnit.DataSource = dt;
            cmbReceiveUnit.DisplayMember = "UnitName";
            cmbReceiveUnit.ValueMember = "UnitListID";
            cmbReceiveUnit.SelectedValue = 1;

            string sqll = "Select UnitName, UnitListID  From tblUnitList";
            DataTable dtt = DPL.getDataTable(sqll);
            cmbRcvdUnit.DataSource = dtt;
            cmbRcvdUnit.DisplayMember = "UnitName";
            cmbRcvdUnit.ValueMember = "UnitListID";
            cmbRcvdUnit.SelectedValue = -1;


            string sql1 = "Select GenStoreSupplierListName, GenStoreSupplierListID  From tbl_GenStoreSupplierList";
            DataTable dt1 = DPL.getDataTable(sql1);
            cmbSupplierUnit.DataSource = dt1;
            cmbSupplierUnit.DisplayMember = "GenStoreSupplierListName";
            cmbSupplierUnit.ValueMember = "GenStoreSupplierListID";
            cmbSupplierUnit.SelectedValue = -1;

            string sql2 = "Select ItemGroupName, ItemgroupID  From tbl_GenStoreItemgroup";
            DataTable dt2 = DPL.getDataTable(sql2);
            cmbItemGroup.DataSource = dt2;
            cmbItemGroup.DisplayMember = "ItemGroupName";
            cmbItemGroup.ValueMember = "ItemgroupID";
            cmbItemGroup.SelectedValue = -1;

            string sql3 = "Select MeasureUnitName, MeasureUnitID  From tblMeasureUnit";
            DataTable dt3 = DPL.getDataTable(sql3);
            cmbMeasureUnit.DataSource = dt3;
            cmbMeasureUnit.DisplayMember = "MeasureUnitName";
            cmbMeasureUnit.ValueMember = "MeasureUnitID";
            cmbMeasureUnit.SelectedValue = 1;

            string sql4 = "Select MeasureUnitName, MeasureUnitID  From tblMeasureUnit";
            DataTable dt4 = DPL.getDataTable(sql4);
            cmbPerMeasureUnit.DataSource = dt4;
            cmbPerMeasureUnit.DisplayMember = "MeasureUnitName";
            cmbPerMeasureUnit.ValueMember = "MeasureUnitID";
            cmbPerMeasureUnit.SelectedValue = 1;

            string sqel2 = "select DISTINCT  Location from tbl_GenStoreRcvdNormalDetails";
            DataTable dtd2 = DPL.getDataTable(sqel2);
            cmbLocation.DataSource = dtd2;
            cmbLocation.DisplayMember = "Location";
            //cmbLocation.ValueMember = "Location";
            cmbLocation.SelectedIndex = -1;

        }
        #region MemberVariable
        int NormalHeadMaxID = 0;
        int NormalDetailsID = 0;
        bool EDIT = false;
        decimal TotalQty = 0;
        int measureID = 0;
        string dateCheck = "";
        #endregion
        private void date()
        {
            string sql = "SELECT GETDATE() AS GetDate";
            DataTable dt = DPL.getDataTable(sql);
            string date = dt.Rows[0]["GetDate"].ToString();
            DateTime datet = Convert.ToDateTime(date);
            dateCheck = datet.ToString("dd/MMM/yyyy");
            dtpReceiveDate.Value = Convert.ToDateTime(date);
            dtpRequisitionDate.Value = Convert.ToDateTime(date);
            dtpFron.Value = Convert.ToDateTime(date);
            dtpTo.Value = Convert.ToDateTime(date);
        }
        private void cmbReceiveUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbReceiveUnit.SelectedValue) > 0)
                {
                    cmbSupplierUnit.Focus();
                    cmbSupplierUnit.Select();
                }
                else
                {
                    MessageBox.Show("Please select unit name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbReceiveUnit.Focus();
                    cmbReceiveUnit.Select();
                }
            }
        }

        private void cmbReceiveUnit_Leave(object sender, EventArgs e)
        {
            cmbReceiveUnit.BackColor = Color.White;
        }

        private void cmbSupplierUnit_Enter(object sender, EventArgs e)
        {
            cmbSupplierUnit.BackColor = Color.Aquamarine;
        }

        private void cmbSupplierUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {

                if (Mainfrm.SupplierID == 0)
                {
                    if (Convert.ToInt32(cmbSupplierUnit.SelectedValue) > 0)
                    {
                        dtpReceiveDate.Focus();
                        dtpReceiveDate.Select();
                    }
                    else
                    {
                        if (cmbSupplierUnit.Text.Trim().ToString() != "")
                        {
                            DialogResult dr = MessageBox.Show("It is not available ; would you like to add a new item in the list ?", "Warning", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                string supplierName = cmbSupplierUnit.Text.Trim().ToString();
                                frmEntryGS FOBH = new frmEntryGS();
                                FOBH.Show();
                                FOBH.txtSupplierName.Text = supplierName;
                                FOBH.txtItemList.Visible = false;
                                FOBH.label1.Visible = false;
                                FOBH.label2.Visible = false;
                                FOBH.txtSerial.Visible = false;                               
                                FOBH.txtSupplierName.Focus();
                                this.Enabled = false;
                            }
                            else
                            {
                                cmbSupplierUnit.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select supplier name or write supplier name");
                            cmbSupplierUnit.Focus();
                            cmbSupplierUnit.Select();
                        }

                    }
                }
                else
                {
                    dtpReceiveDate.Focus();
                    dtpReceiveDate.Select();
                }
            }
        }

        private void cmbSupplierUnit_Leave(object sender, EventArgs e)
        {
            cmbSupplierUnit.BackColor = Color.White;
        }

        private void dtpReceiveDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                int unitID = Convert.ToInt32(cmbReceiveUnit.SelectedValue);
                DataTable dt = DAL.GetMaxNumer("tbl_GenStoreRcvdNormalHead", "MRRNo", "UnitListID", unitID);
                string p = dt.Rows[0][0].ToString();
                if (p != "")
                {
                    int k = Convert.ToInt32(p);
                    int MRRNo = k + 1;
                    txtMRRNo.Text = MRRNo.ToString();
                }
                else
                {
                    txtMRRNo.Text = "1";
                }
                txtMRRNo.Focus();
                txtMRRNo.Select();
            }
        }

        private void txtRequisitionNo_Enter(object sender, EventArgs e)
        {
            txtRequisitionNo.BackColor = Color.Aquamarine;
        }

        private void txtRequisitionNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                dtpRequisitionDate.Focus();
                dtpRequisitionDate.Select();
            }
        }

        private void txtRequisitionNo_Leave(object sender, EventArgs e)
        {
            txtRequisitionNo.BackColor = Color.White;
        }

        private void dtpRequisitionDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                txtHeadNote.Focus();
                txtHeadNote.Select();
            }
        }

        private void txtHeadNote_Enter(object sender, EventArgs e)
        {
            txtHeadNote.BackColor = Color.Aquamarine;
        }

        private void txtHeadNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                cmbItemGroup.Focus();
                cmbItemGroup.Select();
            }
        }

        private void txtHeadNote_Leave(object sender, EventArgs e)
        {
            txtHeadNote.BackColor = Color.White;
        }

        private void cmbItemGroup_Enter(object sender, EventArgs e)
        {
            cmbItemGroup.BackColor = Color.Aquamarine;
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
                    MessageBox.Show("Please select group name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbItemGroup.Focus();
                    cmbItemGroup.Select();
                }
            }
        }

        private void cmbItemGroup_Leave(object sender, EventArgs e)
        {
            cmbItemGroup.BackColor = Color.White;
        }

        private void cmbItemName_Enter(object sender, EventArgs e)
        {
            cmbItemName.BackColor = Color.Aquamarine;
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (cmbItemGroup.Text.Trim().ToString() != "")
                {
                    if (Mainfrm.ID == 0)
                    {
                        if (Convert.ToInt32(cmbItemName.SelectedValue) > 0)
                        {
                            numRcvdQty.Focus();
                            numRcvdQty.Select(0, 10);
                        }
                        else
                        {
                            if (cmbItemName.Text.Trim().ToString() != "")
                            {
                                DialogResult dr = MessageBox.Show("It is not available ; would you like to add a new item in the list ?", "Warning", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.Yes)
                                {
                                    string ItemName = cmbItemName.Text.Trim().ToString();
                                    frmEntryGS FOBH = new frmEntryGS();
                                    FOBH.Show();
                                    FOBH.txtItemList.Text = ItemName;
                                    FOBH.GroupID = Convert.ToInt32(cmbItemGroup.SelectedValue);
                                    FOBH.txtSupplierName.Visible = false;
                                    FOBH.label4.Visible = false;
                                    FOBH.txtItemList.Focus();
                                    this.Enabled = false;
                                }
                                else
                                {
                                    cmbItemGroup.Focus();
                                }
                                //MessageBox.Show("Please select item name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //cmbItemName.Focus();
                                //cmbItemName.Select();
                            }
                            else
                            {
                                MessageBox.Show("Please select item name or write item name");
                                cmbItemName.Focus();
                                cmbItemName.Select();
                            }
                        }
                    }
                    else
                    {
                        numRcvdQty.Focus();
                        numRcvdQty.Select(0, 10);
                    }
                }
                else
                {
                    MessageBox.Show("Please select item group");
                    cmbItemGroup.Focus();
                    cmbItemGroup.Select();
                }
            }
        }

        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            cmbItemName.BackColor = Color.White;
        }

        private void numRcvdQty_Enter(object sender, EventArgs e)
        {
            numRcvdQty.BackColor = Color.Aquamarine;
        }

        private void numRcvdQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(numRcvdQty.Value) > 0)
                {
                    cmbMeasureUnit.Focus();
                    cmbMeasureUnit.Select();
                }
                else
                {
                    MessageBox.Show("Please write receive qty", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numRcvdQty.Focus();
                    numRcvdQty.Select();
                }
            }
        }

        private void numRcvdQty_Leave(object sender, EventArgs e)
        {
            numRcvdQty.BackColor = Color.White;
        }

        private void cmbMeasureUnit_Enter(object sender, EventArgs e)
        {
            cmbMeasureUnit.BackColor = Color.Aquamarine;
        }

        private void cmbMeasureUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbMeasureUnit.SelectedValue) > 0)
                {
                    numPerUnit.Focus();
                    numPerUnit.Select(0, 10);
                }
                else
                {
                    MessageBox.Show("Please select measure unit", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMeasureUnit.Focus();
                    cmbMeasureUnit.Select();
                }
            }
        }

        private void cmbMeasureUnit_Leave(object sender, EventArgs e)
        {
            cmbMeasureUnit.BackColor = Color.White;
        }

        private void numPerUnit_Enter(object sender, EventArgs e)
        {
            cmbMeasureUnit.BackColor = Color.White;
        }

        private void numPerUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                cmbPerMeasureUnit.Focus();
                cmbPerMeasureUnit.Select();
                ////decimal TotalQty = 0;
                //if (Convert.ToInt32(numPerUnit.Value) > 0)
                //{
                //    //TotalQty = Convert.ToDecimal(numRcvdQty.Value) * Convert.ToDecimal(numPerUnit.Value);
                //    cmbPerMeasureUnit.Focus();
                //    cmbPerMeasureUnit.Select();
                //}
                //else
                //{
                //    //TotalQty = Convert.ToDecimal(numRcvdQty.Value);
                //    //MessageBox.Show("Please write per unit qty", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    cmbPerMeasureUnit.Focus();
                //    cmbPerMeasureUnit.Select();

                //}
            }
        }

        private void numPerUnit_Leave(object sender, EventArgs e)
        {
            cmbMeasureUnit.BackColor = Color.White;
        }

        private void cmbPerMeasureUnit_Enter(object sender, EventArgs e)
        {
            cmbPerMeasureUnit.BackColor = Color.Aquamarine;
        }

        private void cmbPerMeasureUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbPerMeasureUnit.SelectedValue) > 0)
                {
                    if (Convert.ToInt32(numPerUnit.Value) > 0)
                    {
                        TotalQty = Convert.ToDecimal(numRcvdQty.Value) * Convert.ToDecimal(numPerUnit.Value);
                        measureID = Convert.ToInt32(cmbPerMeasureUnit.SelectedValue);
                        numPrice.Focus();
                        numPrice.Select(0, 15);
                    }
                    else
                    {
                        TotalQty = Convert.ToDecimal(numRcvdQty.Value);
                        measureID = Convert.ToInt32(cmbMeasureUnit.SelectedValue);
                        //MessageBox.Show("Please write per unit qty", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        numPrice.Focus();
                        numPrice.Select(0, 15);

                    }
                    
                    //numPrice.Focus();
                    //numPrice.Select(0, 10);
                }
                else
                {
                    MessageBox.Show("Please select measure per unit", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbPerMeasureUnit.Focus();
                    cmbPerMeasureUnit.Select();
                }
            }
        }

        private void cmbPerMeasureUnit_Leave(object sender, EventArgs e)
        {
            cmbPerMeasureUnit.BackColor = Color.White;
        }

        private void numPrice_Enter(object sender, EventArgs e)
        {
            numPrice.BackColor = Color.Aquamarine;
        }

        private void numPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                decimal totalvalue = TotalQty * Convert.ToDecimal(numPrice.Value);
                txtTotalAmount.Text = totalvalue.ToString();
                txtBillNo.Focus();
                txtBillNo.Select(); ;
            }
        }

        private void numPrice_Leave(object sender, EventArgs e)
        {
            numPrice.BackColor = Color.White;
        }

        private void txtBillNo_Enter(object sender, EventArgs e)
        {
            txtBillNo.BackColor = Color.Aquamarine;
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtBillNo.Text.Trim().ToString() != "")
                {
                    cmbLocation.Focus();
                    cmbLocation.Select();
                }
                else
                {
                    MessageBox.Show("Please write bill no", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBillNo.Focus();
                    txtBillNo.Select();
                }
            }
        }

        private void txtBillNo_Leave(object sender, EventArgs e)
        {
            txtBillNo.BackColor = Color.White;
        }

        private void txtLocation_Enter(object sender, EventArgs e)
        {
            txtBillNo.BackColor = Color.Aquamarine;
        }

        
        private void txtLocation_Leave(object sender, EventArgs e)
        {
            txtBillNo.BackColor = Color.White;
        }

        private void txtRemarks_Enter(object sender, EventArgs e)
        {
            txtRemarks.BackColor = Color.Aquamarine;
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                btnSave.Focus();
                btnSave.Select();
            }
        }

        private void txtRemarks_Leave(object sender, EventArgs e)
        {
            txtRemarks.BackColor = Color.White;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Mainfrm.GeReceive = true;
            this.Close();
        }
        private void CLEAN()
        {
            cmbItemGroup.SelectedValue = -1;
            cmbItemName.SelectedValue = -1;
            cmbItemName.Text = "";
            numRcvdQty.Value = 0;
            cmbMeasureUnit.SelectedValue = 1;
            numPerUnit.Value = 0;
            cmbPerMeasureUnit.SelectedValue = 1;
            numPrice.Value = 0;
            txtBillNo.Text = "";
            cmbLocation.Text = "";
            txtRemarks.Text = "";
            txtTotalAmount.Text = "";
            NormalDetailsID = 0;
            EDIT = false;
            Mainfrm.ID = 0;
            Mainfrm.SupplierID = 0;
            TotalQty = 0;

            string sqel2 = "select DISTINCT  Location from tbl_GenStoreRcvdNormalDetails";
            DataTable dtd2 = DPL.getDataTable(sqel2);
            cmbLocation.DataSource = dtd2;
            cmbLocation.DisplayMember = "Location";
            cmbLocation.ValueMember = "Location";
            cmbLocation.SelectedIndex = -1;
        }

        private void ShowAllData()
        {
            //            string sql = @"SELECT TOP 100 PERCENT dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreRcvdNormalDetails.RcvdQty, 
            //                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.PerUnitQty, 
            //                      PerMeauserUnit.MeasureUnitName AS PerMeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.Price, dbo.tbl_GenStoreRcvdNormalDetails.BillNo, 
            //                      dbo.tbl_GenStoreRcvdNormalDetails.Location,dbo.tbl_GenStoreRcvdNormalDetails.Remarks, dbo.tbl_GenStoreRcvdNormalDetails.ItemgroupID, dbo.tbl_GenStoreRcvdNormalDetails.ItemListID, 
            //                      dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID, dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID, 
            //                      dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID
            //                      FROM dbo.tbl_GenStoreRcvdNormalDetails INNER JOIN
            //                      dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreRcvdNormalDetails.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID INNER JOIN
            //                      dbo.tbl_GenStoreItemList ON dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
            //                      dbo.tbl_GenStoreMeasureUnit ON dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID = dbo.tbl_GenStoreMeasureUnit.MeasureUnitID INNER JOIN
            //                      dbo.tbl_GenStoreMeasureUnit PerMeauserUnit ON dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID = PerMeauserUnit.MeasureUnitID
            //                      WHERE (dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalHeadID = " + NormalHeadMaxID + ")" +
            //                      "ORDER BY dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID";
            string sql = @"SELECT     TOP 100 PERCENT dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreRcvdNormalDetails.RcvdQty, 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.PerUnitQty, 
                      PerMeauserUnit.MeasureUnitName AS PerMeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.Price, 
                      dbo.tbl_GenStoreRcvdNormalDetails.TotalQty * dbo.tbl_GenStoreRcvdNormalDetails.Price AS TotalAmount, 
                      dbo.tbl_GenStoreRcvdNormalDetails.BillNo, dbo.tbl_GenStoreRcvdNormalDetails.Location, dbo.tbl_GenStoreRcvdNormalDetails.Remarks, 
                      dbo.tbl_GenStoreItemList.ItemgroupID, dbo.tbl_GenStoreRcvdNormalDetails.ItemListID, dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID, 
                      dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID, dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID
                      FROM dbo.tbl_GenStoreRcvdNormalDetails INNER JOIN
                      dbo.tbl_GenStoreItemList ON dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit ON dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID = dbo.tbl_GenStoreMeasureUnit.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit PerMeauserUnit ON 
                      dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID = PerMeauserUnit.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID
                      WHERE     (dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalHeadID = " + NormalHeadMaxID + ")" +
                      "ORDER BY dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID";
            DataTable dt = DPL.getDataTable(sql);
            dtgviewGStoreReceive.DataSource = dt;
            dtgviewGStoreReceive.Columns["ItemgroupID"].Visible = false;
            dtgviewGStoreReceive.Columns["ItemListID"].Visible = false;
            dtgviewGStoreReceive.Columns["MeasureUnitID"].Visible = false;
            dtgviewGStoreReceive.Columns["PerMeasureUnitID"].Visible = false;
            dtgviewGStoreReceive.Columns["GenStoreRcvdNormalDetailsID"].Visible = false;
        }
        private void UPDATEHEAD()
        {
            string UPDATEHEAD = "";          
            int ReceiveUnitID = Convert.ToInt32(cmbReceiveUnit.SelectedValue);

            int SupplierUnitID = 0;
            if (Mainfrm.SupplierID == 0)
            {

                SupplierUnitID = Convert.ToInt32(cmbSupplierUnit.SelectedValue);
            }
            else
            {
                SupplierUnitID = Mainfrm.SupplierID;
            }
            if (NormalHeadMaxID != 0 && cmbReceiveUnit.Text.Trim().ToString() != "" && SupplierUnitID != 0 && txtMRRNo.Text.Trim().ToString() != "")
            {

                string ReceiveDateTime = dtpReceiveDate.Value.ToString("dd/MMM/yyyy hh:mm:ss tt");
                string ReceiveDate = dtpReceiveDate.Value.ToString("dd/MMM/yyyy");
                int MRRNo = Convert.ToInt32(txtMRRNo.Text);
                string RequisitionNo = txtRequisitionNo.Text.Trim().ToString().Replace("'", "''");
                string RequisitionDate = dtpRequisitionDate.Value.ToString("dd/MMM/yyyy");
                string HeadNote = txtHeadNote.Text.Trim().ToString().Replace("'", "''");
                UPDATEHEAD = DAL.UPDATE_tbl_GenStoreRcvdNormalHead(ReceiveUnitID, SupplierUnitID, ReceiveDateTime, MRRNo, RequisitionNo, RequisitionDate, HeadNote, ReceiveDate, NormalHeadMaxID, UserName);
                if (UPDATEHEAD == "Succeeded")
                {
                    MessageBox.Show("Head Update Succeeded");
                }

            }
            else
            {
                MessageBox.Show("Some essential value must be added", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void INSERT()
        {
            string INSERTHEAD = "";
            string INSERTDETAILS = "";
            int ReceiveUnitID = Convert.ToInt32(cmbReceiveUnit.SelectedValue);

            int SupplierUnitID = 0;
            if (Mainfrm.SupplierID == 0)
            {

                SupplierUnitID = Convert.ToInt32(cmbSupplierUnit.SelectedValue);
            }
            else
            {
                SupplierUnitID = Mainfrm.SupplierID;
            }
            if (cmbReceiveUnit.Text.Trim().ToString() != "" && SupplierUnitID != 0 && txtMRRNo.Text.Trim().ToString() != "" && cmbItemGroup.Text.Trim().ToString() != "" && numRcvdQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbPerMeasureUnit.Text.Trim().ToString() != "" && cmbLocation.Text.Trim().ToString() != "" && txtBillNo.Text.Trim().ToString() != "")
            {
                if (NormalHeadMaxID == 0)
                {
                    //int ReceiveUnitID = Convert.ToInt32(cmbReceiveUnit.SelectedValue);

                    //int SupplierUnitID = 0;
                    //if (Mainfrm.SupplierID == 0)
                    //{

                    //    SupplierUnitID = Convert.ToInt32(cmbSupplierUnit.SelectedValue);
                    //}
                    //else
                    //{
                    //    SupplierUnitID = Mainfrm.SupplierID;
                    //}
                    //int SupplierUnitID = Convert.ToInt32(cmbSupplierUnit.SelectedValue);
                    string datetd = dtpReceiveDate.Value.ToString("dd/MMM/yyyy");
                    if (datetd == dateCheck)
                    {
                        date();
                    }
                    string ReceiveDateTime = dtpReceiveDate.Value.ToString("dd/MMM/yyyy hh:mm:ss tt");
                    string ReceiveDate = dtpReceiveDate.Value.ToString("dd/MMM/yyyy");
                    int MRRNo = Convert.ToInt32(txtMRRNo.Text);
                    string RequisitionNo = txtRequisitionNo.Text.Trim().ToString().Replace("'", "''");
                    string RequisitionDate = dtpRequisitionDate.Value.ToString("dd/MMM/yyyy");
                    string HeadNote = txtHeadNote.Text.Trim().ToString().Replace("'", "''");
                    INSERTHEAD = DAL.INSERT_tbl_GenStoreRcvdNormalHead(ReceiveUnitID, SupplierUnitID, ReceiveDateTime, MRRNo, RequisitionNo, RequisitionDate, HeadNote, ReceiveDate, UserName);
                    if (INSERTHEAD == "Succeeded")
                    {
                        DataTable dt = DAL.GetMaxID("tbl_GenStoreRcvdNormalHead", "GenStoreRcvdNormalHeadID");
                        string maxid = dt.Rows[0][0].ToString();
                        if (maxid != "")
                        {
                            NormalHeadMaxID = Convert.ToInt32(maxid);
                        }
                    }
                }
                if (NormalHeadMaxID != 0)
                {
                    int ItemNameID = 0;
                    if (Mainfrm.ID == 0)
                    {

                        ItemNameID = Convert.ToInt32(cmbItemName.SelectedValue);
                    }
                    else
                    {
                        ItemNameID = Mainfrm.ID;
                    }
                    if (cmbItemGroup.Text.Trim().ToString() != "" && ItemNameID != 0 && numRcvdQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbPerMeasureUnit.Text.Trim().ToString() != "" && cmbLocation.Text.Trim().ToString() != "" && txtBillNo.Text.Trim().ToString() != "")
                    {
                        //int ItemNameID = 0;
                        //if (Mainfrm.ID == 0)
                        //{

                        //    ItemNameID = Convert.ToInt32(cmbItemName.SelectedValue);
                        //}
                        //else
                        //{
                        //    ItemNameID = Mainfrm.ID;
                        //}
                        decimal RcvdQty = Convert.ToDecimal(numRcvdQty.Value);
                        int MeasureUnitID = Convert.ToInt32(cmbMeasureUnit.SelectedValue);
                        decimal PerUnitQty = Convert.ToDecimal(numPerUnit.Value);
                        int PerMeasureUnitID = Convert.ToInt32(cmbPerMeasureUnit.SelectedValue);
                        decimal Price = Convert.ToDecimal(numPrice.Value);
                        string BillNo = txtBillNo.Text.Trim().ToString().Replace("'", "''");
                        string Location = cmbLocation.Text.Trim().ToString().Replace("'", "''");
                        string Remarks = txtRemarks.Text.Trim().ToString().Replace("'", "''");
                        INSERTDETAILS = DAL.INSERT_tbl_GenStoreRcvdNormalDetails(ItemNameID, RcvdQty, MeasureUnitID, PerUnitQty, PerMeasureUnitID, Price, BillNo, Location, Remarks, NormalHeadMaxID, TotalQty, measureID,UserName);
                        if (INSERTDETAILS == "Succeeded")
                        {
                            MessageBox.Show("Save Succeeded");
                            ShowAllData();
                            CLEAN();
                            cmbItemGroup.Focus();
                            cmbItemGroup.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Some essential value must be added", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Some essential value must be added", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Some essential value must be added");
            }
        }

        private void btnSave_Enter(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.Aquamarine;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!EDIT)
             {
                if (cmbItemGroup.Text.Trim().ToString() != "" && cmbItemName.Text.Trim().ToString() != "" && numRcvdQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbPerMeasureUnit.Text.Trim().ToString() != "" && txtBillNo.Text.Trim().ToString() != "")
                {
                    INSERT();
                }
                else
                {
                    UPDATEHEAD();
                }
            }
            else
            {
                UPDATE();
            }
        }
        private void UPDATE()
        {
            string UPDATEDETAILS = "";

            string UPDATEHEAD = "";
            int ReceiveUnitID = Convert.ToInt32(cmbReceiveUnit.SelectedValue);

            int SupplierUnitID = 0;
            if (Mainfrm.SupplierID == 0)
            {

                SupplierUnitID = Convert.ToInt32(cmbSupplierUnit.SelectedValue);
            }
            else
            {
                SupplierUnitID = Mainfrm.SupplierID;
            }

            int ItemNameID = 0;
            if (Mainfrm.ID == 0)
            {

                ItemNameID = Convert.ToInt32(cmbItemName.SelectedValue);
            }
            else
            {
                ItemNameID = Mainfrm.ID;
            }

            if (NormalDetailsID != 0 && cmbItemGroup.Text.Trim().ToString() != "" && ItemNameID != 0 && numRcvdQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbPerMeasureUnit.Text.Trim().ToString() != "" && txtBillNo.Text.Trim().ToString() != "" && NormalHeadMaxID != 0 && cmbReceiveUnit.Text.Trim().ToString() != "" && SupplierUnitID != 0 && txtMRRNo.Text.Trim().ToString() != "")
            {

                string ReceiveDateTime = dtpReceiveDate.Value.ToString("dd/MMM/yyyy hh:mm:ss tt");
                string ReceiveDate = dtpReceiveDate.Value.ToString("dd/MMM/yyyy");
                int MRRNo = Convert.ToInt32(txtMRRNo.Text);
                string RequisitionNo = txtRequisitionNo.Text.Trim().ToString().Replace("'", "''");
                string RequisitionDate = dtpRequisitionDate.Value.ToString("dd/MMM/yyyy");
                string HeadNote = txtHeadNote.Text.Trim().ToString().Replace("'", "''");
                UPDATEHEAD = DAL.UPDATE_tbl_GenStoreRcvdNormalHead(ReceiveUnitID, SupplierUnitID, ReceiveDateTime, MRRNo, RequisitionNo, RequisitionDate, HeadNote, ReceiveDate, NormalHeadMaxID,UserName);

               

                decimal RcvdQty = Convert.ToDecimal(numRcvdQty.Value);
                int MeasureUnitID = Convert.ToInt32(cmbMeasureUnit.SelectedValue);
                decimal PerUnitQty = Convert.ToDecimal(numPerUnit.Value);
                int PerMeasureUnitID = Convert.ToInt32(cmbPerMeasureUnit.SelectedValue);
                decimal Price = Convert.ToDecimal(numPrice.Value);
                string BillNo = txtBillNo.Text.Trim().ToString().Replace("'", "''");
                string Location = cmbLocation.Text.Trim().ToString().Replace("'", "''");
                string Remarks = txtRemarks.Text.Trim().ToString().Replace("'", "''");
                UPDATEDETAILS = DAL.UPDATE_tbl_GenStoreRcvdNormalDetails(ItemNameID, RcvdQty, MeasureUnitID, PerUnitQty, PerMeasureUnitID, Price, BillNo, Location, Remarks, NormalDetailsID, TotalQty, measureID, UserName);
                if (UPDATEDETAILS == "Succeeded")
                {
                    MessageBox.Show("Update Succeeded");
                    ShowAllData();
                    CLEAN();
                    cmbItemGroup.Focus();
                    cmbItemGroup.Select();
                }
            }
            else
            {
                MessageBox.Show("Some essential value must be added", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLEAN();                
            }
        }
        

        private void btnSave_Leave(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.White;
        }

        private void cmbRcvdUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (e.KeyCode.ToString() == "Return")
                {
                    if (Convert.ToInt32(cmbRcvdUnit.SelectedValue) > 0)
                    {
                        int unitid = Convert.ToInt32(cmbRcvdUnit.SelectedValue);
                        string sql = "SELECT MRRNo, UnitListID FROM dbo.tbl_GenStoreRcvdNormalHead WHERE (UnitListID = " + unitid + ")";
                        DataTable dt = DPL.getDataTable(sql);
                        cmbMRRNo.DataSource = dt;
                        cmbMRRNo.DisplayMember = "MRRNo";
                        cmbMRRNo.SelectedIndex = -1;
                        cmbMRRNo.Focus();
                        cmbMRRNo.Select(0, 10);



                        string sqel = @"SELECT dbo.tblUnitList.UnitName, dbo.tbl_GenStoreRcvdNormalHead.MRRNo, dbo.tbl_GenStoreRcvdNormalHead.RcvDateTime, 
                                     dbo.tbl_GenStoreRcvdNormalHead.UnitListID, dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID
                                     FROM dbo.tbl_GenStoreRcvdNormalHead INNER JOIN
                                     dbo.tblUnitList ON dbo.tbl_GenStoreRcvdNormalHead.UnitListID = dbo.tblUnitList.UnitListID
                                     WHERE (dbo.tbl_GenStoreRcvdNormalHead.UnitListID = " + unitid + ")ORDER BY dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID";
                        DataTable dd = DPL.getDataTable(sqel);
                        PreEntrydtgveiw.DataSource = dd;
                        PreEntrydtgveiw.Columns["UnitListID"].Visible = false;
                        PreEntrydtgveiw.Columns["GenStoreRcvdNormalHeadID"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Please select unit name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbRcvdUnit.Focus();
                        cmbRcvdUnit.Select();
                    }
                }
            }
        }

        private void cmbMRRNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (e.KeyCode.ToString() == "Return")
                {
                    if (cmbMRRNo.Text.Trim().ToString() != "")
                    {
                        int unitid = Convert.ToInt32(cmbRcvdUnit.SelectedValue);
                        int mrrno = Convert.ToInt32(cmbMRRNo.Text.Trim().ToString());

                        string sql = @"SELECT dbo.tblUnitList.UnitName, dbo.tbl_GenStoreRcvdNormalHead.MRRNo, dbo.tbl_GenStoreRcvdNormalHead.RcvDateTime, 
                                     dbo.tbl_GenStoreRcvdNormalHead.UnitListID, dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID
                                     FROM dbo.tbl_GenStoreRcvdNormalHead INNER JOIN
                                     dbo.tblUnitList ON dbo.tbl_GenStoreRcvdNormalHead.UnitListID = dbo.tblUnitList.UnitListID
                                     WHERE (dbo.tbl_GenStoreRcvdNormalHead.MRRNo = " + mrrno + ") AND (dbo.tbl_GenStoreRcvdNormalHead.UnitListID = " + unitid + ")ORDER BY dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID";
                        DataTable dd = DPL.getDataTable(sql);
                        PreEntrydtgveiw.DataSource = dd;
                        PreEntrydtgveiw.Columns["UnitListID"].Visible = false;
                        PreEntrydtgveiw.Columns["GenStoreRcvdNormalHeadID"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Please select M.R.R.No", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbMRRNo.Focus();
                        cmbMRRNo.Select();
                    }
                }
            }
        }

        private void PreEntrydtgveiw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int HeadID = Convert.ToInt32(PreEntrydtgveiw.Rows[e.RowIndex].Cells["GenStoreRcvdNormalHeadID"].Value.ToString());
                //                string sql = @"SELECT TOP 100 PERCENT dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreRcvdNormalDetails.RcvdQty, 
                //                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.PerUnitQty, 
                //                      PerMeauserUnit.MeasureUnitName AS PerMeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.Price, dbo.tbl_GenStoreRcvdNormalDetails.BillNo, 
                //                      dbo.tbl_GenStoreRcvdNormalDetails.Location,dbo.tbl_GenStoreRcvdNormalDetails.Remarks, dbo.tbl_GenStoreRcvdNormalDetails.ItemgroupID, dbo.tbl_GenStoreRcvdNormalDetails.ItemListID, 
                //                      dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID, dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID, 
                //                      dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID
                //                      FROM dbo.tbl_GenStoreRcvdNormalDetails INNER JOIN
                //                      dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreRcvdNormalDetails.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID INNER JOIN
                //                      dbo.tbl_GenStoreItemList ON dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                //                      dbo.tbl_GenStoreMeasureUnit ON dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID = dbo.tbl_GenStoreMeasureUnit.MeasureUnitID INNER JOIN
                //                      dbo.tbl_GenStoreMeasureUnit PerMeauserUnit ON dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID = PerMeauserUnit.MeasureUnitID
                //                      WHERE (dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalHeadID = " + HeadID + ")" +
                //                      "ORDER BY dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID";

                string sql = @"SELECT     TOP 100 PERCENT dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreRcvdNormalDetails.RcvdQty, 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.PerUnitQty, 
                      PerMeauserUnit.MeasureUnitName AS PerMeasureUnitName, dbo.tbl_GenStoreRcvdNormalDetails.Price, 
                      dbo.tbl_GenStoreRcvdNormalDetails.PerUnitQty * dbo.tbl_GenStoreRcvdNormalDetails.Price AS TotalAmount, 
                      dbo.tbl_GenStoreRcvdNormalDetails.BillNo, dbo.tbl_GenStoreRcvdNormalDetails.Location, dbo.tbl_GenStoreRcvdNormalDetails.Remarks, 
                      dbo.tbl_GenStoreItemList.ItemgroupID, dbo.tbl_GenStoreRcvdNormalDetails.ItemListID, dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID, 
                      dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID, dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID
                      FROM                  dbo.tbl_GenStoreRcvdNormalDetails INNER JOIN
                      dbo.tbl_GenStoreItemList ON dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit ON dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID = dbo.tbl_GenStoreMeasureUnit.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit PerMeauserUnit ON 
                      dbo.tbl_GenStoreRcvdNormalDetails.PerMeasureUnitID = PerMeauserUnit.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID
                      WHERE     (dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalHeadID = "+HeadID+")" +
                      "ORDER BY dbo.tbl_GenStoreRcvdNormalDetails.GenStoreRcvdNormalDetailsID";
                DataTable dt = DPL.getDataTable(sql);
                preEntrydtgDetails.DataSource = dt;
                preEntrydtgDetails.Columns["ItemgroupID"].Visible = false;
                preEntrydtgDetails.Columns["ItemListID"].Visible = false;
                preEntrydtgDetails.Columns["MeasureUnitID"].Visible = false;
                preEntrydtgDetails.Columns["PerMeasureUnitID"].Visible = false;
                preEntrydtgDetails.Columns["GenStoreRcvdNormalDetailsID"].Visible = false;
            }
        }

        private void PreEntrydtgveiw_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                NormalHeadMaxID = Convert.ToInt32(PreEntrydtgveiw.Rows[e.RowIndex].Cells["GenStoreRcvdNormalHeadID"].Value.ToString());
                string sql = @"SELECT UnitListID, GenStoreSupplierListID, RcvDateTime, MRRNo, RequisitionNo, RequisitionDate, HeadNote, GenStoreRcvdNormalHeadID
                             FROM dbo.tbl_GenStoreRcvdNormalHead
                             WHERE (GenStoreRcvdNormalHeadID = " + NormalHeadMaxID + ")";
                DataTable dtd = DPL.getDataTable(sql);

                cmbReceiveUnit.SelectedValue = Convert.ToInt32(dtd.Rows[0]["UnitListID"].ToString());
                cmbSupplierUnit.SelectedValue = Convert.ToInt32(dtd.Rows[0]["GenStoreSupplierListID"].ToString());
                string ReceiveDate = dtd.Rows[0]["RcvDateTime"].ToString();
                dtpReceiveDate.Value = Convert.ToDateTime(ReceiveDate);
                txtMRRNo.Text = dtd.Rows[0]["MRRNo"].ToString();
                txtRequisitionNo.Text = dtd.Rows[0]["RequisitionNo"].ToString();
                string RequisitionDate = dtd.Rows[0]["RequisitionDate"].ToString();
                dtpRequisitionDate.Value = Convert.ToDateTime(RequisitionDate);
                txtHeadNote.Text = dtd.Rows[0]["HeadNote"].ToString();

                ShowAllData();
                tabControl1.SelectedIndex = 0;
            }
        }
        private void EDITUPDATE()
        {
            int groupid = Convert.ToInt32(dtgviewGStoreReceive.SelectedRows[0].Cells["ItemgroupID"].Value.ToString());
            cmbItemGroup.SelectedValue = Convert.ToInt32(dtgviewGStoreReceive.SelectedRows[0].Cells["ItemgroupID"].Value.ToString());
            int ItemID = Convert.ToInt32(dtgviewGStoreReceive.SelectedRows[0].Cells["ItemListID"].Value.ToString());
            numRcvdQty.Value = Convert.ToDecimal(dtgviewGStoreReceive.SelectedRows[0].Cells["RcvdQty"].Value.ToString());
            cmbMeasureUnit.SelectedValue = Convert.ToInt32(dtgviewGStoreReceive.SelectedRows[0].Cells["MeasureUnitID"].Value.ToString());
            numPerUnit.Value = Convert.ToDecimal(dtgviewGStoreReceive.SelectedRows[0].Cells["PerUnitQty"].Value.ToString());
            cmbPerMeasureUnit.SelectedValue = Convert.ToInt32(dtgviewGStoreReceive.SelectedRows[0].Cells["PerMeasureUnitID"].Value.ToString());
            numPrice.Value = Convert.ToDecimal(dtgviewGStoreReceive.SelectedRows[0].Cells["Price"].Value.ToString());
            txtBillNo.Text = dtgviewGStoreReceive.SelectedRows[0].Cells["BillNo"].Value.ToString();
            cmbLocation.Text = dtgviewGStoreReceive.SelectedRows[0].Cells["Location"].Value.ToString();
            txtRemarks.Text = dtgviewGStoreReceive.SelectedRows[0].Cells["Remarks"].Value.ToString();
            NormalDetailsID = Convert.ToInt32(dtgviewGStoreReceive.SelectedRows[0].Cells["GenStoreRcvdNormalDetailsID"].Value.ToString());
            //decimal totalvalue = Convert.ToDecimal(numPerUnit.Value) * Convert.ToDecimal(numPrice.Value);TotalAmount
            txtTotalAmount.Text = dtgviewGStoreReceive.SelectedRows[0].Cells["TotalAmount"].Value.ToString();

            string sql = "SELECT ItemName, ItemListID FROM dbo.tbl_GenStoreItemList WHERE (ItemgroupID = " + groupid + ")";
            DataTable dt = DPL.getDataTable(sql);
            cmbItemName.DataSource = dt;
            cmbItemName.DisplayMember = "ItemName";
            cmbItemName.ValueMember = "ItemListID";
            cmbItemName.SelectedValue = ItemID;
            EDIT = true;
            cmbItemName.Focus();
            cmbItemName.Select();
        }

        private void dtgviewGStoreReceive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                EDITUPDATE();
                //int groupid =  Convert.ToInt32(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["ItemgroupID"].Value.ToString());
                //cmbItemGroup.SelectedValue = Convert.ToInt32(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["ItemgroupID"].Value.ToString());
                //int ItemID = Convert.ToInt32(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["ItemListID"].Value.ToString());
                //numRcvdQty.Value = Convert.ToDecimal(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["RcvdQty"].Value.ToString());
                //cmbMeasureUnit.SelectedValue = Convert.ToInt32(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["MeasureUnitID"].Value.ToString());
                //numPerUnit.Value = Convert.ToDecimal(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["PerUnitQty"].Value.ToString());
                //cmbPerMeasureUnit.SelectedValue = Convert.ToInt32(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["PerMeasureUnitID"].Value.ToString());
                //numPrice.Value = Convert.ToDecimal(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["Price"].Value.ToString());
                //txtBillNo.Text = dtgviewGStoreReceive.Rows[e.RowIndex].Cells["BillNo"].Value.ToString();
                //txtLocation.Text = dtgviewGStoreReceive.Rows[e.RowIndex].Cells["Location"].Value.ToString();
                //txtRemarks.Text = dtgviewGStoreReceive.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();
                //NormalDetailsID = Convert.ToInt32(dtgviewGStoreReceive.Rows[e.RowIndex].Cells["GenStoreRcvdNormalDetailsID"].Value.ToString());

                //string sql = "SELECT ItemName, ItemListID FROM dbo.tbl_GenStoreItemList WHERE (ItemgroupID = " + groupid + ")";
                //DataTable dt = DPL.getDataTable(sql);
                //cmbItemName.DataSource = dt;
                //cmbItemName.DisplayMember = "ItemName";
                //cmbItemName.ValueMember = "ItemListID";
                //cmbItemName.SelectedValue = ItemID;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EDITUPDATE();
        }
        private void ALLCLEAN()
        {
            cmbReceiveUnit.SelectedValue = 1;
            cmbSupplierUnit.SelectedValue = -1;
            cmbSupplierUnit.Text = "";
            dtpReceiveDate.Value = DateTime.Now;
            txtMRRNo.Text = "";
            txtRequisitionNo.Text = "";
            dtpRequisitionDate.Value = DateTime.Now;
            txtHeadNote.Text = "";
            NormalHeadMaxID = 0;
            cmbItemGroup.SelectedValue = -1;
            cmbItemName.SelectedValue = -1;
            cmbItemName.Text = "";
            numRcvdQty.Value = 0;
            cmbMeasureUnit.SelectedValue = 1;
            numPerUnit.Value = 0;
            cmbPerMeasureUnit.SelectedValue = 1;
            numPrice.Value = 0;
            txtBillNo.Text = "";
            cmbLocation.Text = "";
            txtRemarks.Text = "";
            txtTotalAmount.Text = "";
            NormalDetailsID = 0;
            EDIT = false;
            dtgviewGStoreReceive.DataSource = null;
            Mainfrm.ID = 0;
            Mainfrm.SupplierID = 0;
            cmbReceiveUnit.Focus();
            cmbReceiveUnit.Select();
            cmbReceiveUnit.BackColor = Color.Aquamarine;
            TotalQty = 0;
            string sqel2 = "select DISTINCT  Location from tbl_GenStoreRcvdNormalDetails";
            DataTable dtd2 = DPL.getDataTable(sqel2);
            cmbLocation.DataSource = dtd2;
            cmbLocation.DisplayMember = "Location";
            cmbLocation.ValueMember = "Location";
            cmbLocation.SelectedIndex = -1;
        }
        private void btnAddnew_Click(object sender, EventArgs e)
        {
            ALLCLEAN();
        }      

        private void dtpFron_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                dtpTo.Focus();
            }
        }

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbRcvdUnit.SelectedValue) > 0)
                {
                    int unit = Convert.ToInt32(cmbRcvdUnit.SelectedValue);
                    string FDate = dtpFron.Value.ToString("dd/MMM/yyyy");
                    string TDate = dtpTo.Value.ToString("dd/MMM/yyyy");
                    string sql = @"SELECT  dbo.tblUnitList.UnitName, dbo.tbl_GenStoreRcvdNormalHead.MRRNo, dbo.tbl_GenStoreRcvdNormalHead.RcvDateTime, 
                                   dbo.tbl_GenStoreRcvdNormalHead.UnitListID, dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID
                                   FROM dbo.tbl_GenStoreRcvdNormalHead INNER JOIN
                                   dbo.tblUnitList ON dbo.tbl_GenStoreRcvdNormalHead.UnitListID = dbo.tblUnitList.UnitListID
                                   WHERE (dbo.tbl_GenStoreRcvdNormalHead.UnitListID = " + unit + ") AND (dbo.tbl_GenStoreRcvdNormalHead.RcvDate BETWEEN '" + FDate + "' AND '" + TDate + "')ORDER BY dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID";
                    DataTable dt = DPL.getDataTable(sql);
                    PreEntrydtgveiw.DataSource = dt;
                    PreEntrydtgveiw.Columns["UnitListID"].Visible = false;
                    PreEntrydtgveiw.Columns["GenStoreRcvdNormalHeadID"].Visible = false;
                }
                else
                {                   
                    string FDate = dtpFron.Value.ToString("dd/MMM/yyyy");
                    string TDate = dtpTo.Value.ToString("dd/MMM/yyyy");
                    string sql = @"SELECT  dbo.tblUnitList.UnitName, dbo.tbl_GenStoreRcvdNormalHead.MRRNo, dbo.tbl_GenStoreRcvdNormalHead.RcvDateTime, 
                                   dbo.tbl_GenStoreRcvdNormalHead.UnitListID, dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID
                                   FROM dbo.tbl_GenStoreRcvdNormalHead INNER JOIN
                                   dbo.tblUnitList ON dbo.tbl_GenStoreRcvdNormalHead.UnitListID = dbo.tblUnitList.UnitListID
                                   WHERE (dbo.tbl_GenStoreRcvdNormalHead.RcvDate BETWEEN '" + FDate + "' AND '" + TDate + "')ORDER BY dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID";
                    DataTable dt = DPL.getDataTable(sql);
                    PreEntrydtgveiw.DataSource = dt;
                    PreEntrydtgveiw.Columns["UnitListID"].Visible = false;
                    PreEntrydtgveiw.Columns["GenStoreRcvdNormalHeadID"].Visible = false;
                }
            }

        }

        private void txtMRRNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                txtRequisitionNo.Focus();
                txtRequisitionNo.Select();
            }

        }

        private void cmbLocation_Enter(object sender, EventArgs e)
        {
            cmbLocation.BackColor = Color.Aquamarine;
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode.ToString() == "Return")
            {
                if (cmbLocation.Text.Trim().ToString() != "")
                {
                    txtRemarks.Focus();
                    txtRemarks.Select();
                }
                else
                {
                    MessageBox.Show("Please write location");
                    cmbLocation.Focus();
                    cmbLocation.Select();
                }
            }
            
        }

        private void cmbLocation_Leave(object sender, EventArgs e)
        {
            cmbLocation.BackColor = Color.White;
        }       
        int RowIndex = 0;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string DetailsDelete = "";
            if (dtgviewGStoreReceive.Rows.Count > 1)
            {
                DialogResult dr = MessageBox.Show("Do you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    NormalDetailsID = Convert.ToInt32(dtgviewGStoreReceive.Rows[RowIndex].Cells["GenStoreRcvdNormalDetailsID"].Value.ToString());
                    string sql = "delete from tbl_GenStoreRcvdNormalDetails Where GenStoreRcvdNormalDetailsID=" + NormalDetailsID + "";
                    DetailsDelete = DPL.executeSQL(sql);
                    if (DetailsDelete == "Succeeded")
                    {
                        MessageBox.Show("Delete Succeeded");
                        ShowAllData();
                        NormalDetailsID = 0;                       
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("Do you want to delete this M.R.R?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    string sql = "delete from tbl_GenStoreRcvdNormalHead Where GenStoreRcvdNormalHeadID=" + NormalHeadMaxID + "";
                    DetailsDelete = DPL.executeSQL(sql);
                    if (DetailsDelete == "Succeeded")
                    {
                        MessageBox.Show("Delete Succeeded");
                        ALLCLEAN();
                    }
                }
            }
        }
         
        private void dtgviewGStoreReceive_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = Convert.ToInt32(dtgviewGStoreReceive.Rows[e.RowIndex].Index);
        }
    }
}


 
            
        
