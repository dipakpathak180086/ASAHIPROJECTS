using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AISCOMServer;
using System.IO;
//using UmsDLL;
using System.Windows.Forms;
namespace AISCOMServer.Classes
{
    class clsSecurity
    {
        StringBuilder _SbQry = null;
        clsMsgRule oRule = new clsMsgRule();

        #region App New Version Update

        internal string GetAppVersion()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetAppVersion] '" + EnumAppType.ANDROIDAPP + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["Version"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~App new version information not found,please check";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string GetNewExeDevice()
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\NewApp\\AndroidApp"))
                {
                    throw new Exception("Location not defined for new app");
                }
                string[] AllFiles = Directory.GetFiles(Application.StartupPath + "\\NewApp\\AndroidApp");
                string FileName = Path.GetFileName(AllFiles[0]);
                byte[] FileNewExe = File.ReadAllBytes(Application.StartupPath + "\\NewApp\\AndroidApp\\" + FileName);

                string exestring = Convert.ToBase64String(FileNewExe);
                oRule.sResponse = clsMsgRule.sValid + "~" + exestring + "~" + FileName;
            }
            catch (Exception ex) { throw ex; }
            return oRule.sResponse + "}";
        }
        internal string GetNewExeDesktop()
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\NewApp\\DesktopApp"))
                {
                    throw new Exception("Location not defined for new app");
                }
                string[] AllFiles = Directory.GetFiles(Application.StartupPath + "\\NewApp\\DesktopApp");
                string FileName = Path.GetFileName(AllFiles[0]);
                byte[] FileNewExe = File.ReadAllBytes(Application.StartupPath + "\\NewApp\\DesktopApp\\" + FileName);

                string exestring = Convert.ToBase64String(FileNewExe);
                oRule.sResponse = clsMsgRule.sValid + "~" + exestring + "~" + FileName;
            }
            catch (Exception ex) { throw ex; }
            return oRule.sResponse;
        }

        #endregion

        #region Login & Menu
        internal string ManageUser(User user)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_UserMaster '" + EnumDbType.VALIDATEUSER + "','" + user.UserId + "','" + user.Name + "'");
                _SbQry.AppendLine(",'" + user.Password + "','" + user.Group + "','" + user.CreatedBy + "','" + user.NewPassword + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["USERID"].ToString() + "~" + dt.Rows[0]["UserName"].ToString() + "~" + dt.Rows[0]["GroupName"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Wrong UserId / Password";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string GetUserRights(string UserGroup)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("SELECT ModuleId FROM TBL_GroupRight Where GroupName = '" + UserGroup + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                oRule.sResponse = clsMsgRule.sValid + "~";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        oRule.sResponse += row["ModuleId"].ToString() + "#";
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        #endregion



    }
}
