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
    public partial class UserLoginForm : Form
    {
        DataProcessLayer DPL = new DataProcessLayer();

        public UserLoginForm()
        {
            InitializeComponent();
            Getdata();
            cmbUserName.Focus();
            cmbUserName.Select();
        }
        #region Member Variable
        bool b = false;
        public static UserLoginForm Ulf = null;
        public static string UnitName = "";
        public static string UserName = "";
        public static string Department = "";
        public static string Section = "";
        public static string UserRole = "";
        public static int UnitListID = 0;
        public static int UserRoleID = 0;
        public static int UserID = 0;
        public static int PermissionStatus = 0;
        public static int fromid = 0;
        public static int userid = 0;
        public static string Pass = "";

        #endregion

        private void Getdata()
        {
//            //string sql = "SELECT GETDATE() AS GetDate";
//            //DataTable dt = DPL.getDataTable(sql);
//            //string dated = dt.Rows[0]["GetDate"].ToString();
//            //dateTimePicker1.Value = Convert.ToDateTime(dated);
//            //label8.Text = dateTimePicker1.Value.ToString("dddd,dd MMMM,yy  hh:ss:tt");



//            string str = @"SELECT     dbo.tbl_User_Based_UserInformation.UserID, dbo.tbl_User_Based_UserInformation.UserName, dbo.tbl_User_Based_UserInformation.Password, 
//                      dbo.tblUnitList.UnitName, dbo.tbl_User_Based_UserInformation.Department, dbo.tbl_User_Based_Permission.PermissionStatus, 
//                      dbo.tbl_User_Based_UserInformation.UnitListID, dbo.tbl_User_Based_ModuleGroupList.ModuleGroupName
//FROM         dbo.tbl_User_Based_UserInformation INNER JOIN
//                      dbo.tblUnitList ON dbo.tbl_User_Based_UserInformation.UnitListID = dbo.tblUnitList.UnitListID INNER JOIN
//                      dbo.tbl_User_Based_Permission ON dbo.tbl_User_Based_UserInformation.UserID = dbo.tbl_User_Based_Permission.UserID INNER JOIN
//                      dbo.tbl_User_Based_ModuleGroupList ON 
//                      dbo.tbl_User_Based_Permission.ModuleGroupID = dbo.tbl_User_Based_ModuleGroupList.ModuleGroupID INNER JOIN
//                      dbo.tbl_User_Based_FormList ON dbo.tbl_User_Based_Permission.FormID = dbo.tbl_User_Based_FormList.FormID";
//            string str = @"SELECT     TOP (100) PERCENT dbo.tblUnitList.UnitName, dbo.tbl_UserInformation.UserName, dbo.tbl_UserInformation.Designation, 
//                                  dbo.tbl_UserInformation.Department, dbo.tbl_UserInformation.SectionName, dbo.tbl_UserInformation.Password, dbo.tbl_UserInformation.UserStatus, 
//                                  dbo.tbl_User_Based_ModuleList.ModuleName, dbo.tbl_User_Based_FormList.FormName, dbo.tbl_User_Based_ModuleList.ModuleType, 
//                                  dbo.tbl_User_Based_PermissionType.PermisitionType, dbo.tbl_User_Based_Permission.PermissionStatus, dbo.tbl_UserInformation.UserID, dbo.tblUnitList.UnitListID
//            FROM         dbo.tbl_UserInformation INNER JOIN
//                                  dbo.tblUnitList ON dbo.tbl_UserInformation.UnitListID = dbo.tblUnitList.UnitListID INNER JOIN
//                                  dbo.tbl_User_Based_Permission ON dbo.tbl_UserInformation.UserID = dbo.tbl_User_Based_Permission.UserID INNER JOIN
//                                  dbo.tbl_User_Based_FormList ON dbo.tbl_User_Based_Permission.FormID = dbo.tbl_User_Based_FormList.FormID INNER JOIN
//                                  dbo.tbl_User_Based_ModuleList ON dbo.tbl_User_Based_FormList.ModuleID = dbo.tbl_User_Based_ModuleList.ModuleID INNER JOIN
//                                  dbo.tbl_User_Based_PermissionType ON 
//                                  dbo.tbl_User_Based_Permission.PermisitionTypeID = dbo.tbl_User_Based_PermissionType.PermisitionTypeID
//            WHERE     (dbo.tbl_UserInformation.UserStatus = 'Active')
//            ORDER BY dbo.tbl_UserInformation.UserID";
            string str = @"SELECT     UserName, UserID FROM         dbo.tbl_User_Based_UserInformation";
            DataTable dtt = DPL.getDataTable(str);
            DataView dv = new DataView(dtt);
            string[] col = { "UserName", "UserID" };
            DataTable dd = dv.ToTable(true, col);
            cmbUserName.DataSource = dd;
            cmbUserName.DisplayMember = "UserName";
            cmbUserName.ValueMember = "UserID";
            cmbUserName.SelectedValue = -1;
            
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbUserName.SelectedValue) > 0)
            {
                if (txtPassword.Text.Trim().ToString() != "")
                {
                    userid = (Convert.ToInt32(cmbUserName.SelectedValue));
                    Pass = txtPassword.Text.Trim().ToString();
                    string sql = @"SELECT     UserID, UserName, UserFullName, SectionName, Department, Password
                                  FROM         dbo.tbl_User_Based_UserInformation
                                  WHERE     (UserID = " + userid + ") AND (Password = '" + Pass + "')";

//                    string sql = @"SELECT     TOP (100) PERCENT dbo.tblUnitList.UnitName, dbo.tbl_UserInformation.UserName, dbo.tbl_UserInformation.UserFullName, dbo.tbl_UserInformation.Designation, 
//                                  dbo.tbl_UserInformation.Department, dbo.tbl_UserInformation.SectionName, dbo.tbl_UserInformation.Password, dbo.tbl_UserInformation.UserStatus, 
//                                  dbo.tbl_User_Based_ModuleList.ModuleName, dbo.tbl_User_Based_FormList.FormName, dbo.tbl_User_Based_ModuleList.ModuleType, 
//                                  dbo.tbl_User_Based_PermissionType.PermisitionType, dbo.tbl_User_Based_Permission.PermissionStatus, dbo.tbl_UserInformation.UserID, dbo.tblUnitList.UnitListID
//            FROM         dbo.tbl_UserInformation INNER JOIN
//                                  dbo.tblUnitList ON dbo.tbl_UserInformation.UnitListID = dbo.tblUnitList.UnitListID INNER JOIN
//                                  dbo.tbl_User_Based_Permission ON dbo.tbl_UserInformation.UserID = dbo.tbl_User_Based_Permission.UserID INNER JOIN
//                                  dbo.tbl_User_Based_FormList ON dbo.tbl_User_Based_Permission.FormID = dbo.tbl_User_Based_FormList.FormID INNER JOIN
//                                  dbo.tbl_User_Based_ModuleList ON dbo.tbl_User_Based_FormList.ModuleID = dbo.tbl_User_Based_ModuleList.ModuleID INNER JOIN
//                                  dbo.tbl_User_Based_PermissionType ON 
//                                  dbo.tbl_User_Based_Permission.PermisitionTypeID = dbo.tbl_User_Based_PermissionType.PermisitionTypeID
//            WHERE     (dbo.tbl_UserInformation.UserStatus = 'Active') AND (dbo.tbl_UserInformation.UserID = " + userid + ") AND (dbo.tbl_UserInformation.Password = '" + Pass + "')ORDER BY dbo.tbl_UserInformation.UserID";
                    DataTable dtt = DPL.getDataTable(sql); ;
                                        if (dtt.Rows.Count > 0)
                                        {
                                            UserName = dtt.Rows[0]["UserFullName"].ToString();
                                            //Department = dtt.Rows[0]["Department"].ToString();
                                            //Section = dtt.Rows[0]["SectionName"].ToString();
                                            //UserRole = dtt.Rows[0]["PermisitionType"].ToString();
                                            //PermissionStatus = Convert.ToInt32(dtt.Rows[0]["PermissionStatus"].ToString());
                                            //UnitName = dtt.Rows[0]["UnitName"].ToString();
                                            //UnitListID = Convert.ToInt32(dtt.Rows[0]["UnitListID"].ToString());
                    Hide();
                    Mainfrm ObjForm1 = new Mainfrm();
                    ObjForm1.Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Did you forget your password? Please type your password again. Be sure to use the correct Uppercase and Lowercase Letters.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            txtPassword.Text = "";
                                            cmbUserName.Focus();
                                            cmbUserName.Select();
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Please type your passward");
                                        txtPassword.Focus();
                                        txtPassword.Select();
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Please select user name");
                                    cmbUserName.Focus();
                                    cmbUserName.Select();
                                }
            //    }
            //}
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbUserName_Enter(object sender, EventArgs e)
        {
            cmbUserName.BackColor = Color.MistyRose;
        }

        private void cmbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (Convert.ToInt32(cmbUserName.SelectedValue) > 0)
                {
                    b = false;
                    txtPassword.Focus();
                    txtPassword.Select();
                }
                else
                {
                    MessageBox.Show("Please select user name");
                    cmbUserName.Focus();
                    cmbUserName.Select();
                }
            }
        }

        private void cmbUserName_Leave(object sender, EventArgs e)
        {
            cmbUserName.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.MistyRose;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                if (txtPassword.Text.Trim().ToString() != "")
                {
                    btnLogin.Focus();
                    btnLogin.Select();
                }
                else
                {
                    MessageBox.Show("Please type your passward");
                    txtPassword.Focus();
                    txtPassword.Select();
                }
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }

        private void cmbUserName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (b)
            {
                try
                {
                    txtPassword.Focus();
                    txtPassword.Select();
                    b = false;
                }
                catch
                {

                }
            }
        }

        private void cmbUserName_Click(object sender, EventArgs e)
        {
            b = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sql = "SELECT GETDATE() AS GetDate";
            DataTable dt = DPL.getDataTable(sql);
            string dated = dt.Rows[0]["GetDate"].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dated);
            label8.Text = dateTimePicker1.Value.ToString("dddd,dd MMMM,yy  hh:mm:ss:tt");
        }
    }
}
