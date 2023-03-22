using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoreInformationSystem.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace StoreInformationSystem.UI
{
    public partial class frmReportMenue : Form
    {
        DataProcessLayer DPL = new DataProcessLayer();
        //int UnitListID = UserLoginForm.UnitListID;
        public frmReportMenue()
        {
            InitializeComponent();

            string sql = "Select ItemGroupName,ItemgroupID From tbl_GenStoreItemgroup Order By ItemgroupID";
            DataTable dt = DPL.getDataTable(sql);
            cmbGroupName.DataSource = dt;
            cmbGroupName.DisplayMember = "ItemGroupName";
            //cmbGroupName.ValueMember = "ItemGroupName";
            cmbGroupName.SelectedIndex = -1;


            string sqel = "Select UnitName, UnitListID  From tblUnitList";
            DataTable dtt = DPL.getDataTable(sqel);
            cmbUnitName.DataSource = dtt;
            cmbUnitName.DisplayMember = "UnitName";
            cmbUnitName.ValueMember = "UnitListID";
            cmbUnitName.SelectedValue = -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Mainfrm.bReportMenue = true;
            this.Close();
        }
        bool B = true;
        private void DateRange_Click(object sender, EventArgs e)
        {
            if (B)
            {
                if (DateRange.Checked)
                {
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                    B = false;
                }
            }
            else
            {
                DateRange.Checked = false;
                B = true;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int UnitListID = 0;
            string sql = "";
            ReportClass rpt = null;
            
            if (cmbReportName.Text.Trim().ToString() == "BarCode Print")
            {
                sql = @"SELECT     IssueQty, GenStoreIssueHeadID
FROM         dbo.tbl_GenStoreIssueDetails
WHERE     (GenStoreIssueHeadID = 14985)";

                rpt = new rptBarcode();
            }
            else
            {
                if (Convert.ToInt32(cmbUnitName.SelectedValue) > 0)
                {
                    UnitListID = Convert.ToInt32(cmbUnitName.SelectedValue);
                }                
                string groupname = cmbGroupName.Text.ToString();
                string date1 = dateTimePicker1.Value.ToString("dd/MMM/yyyy");
                string date2 = dateTimePicker2.Value.ToString("dd/MMM/yyyy");
                rpt = new rptDateWiseReceiveDetails();
                if (DateRange.Checked)
                {
                    if (Convert.ToInt32(cmbUnitName.SelectedValue) > 0)
                    {
                        if (cmbGroupName.Text.ToString() != "")
                        {
                            sql = @"SELECT DISTINCT TOP (100) PERCENT f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.ItemListID, f.EntryDate
FROM         (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, SUM(x.TotalQty) AS TotalReceive, SUM(x.IssueQty) AS TotalIssue, 
                                              tbl_GenStoreItemList_1.ItemListID, x.EntryDate
                       FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(IssueQty) AS IssueQty, EntryDate
                                               FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS IssueQty, 
                                                                                              CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) AS EntryDate
                                                                       FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                              dbo.tbl_GenStoreRcvdNormalHead AS tbl_GenStoreRcvdNormalHead_1 ON 
                                                                                              tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = tbl_GenStoreRcvdNormalHead_1.GenStoreRcvdNormalHeadID
                                                                       WHERE      (tbl_GenStoreRcvdNormalHead_1.UnitListID = " + UnitListID + ") AND (CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date2 + "', 102))"
                                                                             + " UNION ALL " + " " +
                                                                              @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                                                                                             CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) AS EntryDate
                                                                       FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                                                                                             dbo.tbl_GenStoreIssueHead ON 
                                                                                             dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                                                                       WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + UnitListID + ") AND (CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date1 + "', 102))) AS y  GROUP BY ItemListID, EntryDate) AS x INNER JOIN dbo.tbl_GenStoreItemList AS tbl_GenStoreItemList_1 ON x.ItemListID = tbl_GenStoreItemList_1.ItemListID INNER JOIN   dbo.tbl_GenStoreItemgroup ON tbl_GenStoreItemList_1.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID  GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, tbl_GenStoreItemList_1.ItemListID, x.EntryDate) AS f LEFT OUTER JOIN dbo.tbl_GenStoreRcvdNormalDetails ON f.ItemListID = dbo.tbl_GenStoreRcvdNormalDetails.ItemListID WHERE     (f.ItemGroupName = '" + groupname + "')ORDER BY f.ItemName";
                        }
                        else
                        {
                            sql = @"SELECT DISTINCT TOP (100) PERCENT f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.ItemListID, f.EntryDate
FROM         (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, SUM(x.TotalQty) AS TotalReceive, SUM(x.IssueQty) AS TotalIssue, 
                                              tbl_GenStoreItemList_1.ItemListID, x.EntryDate
                       FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(IssueQty) AS IssueQty, EntryDate
                                               FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS IssueQty, 
                                                                                              CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) AS EntryDate
                                                                       FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                              dbo.tbl_GenStoreRcvdNormalHead AS tbl_GenStoreRcvdNormalHead_1 ON 
                                                                                              tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = tbl_GenStoreRcvdNormalHead_1.GenStoreRcvdNormalHeadID
                                                                       WHERE      (tbl_GenStoreRcvdNormalHead_1.UnitListID = " + UnitListID + ") AND (CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date2 + "', 102))"
                                                                                                    + " UNION ALL " + " " +
                                                                                                     @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                                                                                             CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) AS EntryDate
                                                                       FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                                                                                             dbo.tbl_GenStoreIssueHead ON 
                                                                                             dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                                                                       WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + UnitListID + ") AND (CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date1 + "', 102))) AS y  GROUP BY ItemListID, EntryDate) AS x INNER JOIN dbo.tbl_GenStoreItemList AS tbl_GenStoreItemList_1 ON x.ItemListID = tbl_GenStoreItemList_1.ItemListID INNER JOIN   dbo.tbl_GenStoreItemgroup ON tbl_GenStoreItemList_1.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID  GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, tbl_GenStoreItemList_1.ItemListID, x.EntryDate) AS f LEFT OUTER JOIN dbo.tbl_GenStoreRcvdNormalDetails ON f.ItemListID = dbo.tbl_GenStoreRcvdNormalDetails.ItemListID ORDER BY f.ItemName";
                        }

                    }
                    else
                    {
                        if (cmbGroupName.Text.ToString() != "")
                        {
                            sql = @"SELECT DISTINCT TOP (100) PERCENT f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.ItemListID, f.EntryDate
FROM         (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, SUM(x.TotalQty) AS TotalReceive, SUM(x.IssueQty) AS TotalIssue, 
                                              tbl_GenStoreItemList_1.ItemListID, x.EntryDate
                       FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(IssueQty) AS IssueQty, EntryDate
                                               FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS IssueQty, 
                                                                                              CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) AS EntryDate
                                                                       FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                              dbo.tbl_GenStoreRcvdNormalHead AS tbl_GenStoreRcvdNormalHead_1 ON 
                                                                                              tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = tbl_GenStoreRcvdNormalHead_1.GenStoreRcvdNormalHeadID
                                                                       WHERE       (CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date2 + "', 102))"
                                                                                                    + " UNION ALL " + " " +
                                                                                                     @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                                                                                             CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) AS EntryDate
                                                                       FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                                                                                             dbo.tbl_GenStoreIssueHead ON 
                                                                                             dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                                                                       WHERE     (CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date1 + "', 102))) AS y  GROUP BY ItemListID, EntryDate) AS x INNER JOIN dbo.tbl_GenStoreItemList AS tbl_GenStoreItemList_1 ON x.ItemListID = tbl_GenStoreItemList_1.ItemListID INNER JOIN   dbo.tbl_GenStoreItemgroup ON tbl_GenStoreItemList_1.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID  GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, tbl_GenStoreItemList_1.ItemListID, x.EntryDate) AS f LEFT OUTER JOIN dbo.tbl_GenStoreRcvdNormalDetails ON f.ItemListID = dbo.tbl_GenStoreRcvdNormalDetails.ItemListID WHERE     (f.ItemGroupName = '" + groupname + "')ORDER BY f.ItemName";
                        }
                        else
                        {
                            sql = @"SELECT DISTINCT TOP (100) PERCENT f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.ItemListID, f.EntryDate
FROM         (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, SUM(x.TotalQty) AS TotalReceive, SUM(x.IssueQty) AS TotalIssue, 
                                              tbl_GenStoreItemList_1.ItemListID, x.EntryDate
                       FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(IssueQty) AS IssueQty, EntryDate
                                               FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS IssueQty, 
                                                                                              CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) AS EntryDate
                                                                       FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                              dbo.tbl_GenStoreRcvdNormalHead AS tbl_GenStoreRcvdNormalHead_1 ON 
                                                                                              tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = tbl_GenStoreRcvdNormalHead_1.GenStoreRcvdNormalHeadID
                                                                       WHERE       (CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date2 + "', 102))"
                                                                                                                           + " UNION ALL " + " " +
                                                                                                                            @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                                                                                             CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) AS EntryDate
                                                                       FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                                                                                             dbo.tbl_GenStoreIssueHead ON 
                                                                                             dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                                                                       WHERE     (CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) BETWEEN  CONVERT(DATETIME, '" + date1 + "', 102) AND CONVERT(DATETIME, '" + date1 + "', 102))) AS y  GROUP BY ItemListID, EntryDate) AS x INNER JOIN dbo.tbl_GenStoreItemList AS tbl_GenStoreItemList_1 ON x.ItemListID = tbl_GenStoreItemList_1.ItemListID INNER JOIN   dbo.tbl_GenStoreItemgroup ON tbl_GenStoreItemList_1.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID  GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, tbl_GenStoreItemList_1.ItemListID, x.EntryDate) AS f LEFT OUTER JOIN dbo.tbl_GenStoreRcvdNormalDetails ON f.ItemListID = dbo.tbl_GenStoreRcvdNormalDetails.ItemListID ORDER BY f.ItemName";
                        }
                    }
                    CrystalDecisions.CrystalReports.Engine.TextObject root;
                    root = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt.ReportDefinition.ReportObjects["Text7"];
                    root.Text = "Date :" + date1 + " To " + date2;
                }
                else
                {
                    if (Convert.ToInt32(cmbUnitName.SelectedValue) > 0 && cmbGroupName.Text.ToString() != "")
                    {

                        sql = @"SELECT DISTINCT TOP (100) PERCENT f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.ItemListID, f.EntryDate
FROM         (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, SUM(x.TotalQty) AS TotalReceive, SUM(x.IssueQty) AS TotalIssue, 
                                              tbl_GenStoreItemList_1.ItemListID, x.EntryDate
                       FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(IssueQty) AS IssueQty, EntryDate
                                               FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS IssueQty, 
                                                                                              CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) AS EntryDate
                                                                       FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                              dbo.tbl_GenStoreRcvdNormalHead AS tbl_GenStoreRcvdNormalHead_1 ON 
                                                                                              tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = tbl_GenStoreRcvdNormalHead_1.GenStoreRcvdNormalHeadID
                                                                       WHERE      (tbl_GenStoreRcvdNormalHead_1.UnitListID = " + UnitListID + ")"
                                                                              + " UNION ALL " + " " +
                                                                               @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                                                                                             CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) AS EntryDate
                                                                       FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                                                                                             dbo.tbl_GenStoreIssueHead ON 
                                                                                             dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                                                                       WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + UnitListID + ")) AS y  GROUP BY ItemListID, EntryDate) AS x INNER JOIN dbo.tbl_GenStoreItemList AS tbl_GenStoreItemList_1 ON x.ItemListID = tbl_GenStoreItemList_1.ItemListID INNER JOIN   dbo.tbl_GenStoreItemgroup ON tbl_GenStoreItemList_1.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID  GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, tbl_GenStoreItemList_1.ItemListID, x.EntryDate) AS f LEFT OUTER JOIN dbo.tbl_GenStoreRcvdNormalDetails ON f.ItemListID = dbo.tbl_GenStoreRcvdNormalDetails.ItemListID WHERE     (f.ItemGroupName = '" + groupname + "')ORDER BY f.ItemName";
                    }
                    else
                    {
                        if (Convert.ToInt32(cmbUnitName.SelectedValue) > 0)
                        {
                            sql = @"SELECT DISTINCT TOP (100) PERCENT f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.ItemListID, f.EntryDate
FROM         (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, SUM(x.TotalQty) AS TotalReceive, SUM(x.IssueQty) AS TotalIssue, 
                                              tbl_GenStoreItemList_1.ItemListID, x.EntryDate
                       FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(IssueQty) AS IssueQty, EntryDate
                                               FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS IssueQty, 
                                                                                              CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) AS EntryDate
                                                                       FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                              dbo.tbl_GenStoreRcvdNormalHead AS tbl_GenStoreRcvdNormalHead_1 ON 
                                                                                              tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = tbl_GenStoreRcvdNormalHead_1.GenStoreRcvdNormalHeadID
                                                                       WHERE      (tbl_GenStoreRcvdNormalHead_1.UnitListID = " + UnitListID + ")"
                                                                                                     + " UNION ALL " + " " +
                                                                                                      @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                                                                                             CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) AS EntryDate
                                                                       FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                                                                                             dbo.tbl_GenStoreIssueHead ON 
                                                                                             dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID
                                                                       WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + UnitListID + ")) AS y  GROUP BY ItemListID, EntryDate) AS x INNER JOIN dbo.tbl_GenStoreItemList AS tbl_GenStoreItemList_1 ON x.ItemListID = tbl_GenStoreItemList_1.ItemListID INNER JOIN   dbo.tbl_GenStoreItemgroup ON tbl_GenStoreItemList_1.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID  GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, tbl_GenStoreItemList_1.ItemListID, x.EntryDate) AS f LEFT OUTER JOIN dbo.tbl_GenStoreRcvdNormalDetails ON f.ItemListID = dbo.tbl_GenStoreRcvdNormalDetails.ItemListID ORDER BY f.ItemName";
                        }
                        else
                        {
                            sql = @"SELECT DISTINCT TOP (100) PERCENT f.ItemGroupName, f.ItemName, f.TotalReceive, f.TotalIssue, f.ItemListID, f.EntryDate
FROM         (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, SUM(x.TotalQty) AS TotalReceive, SUM(x.IssueQty) AS TotalIssue, 
                                              tbl_GenStoreItemList_1.ItemListID, x.EntryDate
                       FROM          (SELECT     TOP (100) PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(IssueQty) AS IssueQty, EntryDate
                                               FROM          (SELECT     tbl_GenStoreRcvdNormalDetails_1.ItemListID, tbl_GenStoreRcvdNormalDetails_1.TotalQty, 0 AS IssueQty, 
                                                                                              CAST(tbl_GenStoreRcvdNormalHead_1.RcvDate AS datetime) AS EntryDate
                                                                       FROM          dbo.tbl_GenStoreRcvdNormalDetails AS tbl_GenStoreRcvdNormalDetails_1 INNER JOIN
                                                                                              dbo.tbl_GenStoreRcvdNormalHead AS tbl_GenStoreRcvdNormalHead_1 ON 
                                                                                              tbl_GenStoreRcvdNormalDetails_1.GenStoreRcvdNormalHeadID = tbl_GenStoreRcvdNormalHead_1.GenStoreRcvdNormalHeadID"
                                                                                                     + " UNION ALL " + " " +
                                                                                                      @"SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, dbo.tbl_GenStoreIssueDetails.IssueQty, 
                                                                                             CAST(dbo.tbl_GenStoreIssueHead.IssueDate AS datetime) AS EntryDate
                                                                       FROM         dbo.tbl_GenStoreIssueDetails INNER JOIN
                                                                                             dbo.tbl_GenStoreIssueHead ON 
                                                                                             dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID) AS y  GROUP BY ItemListID, EntryDate) AS x INNER JOIN dbo.tbl_GenStoreItemList AS tbl_GenStoreItemList_1 ON x.ItemListID = tbl_GenStoreItemList_1.ItemListID INNER JOIN   dbo.tbl_GenStoreItemgroup ON tbl_GenStoreItemList_1.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID  GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, tbl_GenStoreItemList_1.ItemName, tbl_GenStoreItemList_1.ItemListID, x.EntryDate) AS f LEFT OUTER JOIN dbo.tbl_GenStoreRcvdNormalDetails ON f.ItemListID = dbo.tbl_GenStoreRcvdNormalDetails.ItemListID WHERE     (f.ItemGroupName = '" + groupname + "')ORDER BY f.ItemName";
                        }
                    }
                }
            }
            
            DataTable dt = DPL.getDataTable(sql);
            rpt.SetDataSource(dt);
            if (Convert.ToInt32(cmbUnitName.SelectedValue) > 0)
            {
                //UnitListID = Convert.ToInt32(cmbUnitName.SelectedValue);
                string sq = @"SELECT     UnitListID, UnitFullName,Address
FROM         dbo.tblUnitList
WHERE     (UnitListID = " + UnitListID + ")";
                DataTable dtt = DPL.getDataTable(sq);
                string unitname = dtt.Rows[0]["UnitFullName"].ToString();
                string address = dtt.Rows[0]["Address"].ToString();
                CrystalDecisions.CrystalReports.Engine.TextObject root1;
                CrystalDecisions.CrystalReports.Engine.TextObject root2;
                root1 = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt.ReportDefinition.ReportObjects["Text10"];
                root2 = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                root1.Text = unitname;
                root2.Text = address;
            }

            frmrptPreview rv = new frmrptPreview(rpt);
            rv.Show();
        }
    }
}

