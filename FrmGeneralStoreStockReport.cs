using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using StoreInformationSystem.Data;
//using StoreInformationSystem.GeneralStore;


namespace StoreInformationSystem
{
    public partial class FrmGeneralStoreStockReport : Form
    {
        DataProcessLayer DPL = new DataProcessLayer();
        ParameterFields myParams = new ParameterFields();
        public FrmGeneralStoreStockReport()
        {
            InitializeComponent();


            string sql = "Select ItemGroupName From tbl_GenStoreItemgroup Order By ItemgroupID";
            DataTable dt = DPL.getDataTable(sql);
            cmbGroupName.DataSource = dt;
            cmbGroupName.DisplayMember = "ItemGroupName";
            cmbGroupName.SelectedIndex = -1;


            string sqel = "Select UnitName, UnitListID  From tblUnitList";
            DataTable dtt = DPL.getDataTable(sqel);
            cmbUnitName.DataSource = dtt;
            cmbUnitName.DisplayMember = "UnitName";
            cmbUnitName.ValueMember = "UnitListID";
            cmbUnitName.SelectedValue = -1;

        }

        private void cmbGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (cmbGroupName.SelectedIndex != -1)
                {
                    btnPrint.Focus();
                    btnPrint.Select();
                }
                else
                {
                    MessageBox.Show("Please Select item group", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbGroupName.Focus();
                    cmbGroupName.Select();
                }
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (cmbGroupName.SelectedIndex != -1)
            //{
            string GroupName = cmbGroupName.Text;

            string sql = "";
            if (Convert.ToInt32(cmbUnitName.SelectedValue) > 0)
            {
                int unitID = Convert.ToInt32(cmbUnitName.SelectedValue);
                if (cmbGroupName.Text.ToString() != "")
                {

                    sql = @"SELECT DISTINCT 
                      f.SerialNo, f.ItemGroupName, f.ItemName, f.TotalIssueBalanceQty AS TotalBalanceQty, 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, f.ItemListID
FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.SerialNo, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
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
                                                                                "SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, 0 AS OpeningQty, dbo.tbl_GenStoreIssueDetails.IssueQty FROM  dbo.tbl_GenStoreIssueDetails INNER JOIN dbo.tbl_GenStoreIssueHead ON dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitID + ")) AS y  GROUP BY ItemListID) AS x INNER JOIN dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.SerialNo, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON  dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID WHERE     (f.ItemGroupName = '" + GroupName + "')ORDER BY f.SerialNo";

                }
                else
                {
                    sql = @"SELECT DISTINCT 
                      f.SerialNo, f.ItemGroupName, f.ItemName, f.TotalIssueBalanceQty AS TotalBalanceQty, 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, f.ItemListID
FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                          (SELECT     dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.SerialNo, dbo.tbl_GenStoreItemList.ItemName, SUM(x.TotalQty + x.OpeningQty) AS TotalReceive, 
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
                                                                                    "SELECT     dbo.tbl_GenStoreIssueDetails.ItemListID, 0 AS TotalQty, 0 AS OpeningQty, dbo.tbl_GenStoreIssueDetails.IssueQty FROM  dbo.tbl_GenStoreIssueDetails INNER JOIN dbo.tbl_GenStoreIssueHead ON dbo.tbl_GenStoreIssueDetails.GenStoreIssueHeadID = dbo.tbl_GenStoreIssueHead.GenStoreIssueHeadID WHERE     (dbo.tbl_GenStoreIssueHead.UnitListID = " + unitID + ")) AS y  GROUP BY ItemListID) AS x INNER JOIN dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.SerialNo, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID) AS f ON  dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID ORDER BY f.SerialNo";

                }

            }
            else
            {
                if (cmbGroupName.SelectedIndex != -1)
                {

                    sql = @"SELECT DISTINCT 
                      TOP 100 PERCENT f.SerialNo, f.ItemGroupName, f.ItemName, f.TotalBalanceQty, dbo.tbl_GenStoreMeasureUnit.MeasureUnitName, f.ItemListID
                      FROM         dbo.tbl_GenStoreMeasureUnit INNER JOIN
                      dbo.tbl_GenStoreRcvdNormalDetails ON 
                      dbo.tbl_GenStoreMeasureUnit.MeasureUnitID = dbo.tbl_GenStoreRcvdNormalDetails.MeasureUnitID RIGHT OUTER JOIN
                          (SELECT     dbo.tbl_GenStoreItemList.SerialNo, dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, 
                                                   SUM(x.TotalQty + x.OpeningQty - x.IssueQty) AS TotalBalanceQty, dbo.tbl_GenStoreItemList.ItemListID
                            FROM          (SELECT     TOP 100 PERCENT ItemListID, SUM(TotalQty) AS TotalQty, SUM(OpeningQty) AS OpeningQty, SUM(IssueQty) 
                                                                           AS IssueQty
                                                    FROM          (SELECT     ItemListID, TotalQty, 0 AS OpeningQty, 0 AS IssueQty
                                                                            FROM          dbo.tbl_GenStoreRcvdNormalDetails
                                                                            UNION ALL
                                                                            SELECT     ItemListID, 0 AS TotalQty, OpeningQty, 0 AS IssueQty
                                                                            FROM         dbo.tbl_GenStoreOpening
                                                                            UNION ALL
                                                                            SELECT     ItemListID, 0 AS TotalQty, 0 AS OpeningQty, IssueQty
                                                                            FROM         dbo.tbl_GenStoreIssueDetails) y
                                                    GROUP BY ItemListID) x INNER JOIN
                                                   dbo.tbl_GenStoreItemList ON x.ItemListID = dbo.tbl_GenStoreItemList.ItemListID INNER JOIN
                                                   dbo.tbl_GenStoreItemgroup ON dbo.tbl_GenStoreItemList.ItemgroupID = dbo.tbl_GenStoreItemgroup.ItemgroupID
                            GROUP BY dbo.tbl_GenStoreItemgroup.ItemGroupName, dbo.tbl_GenStoreItemList.ItemName, dbo.tbl_GenStoreItemList.ItemListID, 
                                                   dbo.tbl_GenStoreItemList.SerialNo) f ON dbo.tbl_GenStoreRcvdNormalDetails.ItemListID = f.ItemListID
                            WHERE     (f.ItemGroupName = '" + GroupName + "')" +
                               "ORDER BY f.SerialNo";
                }

                else
                {
                    MessageBox.Show("Please Select item group", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbGroupName.Focus();
                    cmbGroupName.Select();
                }
            }
                DataTable dt = DPL.getDataTable(sql);
                ReportClass rpt = null;
                if (cmbGroupName.SelectedIndex != -1)
                {
                    rpt = new UI.rptGeneralStoreStockBalance();
                }
                else
                {
                    rpt = new UI.rptItemGroupWiseStock();
                }


                if (Convert.ToInt32(cmbUnitName.SelectedValue) > 0)
                {
                    CrystalDecisions.CrystalReports.Engine.TextObject root;
                    root = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt.ReportDefinition.ReportObjects["Text2"];

                    if (Convert.ToInt32(cmbUnitName.SelectedValue) == 1)
                    {
                        root.Text = "DBL TELECOM.";
                    }
                    //else if (Convert.ToInt32(cmbUnitName.SelectedValue) == 2)
                    //{
                    //    root.Text = "HAMZA TEXTILES LTD.";
                    //}
                    //if (Convert.ToInt32(cmbUnitName.SelectedValue) == 3)
                    //{
                    //    root.Text = "DB TEX LTD.";
                    //}
                    //else if (Convert.ToInt32(cmbUnitName.SelectedValue) == 4)
                    //{
                    //    root.Text = "HAMZA WASHING LTD.";
                    //}
                    //if (Convert.ToInt32(cmbUnitName.SelectedValue) == 5)
                    //{
                    //    root.Text = "TEXTILE TESTING SERVICES LTD.";
                    //}
                    //else if (Convert.ToInt32(cmbUnitName.SelectedValue) == 6)
                    //{
                    //    root.Text = "COLOUR CITY LTD.";
                    //}
                }
                else
                {
                    CrystalDecisions.CrystalReports.Engine.TextObject root1;
                    root1 = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt.ReportDefinition.ReportObjects["Text2"];
                    root1.Text = "DBL TELECOM";
                }

                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;

                //rptGeneralStoreStockBalance1.SetDataSource(dt);
                //crystalReportViewer1.ParameterFieldInfo = myParams;
                //crystalReportViewer1.ReportSource = rptGeneralStoreStockBalance1;
                //}
                //else
                //{
                //    MessageBox.Show("Please Select item group", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    cmbGroupName.Focus();
                //    cmbGroupName.Select();
                //}
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Mainfrm.bStock = true;
            this.Close();
        }      
    }
}
