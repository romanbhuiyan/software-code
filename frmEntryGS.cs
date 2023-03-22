//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using Gate.Data;
//using System.Collections;

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
    public partial class frmEntryGS : Form
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataProcessLayer DPL = new DataProcessLayer();
        public int GroupID = 0;
        public frmEntryGS()
        {
            InitializeComponent();
            //Item_group();
            distinct();
        }
        private void Item_group()
        {
            string sql = "select * from tbl_GenStoreItemgroup";
            DataTable vv = DPL.getDataTable(sql);
            cmbItemgroup.DataSource = vv;
            cmbItemgroup.DisplayMember = "ItemGroupName";
            cmbItemgroup.ValueMember = "ItemgroupID";
            cmbItemgroup.SelectedValue = GroupID;
           
        }
        private void distinct()
        {

            DataTable dt = DAL.GetMaxID(" (SELECT     ItemgroupID, CONVERT(int, SerialNo, 32) AS SerialNo  FROM          dbo.tbl_GenStoreItemList  WHERE      (ItemgroupID = 3)) AS x", "SerialNo");
            string p = dt.Rows[0][0].ToString();
            if (p != "")
            {
                int k = Convert.ToInt32(p);
                int MRRNo = k + 1;
                txtSerial.Text = MRRNo.ToString();
                //string [] ss=MRRNo.ToString().Split();
                //for (int i = 4; i < 4; i++)
                //{
                //}              
                //txtMRRNo.Text = MRRNo.ToString();
            }
            else
            {
                txtSerial.Text = "1";
            }
        }
        private void txtItemList_Enter(object sender, EventArgs e)
        {
            txtItemList.BackColor = Color.Aquamarine;
        }

        private void txtItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtItemList.Text.Trim().ToString() != "")
                {
                    Item_group();
                    //cmbItemgroup.Focus();
                    //cmbItemgroup.Select();
                    btsave.Focus();
                    btsave.Select();
                }
                else
                {
                    MessageBox.Show("Please write item name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtItemList.Focus();
                    txtItemList.Select();
                }
            }
        }
        private void txtItemList_Leave(object sender, EventArgs e)
        {
            cmbItemgroup.BackColor = Color.White;
        }

        private void cmbItemgroup_Enter(object sender, EventArgs e)
        {
            txtItemList.BackColor = Color.Aquamarine;
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
                    MessageBox.Show("Please select Itemgroup name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbItemgroup.Focus();
                    cmbItemgroup.Select();
                }
            }
        }

        private void cmbItemgroup_Leave(object sender, EventArgs e)
        {
            btsave.BackColor = Color.White;
        }

        private void btsave_Enter(object sender, EventArgs e)
        {
            cmbItemgroup.BackColor = Color.Aquamarine;
        }      

        private void btsave_Leave(object sender, EventArgs e)
        {
            btsave.BackColor = Color.White;
        }
        private string INSERT()
        {
            string Insert = "";
            try
            {
                if (txtItemList.Text.Trim() != "" & cmbItemgroup.Text.Trim() != "")
                {
                    string Itemlist = txtItemList.Text.Trim();
                    int Itemgroup = Convert.ToInt32(cmbItemgroup.SelectedValue);
                    int Serial = Convert.ToInt32(txtSerial.Text.Trim());
                    Insert = DAL.Insert_Into_Item_List(Itemlist, Itemgroup, Serial);
                    if (Insert == "Succeeded")
                    {
                        DataTable dtt = DAL.GetMaxID("tbl_GenStoreItemList", "ItemListID");
                        string maxid = dtt.Rows[0][0].ToString();
                        if (maxid != "")
                        {
                            Mainfrm.ID = Convert.ToInt32(maxid);
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
            return Insert;
        }
        

        private void btsave_Click(object sender, EventArgs e)
        {
            if (GroupID != 0 && txtItemList.Text.Trim() != "" && cmbItemgroup.Text.Trim() != "")
            {
                INSERT();
            }
            else
            {
                INSERTSU();
            }
            this.Hide();
            Mainfrm.GeneralRcv.Enabled = true;
        }
        private string INSERTSU()
        {
            string Insert = "";
            try
            {
                if (txtSupplierName.Text.Trim() != "" )
                {
                    string SupplierName = txtSupplierName.Text.Trim();
                    Insert = DAL.Insert_Into_Supplier(SupplierName);
                    if (Insert == "Succeeded")
                    {
                        DataTable dtt = DAL.GetMaxID("tbl_GenStoreSupplierList", "GenStoreSupplierListID");
                        string maxid = dtt.Rows[0][0].ToString();
                        if (maxid != "")
                        {
                            Mainfrm.SupplierID = Convert.ToInt32(maxid);
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
            return Insert;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainfrm.GeneralRcv.Enabled = true;
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtSupplierName.Text.Trim().ToString() != "")
                {                   
                    btsave.Focus();
                    btsave.Select();
                }
                else
                {
                    MessageBox.Show("Please write supplier name", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSupplierName.Focus();
                    txtSupplierName.Select();
                }
            }
        }      
    }
}
