//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using Gate.Data;
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
    public partial class frmGStoreIssueItem : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        string UserName = UserLoginForm.UserName;
        public frmGStoreIssueItem()
        {
            InitializeComponent();
            CMBFILL();
            cmbRequnit.Focus();
            cmbRequnit.Select();
            date();
        }
        private void CMBFILL()
        {
            string sql = "Select UnitName, UnitListID  From tblUnitList";
            DataTable dt = DPL.getDataTable(sql);
            cmbRequnit.DataSource = dt;
            cmbRequnit.DisplayMember = "UnitName";
            cmbRequnit.ValueMember = "UnitListID";
            cmbRequnit.SelectedValue = 1;

            string sql1 = "Select IssueDepartmentName, IssueDepartmentID  From tbl_GenStoreIssueDepartment";
            DataTable dt1 = DPL.getDataTable(sql1);
            cmbReqDepartment.DataSource = dt1;
            cmbReqDepartment.DisplayMember = "IssueDepartmentName";
            cmbReqDepartment.ValueMember = "IssueDepartmentID";
            cmbReqDepartment.SelectedValue = -1;

            string sql2 = "Select ItemGroupName, ItemgroupID  From tbl_GenStoreItemgroup";
            DataTable dt2 = DPL.getDataTable(sql2);
            cmbItemGroup.DataSource = dt2;
            cmbItemGroup.DisplayMember = "ItemGroupName";
            cmbItemGroup.ValueMember = "ItemgroupID";
            cmbItemGroup.SelectedValue = -1;


            string sql3 = "Select MeasureUnitName, MeasureUnitID  From tbl_GenStoreMeasureUnit";
            DataTable dt3 = DPL.getDataTable(sql3);
            cmbMeasureReqUnit.DataSource = dt3;
            cmbMeasureReqUnit.DisplayMember = "MeasureUnitName";
            cmbMeasureReqUnit.ValueMember = "MeasureUnitID";
            cmbMeasureReqUnit.SelectedValue = 1;



            string sql4 = "Select MeasureUnitName, MeasureUnitID  From tbl_GenStoreMeasureUnit";
            DataTable dt4 = DPL.getDataTable(sql4);
            cmbMeasureUnit.DataSource = dt4;
            cmbMeasureUnit.DisplayMember = "MeasureUnitName";
            cmbMeasureUnit.ValueMember = "MeasureUnitID";
            cmbMeasureUnit.SelectedValue = 1;

            string sql5 = "Select MeasureUnitName, MeasureUnitID  From tbl_GenStoreMeasureUnit";
            DataTable dt5 = DPL.getDataTable(sql5);
            cmbMeasurePerUnit.DataSource = dt5;
            cmbMeasurePerUnit.DisplayMember = "MeasureUnitName";
            cmbMeasurePerUnit.ValueMember = "MeasureUnitID";
            cmbMeasurePerUnit.SelectedValue = 1;


            string sql6 = "Select UnitName, UnitListID  From tblUnitList";
            DataTable dt6 = DPL.getDataTable(sql6);
            cmbUnit.DataSource = dt6;
            cmbUnit.DisplayMember = "UnitName";
            cmbUnit.ValueMember = "UnitListID";
            cmbUnit.SelectedValue = 1;


            string sql7 = "Select ItemGroupName, ItemgroupID  From tbl_GenStoreItemgroup";
            DataTable dt7 = DPL.getDataTable(sql7);
            cmbItemGroupSearch.DataSource = dt7;
            cmbItemGroupSearch.DisplayMember = "ItemGroupName";
            cmbItemGroupSearch.ValueMember = "ItemgroupID";
            cmbItemGroupSearch.SelectedValue = -1;



        }
        #region Member Variable
        int NormalHeadMaxID = 0;
        int NormalDetailsID = 0;
        int ItemNameID = 0;
        bool EDIT = false;
        string dateCheck = "";
        decimal TotalIssueQty = 0;
        decimal BalanceQty = 0;
        string Measure = "";
        int RowIndex = 0;
        int PicRowIndex = 0;
        #endregion

        private void date()
        {
            string sql = "SELECT GETDATE() AS GetDate";
            DataTable dt = DPL.getDataTable(sql);
            string date = dt.Rows[0]["GetDate"].ToString();
            DateTime datet = Convert.ToDateTime(date);
            dateCheck = datet.ToString("dd/MMM/yyyy");
            dtpReqDate.Value = Convert.ToDateTime(date);
            dtpIssueDate.Value = Convert.ToDateTime(date);
            dtpF.Value = Convert.ToDateTime(date);
            dtpTo.Value = Convert.ToDateTime(date);
        }
        private void cmbRequnit_Enter(object sender, EventArgs e)
        {
            cmbRequnit.BackColor = Color.Aquamarine;
        }

        private void cmbRequnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbRequnit.SelectedValue) > 0)
                {
                    dtpIssueDate.Focus();
                    dtpIssueDate.Select();
                }
                else
                {
                    MessageBox.Show("Please select unit name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbRequnit.Focus();
                    cmbRequnit.Select();
                }
            }
        }
        int unid = 0;
        private void cmbRequnit_Leave(object sender, EventArgs e)
        {
            if (NormalHeadMaxID == 0)
            {
                unid = Convert.ToInt32(cmbRequnit.SelectedValue);
                cmbRequnit.BackColor = Color.White;
            }
            else
            {
                //string sql = "Select UnitName, UnitListID  From tblUnitList where UnitListID=" + unid + "";
                //DataTable dt = DPL.getDataTable(sql);
                //cmbRequnit.DataSource = dt;
                //cmbRequnit.DisplayMember = "UnitName";
                //cmbRequnit.ValueMember = "UnitListID";
                cmbRequnit.SelectedValue = unid;
                MessageBox.Show("Previous entry " + cmbRequnit.Text + " unit");
            }
        }

        private void cmbReqDepartment_Enter(object sender, EventArgs e)
        {
            cmbReqDepartment.BackColor = Color.Aquamarine;
        }

        private void cmbReqDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbReqDepartment.SelectedValue) > 0)
                {
                    dtpReqDate.Focus();
                    dtpReqDate.Select();
                }
                else
                {
                    MessageBox.Show("Please select department name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbReqDepartment.Focus();
                    cmbReqDepartment.Select();
                }
            }
        }

        private void cmbReqDepartment_Leave(object sender, EventArgs e)
        {
            cmbReqDepartment.BackColor = Color.White;
        }

        private void dtpReqDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                int unitID = Convert.ToInt32(cmbRequnit.SelectedValue);
                DataTable dt = DAL.GetMaxNumer("tbl_GenStoreIssueHead", "IssueSRNo", "UnitListID", unitID);
                string p = dt.Rows[0][0].ToString();
                if (p != "")
                {
                    int k = Convert.ToInt32(p);
                    int SRNo = k + 1;
                    txtIssueSRNo.Text = SRNo.ToString();
                }
                else
                {
                    txtIssueSRNo.Text = "1";
                }
                txtRequisitionNo.Focus();
                txtRequisitionNo.Select();
            }
        }


        private void txtRequisitionNo_Enter(object sender, EventArgs e)
        {
            txtRequisitionNo.BackColor = Color.Cyan;
            txtRequisitionNo.ForeColor = Color.Black;

        }

        private void txtRequisitionNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                try
                {
                    int ff = Convert.ToInt32(txtRequisitionNo.Text.Trim().ToString());
                    txtHeadNote.Focus();
                    txtHeadNote.Select();
                }
                catch
                {
                    MessageBox.Show("Please enter just mobile number");
                    txtRequisitionNo.Focus();
                    txtRequisitionNo.Select();
                }
            }
        }

        private void txtRequisitionNo_Leave(object sender, EventArgs e)
        {
            txtRequisitionNo.BackColor = Color.White;
            txtRequisitionNo.ForeColor = Color.Black;
        }

        private void dtpIssueDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (NormalHeadMaxID == 0)
                {
                    int unit = Convert.ToInt32(cmbUnit.SelectedValue);
                    DataTable dt = DAL.GetMaxNumer("dbo.tbl_GenStoreIssueHead", "IssueSRNo", "UnitListID", unit);
                    string p = dt.Rows[0][0].ToString();
                    if (p != "")
                    {
                        int k = Convert.ToInt32(p);
                        int MRRNo = k + 1;
                        txtIssueSRNo.Text = MRRNo.ToString();
                    }
                    else
                    {
                        txtIssueSRNo.Text = "1";
                    }
                }
                txtCustomerName.Focus();
                txtCustomerName.Select();
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
            //if (NormalHeadMaxID == 0)
            //{

            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbRequnit.SelectedValue) > 0)
                {
                    if (Convert.ToInt32(cmbItemGroup.SelectedValue) > 0)
                    {
                        int unitID = Convert.ToInt32(cmbRequnit.SelectedValue);
                        int ItemGroupID = Convert.ToInt32(cmbItemGroup.SelectedValue);
                        string ItemGroupName = cmbItemGroup.Text.Trim().ToString();

                        string sql = "SELECT ItemName, ItemListID FROM dbo.tbl_GenStoreItemList WHERE (ItemgroupID = " + ItemGroupID + ")";
                        DataTable dt = DPL.getDataTable(sql);
                        cmbItemName.DataSource = dt;
                        cmbItemName.DisplayMember = "ItemName";
                        cmbItemName.ValueMember = "ItemListID";
                        cmbItemName.SelectedValue = -1;

                        //                        string sqel = @"SELECT DISTINCT 
                        //                      f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.TotalIssueBalanceQty, dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, f.ItemListID
                        //                      FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                        //                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                        //                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                        //                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
                        //                                                   SUM(x.IssueQty) AS TotalIssue, CASE WHEN SUM(x.IssueQty) > SUM(x.TotalQty + x.OpeningQty) 
                        //                                                   THEN 0 ELSE SUM(x.TotalQty + x.OpeningQty) - SUM(x.IssueQty) END AS TotalIssueBalanceQty, dbo.tbl_GenStoreItemList.ItemListID
                        //                            FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(OpeningQty) AS OpeningQty, SUM(IssueQty) 
                        //                                                                           AS IssueQty
                        //                                                    FROM          (SELECT     ItemListID, TotalQty, 0 AS OpeningQty, 0 AS IssueQty
                        //                                                                            FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1
                        //                                                                            UNION ALL
                        //                                                                            SELECT     ItemListID, 0 AS TotalQty, OpeningQty, 0 AS IssueQty
                        //                                                                            FROM         dbo.tbl_GenStoreOpening
                        //                                                                            UNION ALL
                        //                                                                            SELECT     ItemListID, 0 AS TotalQty, 0 AS OpeningQty, IssueQty
                        //                                                                            FROM         dbo.tbl_GenStoreIssueDetails) AS y
                        //                                                    GROUP BY ItemListID) AS x INNER JOIN
                        //                                                   dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                        //                                                   dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID
                        //                            GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON 
                        //                      dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID
                        //                      WHERE     (f.ItemGroupName = '" + ItemGroupName + "')";


                        //                        string sqel = @"SELECT DISTINCT 
                        //                      f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.TotalIssueBalanceQty, dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, 
                        //                      f.ItemListID
                        //FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                        //                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                        //                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                        //                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
                        //                                                   SUM(x.IssueQty) AS TotalIssue, CASE WHEN SUM(x.IssueQty) > SUM(x.TotalQty + x.OpeningQty) 
                        //                                                   THEN 0 ELSE SUM(x.TotalQty + x.OpeningQty) - SUM(x.IssueQty) END AS TotalIssueBalanceQty, 
                        //                                                   dbo.tbl_GenStoreItemList.ItemListID
                        //                            FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(OpeningQty) AS OpeningQty, SUM(IssueQty) 
                        //                                                                           AS IssueQty
                        //                                                    FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS OpeningQty, 
                        //                                                                                                   0 AS IssueQty
                        //                                                                            FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                        //                                                                                                   dbo.tbl_GenStoreRcvdNormalHead ON 
                        //                                                                                                   tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID
                        //                                                                            WHERE      (dbo.tbl_GenStoreRcvdNormalHead.UnitListID = "+unitID+")"
                        //                                                                            +"UNION ALL"+""+
                        //                                                                            @"SELECT     ItemListID, 0 AS TotalQty, OpeningQty, 0 AS IssueQty
                        //                                                                            FROM         dbo.tbl_GenStoreOpening
                        //                                                                            WHERE     (UnitListID = "+unitID+")"
                        //                                                                            +"UNION ALL"+""+
                        //                                                                            @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, 0 AS OpeningQty, 
                        //                                                                                                  dbo.tbl_GenStoreIssueDetails.IssueQty
                        //                                                                            FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                        //                                                                                                  dbo.tbl_GenStoreIssueHead ON 
                        //                                                                                                  dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                        //                                                                            WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = "+unitID+")) AS y  GROUP BY ItemListID) AS x INNER JOIN dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON  dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID WHERE     (f.ItemGroupName = '" + ItemGroupName + "')";
                        string sqel = @"SELECT DISTINCT 
                      f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.TotalIssueBalanceQty, dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, 
                      f.ItemListID
FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
                                                   SUM(x.IssueQty) AS TotalIssue, CASE WHEN SUM(x.IssueQty) > SUM(x.TotalQty + x.OpeningQty) 
                                                   THEN 0 ELSE SUM(x.TotalQty + x.OpeningQty) - SUM(x.IssueQty) END AS TotalIssueBalanceQty, 
                                                   dbo.tbl_GenStoreItemList.ItemListID
                            FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(OpeningQty) AS OpeningQty, SUM(IssueQty) 
                                                                           AS IssueQty
                                                    FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS OpeningQty, 
                                                                                                   0 AS IssueQty
                                                                            FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                                   dbo.tbl_GenStoreRcvdNormalHead ON 
                                                                                                   tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID
                                                                            WHERE      (dbo.tbl_GenStoreRcvdNormalHead.UnitListID = " + unitID + ")"
                                                                               + " UNION ALL " + " " +
                                                                               "SELECT     ItemListID, 0 AS TotalQty, OpeningQty, 0 AS IssueQty  FROM  dbo.tbl_GenStoreOpening  WHERE  (UnitListID = " + unitID + ")"
                                                                                + " UNION ALL " + " " +
                                                                                "SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, 0 AS OpeningQty, dbo.tbl_GenStoreIssueDetails.IssueQty FROM  dbo.tbl_GenStoreIssueDetails INNER JOIN dbo.tbl_GenStoreIssueHead ON dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitID + ")) AS y  GROUP BY ItemListID) AS x INNER JOIN dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON  dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID WHERE     (f.ItemGroupName = '" + ItemGroupName + "')";


                        DataTable dtt = DPL.getDataTable(sqel);
                        dtgvPickup.DataSource = dtt;
                        dtgvPickup.Columns["ItemListID"].Visible = false;


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
                else
                {
                    MessageBox.Show("Please select unit name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbRequnit.Focus();
                    cmbRequnit.Select();
                }
            }
            //}
            //else
            //{
            //    cmbRequnit.SelectedValue = unid;
            //    MessageBox.Show("Previous entry " + cmbRequnit.Text + " unit");
            //}
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

                if (Convert.ToInt32(cmbItemName.SelectedValue) > 0)
                {
                    int unitID = Convert.ToInt32(cmbRequnit.SelectedValue);
                    string ItemGroupName = cmbItemGroup.Text.Trim().ToString();
                    int ItemID = Convert.ToInt32(cmbItemName.SelectedValue);

                    //                    string sqel = @"SELECT DISTINCT 
                    //                      f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.TotalIssueBalanceQty, dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, f.ItemListID
                    //                      FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                    //                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                    //                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                    //                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
                    //                                                   SUM(x.IssueQty) AS TotalIssue, CASE WHEN SUM(x.IssueQty) > SUM(x.TotalQty + x.OpeningQty) 
                    //                                                   THEN 0 ELSE SUM(x.TotalQty + x.OpeningQty) - SUM(x.IssueQty) END AS TotalIssueBalanceQty, dbo.tbl_GenStoreItemList.ItemListID
                    //                            FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(OpeningQty) AS OpeningQty, SUM(IssueQty) 
                    //                                                                           AS IssueQty
                    //                                                    FROM          (SELECT     ItemListID, TotalQty, 0 AS OpeningQty, 0 AS IssueQty
                    //                                                                            FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1
                    //                                                                            UNION ALL
                    //                                                                            SELECT     ItemListID, 0 AS TotalQty, OpeningQty, 0 AS IssueQty
                    //                                                                            FROM         dbo.tbl_GenStoreOpening
                    //                                                                            UNION ALL
                    //                                                                            SELECT     ItemListID, 0 AS TotalQty, 0 AS OpeningQty, IssueQty
                    //                                                                            FROM         dbo.tbl_GenStoreIssueDetails) AS y
                    //                                                    GROUP BY ItemListID) AS x INNER JOIN
                    //                                                   dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                    //                                                   dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID
                    //                            GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON 
                    //                      dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID
                    //                      WHERE     (f.ItemGroupName = '" + ItemGroupName + "') AND (f.ItemListID = " + ItemID + ")";



                    string sqel = @"SELECT DISTINCT 
                      f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.TotalIssueBalanceQty, dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, 
                      f.ItemListID
FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
                                                   SUM(x.IssueQty) AS TotalIssue, CASE WHEN SUM(x.IssueQty) > SUM(x.TotalQty + x.OpeningQty) 
                                                   THEN 0 ELSE SUM(x.TotalQty + x.OpeningQty) - SUM(x.IssueQty) END AS TotalIssueBalanceQty, 
                                                   dbo.tbl_GenStoreItemList.ItemListID
                            FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(OpeningQty) AS OpeningQty, SUM(IssueQty) 
                                                                           AS IssueQty
                                                    FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS OpeningQty, 
                                                                                                   0 AS IssueQty
                                                                            FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                                   dbo.tbl_GenStoreRcvdNormalHead ON 
                                                                                                   tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = dbo.tbl_GenStoreRcvdNormalHead.GenStoreRcvdNormalHeadID
                                                                            WHERE      (dbo.tbl_GenStoreRcvdNormalHead.UnitListID = " + unitID + ")"
                                                                               + " UNION ALL " + " " +
                                                                               "SELECT     ItemListID, 0 AS TotalQty, OpeningQty, 0 AS IssueQty  FROM  dbo.tbl_GenStoreOpening  WHERE  (UnitListID = " + unitID + ")"
                                                                                + " UNION ALL " + " " +
                                                                                "SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, 0 AS OpeningQty, dbo.tbl_GenStoreIssueDetails.IssueQty FROM  dbo.tbl_GenStoreIssueDetails INNER JOIN dbo.tbl_GenStoreIssueHead ON dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitID + ")) AS y  GROUP BY ItemListID) AS x INNER JOIN dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON  dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID WHERE     (f.ItemGroupName = '" + ItemGroupName + "')AND(f.ItemListID=" + ItemID + ")";


                    DataTable dtt = DPL.getDataTable(sqel);
                    dtgvPickup.DataSource = dtt;
                    dtgvPickup.Columns["ItemListID"].Visible = false;
                    BalanceQty = Convert.ToDecimal(dtt.Rows[0]["TotalIssueBalanceQty"].ToString());
                    Measure = dtt.Rows[0]["MeasureUnitName"].ToString();

                    numIssueQty.Focus();
                    numIssueQty.Select(0, 10);
                }
                else
                {
                    MessageBox.Show("Please select group name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbItemGroup.Focus();
                    cmbItemGroup.Select();
                }
            }
        }

        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            cmbItemName.BackColor = Color.White;
        }

        private void btnSave_Enter(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.Aquamarine;
        }
        private void CLEAN()
        {
            cmbItemName.SelectedValue = -1;
            numReqQty.Value = 0;
            cmbMeasureReqUnit.SelectedValue = 1;
            numIssueQty.Value = 0;
            cmbMeasureUnit.SelectedValue = 1;
            numIssuePerQty.Value = 0;
            cmbMeasurePerUnit.SelectedValue = 1;
            txtRemarks.Text = "";
            NormalDetailsID = 0;
            EDIT = false;
            cmbItemGroup.Focus();
            cmbItemGroup.Select();

        }
        private void ShowAllData()
        {
            string sql = @"SELECT     TOP (100) PERCENT dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreIssueDetails.ReqQty, 
                      tbl_GenStoreMeasureUnit_1.MeasureUnitName AS MeasureReqUnit, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName AS MeasureIssueUnit, dbo.tbl_GenStoreIssueDetails.IssuePerQty, 
                      tbl_GenStoreMeasureUnit_2.MeasureUnitName AS MeasurePerUnit, dbo.tbl_GenStoreIssueDetails.Remarks, dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID, 
                      dbo.tbl_GenStoreIssueDetails.GenStoreIssueDetailsID, dbo.tbl_GenStoreIssueDetails.SellingPrice, dbo.tbl_GenStoreIssueDetails.ItemListID, 
                      dbo.tbl_GenStoreItemList.ItemgroupID, tbl_GenStoreMeasureUnit_1.MeasureUnitID AS MeasureReqUnitID, dbo.tbl_GenStoreMeasureUnit.MeasureUnitID, 
                      tbl_GenStoreMeasureUnit_2.MeasureUnitID AS MeasurePerUnitID
FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                      dbo.tbl_GenStoreItemList ON dbo.tbl_GenStoreIssueDetails.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                      dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit ON dbo.tbl_GenStoreIssueDetails.MeasureUnitID = dbo.tbl_GenStoreMeasureUnit.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit AS tbl_GenStoreMeasureUnit_1 ON 
                      dbo.tbl_GenStoreIssueDetails.MeasureReqUnitID = tbl_GenStoreMeasureUnit_1.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit AS tbl_GenStoreMeasureUnit_2 ON dbo.tbl_GenStoreIssueDetails.MeasurePerUnitID = tbl_GenStoreMeasureUnit_2.MeasureUnitID
                      WHERE (dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = " + NormalHeadMaxID + ")" +
                     "ORDER BY dbo.tbl_GenStoreIssueDetails.GenStoreIssueDetailsID";
            DataTable dt = DPL.getDataTable(sql);
            dtgv1st.DataSource = dt;
            dtgv1st.Columns["GenStoreIssueHeadID"].Visible = false;
            dtgv1st.Columns["GenStoreIssueDetailsID"].Visible = false;
            dtgv1st.Columns["ItemListID"].Visible = false;
            dtgv1st.Columns["ItemgroupID"].Visible = false;
            dtgv1st.Columns["MeasureReqUnitID"].Visible = false;
            dtgv1st.Columns["MeasureUnitID"].Visible = false;
            dtgv1st.Columns["MeasurePerUnitID"].Visible = false;
            dtgv1st.Columns["MeasurePerUnitID"].Visible = false;
            dtgv1st.Columns["IssuePerQty"].Visible = false;
            dtgv1st.Columns["MeasurePerUnit"].Visible = false;
        }
        private void UPDATEHEAD()
        {
            string UPDATEHEAD = "";

            if (NormalHeadMaxID != 0 && cmbRequnit.Text.Trim().ToString() != "" && cmbReqDepartment.Text.Trim().ToString() != "" && txtIssueSRNo.Text.Trim().ToString() != "")
            {
                int RequnitID = Convert.ToInt32(cmbRequnit.SelectedValue);
                //int ReqDepartmentID = Convert.ToInt32(cmbReqDepartment.SelectedValue);
                int ReqDepartmentID = 1;
                string ReqDate = dtpReqDate.Value.ToString("dd/MMM/yyyy");
                int IssueSRNo = Convert.ToInt32(txtIssueSRNo.Text);
                string RequisitionNo = txtRequisitionNo.Text.Trim().ToString().Replace("'", "''");
                string IssueDateTime = dtpIssueDate.Value.ToString("dd/MMM/yyyy hh:mm:ss tt");
                string IssueDate = dtpIssueDate.Value.ToString("dd/MMM/yyyy");
                string HeadNote = txtHeadNote.Text.Trim().ToString().Replace("'", "''");
                UPDATEHEAD = DAL.UPDATE_tbl_GenStoreIssueHead(RequnitID, ReqDepartmentID, ReqDate, IssueSRNo, RequisitionNo, IssueDateTime, HeadNote, IssueDate, NormalHeadMaxID, UserName);
                if (UPDATEHEAD == "Succeeded")
                {
                    MessageBox.Show("Head Update Succeeded");
                }
            }
        }

        private void INSERT()
        {
            string INSERTHEAD = "";
            string INSERTDETAILS = "";


            //if (cmbRequnit.Text.Trim().ToString() != "" && cmbReqDepartment.Text.Trim().ToString() != "" && txtIssueSRNo.Text.Trim().ToString() != "")
            //{
                if (NormalHeadMaxID == 0)
                {
                    string datetd = dtpIssueDate.Value.ToString("dd/MMM/yyyy");
                    if (datetd == dateCheck)
                    {
                        date();
                    }
                    int RequnitID = Convert.ToInt32(cmbRequnit.SelectedValue);
                    //int ReqDepartmentID = Convert.ToInt32(cmbReqDepartment.SelectedValue);
                    int ReqDepartmentID = 1;
                    string ReqDate = dtpReqDate.Value.ToString("dd/MMM/yyyy");
                    int IssueSRNo = Convert.ToInt32(txtIssueSRNo.Text);
                    string RequisitionNo = txtRequisitionNo.Text.Trim().ToString().Replace("'", "''");
                    string IssueDateTime = dtpIssueDate.Value.ToString("dd/MMM/yyyy hh:mm:ss tt");
                    string IssueDate = dtpIssueDate.Value.ToString("dd/MMM/yyyy");
                    string HeadNote = txtHeadNote.Text.Trim().ToString().Replace("'", "''");
                    int Mobile = Convert.ToInt32(txtRequisitionNo.Text.Trim().ToString());
                    INSERTHEAD = DAL.INSERT_tbl_GenStoreIssueHead(RequnitID, ReqDepartmentID, ReqDate, IssueSRNo, RequisitionNo, IssueDateTime, HeadNote, IssueDate, Mobile, UserName);
                    if (INSERTHEAD == "Succeeded")
                    {
                        DataTable dt = DAL.GetMaxID("tbl_GenStoreIssueHead", "GenStoreIssueHeadID");
                        string maxid = dt.Rows[0][0].ToString();
                        if (maxid != "")
                        {
                            NormalHeadMaxID = Convert.ToInt32(maxid);
                        }
                    }
                }
                if (NormalHeadMaxID != 0)
                {

                    //if (cmbItemName.Text.Trim().ToString() != "" && numReqQty.Value != 0 && cmbMeasureReqUnit.Text.Trim().ToString() != "" )
                    //{

                        ItemNameID = Convert.ToInt32(cmbItemName.SelectedValue);
                        decimal ReqQty = Convert.ToDecimal(numReqQty.Value);
                        int MeasureReqUnitID = Convert.ToInt32(cmbMeasureReqUnit.SelectedValue);
                        //int MeasureReqUnitID = 0;
                        decimal IssueQty = Convert.ToDecimal(numIssueQty.Value);
                        int MeasureUnitID = Convert.ToInt32(cmbMeasureUnit.SelectedValue);
                        decimal IssuePerQty = Convert.ToDecimal(numIssuePerQty.Value);
                        int PerMeasureUnitID = Convert.ToInt32(cmbMeasureUnit.SelectedValue);
                        //int PerMeasureUnitID = 1;
                        string Remarks = txtRemarks.Text.Trim().ToString().Replace("'", "''");
                        decimal SellingPrice = Convert.ToDecimal(numSellingPrice.Value);
                        INSERTDETAILS = DAL.INSERT_tbl_GenStoreIssueDetails(ItemNameID, ReqQty, MeasureReqUnitID, IssueQty, MeasureUnitID, IssuePerQty, PerMeasureUnitID, Remarks, NormalHeadMaxID, SellingPrice, UserName);
                        if (INSERTDETAILS == "Succeeded")
                        {
                            MessageBox.Show("Save Succeeded");
                            ShowAllData();
                            CLEAN();
                            cmbItemGroup.Focus();
                            cmbItemGroup.Select();

                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Some essential value must be added", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            //}
            //else
            //{
            //    MessageBox.Show("Some essential value must be added", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //MessageBox.Show("Some essential value must be added");
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal comapareQty = 0;
            comapareQty = numIssueQty.Value;
            if (comapareQty <= BalanceQty)
            {
                if (EDIT)
                {
                    UPDATE();
                }
                else
                {
                    if (NormalDetailsID != 0 && cmbItemName.Text.Trim().ToString() != "" && numReqQty.Value != 0 && cmbMeasureReqUnit.Text.Trim().ToString() != "" && numIssueQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbMeasurePerUnit.Text.Trim().ToString() != "")
                    {
                        UPDATEHEAD();
                    }
                    else
                    {
                        INSERT();
                    }
                }
            }
            else
            {
                if (comapareQty >= TotalIssueQty)
                {
                    decimal totalAdd = comapareQty - TotalIssueQty;
                    if (totalAdd <= BalanceQty)
                    {
                        if (EDIT)
                        {
                            UPDATE();
                        }
                        else
                        {
                            if (NormalDetailsID != 0 && cmbItemName.Text.Trim().ToString() != "" && numReqQty.Value != 0 && cmbMeasureReqUnit.Text.Trim().ToString() != "" && numIssueQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbMeasurePerUnit.Text.Trim().ToString() != "")
                            {
                                UPDATEHEAD();
                            }
                            else
                            {
                                INSERT();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You maximum add " + BalanceQty + " Kg.");
                        numIssueQty.Focus();
                        numIssueQty.Select(0, 10);
                    }
                }
                else
                {
                    if (EDIT)
                    {
                        UPDATE();
                    }
                    else
                    {
                        if (NormalDetailsID != 0 && cmbItemName.Text.Trim().ToString() != "" && numReqQty.Value != 0 && cmbMeasureReqUnit.Text.Trim().ToString() != "" && numIssueQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbMeasurePerUnit.Text.Trim().ToString() != "")
                        {
                            UPDATEHEAD();
                        }
                        else
                        {
                            INSERT();
                        }
                    }
                }
            }
        }

        private void UPDATE()
        {
            string UPDATEHEAD = "";
            string UPDATEDETAILS = "";


            if (cmbRequnit.Text.Trim().ToString() != "" && txtIssueSRNo.Text.Trim().ToString() != "")
            {
                if (NormalHeadMaxID != 0)
                {
                    int RequnitID = Convert.ToInt32(cmbRequnit.SelectedValue);
                    int ReqDepartmentID = Convert.ToInt32(cmbReqDepartment.SelectedValue);
                    string ReqDate = dtpReqDate.Value.ToString("dd/MMM/yyyy");
                    int IssueSRNo = Convert.ToInt32(txtIssueSRNo.Text);
                    string RequisitionNo = txtRequisitionNo.Text.Trim().ToString().Replace("'", "''");
                    string IssueDateTime = dtpIssueDate.Value.ToString("dd/MMM/yyyy hh:mm:ss tt");
                    string IssueDate = dtpIssueDate.Value.ToString("dd/MMM/yyyy");
                    string HeadNote = txtHeadNote.Text.Trim().ToString().Replace("'", "''");
                    
                    UPDATEHEAD = DAL.UPDATE_tbl_GenStoreIssueHead(RequnitID, ReqDepartmentID, ReqDate, IssueSRNo, RequisitionNo, IssueDateTime, HeadNote, IssueDate, NormalHeadMaxID,UserName);

                }
                if (NormalDetailsID != 0)
                {

                    if (NormalDetailsID != 0 && cmbItemName.Text.Trim().ToString() != "" && numReqQty.Value != 0 && cmbMeasureReqUnit.Text.Trim().ToString() != "" && numIssueQty.Value != 0 && cmbMeasureUnit.Text.Trim().ToString() != "" && cmbMeasurePerUnit.Text.Trim().ToString() != "")
                    {

                        ItemNameID = Convert.ToInt32(cmbItemName.SelectedValue);
                        decimal ReqQty = Convert.ToDecimal(numReqQty.Value);
                        int MeasureReqUnitID = Convert.ToInt32(cmbMeasureReqUnit.SelectedValue);
                        decimal IssueQty = Convert.ToDecimal(numIssueQty.Value);
                        int MeasureUnitID = Convert.ToInt32(cmbMeasureUnit.SelectedValue);
                        decimal IssuePerQty = Convert.ToDecimal(numIssuePerQty.Value);
                        int PerMeasureUnitID = Convert.ToInt32(cmbMeasurePerUnit.SelectedValue);
                        string Remarks = txtRemarks.Text.Trim().ToString().Replace("'", "''");
                        decimal SellingPrice = Convert.ToDecimal(numSellingPrice.Value);
                        UPDATEDETAILS = DAL.UPDATE_tbl_GenStoreIssueDetails(ItemNameID, ReqQty, MeasureReqUnitID, IssueQty, MeasureUnitID, IssuePerQty, PerMeasureUnitID, Remarks, NormalDetailsID, SellingPrice,UserName);
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
                    }
                }
            }
            else
            {
                MessageBox.Show("Some essential value must be added", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Leave(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.White;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Mainfrm.bIssue = true;
            this.Close();
        }

        private void numReqQty_Enter(object sender, EventArgs e)
        {
            numReqQty.BackColor = Color.Aquamarine;
        }

        private void numReqQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(numReqQty.Value) > 0)
                {
                    cmbMeasureReqUnit.Focus();
                    cmbMeasureReqUnit.Select();
                }
                else
                {
                    MessageBox.Show("Please write required qty", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numReqQty.Focus();
                    numReqQty.Select(0, 10);
                }
            }
        }

        private void numReqQty_Leave(object sender, EventArgs e)
        {
            numReqQty.BackColor = Color.White;
        }

        private void cmbMeasureReqUnit_Enter(object sender, EventArgs e)
        {
            cmbMeasureReqUnit.BackColor = Color.Aquamarine;
        }

        private void cmbMeasureReqUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbMeasureReqUnit.SelectedValue) > 0)
                {
                    numIssueQty.Focus();
                    numIssueQty.Select(0, 10);
                }
                else
                {
                    MessageBox.Show("Please select measure req unit", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMeasureReqUnit.Focus();
                    cmbMeasureReqUnit.Select();
                }
            }
        }

        private void cmbMeasureReqUnit_Leave(object sender, EventArgs e)
        {
            cmbMeasureReqUnit.BackColor = Color.White;
        }

        private void numIssueQty_Enter(object sender, EventArgs e)
        {
            numIssueQty.BackColor = Color.Aquamarine;
        }

        private void numIssueQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (numIssueQty.Value != 0)
                {
                    decimal comapareQty = 0;
                    comapareQty = numIssueQty.Value;
                    if (comapareQty <= BalanceQty)
                    {
                        cmbMeasureUnit.Focus();
                    }
                    else
                    {
                        if (comapareQty >= TotalIssueQty)
                        {
                            decimal totalAdd = comapareQty - TotalIssueQty;
                            if (totalAdd <= BalanceQty)
                            {
                                cmbMeasureUnit.Focus();
                                cmbMeasureUnit.Select(0, 10);

                            }
                            else
                            {
                                MessageBox.Show("You maximum add " + BalanceQty + " " + Measure + "");
                                numIssueQty.Focus();
                                numIssueQty.Select(0, 10);
                            }
                        }
                        else
                        {
                            cmbMeasureUnit.Focus();
                            cmbMeasureUnit.Select();
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Please put some value for issue qty ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numIssueQty.Focus();
                    numIssueQty.Select(0, 10);
                }
            }
            //if (e.KeyCode.ToString() == "Return")
            //{
            //    if (Convert.ToInt32(numIssueQty.Value) > 0)
            //    {
            //        cmbMeasureUnit.Focus();
            //        cmbMeasureUnit.Select();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please write issue qty", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        numIssueQty.Focus();
            //        numIssueQty.Select(0, 10);
            //    }
            //}
        }

        private void numIssueQty_Leave(object sender, EventArgs e)
        {
            numIssueQty.BackColor = Color.White;
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
                    numSellingPrice.Focus();
                    numSellingPrice.Select(0, 10);
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

        private void numIssuePerQty_Enter(object sender, EventArgs e)
        {
            numIssuePerQty.BackColor = Color.Aquamarine;
        }

        private void numIssuePerQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(numIssuePerQty.Value) > 0)
                {
                    cmbMeasurePerUnit.Focus();
                    cmbMeasurePerUnit.Select();
                }
                else
                {
                    MessageBox.Show("Please write issueper qty", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numIssuePerQty.Focus();
                    numIssuePerQty.Select(0, 10);
                }
            }
        }

        private void numIssuePerQty_Leave(object sender, EventArgs e)
        {
            numIssuePerQty.BackColor = Color.White;
        }

        private void cmbMeasurePerUnit_Enter(object sender, EventArgs e)
        {
            cmbMeasurePerUnit.BackColor = Color.Aquamarine;
        }

        private void cmbMeasurePerUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbMeasurePerUnit.SelectedValue) > 0)
                {
                    txtRemarks.Focus();
                    txtRemarks.Select();
                }
                else
                {
                    MessageBox.Show("Please select measureper unit", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMeasurePerUnit.Focus();
                    cmbMeasurePerUnit.Select();
                }
            }
        }

        private void cmbMeasurePerUnit_Leave(object sender, EventArgs e)
        {
            cmbMeasurePerUnit.BackColor = Color.White;
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
                btnSave.Focus();
            }
        }

        private void txtRemarks_Leave(object sender, EventArgs e)
        {
            txtRemarks.BackColor = Color.White;
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbUnit.SelectedValue) > 0)
                {
                    int unitId = Convert.ToInt32(cmbUnit.SelectedValue);
                    string sql = @"SELECT dbo.tblUnitList.UnitName, dbo.tbl_GenStoreIssueHead.IssueSRNo, dbo.tbl_GenStoreIssueHead.IssueDateTime, dbo.tbl_GenStoreIssueHead.UnitListID, 
                      dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                      FROM  dbo.tbl_GenStoreIssueHead INNER JOIN
                      dbo.tblUnitList ON dbo.tbl_GenStoreIssueHead.UnitListID = dbo.tblUnitList.UnitListID
                      WHERE (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitId + ")";
                    DataTable dt = DPL.getDataTable(sql);
                    dtgvPrevious1st.DataSource = dt;
                    dtgvPrevious1st.Columns["GenStoreIssueHeadID"].Visible = false;
                    dtgvPrevious1st.Columns["UnitListID"].Visible = false;

                    cmbIssueSRNo.DataSource = dt;
                    cmbIssueSRNo.DisplayMember = "IssueSRNo";
                    cmbIssueSRNo.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Please select unit name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbUnit.Focus();
                    cmbUnit.Select();
                }
            }
        }

        private void cmbIssueSRNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (cmbIssueSRNo.Text.Trim().ToString() != "")
                {
                    int unitId = Convert.ToInt32(cmbUnit.SelectedValue);
                    int SRNo = Convert.ToInt32(cmbIssueSRNo.Text.Trim().ToString());
                    string sql = @"SELECT     dbo.tblUnitList.UnitName, dbo.tbl_GenStoreIssueHead.IssueSRNo, dbo.tbl_GenStoreIssueHead.IssueDateTime, dbo.tbl_GenStoreIssueHead.UnitListID, 
                      dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                      FROM dbo.tbl_GenStoreIssueHead INNER JOIN
                      dbo.tblUnitList ON dbo.tbl_GenStoreIssueHead.UnitListID = dbo.tblUnitList.UnitListID
                      WHERE (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitId + ") AND (dbo.tbl_GenStoreIssueHead.IssueSRNo = " + SRNo + ")" +
                      "ORDER BY dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID";
                    DataTable dt = DPL.getDataTable(sql);
                    dtgvPrevious1st.DataSource = dt;
                    dtgvPrevious1st.Columns["GenStoreIssueHeadID"].Visible = false;
                    dtgvPrevious1st.Columns["UnitListID"].Visible = false;
                }
                else
                {
                    MessageBox.Show("Please select SR No", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbIssueSRNo.Focus();
                    cmbIssueSRNo.Select();
                }
            }
        }

        private void dtgvPrevious1st_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int HeadID = Convert.ToInt32(dtgvPrevious1st.SelectedRows[0].Cells["GenStoreIssueHeadID"].Value);

                string sql = @"SELECT TOP 100 PERCENT dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreIssueDetails.ReqQty, 
                      tbl_GenStoreMeasureUnit_1.MeasureUnitName AS MeasureReqUnit, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName AS MeasureIssueUnit, dbo.tbl_GenStoreIssueDetails.IssuePerQty, 
                      tbl_GenStoreMeasureUnit_2.MeasureUnitName AS MeasurePerUnit, dbo.tbl_GenStoreIssueDetails.Remarks, 
                      dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID, dbo.tbl_GenStoreIssueDetails.GenStoreIssueDetailsID, dbo.tbl_GenStoreIssueDetails.ItemListID, 
                      dbo.tbl_GenStoreItemList.ItemgroupID, tbl_GenStoreMeasureUnit_1.MeasureUnitID AS MeasureReqUnitID, 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID, tbl_GenStoreMeasureUnit_2.MeasureUnitID AS MeasurePerUnitID
                      FROM dbo.tbl_GenStoreIssueDetails INNER JOIN
                      dbo.tbl_GenStoreItemList ON dbo.tbl_GenStoreIssueDetails.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                      dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit ON dbo.tbl_GenStoreIssueDetails.MeasureUnitID = dbo.tbl_GenStoreMeasureUnit.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit tbl_GenStoreMeasureUnit_1 ON 
                      dbo.tbl_GenStoreIssueDetails.MeasureReqUnitID = tbl_GenStoreMeasureUnit_1.MeasureUnitID INNER JOIN
                      dbo.tbl_GenStoreMeasureUnit tbl_GenStoreMeasureUnit_2 ON 
                      dbo.tbl_GenStoreIssueDetails.MeasurePerUnitID = tbl_GenStoreMeasureUnit_2.MeasureUnitID
                      WHERE (dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = " + HeadID + ")" +
                     "ORDER BY dbo.tbl_GenStoreIssueDetails.GenStoreIssueDetailsID";
                DataTable dt = DPL.getDataTable(sql);
                dtgvPrevious2nd.DataSource = dt;
                dtgvPrevious2nd.Columns["GenStoreIssueHeadID"].Visible = false;
                dtgvPrevious2nd.Columns["GenStoreIssueDetailsID"].Visible = false;
                dtgvPrevious2nd.Columns["ItemListID"].Visible = false;
                dtgvPrevious2nd.Columns["ItemgroupID"].Visible = false;
                dtgvPrevious2nd.Columns["MeasureReqUnitID"].Visible = false;
                dtgvPrevious2nd.Columns["MeasureUnitID"].Visible = false;
                dtgvPrevious2nd.Columns["MeasurePerUnitID"].Visible = false;
                dtgvPrevious2nd.Columns["IssuePerQty"].Visible = false;
                dtgvPrevious2nd.Columns["MeasurePerUnit"].Visible = false;
            }

        }

        private void dtgvPrevious1st_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                NormalHeadMaxID = Convert.ToInt32(dtgvPrevious1st.SelectedRows[0].Cells["GenStoreIssueHeadID"].Value);


                string sql = @"SELECT TOP 100 PERCENT UnitListID, IssueDepartmentID, ReqDate, IssueSRNo, RequisitionNo, IssueDateTime, HeadNote, GenStoreIssueHeadID
                             FROM dbo.tbl_GenStoreIssueHead
                             WHERE (GenStoreIssueHeadID = " + NormalHeadMaxID + ")";
                DataTable dtd = DPL.getDataTable(sql);
                cmbRequnit.SelectedValue = Convert.ToInt32(dtd.Rows[0]["UnitListID"].ToString());
                cmbReqDepartment.SelectedValue = Convert.ToInt32(dtd.Rows[0]["IssueDepartmentID"].ToString());
                string Rdate = dtd.Rows[0]["ReqDate"].ToString();
                dtpReqDate.Value = Convert.ToDateTime(Rdate);
                txtIssueSRNo.Text = dtd.Rows[0]["IssueSRNo"].ToString();
                txtRequisitionNo.Text = dtd.Rows[0]["RequisitionNo"].ToString();
                string Idate = dtd.Rows[0]["IssueDateTime"].ToString();
                dtpIssueDate.Value = Convert.ToDateTime(Idate);
                txtHeadNote.Text = dtd.Rows[0]["HeadNote"].ToString();
                unid = Convert.ToInt32(dtd.Rows[0]["UnitListID"].ToString());
                ShowAllData();
                tabControl1.SelectedIndex = 0;
            }
        }
        private void EDITUPDATE()
        {
            int groupid = Convert.ToInt32(dtgv1st.SelectedRows[0].Cells["ItemgroupID"].Value.ToString());
            cmbItemGroup.SelectedValue = Convert.ToInt32(dtgv1st.SelectedRows[0].Cells["ItemgroupID"].Value.ToString());
            int ItemID = Convert.ToInt32(dtgv1st.SelectedRows[0].Cells["ItemListID"].Value.ToString());
            numReqQty.Value = Convert.ToDecimal(dtgv1st.SelectedRows[0].Cells["ReqQty"].Value.ToString());
            cmbMeasureReqUnit.SelectedValue = Convert.ToInt32(dtgv1st.SelectedRows[0].Cells["MeasureReqUnitID"].Value.ToString());
            numIssueQty.Value = Convert.ToDecimal(dtgv1st.SelectedRows[0].Cells["IssueQty"].Value.ToString());
            cmbMeasureUnit.SelectedValue = Convert.ToInt32(dtgv1st.SelectedRows[0].Cells["MeasureUnitID"].Value.ToString());
            numIssuePerQty.Value = Convert.ToDecimal(dtgv1st.SelectedRows[0].Cells["IssuePerQty"].Value.ToString());
            cmbMeasurePerUnit.SelectedValue = Convert.ToInt32(dtgv1st.SelectedRows[0].Cells["MeasurePerUnitID"].Value.ToString());
            txtRemarks.Text = dtgv1st.SelectedRows[0].Cells["Remarks"].Value.ToString();
            NormalDetailsID = Convert.ToInt32(dtgv1st.SelectedRows[0].Cells["GenStoreIssueDetailsID"].Value.ToString());
            TotalIssueQty = Convert.ToDecimal(dtgv1st.SelectedRows[0].Cells["IssueQty"].Value.ToString());
            numSellingPrice.Value = Convert.ToDecimal(dtgv1st.SelectedRows[0].Cells["SellingPrice"].Value.ToString());


            string sql = "SELECT ItemName, ItemListID FROM dbo.tbl_GenStoreItemList WHERE (ItemgroupID = " + groupid + ")";
            DataTable dt = DPL.getDataTable(sql);
            cmbItemName.DataSource = dt;
            cmbItemName.DisplayMember = "ItemName";
            cmbItemName.ValueMember = "ItemListID";
            cmbItemName.SelectedValue = ItemID;
            EDIT = true;
            numReqQty.Focus();
            numReqQty.Select(0, 10);

            string sqel = @"SELECT DISTINCT 
                      f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.TotalIssueBalanceQty, dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, f.ItemListID
                      FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
                                                   SUM(x.IssueQty) AS TotalIssue, CASE WHEN SUM(x.IssueQty) > SUM(x.TotalQty + x.OpeningQty) 
                                                   THEN 0 ELSE SUM(x.TotalQty + x.OpeningQty) - SUM(x.IssueQty) END AS TotalIssueBalanceQty, dbo.tbl_GenStoreItemList.ItemListID
                            FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(OpeningQty) AS OpeningQty, SUM(IssueQty) 
                                                                           AS IssueQty
                                                    FROM          (SELECT     ItemListID, TotalQty, 0 AS OpeningQty, 0 AS IssueQty
                                                                            FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1
                                                                            UNION ALL
                                                                            SELECT     ItemListID, 0 AS TotalQty, OpeningQty, 0 AS IssueQty
                                                                            FROM         dbo.tbl_GenStoreOpening
                                                                            UNION ALL
                                                                            SELECT     ItemListID, 0 AS TotalQty, 0 AS OpeningQty, IssueQty
                                                                            FROM         dbo.tbl_GenStoreIssueDetails) AS y
                                                    GROUP BY ItemListID) AS x INNER JOIN
                                                   dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                                                   dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID
                            GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON 
                      dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID
                      WHERE  (f.ItemListID = " + ItemID + ")";



            DataTable dtt = DPL.getDataTable(sqel);
            dtgvPickup.DataSource = dtt;
            dtgvPickup.Columns["ItemListID"].Visible = false;
            BalanceQty = Convert.ToDecimal(dtt.Rows[0]["TotalIssueBalanceQty"].ToString());
            Measure = dtt.Rows[0]["MeasureUnitName"].ToString();




        }
        private void dtgv1st_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                EDITUPDATE();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EDITUPDATE();
        }
        private void ALLCLEAN()
        {
            cmbRequnit.SelectedValue = 1;
            cmbReqDepartment.SelectedValue = -1;
            dtpReqDate.Value = DateTime.Now;
            txtIssueSRNo.Text = "";
            txtRequisitionNo.Text = "";
            dtpIssueDate.Value = DateTime.Now;
            txtHeadNote.Text = "";
            NormalHeadMaxID = 0;
            cmbItemName.SelectedValue = -1;
            numReqQty.Value = 0;
            cmbMeasureReqUnit.SelectedValue = 1;
            numIssueQty.Value = 0;
            cmbMeasureUnit.SelectedValue = 1;
            numIssuePerQty.Value = 0;
            cmbMeasurePerUnit.SelectedValue = 1;
            txtRemarks.Text = "";
            NormalDetailsID = 0;
            EDIT = false;
            dtgv1st.DataSource = null;
            cmbRequnit.Focus();
            cmbRequnit.Select();
            unid = 0;
        }
        private void btnAddnew_Click(object sender, EventArgs e)
        {
            ALLCLEAN();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string DetailsDelete = "";
            if (dtgv1st.Rows.Count > 1)
            {
                DialogResult dr = MessageBox.Show("Do you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    NormalDetailsID = Convert.ToInt32(dtgv1st.Rows[RowIndex].Cells["GenStoreIssueDetailsID"].Value.ToString());
                    string sql = "delete from tbl_GenStoreIssueDetails Where GenStoreIssueDetailsID=" + NormalDetailsID + "";
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
                DialogResult dr = MessageBox.Show("Do you want to delete this requisition?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    string sql = "delete from tbl_GenStoreIssueHead Where GenStoreIssueHeadID=" + NormalHeadMaxID + "";
                    DetailsDelete = DPL.executeSQL(sql);
                    if (DetailsDelete == "Succeeded")
                    {
                        MessageBox.Show("Delete Succeeded");
                        ALLCLEAN();
                    }
                }
            }
        }

        private void dtgv1st_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = Convert.ToInt32(dtgv1st.Rows[e.RowIndex].Index);
        }

        private void dtgvPickup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ItemNameID = Convert.ToInt32(dtgvPickup.Rows[PicRowIndex].Cells["ItemListID"].Value.ToString());
                //BalanceQty = Convert.ToDecimal(dtgvPickup.Rows[PicRowIndex].Cells["TotalIssueBalanceQty"].ToString());
                BalanceQty = Convert.ToDecimal(dtgvPickup.Rows[PicRowIndex].Cells["TotalIssueBalanceQty"].Value.ToString());
                Measure = dtgvPickup.Rows[PicRowIndex].Cells["MeasureUnitName"].Value.ToString();
                cmbItemName.SelectedValue = ItemNameID;
                numReqQty.Focus();
                numReqQty.Select(0, 10);
            }
        }

        private void dtgvPickup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PicRowIndex = Convert.ToInt32(dtgvPickup.Rows[e.RowIndex].Index);
        }

        private void dtgvPickup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                ItemNameID = Convert.ToInt32(dtgvPickup.Rows[PicRowIndex].Cells["ItemListID"].Value.ToString());
                BalanceQty = Convert.ToDecimal(dtgvPickup.Rows[PicRowIndex].Cells["TotalIssueBalanceQty"].Value.ToString());
                Measure = dtgvPickup.Rows[PicRowIndex].Cells["MeasureUnitName"].Value.ToString();
                cmbItemName.SelectedValue = ItemNameID;
                numReqQty.Focus();
                numReqQty.Select(0, 10);
            }
        }

        private void cmbItemGroupSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbItemGroupSearch.SelectedValue) > 0)
                {
                    int groupid = Convert.ToInt32(cmbItemGroupSearch.SelectedValue);
                    string sqg = @"SELECT DISTINCT TOP (100) PERCENT ItemListID, ItemName
FROM         dbo.tbl_GenStoreItemList
WHERE     (ItemgroupID = " + groupid + ")ORDER BY ItemName";
                    DataTable dtg = DPL.getDataTable(sqg);
                    cmbItemSerch.DataSource = dtg;
                    cmbItemSerch.DisplayMember = "ItemName";
                    cmbItemSerch.ValueMember = "ItemListID";
                    cmbItemSerch.SelectedValue = -1;
                    cmbItemSerch.Focus();
                    cmbItemSerch.Select();
                }
                else
                {
                    MessageBox.Show("Please select item group");
                    cmbItemGroupSearch.Focus();
                    cmbItemGroupSearch.Select();
                }
            }
        }

        private void cmbItemGroupSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbItemGroupSearch.SelectedValue) > 0)
                {                   
                    int groupid = Convert.ToInt32(cmbItemGroupSearch.SelectedValue);
                    string sqg = @"SELECT DISTINCT TOP (100) PERCENT ItemListID, ItemName
FROM         dbo.tbl_GenStoreItemList
WHERE     (ItemgroupID = " + groupid + ")ORDER BY ItemName";
                    DataTable dtg = DPL.getDataTable(sqg);
                    cmbItemSerch.DataSource = dtg;
                    cmbItemSerch.DisplayMember = "ItemName";
                    cmbItemSerch.ValueMember = "ItemListID";
                    cmbItemSerch.SelectedValue = -1;
                    cmbItemSerch.Focus();
                    cmbItemSerch.Select();
                }

            }
            catch
            {

            }
        }

        private void cmbItemSerch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbItemSerch.SelectedValue) > 0)
                {
                    int unitId = Convert.ToInt32(cmbUnit.SelectedValue);                   
                    int itemid = Convert.ToInt32(cmbItemSerch.SelectedValue);
                    string sql = @"SELECT     dbo.tblUnitList.UnitName, dbo.tbl_GenStoreIssueHead.IssueSRNo, dbo.tbl_GenStoreIssueHead.IssueDateTime, 
                      dbo.tbl_GenStoreIssueHead.UnitListID, dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
FROM         dbo.tbl_GenStoreIssueHead INNER JOIN
                      dbo.tblUnitList ON dbo.tbl_GenStoreIssueHead.UnitListID = dbo.tblUnitList.UnitListID INNER JOIN
                      dbo.tbl_GenStoreIssueDetails ON dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID
                      WHERE (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitId + ") AND (dbo.tbl_GenStoreIssueDetails.ItemListID = " + itemid + ")" +
                      "ORDER BY dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID";
                    DataTable dt = DPL.getDataTable(sql);
                    dtgvPrevious1st.DataSource = dt;
                    dtgvPrevious1st.Columns["GenStoreIssueHeadID"].Visible = false;
                    dtgvPrevious1st.Columns["UnitListID"].Visible = false;
                }
                else
                {
                    MessageBox.Show("Please select SR No", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbIssueSRNo.Focus();
                    cmbIssueSRNo.Select();
                }
            }
        }

        private void cmbItemSerch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbItemSerch.SelectedValue) > 0)
                {
                    int unitId = Convert.ToInt32(cmbUnit.SelectedValue);
                    int itemid = Convert.ToInt32(cmbItemSerch.SelectedValue);
                    string sql = @"SELECT     dbo.tblUnitList.UnitName, dbo.tbl_GenStoreIssueHead.IssueSRNo, dbo.tbl_GenStoreIssueHead.IssueDateTime, 
                      dbo.tbl_GenStoreIssueHead.UnitListID, dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
FROM         dbo.tbl_GenStoreIssueHead INNER JOIN
                      dbo.tblUnitList ON dbo.tbl_GenStoreIssueHead.UnitListID = dbo.tblUnitList.UnitListID INNER JOIN
                      dbo.tbl_GenStoreIssueDetails ON dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID
                      WHERE (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitId + ") AND (dbo.tbl_GenStoreIssueDetails.ItemListID = " + itemid + ")" +
                      "ORDER BY dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID";
                    DataTable dt = DPL.getDataTable(sql);
                    dtgvPrevious1st.DataSource = dt;
                    dtgvPrevious1st.Columns["GenStoreIssueHeadID"].Visible = false;
                    dtgvPrevious1st.Columns["UnitListID"].Visible = false;
                }
            }
            catch
            {

            }
        }

        private void txtIssueSRNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return") 
            {
                if (txtIssueSRNo.Text.ToString() != "")
                {
                    txtCustomerName.Focus();
                    txtCustomerName.Select();
                }
                else 
                {
                    MessageBox.Show("Please Select your SalseNo");
                    txtIssueSRNo.Focus();
                    txtIssueSRNo.Select();
                }
            }
        }

        private void txtIssueSRNo_Enter(object sender, EventArgs e)
        {
            txtIssueSRNo.BackColor = Color.Cyan;
            txtIssueSRNo.ForeColor = Color.Black;
        }

        private void txtIssueSRNo_Leave(object sender, EventArgs e)
        {
            txtIssueSRNo.BackColor = Color.White;
            txtIssueSRNo.ForeColor = Color.Black;
        }

        private void txtCustomerName_Enter(object sender, EventArgs e)
        {
            txtCustomerName.BackColor = Color.Cyan;
            txtCustomerName.ForeColor = Color.Black;
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtCustomerName.Text.ToString() != "")
                {
                    txtRequisitionNo.Focus();
                    txtRequisitionNo.Select();
                }
                else
                {
                    MessageBox.Show("Please write customer name");
                    txtCustomerName.Focus();
                    txtCustomerName.Select();
                }
            }
        }

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            txtCustomerName.BackColor = Color.White;
            txtCustomerName.ForeColor = Color.Black;
        }

        private void numSellingPrice_Enter(object sender, EventArgs e)
        {
            numSellingPrice.BackColor = Color.Cyan;
            numSellingPrice.ForeColor = Color.Black;
        }

        private void numSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(numSellingPrice.Value)>0)
                {
                    txtRemarks.Focus();
                    txtRemarks.Select();
                }
                else
                {
                    MessageBox.Show("Please input Selling Price");
                    numSellingPrice.Focus();
                    numSellingPrice.Select();
                }
            }
        }

        private void numSellingPrice_Leave(object sender, EventArgs e)
        {
            numSellingPrice.BackColor = Color.White;
            numSellingPrice.ForeColor = Color.Black;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Report.RptSellsSlip rpt = new Report.RptSellsSlip();
            string sql = @"SELECT DISTINCT 
                      dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreIssueDetails.IssueQty, dbo.tbl_GenStoreIssueDetails.SellingPrice, dbo.tbl_GenStoreItemList.SerialNo, 
                      dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID, dbo.tbl_GenStoreIssueDetails.GenStoreIssueDetailsID
FROM         dbo.tbl_GenStoreItemList INNER JOIN
                      dbo.tbl_GenStoreIssueDetails ON dbo.tbl_GenStoreItemList.ItemListID = dbo.tbl_GenStoreIssueDetails.ItemListID INNER JOIN
                      dbo.tbl_GenStoreIssueHead ON dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
WHERE     (dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID = " + NormalHeadMaxID + ")";
            DataTable dt = DPL.getDataTable(sql);
            rpt.SetDataSource(dt);
            Report.frmReportPreview frr = new Report.frmReportPreview(rpt);
            frr.Show();
        }
    }
}
