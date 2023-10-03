using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Doctor
    {
         private string strQry = string.Empty;

         public DataSet getDocSpec(string divcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;

             strQry = " SELECT Doc_Special_Code Doc_Cat_Code, Doc_Special_SName  Doc_Cat_Name,  Doc_Special_SName Doc_Cat_SName FROM  Mas_Doctor_Speciality " +
                      " WHERE Doc_Special_Active_Flag =0 AND Division_Code= '" + divcode + "' " +
                      " ORDER BY 2";
             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }

         public DataSet getDocClass(string divcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;

             strQry = " SELECT Doc_ClsCode Doc_Cat_Code, Doc_ClsSName Doc_Cat_Name,Doc_ClsSName Doc_Cat_SName FROM  Mas_Doc_Class " +
                      " WHERE Doc_Cls_ActiveFlag =0 AND Division_Code= '" + divcode + "' " +
                      " ORDER BY 2";
             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }

         public DataSet getDocQual(string divcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;

             strQry = " SELECT Doc_QuaCode Doc_Cat_Code, Doc_QuaName Doc_Cat_Name, Doc_QuaName Doc_Cat_SName FROM  Mas_Doc_Qualification " +
                      " WHERE Doc_Qua_ActiveFlag =0 AND Division_Code= '" + divcode + "' " +
                      " ORDER BY 2";
             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }
         public DataSet getDocCat_terr(string divcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;

             strQry = " SELECT Doc_Cat_Code,Doc_Cat_SName Doc_Cat_Name,No_of_visit FROM  Mas_Doctor_Category " +
                      " WHERE Doc_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                      " ORDER BY Doc_Cat_Sl_No";
             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }
         public DataSet getDocterr_type(string divcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;

             strQry = " SELECT Doc_ClsCode Doc_Cat_Code, Doc_ClsSName Doc_Cat_Name FROM  Mas_Doc_Class " +
                      " WHERE Doc_Cls_ActiveFlag =0 AND Division_Code= '" + divcode + "' " +
                      " ORDER BY 2";
             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }


        public DataSet getDocCat(string divcode )
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name, " +
                     " c.No_of_visit, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code and ListedDr_Active_Flag=0) as Cat_Count," +
                     "   (select count(d.Dis_Cat_Code) from Mas_stockist d where d.Dis_Cat_Code = c.Doc_Cat_Code and Stockist_Active_Flag=0) as dist_Count FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_Sl_No";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }


         public DataSet getDoctorCategory(string sf_code, string cat_code, string type)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;
             string swhere = string.Empty;

             if ((type == "0") && (cat_code != "-1"))
             {
                 swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
             }
             else if ((type == "1") && (cat_code != "-1"))
             {
                 swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
             }
             else if ((type == "2") && (cat_code != "-1"))
             {
                 swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
             }
             else if ((type == "3") && (cat_code != "-1"))
             {
                 swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
             }

             if (sf_code != "-1")
             {
                 swhere = swhere + "and a.Sf_Code = '" + sf_code + "' ";
             }

             //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
             //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB,ListedDr_DOW, " +
             //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
             //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
             //           " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
             //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
             //           " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
             //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
             //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
             //           " and a.Doc_ClsCode=b.Doc_ClsCode " +
             //           swhere +
             //           " order by ListedDr_Name ";

             strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                        " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code," +
                        " case isnull(ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                        " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                        " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, " +
                        " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                        " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                        " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                        " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                        " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                        " and a.Doc_ClsCode=b.Doc_ClsCode " +
                        swhere +
                        " order by ListedDr_Name ";
             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }

         public DataSet getDoctorMgr(string mgr_code, string type, string div_code)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;
             string swhere = string.Empty;

             if ((type == "0") && (mgr_code != "admin"))
             {
                 swhere = swhere + "and a.Sf_Code in " +
                                 "(select sf_code from Mas_Salesforce " +
                                 " where TP_Reporting_SF = '" + mgr_code + "' ) and a.ListedDr_Active_Flag = 0";
             }
             else if ((type == "0") && (mgr_code == "admin"))
             {
                 swhere = swhere + "and a.ListedDr_Active_Flag = 0 ";
             }
             else
             {
                 swhere = swhere + "and a.Sf_Code in " +
                                 "(select sf_code from Mas_Salesforce " +
                                 " where Sf_Code !='admin' and State_Code = '" + mgr_code + "'  and sf_type = 1)";
             }

             //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
             //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB, " +
             //           " ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
             //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
             //            " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
             //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
             //             " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
             //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
             //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
             //           " and  a.Doc_ClsCode=b.Doc_ClsCode " +
             //           swhere +
             //           " order by ListedDr_Name ";
             strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                        " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case isnull(ListedDr_DOB,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  ListedDr_DOB  end ListedDr_DOB," +
                        " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, " +
                        " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
                        " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                        " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                        " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                        " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                        " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                        " and  a.Doc_ClsCode=b.Doc_ClsCode and a.Division_Code='"+ div_code +"'  " +
                        swhere +
                        " order by ListedDr_Name ";

             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }



         public DataSet getDoctorCategory(string sf_code, string type)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;             

             if (type == "0")
             {
                 strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode) as ListedDrCode,b.Doc_Cat_SName,  a.Doc_Cat_Code" +
                            " from Mas_ListedDr a, Mas_Doctor_Category b " +
                            " where a.Sf_Code = '" + sf_code + "' and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                            " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ,b.Doc_Cat_SName";
             }
             else if (type == "1")
             {
                 strQry = " select b.Doc_Special_Name, count(a.ListedDrCode),b.Doc_Special_SName, a.Doc_Special_Code" +
                            " from Mas_ListedDr a, Mas_Doctor_Speciality b " +
                            " where a.Sf_Code = '" + sf_code + "' and a.Doc_Special_Code = b.Doc_Special_Code " +
                            " group by a.Doc_Special_Code  , b.Doc_Special_Name,b.Doc_Special_SName ";
             }
             else if (type == "2")
             {
                 strQry = " select b.Doc_ClsName, count(a.ListedDrCode),b.Doc_ClsSName, a.Doc_ClsCode" +
                            " from Mas_ListedDr a, Mas_Doc_Class b " +
                            " where a.Sf_Code = '" + sf_code + "' and a.Doc_ClsCode = b.Doc_ClsCode " +
                            " group by a.Doc_ClsCode  , b.Doc_ClsName,b.Doc_ClsSName ";
             }
             else if (type == "3")
             {
                 strQry = " select b.Doc_QuaName, count(a.ListedDrCode), b.Doc_QuaName, a.Doc_QuaCode" +
                            " from Mas_ListedDr a, Mas_Doc_Qualification b " +
                            " where a.Sf_Code = '" + sf_code + "' and a.Doc_QuaCode = b.Doc_QuaCode " +
                            " group by a.Doc_QuaCode  , b.Doc_QuaName ";
             }

             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }
        
        // Sorting For DoctorCategoryList 
         public DataTable getDoctorCategorylist_DataTable(string divcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataTable dtDocCat = null;

             strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name,c.No_of_visit, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +                     
                     "  FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_Sl_No";
             try
             {
                 dtDocCat = db_ER.Exec_DataTable(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dtDocCat;
         }
         public DataSet getDocCat(string divcode,string doccatcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsDocCat = null;

             strQry = " SELECT Doc_Cat_SName,Doc_Cat_Name FROM  Mas_Doctor_Category " +
                      " WHERE Doc_Cat_Code='"+ doccatcode +"' AND Division_Code= '" + divcode + "' " +
                      " ORDER BY 2";
             try
             {
                 dsDocCat = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsDocCat;
         }

         public bool RecordExist(string Doc_Cat_SName, string divcode)
         {

             bool bRecordExist = false;
             try
             {
                 DB_EReporting db = new DB_EReporting();

                 strQry = "SELECT COUNT(Doc_Cat_SName) FROM Mas_Doctor_Category WHERE Doc_Cat_SName='" + Doc_Cat_SName + "'AND Division_Code='" + divcode + "' ";
                 int iRecordExist = db.Exec_Scalar(strQry);

                 if (iRecordExist > 0)
                     bRecordExist = true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return bRecordExist;
         }

        
         public int Update_DocClassSno(string Doc_Cat_Code, string Sno)
         {
             int iReturn = -1;
             //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
             //{
             try
             {

                 DB_EReporting db = new DB_EReporting();

                 strQry = "UPDATE Mas_Doc_Class " +
                          " SET Doc_ClsSNo = '" + Sno + "', " +
                          " LastUpdt_Date = getdate() " +
                          " WHERE Doc_ClsCode = '" + Doc_Cat_Code + "' ";

                 iReturn = db.ExecQry(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return iReturn;
         }



         public int Update_DocCatSno(string Doc_Cat_Code, string Sno)
         {
             int iReturn = -1;
             //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
             //{
             try
             {

                 DB_EReporting db = new DB_EReporting();

                 strQry = "UPDATE Mas_Doctor_Category " +
                          " SET Doc_Cat_Sl_No = '" + Sno + "', " +
                          " LastUpdt_Date = getdate() " +
                          " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                 iReturn = db.ExecQry(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return iReturn;
         }

         ////public int Update_DocClassSno(string Doc_Cat_Code, string Sno)
         ////{
         ////    int iReturn = -1;
         ////    //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
         ////    //{
         ////    try
         ////    {

         ////        DB_EReporting db = new DB_EReporting();

         ////        strQry = "UPDATE Mas_Doctor_Category " +
         ////                 " SET Doc_Cat_Sl_No = '" + Sno + "', " +
         ////                 " LastUpdt_Date = getdate() " +
         ////                 " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

         ////        iReturn = db.ExecQry(strQry);
         ////    }
         ////    catch (Exception ex)
         ////    {
         ////        throw ex;
         ////    }
         ////    return iReturn;
         ////}


        public int Update_DocCampSno(string Doc_Cat_Code, string Sno)
         {
             int iReturn = -1;
             //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
             //{
             try
             {

                 DB_EReporting db = new DB_EReporting();

                 strQry = "UPDATE Mas_Doc_SubCategory " +
                          " SET Doc_SubCat_SlNo = '" + Sno + "', " +
                          " LastUpdt_Date = getdate() " +
                          " WHERE Doc_SubCatcode = '" + Doc_Cat_Code + "' ";

                 iReturn = db.ExecQry(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return iReturn;
         }



        public int Update_DocSpecSno(string Doc_Spec_Code, string Sno)
         {
             int iReturn = -1;
             //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
             //{
             try
             {

                 DB_EReporting db = new DB_EReporting();

                 strQry = "UPDATE Mas_Doctor_Speciality " +
                          " SET Doc_Spec_Sl_No = '" + Sno + "', " +
                          " LastUpdt_Date = getdate() " +
                          " WHERE Doc_Special_Code = '" + Doc_Spec_Code + "' ";

                 iReturn = db.ExecQry(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return iReturn;
         }

        public bool RecordExist(int Doc_Cat_Code, string Doc_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_SName) FROM Mas_Doctor_Category WHERE Doc_Cat_SName = '" + Doc_Cat_SName + "' AND Doc_Cat_Code!='" + Doc_Cat_Code + "' AND Division_Code='" + divcode + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }


         public int RecordAdd(string divcode,string Doc_Cat_SName,string Doc_Cat_Name)
        {
            int iReturn = -1;
          if (!RecordExist(Doc_Cat_SName,divcode))
            {
                if (!sRecordExist(Doc_Cat_Name,divcode))
                {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(Doc_Cat_Code)+1,'1') Doc_Cat_Code from Mas_Doctor_Category ";
                    int Doc_Cat_Code = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO Mas_Doctor_Category(Doc_Cat_Code,Division_Code,Doc_Cat_SName,Doc_Cat_Name,Doc_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                             "values('" + Doc_Cat_Code + "','" + divcode + "','" + Doc_Cat_SName + "', '" + Doc_Cat_Name + "',0,getdate(),getdate())";
                            
                                                    
                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                }
                else
                {
                    iReturn = -2;
                }
            }
          else
          {
              iReturn = -3;
          }
          return iReturn;
        }
     
         public int RecordDelete(int Doc_Cat_Code)
            {
                int iReturn = -1;
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "DELETE FROM  Mas_Doctor_Category WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return iReturn;

            }
            public int DeActivate(int Doc_Cat_Code)
            {
                int iReturn = -1;

                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Doctor_Category " +
                                " SET Doc_Cat_Active_Flag=1 ," +
                                " LastUpdt_Date = getdate() " +
                                " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }   


            public DataSet getDocSpe(string divcode)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsDocSpe = null;

                strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name, " +
                         " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code and ListedDr_Active_Flag=0) as Spec_Count" +
                         " FROM  Mas_Doctor_Speciality s " +
                         " WHERE s.Doc_Special_Active_Flag=0 AND s.Division_Code= '" + divcode + "' "+
                         " ORDER BY s.Doc_Spec_Sl_No ";
                try
                {
                    dsDocSpe = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsDocSpe;
            }
           // Sorting For DoctorSpecialityList 
            public DataTable getDocSpecialitylist_DataTable(string divcode)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataTable dtDocSpe = null;

                strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name,  " +
                        " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code) as Spec_Count" +
                        " FROM  Mas_Doctor_Speciality s " +
                        " WHERE s.Doc_Special_Active_Flag=0 AND s.Division_Code= '" + divcode + "' " +
                        " ORDER BY s.Doc_Spec_Sl_No ";
                try
                {
                    dtDocSpe = db_ER.Exec_DataTable(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dtDocSpe;
            }
              public DataSet getDocSpe(string divcode,string docsplcode)
              {
                  DB_EReporting db_ER = new DB_EReporting();

                  DataSet dsDocSpe = null;

                  strQry = " SELECT Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                           " WHERE Doc_Special_Code='"+docsplcode+"' AND Division_Code= '" + divcode + "' " +
                           " ORDER BY 2";
                  try
                  {
                      dsDocSpe = db_ER.Exec_DataSet(strQry);
                  }
                  catch (Exception ex)
                  {
                      throw ex;
                  }
                  return dsDocSpe;
              }
			  
		public DataSet getDocSpe(string divcode, string sf_type, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            if (sf_type == "4")
            {
                strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name, " +
                        " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code and ListedDr_Active_Flag=0 and charindex(','+'" + sf_code + "'+',',',' + Dist_Name +',')>0  ) as Spec_Count" +
                        " FROM  Mas_Doctor_Speciality s " +
                        " WHERE s.Doc_Special_Active_Flag=0 AND s.Division_Code= '" + divcode + "' " +
                        " ORDER BY s.Doc_Spec_Sl_No ";
            }
            else
            {
                strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name, " +
                        " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code and ListedDr_Active_Flag=0) as Spec_Count" +
                        " FROM  Mas_Doctor_Speciality s " +
                        " WHERE s.Doc_Special_Active_Flag=0 AND s.Division_Code= '" + divcode + "' " +
                        " ORDER BY s.Doc_Spec_Sl_No ";
            }



            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }
			  
			  
              public bool RecordExistDocSpl(string Doc_Special_SName, string divcode)
              {

                  bool bRecordExist = false;
                  try
                  {
                      DB_EReporting db = new DB_EReporting();

                      strQry = "SELECT COUNT(Doc_Special_SName) FROM Mas_Doctor_Speciality WHERE Doc_Special_SName='" + Doc_Special_SName + "'AND Division_Code='" + divcode + "' ";
                      int iRecordExist = db.Exec_Scalar(strQry);

                      if (iRecordExist > 0)
                          bRecordExist = true;
                  }
                  catch (Exception ex)
                  {
                      throw ex;
                  }
                  return bRecordExist;
              }

              public bool RecordExistDocSpl(int Doc_Special_Code, string Doc_Special_SName, string divcode)
              {

                  bool bRecordExist = false;
                  try
                  {
                      DB_EReporting db = new DB_EReporting();

                      strQry = "SELECT COUNT(Doc_Special_SName) FROM Mas_Doctor_Speciality WHERE Doc_Special_SName = '" + Doc_Special_SName + "' AND Doc_Special_Code!='" + Doc_Special_Code + "'AND Division_Code='" + divcode + "'";

                      int iRecordExist = db.Exec_Scalar(strQry);

                      if (iRecordExist > 0)
                          bRecordExist = true;
                  }
                  catch (Exception ex)
                  {
                      throw ex;
                  }
                  return bRecordExist;
              }

          public int RecordAddDocSpl(string divcode, string Doc_Special_SName, string Doc_Special_Name)
            {
                int iReturn = -1;
                if (!RecordExistDocSpl(Doc_Special_SName,divcode))
                {
                    if (!sRecordExistDocSpl(Doc_Special_Name, divcode))
                    {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Doc_Special_Code)+1,'1') Doc_Special_Code from Mas_Doctor_Speciality ";
                        int Doc_Special_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Doctor_Speciality(Doc_Special_Code,Division_Code,Doc_Special_SName,Doc_Special_Name,Doc_Special_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Doc_Special_Code + "','" + divcode + "','" + Doc_Special_SName + "', '" + Doc_Special_Name + "',0,getdate(),getdate())";
                            
                                                    
                        iReturn = db.ExecQry(strQry);
                    }                    
                 catch (Exception ex)
                    {
                        throw ex;
                    }
                    }
                    else
                    {
                        iReturn = -2;
                    }
                }
                else
                {
                    iReturn = -3;
                }
                return iReturn;
            }
          public int RecordUpdateDocSpl(int Doc_Special_Code, string Doc_Special_SName, string Doc_Special_Name, string divcode)
          {
              int iReturn = -1;
              if (!RecordExistDocSpl(Doc_Special_Code, Doc_Special_SName, divcode))
              {
                  if (!sRecordExistDocSpl(Doc_Special_Code, Doc_Special_Name, divcode))
                  {
                      try
                      {

                          DB_EReporting db = new DB_EReporting();


                          strQry = "UPDATE Mas_ListedDr " +
                                 "SET Doc_Spec_ShortName='" + Doc_Special_SName + "' " +
                                 "WHERE Doc_Special_Code= '" + Doc_Special_Code + "' AND Division_Code='" + divcode + "'";

                          iReturn = db.ExecQry(strQry);

                          strQry = "UPDATE Mas_Doctor_Speciality " +
                                   " SET Doc_Special_SName = '" + Doc_Special_SName + "', " +
                                   " Doc_Special_Name = '" + Doc_Special_Name + "', " +
                                   " LastUpdt_Date = getdate() " +
                                   " WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

                          iReturn = db.ExecQry(strQry);
                      }
                      catch (Exception ex)
                      {
                          throw ex;
                      }
                  }
                  else
                  {
                      iReturn = -2;
                  }
              }
              else
              {
                  iReturn = -3;
              }
              return iReturn;

          }

             public int RecordDeleteDocSpl(int Doc_Special_Code)
                {
                    int iReturn = -1;
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "DELETE FROM  Mas_Doctor_Speciality WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return iReturn;

                }
             public int DeActivateDocSpl(int Doc_Special_Code)
                {
                    int iReturn = -1;

                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Doctor_Speciality " +
                                    " SET Doc_Special_Active_Flag=1 ," +
                                    " LastUpdt_Date = getdate() " +
                                    " WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return iReturn;

                }
             public DataSet getDocSubCat( string divcode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataSet dsDocSpe = null;

                 strQry = " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                          " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                          " ORDER BY Doc_SubCat_SlNo";


                 //strQry = " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                 //         " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                 //         " and (Effective_From >= Convert(Date, GetDate(), 101) or Effective_To >= Convert(Date, GetDate(), 101))" +
                 //         " ORDER BY Doc_SubCat_SlNo";
                 try
                 {
                     dsDocSpe = db_ER.Exec_DataSet(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dsDocSpe;
             }
        // Sorting For DoctorCampaignList 
             public DataTable getDocSubCatlist_DataTable(string divcode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataTable dtDocSpe = null;

                 strQry = " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                          " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                          " ORDER BY Doc_SubCat_SlNo";
                 try
                 {
                     dtDocSpe = db_ER.Exec_DataTable(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dtDocSpe;
             }
             public DataSet getDocSubCat(string divcode,string docsubcatcode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataSet dsDocSpe = null;

                 strQry = " SELECT Doc_SubCatSName,Doc_SubCatName,convert(varchar(10),Effective_From,103) Effective_From,convert(varchar(10),Effective_To,103)Effective_To FROM  Mas_Doc_SubCategory " +
                          " WHERE Doc_SubCatCode='"+docsubcatcode+"' AND Division_Code=  '" + divcode + "' " +
                          " ORDER BY 2";
                 try
                 {
                     dsDocSpe = db_ER.Exec_DataSet(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dsDocSpe;
             }
             public bool RecordExistSubCat(string Doc_SubCatSName)
             {

                 bool bRecordExist = false;
                 try
                 {
                     DB_EReporting db = new DB_EReporting();

                     strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName='" + Doc_SubCatSName + "' ";
                     int iRecordExist = db.Exec_Scalar(strQry);

                     if (iRecordExist > 0)
                         bRecordExist = true;
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return bRecordExist;
             }

             public bool RecordExistSubCat(int Doc_SubCatCode, string Doc_SubCatSName)
             {

                 bool bRecordExist = false;
                 try
                 {
                     DB_EReporting db = new DB_EReporting();

                     strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName= '" + Doc_SubCatSName + "' AND Doc_SubCatCode!='" + Doc_SubCatCode + "' ";

                     int iRecordExist = db.Exec_Scalar(strQry);

                     if (iRecordExist > 0)
                         bRecordExist = true;
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return bRecordExist;
             }

             //public int RecordAddSubCat(string divcode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To)
             //{
             //    int iReturn = -1;
             //    if (!RecordExistSubCat(Doc_SubCatSName))
             //    {
             //        try
             //        {
             //            DB_EReporting db = new DB_EReporting();
             //            string EffFromdate = Effective_From.Month.ToString() + "-" + Effective_From.Day + "-" + Effective_From.Year;
             //            string EffTodate = Effective_To.Month.ToString() + "-" + Effective_To.Day + "-" + Effective_To.Year;

             //            strQry = "INSERT INTO Mas_Doc_SubCategory(Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To)" +
             //                     "values('" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "')";


             //            iReturn = db.ExecQry(strQry);
             //        }
             //        catch (Exception ex)
             //        {
             //            throw ex;
             //        }
             //    }
             //    else
             //    {
             //        iReturn = -2;
             //    }
             //    return iReturn;
             //}
             public int RecordUpdateSubCat(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName)
             {
                 int iReturn = -1;
                 if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName))
                 {
                     try
                     {

                         DB_EReporting db = new DB_EReporting();

                         strQry = "UPDATE Mas_Doc_SubCategory " +
                                  " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                  " Doc_SubCatName = '" + Doc_SubCatName + "'," +                                 
                                  
                                      " LastUpdt_Date = getdate() " +
                                  " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                         iReturn = db.ExecQry(strQry);
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }
                 }
                 else
                 {
                     iReturn = -2;
                 }
                 return iReturn;

             }
             public int RecordUpdateSubCatnew(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To)
             {
                 int iReturn = -1;
                 if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName))
                 {
                     try
                     {

                         DB_EReporting db = new DB_EReporting();

                         strQry = "UPDATE Mas_Doc_SubCategory " +
                                  " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                  " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                      " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                     " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                      " LastUpdt_Date = getdate() " +
                                  " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                         iReturn = db.ExecQry(strQry);
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }
                 }
                 else
                 {
                     iReturn = -2;
                 }
                 return iReturn;

             }

             public int RecordDeleteSubCat(int Doc_SubCatCode)
             {
                 int iReturn = -1;
                 try
                 {

                     DB_EReporting db = new DB_EReporting();

                     strQry = "DELETE FROM  Mas_Doc_SubCategory WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                     iReturn = db.ExecQry(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return iReturn;

             }
             public int DeActivateSubCat(int Doc_SubCatCode)
             {
                 int iReturn = -1;

                 try
                 {

                     DB_EReporting db = new DB_EReporting();

                     strQry = "UPDATE Mas_Doc_SubCategory " +
                                 " SET Doc_SubCat_ActiveFlag    =1 ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                     iReturn = db.ExecQry(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }

                 return iReturn;

             }
             public DataSet getDocCls(string divcode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataSet dsDocCls = null;

                 strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName,c.Doc_ClsName, " +
                          " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode and ListedDr_Active_Flag=0) as Cls_Count " +
                          "  FROM  Mas_Doc_Class  c" +
                          " WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code=  '" + divcode + "' " +
                          " ORDER BY c.Doc_ClsSNo";
                 try
                 {
                     dsDocCls = db_ER.Exec_DataSet(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dsDocCls;
             }
        // Sorting For DoctorClassList 
             public DataTable getDocClslist_DataTable(string divcode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataTable dtDocCls = null;
                 strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName,c.Doc_ClsName, " +
                                         " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode) as Cls_Count " +
                                         "  FROM  Mas_Doc_Class  c" +
                                         " WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code=  '" + divcode + "' " +
                                         " ORDER BY c.Doc_ClsSNo";
                 try
                 {
                     dtDocCls = db_ER.Exec_DataTable(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dtDocCls;
             }


            public int getDoctorcount(string sf_code, string cat_code)
            {

                int iReturn = -1;
                
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";

                    iReturn = db.Exec_Scalar(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }

          


            public int getSpecialcount(string sf_code, string spec_code)
            {

                int iReturn = -1;

                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";

                    iReturn = db.Exec_Scalar(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }

            public int getClasscount(string sf_code, string Doc_ClsCode)
            {

                int iReturn = -1;

                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";

                    iReturn = db.Exec_Scalar(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }

            public int getQualcount(string sf_code, string qual_code)
            {

                int iReturn = -1;

                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";

                    iReturn = db.Exec_Scalar(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }

            public DataSet getDocCls(string divcode,string DocClsCode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataSet dsDocCls = null;

                 strQry = " SELECT Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                          " WHERE Doc_ClsCode= '"+DocClsCode+"' AND Division_Code=  '" + divcode + "' " +
                          " ORDER BY 2";
                 try
                 {
                     dsDocCls = db_ER.Exec_DataSet(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dsDocCls;
             }
            public bool RecordExistCls(string Doc_ClsSName, string divcode)
            {

                bool bRecordExist = false;
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(Doc_ClsSName) FROM Mas_Doc_Class WHERE Doc_ClsSName='" + Doc_ClsSName + "'AND Division_Code='" + divcode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bRecordExist;
            }


             public bool RecordExistCls(int Doc_ClsCode, string Doc_ClsSName, string divcode)
             {

                 bool bRecordExist = false;
                 try
                 {
                     DB_EReporting db = new DB_EReporting();

                     strQry = "SELECT COUNT(Doc_ClsSName) FROM Mas_Doc_Class WHERE Doc_ClsSName= '" + Doc_ClsSName + "' AND Doc_ClsCode!='" + Doc_ClsCode + "'AND Division_Code='" + divcode + "' ";

                     int iRecordExist = db.Exec_Scalar(strQry);

                     if (iRecordExist > 0)
                         bRecordExist = true;
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return bRecordExist;
             }

             public int RecordAddCls(string divcode, string Doc_ClsSName, string Doc_ClsName)
             {
                 int iReturn = -1;
                  if (!RecordExistCls(Doc_ClsSName,divcode))
                 {
                     if (!sRecordExistCls(Doc_ClsName,divcode))
                     {
                     try
                     {
                         DB_EReporting db = new DB_EReporting();
                         strQry = "SELECT isnull(max(Doc_ClsCode)+1,'1') Doc_ClsCode from Mas_Doc_Class ";
                         int Doc_ClsCode = db.Exec_Scalar(strQry);

                         strQry = "INSERT INTO Mas_Doc_Class(Doc_ClsCode,Division_Code,Doc_ClsSName,Doc_ClsName,Doc_Cls_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                  "values('" + Doc_ClsCode + "','" + divcode + "','" + Doc_ClsSName + "', '" + Doc_ClsName + "',0,getdate(),getdate())";


                         iReturn = db.ExecQry(strQry);
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }
                     }
                     else
                     {
                         iReturn = -2;
                     }
                 }
                  else
                  {
                      iReturn = -3;
                  }
                  return iReturn;
             }
             public int RecordUpdateCls(int Doc_ClsCode, string Doc_ClsSName, string Doc_ClsName, string divcode)
             {
                 int iReturn = -1;
                  if (!RecordExistCls(Doc_ClsCode, Doc_ClsSName,divcode))
                 {
                     if (!sRecordExistCls(Doc_ClsCode, Doc_ClsName,divcode))
                     {
                     try
                     {

                         DB_EReporting db = new DB_EReporting();

                         strQry = "UPDATE Mas_ListedDr " +
                                "SET Doc_Class_ShortName='" + Doc_ClsSName + "' " +
                                "WHERE Doc_ClsCode='" + Doc_ClsCode + "' AND Division_Code='" + divcode + "'";

                         iReturn = db.ExecQry(strQry);      

                         strQry = "UPDATE Mas_Doc_Class " +
                                  " SET Doc_ClsSName = '" + Doc_ClsSName + "', " +
                                  " Doc_ClsName = '" + Doc_ClsName + "'," +
                                  " LastUpdt_Date = getdate() " +
                                  " WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

                         iReturn = db.ExecQry(strQry);
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }
                     }
                     else
                     {
                         iReturn = -2;
                     }
                 }
                  else
                  {
                      iReturn = -3;
                  }
                  return iReturn;

             }

             public int RecordDeleteCls(int Doc_ClsCode)
             {
                 int iReturn = -1;
                 try
                 {

                     DB_EReporting db = new DB_EReporting();

                     strQry = "DELETE FROM  Mas_Doc_Class WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

                     iReturn = db.ExecQry(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return iReturn;

             }
             public int DeActivateCls(int Doc_ClsCode)
             {
                 int iReturn = -1;

                 try
                 {

                     DB_EReporting db = new DB_EReporting();

                     strQry = "UPDATE Mas_Doc_Class " +
                                 " SET Doc_Cls_ActiveFlag    =1 ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

                     iReturn = db.ExecQry(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }

                 return iReturn;

             }
             public DataSet getDocQua(string divcode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataSet dsDocQua = null;

                 strQry = " SELECT q.Doc_QuaCode,q.Doc_QuaSName,q.Doc_QuaName, " +
                          " (select count(d.Doc_QuaCode) from Mas_ListedDr d where d.Doc_QuaCode = q.Doc_QuaCode and ListedDr_Active_Flag=0) as Qua_Count " +  
                          "  FROM Mas_Doc_Qualification q " +                          
                          " WHERE q.Doc_Qua_ActiveFlag=0 ANd q.Division_Code= '" + divcode + "' " +
                          " ORDER BY q.DocQuaSNo";
                 try
                 {
                     dsDocQua = db_ER.Exec_DataSet(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dsDocQua;
             }
        // Sorting For DoctorQualificationList 
             public DataTable getDocQualist_DataTable(string divcode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataTable dtDocQua = null;

                 strQry = " SELECT q.Doc_QuaCode,q.Doc_QuaSName,q.Doc_QuaName, " +
                         " (select count(d.Doc_QuaCode) from Mas_ListedDr d where d.Doc_QuaCode = q.Doc_QuaCode) as Qua_Count " +
                         "  FROM Mas_Doc_Qualification q " +
                         " WHERE q.Doc_Qua_ActiveFlag=0 ANd q.Division_Code= '" + divcode + "' " +
                         " ORDER BY q.DocQuaSNo";
                 try
                 {
                     dtDocQua = db_ER.Exec_DataTable(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dtDocQua;
             }
             public DataSet getDocQua(string divcode, string DocQuaCode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataSet dsDocQua = null;

                 strQry = "SELECT Doc_QuaSName,Doc_QuaName FROM Mas_Doc_Qualification " +
                          " WHERE Doc_QuaCode= '" + DocQuaCode + "' AND Division_Code= '" + divcode + "' " +
                          " ORDER BY 2";
                 try
                 {
                     dsDocQua = db_ER.Exec_DataSet(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return dsDocQua;
             }
             public bool RecordExistQua(string Doc_QuaName, string divcode)
             {
                 bool bRecordExist = false;
                 try
                 {
                     DB_EReporting db = new DB_EReporting();

                     strQry = "SELECT COUNT(Doc_QuaName) FROM Mas_Doc_Qualification WHERE Doc_QuaName='" + Doc_QuaName + "'AND Division_Code='" + divcode + "' ";
                     int iRecordExist = db.Exec_Scalar(strQry);

                     if (iRecordExist > 0)
                         bRecordExist = true;
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return bRecordExist;
             }
             public bool RecordExistQua(int Doc_QuaCode, string Doc_QuaName, string divcode)
             {

                 bool bRecordExist = false;
                 try
                 {
                     DB_EReporting db = new DB_EReporting();

                     strQry = "SELECT COUNT(Doc_QuaName) FROM Mas_Doc_Qualification WHERE Doc_QuaName='" + Doc_QuaName + "'AND Doc_QuaCode!='" + Doc_QuaCode + "'AND Division_Code='" + divcode + "' ";

                     int iRecordExist = db.Exec_Scalar(strQry);

                     if (iRecordExist > 0)
                         bRecordExist = true;
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return bRecordExist;
             }
             public int RecordAddQua(string divcode, string Doc_QuaSName, string Doc_QuaName)
             {
                 int iReturn = -1;
                if (!RecordExistQua(Doc_QuaName,divcode))
                 {
                     try
                     {
                         DB_EReporting db = new DB_EReporting();
                         strQry = "SELECT isnull(max(Doc_QuaCode)+1,'1') Doc_QuaCode from Mas_Doc_Qualification ";
                         int Doc_QuaCode = db.Exec_Scalar(strQry);

                         strQry = "INSERT INTO Mas_Doc_Qualification(Doc_QuaCode,Division_Code,Doc_QuaSName,Doc_QuaName,Doc_Qua_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                  "values('"+Doc_QuaCode+"','" + divcode + "','" + Doc_QuaSName + "', '" + Doc_QuaName + "' ,0,getdate(),getdate())";

                         iReturn = db.ExecQry(strQry);
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }
                 }
                else
                {
                    iReturn = -2;
                }
                
                 return iReturn;
             }
             public int Update_DocQualificationSno(string Doc_Qua_Code, string Sno)
             {
                 int iReturn = -1;
                 try
                 {

                     DB_EReporting db = new DB_EReporting();

                     strQry = "UPDATE Mas_Doc_Qualification " +
                              " SET DocQuaSNo = '" + Sno + "', " +
                              " LastUpdt_Date = getdate() " +
                              " WHERE Doc_QuaCode = '" + Doc_Qua_Code + "' ";

                     iReturn = db.ExecQry(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return iReturn;
             }
             public int RecordUpdateQua(int Doc_QuaCode, string Doc_QuaSName, string Doc_QuaName, string divcode)
             {
                 int iReturn = -1;
                   if (!RecordExistQua(Doc_QuaCode, Doc_QuaName, divcode))
                 {
                     try
                     {
                         DB_EReporting db = new DB_EReporting();

                         strQry = "UPDATE Mas_ListedDr " +
                                "SET Doc_Qua_Name='" + Doc_QuaName + "'" +
                                "Where Doc_QuaCode='" + Doc_QuaCode + "' AND Division_Code='" + divcode + "'";

                         iReturn = db.ExecQry(strQry);

                         strQry = "UPDATE Mas_Doc_Qualification " +
                                  " SET Doc_QuaSName = '" + Doc_QuaSName + "', " +
                                  " Doc_QuaName = '" + Doc_QuaName + "'," +
                                  " LastUpdt_Date = getdate() " +
                                  " WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";
                         iReturn = db.ExecQry(strQry);
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }
                 }
                   else
                   {
                       iReturn = -2;
                   }
                 return iReturn;
             }
             public int RecordDeleteQua(int Doc_QuaCode)
             {
                 int iReturn = -1;
                 try
                 {

                     DB_EReporting db = new DB_EReporting();

                     strQry = "DELETE FROM Mas_Doc_Qualification WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

                     iReturn = db.ExecQry(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return iReturn;
             }
             public int DeActivateQua(int Doc_QuaCode)
             {
                 int iReturn = -1;

                 try
                 {

                     DB_EReporting db = new DB_EReporting();

                     strQry = "UPDATE Mas_Doc_Qualification " +
                                 " SET Doc_Qua_ActiveFlag  =1 ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

                     iReturn = db.ExecQry(strQry);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 return iReturn;
             }

        
        //Changes done by Priya -14jul // 
        // Begin 

        public DataSet getDocCat_trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "SELECT 0 as Doc_Cat_Code,'--Select--' as Doc_Cat_SName,'' as Doc_Cat_Name " +
                     " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName, Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocSpe_Trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = "SELECT 0 as Doc_Special_Code,'--Select--' as Doc_Special_SName,'' as Doc_Special_Name " +
                 " UNION " +
                " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2 ";
            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocCls_Trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_ClsCode,'--Select--' as Doc_ClsSName,'' as Doc_ClsName " +
                     " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }

       
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocQua_Trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;

            strQry = "SELECT 0 as Doc_QuaCode,'--Select--' as Doc_QuaName " +
                     " UNION " +
                     " SELECT Doc_QuaCode,Doc_QuaName FROM Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag=0 ANd Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }

        //end

        //Changes done by Priya
        //begin
        //Jul 16
        public DataSet getDocSpe_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Doc_Spec_Sl_No ";
            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }
        //end

        //Changes done by Priya
        //begin
        public int RectivateDocSpl(int Doc_Special_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Speciality " +
                            " SET Doc_Special_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocCat_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Doc_Cat_Sl_No";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        //end

        //Changes done by Priya
        //begin
        public int ReActivate(int Doc_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Category " +
                            " SET Doc_Cat_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocCls_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=1 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY Doc_ClsSNo";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        //end

        //Changes done by Priya
        //begin
        public int ReActivateCls(int Doc_ClsCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Class " +
                            " SET Doc_Cls_ActiveFlag =0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocQua_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;

            strQry = " SELECT Doc_QuaCode,Doc_QuaSName,Doc_QuaName FROM Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag=1 ANd Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }
        //end

        //Changes done by Priya
        //begin
        public int ReActivateQua(int Doc_QuaCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Qualification " +
                            " SET Doc_Qua_ActiveFlag  =0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        //Changes Done by Sridevi - Starts
        public DataSet Missed_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC sp_DCR_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_SDP_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet Missed_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC sp_DCR_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_SDP_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }
            else if (sMode == "3")
            {
                strQry = " EXEC sp_DCR_MissedDR_Type '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "','" + vMode + "'  "; 
            }
            else if (sMode == "4")
            {
                strQry = " EXEC sp_DCR_MissedDR_Catg '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "','" + vMode + "'  ";
            }
            else if (sMode == "5")
            {
                strQry = " EXEC sp_DCR_MissedDR_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "6")
            {
                strQry = " EXEC sp_DCR_MissedDR_Class '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "' ,'" + vMode + "' ";
            }
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        //Changes Done by Sridevi - Ends
        public int RecordUpdate(int Doc_Cat_Code, string Doc_Cat_SName, string Doc_Cat_Name, string no_of_visit, string divcode)
        {
            int iReturn = -1;
            if (!RecordExist(Doc_Cat_Code, Doc_Cat_SName, divcode))
            {
                if (!sRecordExist(Doc_Cat_Code, Doc_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_ListedDr " +
                              "SET Doc_Cat_ShortName = '" + Doc_Cat_SName + "'" +
                              "WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' AND Division_Code='" + divcode + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Doctor_Category " +
                                 " SET Doc_Cat_SName = '" + Doc_Cat_SName + "', " +
                                 " Doc_Cat_Name = '" + Doc_Cat_Name + "' ," +
                                 " No_of_visit = '" + no_of_visit + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        //Changes done by Priya
        public DataSet getDocCls_Transfer(string divcode, string Doc_ClsName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_ClsCode,'--Select--' as Doc_ClsName,'' as Doc_ClsSName " +
                     " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=0 AND Division_Code=  '" + divcode + "' and Doc_ClsSName!='" + Doc_ClsName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public DataSet getDocCat_Transfer(string divcode, string Doc_Cat_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_Cat_Code,'--Select--' as Doc_Cat_SName,'' as Doc_Cat_Name " +
                     " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Active_Flag=0 AND Division_Code=  '" + divcode + "' and Doc_Cat_SName!='" + Doc_Cat_SName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public DataSet getDocCat_count(string Doc_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Cat_Code) as Doc_Cat_Code from Mas_ListedDr  where Doc_Cat_Code=" + Doc_Cat_Code + " and ListedDr_Active_Flag =0";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public DataSet getUnDoc_Count(string Doc_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Cat_Code) as Doc_Cat_Code from Mas_UnListedDr  where Doc_Cat_Code=" + Doc_Cat_Code + " and UnListedDr_Active_Flag =0";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        //public int GetDocCat_Count(int Doc_Cat_Code)
        //{
        //    int iReturn = -1;

        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "select COUNT(Doc_Cat_Code) as Doc_Cat_Code from Mas_ListedDr  where Doc_Cat_Code=" + Doc_Cat_Code + "";
        //                    //" WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}

        public int getDoctorMRcount(string Territory_Code, string cat_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr a,Mas_Territory_Creation b " +
                         " where a.Territory_Code=cast(b.Territory_Code as varchar ) and a.ListedDr_Active_Flag=0 and a.Territory_Code in('" + Territory_Code + "') " +
                         " and Doc_Cat_Code = '" + cat_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //Changes done by Priya

      

        public DataSet getDoctorMgr_list(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
           

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name  " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = cast(f.Territory_Code as varchar)  and a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' and f.Territory_Active_Flag=0 " +
                       
                       " order by ListedDr_Name ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getDoctorCategory_list(string sf_code, string cat_code, string terr_code, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;
            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
            }
                      
         
            if (terr_code != "-1")
            {
                swhere = swhere + " and a.Territory_Code = '" + terr_code + "' ";
            }
            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,a.ListedDr_DOB,a.ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name  " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = cast(f.Territory_Code as varchar)  and a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code='" + sf_code + "' and f.Territory_Active_Flag=0  " +
                       swhere +
                       " order by ListedDr_Sl_No desc  ";

            //strQry = " SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName,d.Doc_Qua_Name as Doc_QuaName,d.SDP as Activity_Date, " +
            //    "  ListedDr_Address3, ListedDr_PinCode, d.Doc_Special_Code,d.Doc_Cat_Code,d.Doc_ClsCode,d.Territory_Code,d.ListedDr_DOB,d.ListedDr_DOW," +
            //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and Territory_Active_Flag=0 and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name   FROM  " +
            //        " Mas_ListedDr d  WHERE d.Sf_Code =  '" + sf_code + "' and d.ListedDr_Active_Flag = 0 " +
            //        swhere +
            //        " order by ListedDr_Sl_No desc ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getDoctorCategory_list(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

       
                strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode),  a.Doc_Cat_Code" +
                           " from Mas_ListedDr a, Mas_Doctor_Category b " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                           " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ";
         

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        
        //Change done by Priya
          
        public DataSet getUnListDoctorMgr_list(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select UnListedDrCode, UnListedDr_Name, a.Doc_QuaCode,UnListedDR_Address1,UnListedDR_Address2, " +
                       " UnListedDr_Address3, UnListedDR_Phone, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,UnListedDR_DOB,UnListedDr_DOW, " +
                       " UnListedDr_Mobile,UnListedDR_EMail, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name  " +
                       " from Mas_UnListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = cast(f.Territory_Code as varchar)  and a.Doc_ClsCode=b.Doc_ClsCode  and a.UnListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' and f.Territory_Active_Flag=0 " +

                       " order by UnListedDr_Name ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getUnlistDoctorCategory_list(string sf_code, string cat_code, string terr_code, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
            }


            if (terr_code != "-1")
            {
                swhere = swhere + " and a.Territory_Code = '" + terr_code + "' ";
            }
            strQry = " select UnListedDrCode, UnListedDr_Name, a.Doc_QuaCode,UnListedDR_Address1,UnListedDR_Address2, " +
                       " UnListedDR_Address3, UnListedDR_Phone, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,UnListedDR_DOB,UnListedDr_DOW, " +
                       " UnListedDr_Mobile,UnListedDR_EMail, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name  " +
                       " from Mas_UnListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = cast(f.Territory_Code as varchar)  and a.Doc_ClsCode=b.Doc_ClsCode and a.UnListedDr_Active_Flag=0 and a.sf_code='" + sf_code + "' and f.Territory_Active_Flag=0  " +
                       swhere +
                       " order by UnListedDr_Name ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getUnlistDoctorCategory_list(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select b.Doc_Cat_Name, count(a.UnListedDrCode),  a.Doc_Cat_Code" +
                       " from Mas_UnListedDr a, Mas_Doctor_Category b " +
                       " where a.Sf_Code = '" + sf_code + "' and a.UnListedDr_Active_Flag=0 and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                       " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ";


            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public int getUnlistDoctorMRcount(string Territory_Code, string cat_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr a,Mas_Territory_Creation b " +
                         " where a.Territory_Code=cast(b.Territory_Code as varchar ) and a.UnListedDr_Active_Flag=0 and a.Territory_Code in('" + Territory_Code + "') " +
                         " and Doc_Cat_Code = '" + cat_code + "' and sf_code = '"+sf_code+"' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getTerr_Cat_Count(string Territory_Code, string cat_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                         " where ListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                         " and Doc_Cat_Code = '" + cat_code + "' and sf_code='" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getTerr_Spec_Count(string Territory_Code, string spec_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                                      " where ListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_Special_Code = '" + spec_code + "' and sf_code = '" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getTerr_Cls_Count(string Territory_Code, string cls_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                                      " where ListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_ClsCode = '" + cls_code + "' and sf_code = '" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getTerr_Qua_Count(string Territory_Code, string Qua_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                                      " where ListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_QuaCode = '" + Qua_code + "' and sf_code = '" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Cat_Count(string Territory_Code, string cat_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                         " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                         " and Doc_Cat_Code = '" + cat_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Spec_Count(string Territory_Code, string spec_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                                      " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_Special_Code = '" + spec_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Cls_Count(string Territory_Code, string cls_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                                      " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_ClsCode = '" + cls_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Qua_Count(string Territory_Code, string Qua_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                                      " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_QuaCode = '" + Qua_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getCatgName(string catcode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Division_Code= '" + divcode + "'  and Doc_Cat_Code= '" + catcode + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getSpecName(string specode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Division_Code= '" + divcode + "'  and Doc_Special_Code= '" + specode + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getClassName(string clscode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Division_Code= '" + divcode + "'  and Doc_ClsCode = '" + clscode + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet Checkdel(string Doc_Cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDocCat = null;

            strQry = "Update Mas_Doctor_Category set Doc_Cat_Active_Flag = 1 where Doc_Cat_code ='" + Doc_Cat_code + "'";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public int Update_DocCat_Drs(string Doc_Cat_from, string Doc_cat_to,string Doc_Cat_FSName, string Doc_Cat_TSName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_Cat_Code = '" + Doc_cat_to + "', Doc_Cat_ShortName = '" + Doc_Cat_TSName + "',Transfered_Date = getdate(), " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Cat_Code = '" + Doc_Cat_from + "' and Doc_Cat_ShortName ='" + Doc_Cat_FSName + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                         " SET Doc_Cat_Code = '" + Doc_cat_to + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Cat_Code = '" + Doc_Cat_from + "' and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doctor_Category " +
                        " SET Doc_Cat_Active_Flag = '" + chkdel + "' " +
                        " WHERE Doc_Cat_Code = '" + Doc_Cat_from + "' and Doc_Cat_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Speciality Changes 
        public DataSet getSpec_to(string divcode, string Doc_Special_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_Special_Code,'--Select--' as Doc_Special_SName,'' as Doc_Special_Name " +
                     " UNION " +
                     " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Active_Flag=0 AND Division_Code=  '" + divcode + "' and Doc_Special_SName!='" + Doc_Special_SName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }

        public DataSet getlistSpec_count(string Doc_Special_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Special_Code) as Doc_Special_Code from Mas_ListedDr  where Doc_Special_Code=" + Doc_Special_Code + " and ListedDr_Active_Flag =0";

            
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public DataSet getUnlistSpec_count(string Doc_Special_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Special_Code) as Doc_Special_Code from Mas_UnListedDr  where Doc_Special_Code=" + Doc_Special_Code + " and UnListedDr_Active_Flag =0";


            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public int Update_DocSpec_Drs(string Doc_Spec_from, string Doc_Spec_to, string Spec_Fr_SName, string Spec_To_SName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_Special_Code = '" + Doc_Spec_to + "', Doc_Spec_ShortName = '" + Spec_To_SName + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Special_Code = '" + Doc_Spec_from + "' and Doc_Spec_ShortName = '" + Spec_Fr_SName + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                         " SET Doc_Special_Code = '" + Doc_Spec_to + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Special_Code = '" + Doc_Spec_from + "' and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doctor_Speciality " +
                        " SET Doc_Special_Active_Flag = '" + chkdel + "' " +
                        " WHERE Doc_Special_Code = '" + Doc_Spec_from + "' and Doc_Special_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Class Changes 
        public DataSet getCls_to(string divcode, string Doc_ClsSName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_ClsCode,'--Select--' as Doc_ClsSName,'' as Doc_ClsName " +
                     " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=0 AND Division_Code=  '" + divcode + "' and Doc_ClsSName!='" + Doc_ClsSName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }

        public DataSet getlistCls_count(string Doc_Class_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_ClsCode) as Doc_ClsCode from Mas_ListedDr  where Doc_ClsCode=" + Doc_Class_Code + " and ListedDr_Active_Flag =0";


            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public DataSet getUnlistCls_count(string Doc_Class_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_ClsCode) as Doc_ClsCode from Mas_UnListedDr  where Doc_ClsCode=" + Doc_Class_Code + " and UnListedDr_Active_Flag =0";


            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public int Update_DocClass_Drs(string Doc_Cls_from, string Doc_Cls_to,string Cls_Fr_SName, string Cls_To_SName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_ClsCode = '" + Doc_Cls_to + "', Doc_Class_ShortName = '" + Cls_To_SName + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_ClsCode = '" + Doc_Cls_from + "' and Doc_Class_ShortName = '" + Cls_Fr_SName + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                         " SET Doc_ClsCode = '" + Doc_Cls_to + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_ClsCode = '" + Doc_Cls_from + "' and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doc_Class " +
                        " SET Doc_Cls_ActiveFlag = '" + chkdel + "' " +
                        " WHERE Doc_ClsCode = '" + Doc_Cls_from + "' and Doc_Cls_ActiveFlag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Qulification Changes 
        public DataSet getQua_to(string divcode, string Doc_QuaName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_QuaCode,'--Select--' as Doc_QuaName " +
                     " UNION " +
                     " SELECT Doc_QuaCode,Doc_QuaName FROM  Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag=0 AND Division_Code=  '" + divcode + "' and Doc_QuaName!='" + Doc_QuaName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }

        public DataSet getlistQua_count(string Doc_Qua_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_QuaCode) as Doc_QuaCode from Mas_ListedDr  where Doc_QuaCode=" + Doc_Qua_Code + " and ListedDr_Active_Flag =0";


            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public DataSet getUnlistQua_count(string Doc_Qua_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_QuaCode) as Doc_QuaCode from Mas_UnListedDr  where Doc_QuaCode=" + Doc_Qua_Code + " and UnListedDr_Active_Flag =0";


            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public int Update_DocQua_Drs(string Doc_Qua_from, string Doc_Qua_to, string Qua_Fr_SName, string Qua_To_SName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_QuaCode = '" + Doc_Qua_to + "', Doc_Qua_Name = '" + Qua_To_SName + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_QuaCode = '" + Doc_Qua_from + "' and Doc_Qua_Name ='" + Qua_Fr_SName + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                         " SET Doc_QuaCode = '" + Doc_Qua_to + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_QuaCode = '" + Doc_Qua_from + "' and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doc_Qualification " +
                        " SET Doc_Qua_ActiveFlag = '" + chkdel + "' " +
                        " WHERE Doc_QuaCode = '" + Doc_Qua_from + "' and Doc_Qua_ActiveFlag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Campaign

        public int getCamp_Count(string Doc_SubCatCode, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                         " where ListedDr_Active_Flag=0 and " +
                         " (Doc_SubCatCode like '" + Doc_SubCatCode + ',' + "%'  or " +
                         " Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + "' or" +
                         " Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + ',' + "%' or" +
                         " Doc_SubCatCode like '%" + Doc_SubCatCode + "%') and sf_code = '" + sf_code + "' ";


                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public DataSet getCampMgr_list(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName,   " +
                          " stuff((select ', '+territory_Name from Mas_Territory_Creation t where charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',a.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') Doc_SubCatName FROM " +
                       " Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' " +

                       " order by ListedDr_Name ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataTable getCamp_list(string sf_code, string Doc_SubCatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;
            if ((Doc_SubCatCode != "-1"))
            {
                swhere = " and  (a.Doc_SubCatCode like '" + Doc_SubCatCode + ',' + "%'  or " +
                         " a.Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + "' or" +
                         " a.Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + ',' + "%' or" +
                         " a.Doc_SubCatCode like '%" + Doc_SubCatCode + "%')  ";
            }
            if (sf_code != "-1")
            {
                swhere = swhere + " and a.sf_code in ('" + sf_code + "') ";
            }

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,a.ListedDr_DOB,a.ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName,   " +
                           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                          " stuff((select ', '+territory_Name from Mas_Territory_Creation t where cast(t.Territory_Code as varchar)=a.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',a.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') Doc_SubCatName FROM " +
                       "Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0  " +
                         swhere +
                      "";


            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDocType(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Type_Code Doc_Cat_Code, Type_Name Doc_Cat_Name FROM  Mas_Distance_Type " +
                     " WHERE Type_Active_Flag =0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getTypeName(string catcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Type_Name FROM  Mas_Distance_Type " +
                     " WHERE  Type_Code= '" + catcode + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        // Changes done by Reshmi
        public bool sRecordExist(string Doc_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_Name) FROM Mas_Doctor_Category WHERE Doc_Cat_Name='" + Doc_Cat_Name + "'AND Division_Code='" + divcode + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int RecordUpdateCat(int Doc_Cat_Code, string Doc_Cat_SName, string Doc_Cat_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExist(Doc_Cat_Code, Doc_Cat_SName, divcode))
            {
                if (!sRecordExist(Doc_Cat_Code, Doc_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_ListedDr " +
                            "SET Doc_Cat_ShortName = '" + Doc_Cat_SName + "' " +
                            "WHERE Doc_Cat_Code='" + Doc_Cat_Code + "' AND Division_Code='" + divcode + "'";
                        iReturn = db.ExecQry(strQry);


                        strQry = "UPDATE Mas_Doctor_Category " +
                                 " SET Doc_Cat_SName = '" + Doc_Cat_SName + "', " +
                                 " Doc_Cat_Name = '" + Doc_Cat_Name + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public bool sRecordExist(int Doc_Cat_Code, string Doc_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_Name) FROM Mas_Doctor_Category WHERE Doc_Cat_Name = '" + Doc_Cat_Name + "' AND Doc_Cat_Code!='" + Doc_Cat_Code + "'AND Division_Code='" + divcode + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistDocSpl(int Doc_Special_Code, string Doc_Special_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_Name) FROM Mas_Doctor_Speciality WHERE Doc_Special_Name = '" + Doc_Special_Name + "' AND Doc_Special_Code!='" + Doc_Special_Code + "'AND Division_Code='" + divcode + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistDocSpl(string Doc_Special_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_Name) FROM Mas_Doctor_Speciality WHERE Doc_Special_Name='" + Doc_Special_Name + "'AND Division_Code='" + divcode + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistCls(int Doc_ClsCode, string Doc_ClsName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_ClsName) FROM Mas_Doc_Class WHERE Doc_ClsName= '" + Doc_ClsName + "' AND Doc_ClsCode!='" + Doc_ClsCode + "'AND Division_Code='" + divcode + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistCls(string Doc_ClsName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_ClsName) FROM Mas_Doc_Class WHERE Doc_ClsName='" + Doc_ClsName + "'AND Division_Code='" + divcode + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public DataSet getDocLstName(string Doc_ListName, string Div_Code, string Address, string Sf_Code, string strTerritorName, string strQualName, string StrSpec_Code, string StrCat_Code, string StrCls_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " select ListedDr_Name from Mas_ListedDr where ListedDr_Name='" + Doc_ListName + "' " +
                                " and Division_Code='" + Div_Code + "' and ListedDr_Address1 ='" + Address + "' " +
                                " and Sf_Code='" + Sf_Code + "' and Territory_Code='" + strTerritorName + "' " +
                                " and Doc_Qua_Name='" + strQualName + "' and Doc_Special_Code='" + StrSpec_Code + "' and Doc_Cat_Code='" + StrCat_Code + "' " +
                                " and Doc_ClsCode='" + StrCls_Code + "' and ListedDr_Active_Flag=0";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        //Changes done by Reshmi

        public DataSet getDoctorCat(string Doc_Cat_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "Select Doc_Cat_SName,Doc_Cat_Name,Doc_Cat_Sl_No,No_of_visit " +
                    "FROM Mas_Doctor_Category " +
                    "where Doc_Cat_Code='" + Doc_Cat_Code + "' AND Division_Code='" + div_code + "' and Doc_Cat_Active_Flag=0 " +
                    "ORDER BY Doc_Cat_Sl_No ";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDoctorSpec(string Doc_Special_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpec = null;
            strQry = "Select Doc_Special_SName,Doc_Special_Name,Doc_Spec_Sl_No,No_of_visit " +
                    "FROM Mas_Doctor_Speciality " +
                    "where Doc_Special_Code='" + Doc_Special_Code + "' AND Division_Code='" + div_code + "' and Doc_Special_Active_Flag=0 " +
                    "ORDER BY Doc_Spec_Sl_No ";
            try
            {
                dsDocSpec = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpec;

        }
        public DataSet getDoctorQua(string Doc_QuaCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;
            strQry = "Select Doc_QuaName,DocQuaSNo " +
                    "FROM Mas_Doc_Qualification " +
                    "where Doc_QuaCode ='" + Doc_QuaCode + "' AND Division_Code='" + div_code + "'  AND Doc_Qua_ActiveFlag=0 " +
                    "ORDER BY DocQuaSNo ";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }
        public DataSet getDoctorClass(string Doc_ClsCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoCls = null;
            strQry = "Select Doc_ClsSName,Doc_ClsName,Doc_ClsSNo,No_of_visit " +
                    "FROM Mas_Doc_Class " +
                    "where Doc_ClsCode='" + Doc_ClsCode + "' AND Division_Code='" + div_code + "' AND Doc_Cls_ActiveFlag=0 " +
                    "ORDER BY Doc_ClsSNo ";
            try
            {
                dsDoCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoCls;
        }

        //Changes done by Reshmi
        public bool sRecordExistSubCat(int Doc_SubCatCode, string Doc_SubCatName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatName= '" + Doc_SubCatName + "' AND Doc_SubCatCode!='" + Doc_SubCatCode + "'AND Division_Code='" + divcode + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool RecordExistSubCat(int Doc_SubCatCode, string Doc_SubCatSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName= '" + Doc_SubCatSName + "' AND Doc_SubCatCode!='" + Doc_SubCatCode + "'AND Division_Code='" + divcode + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int RecordUpdateSubCat(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName, divcode))
            {

                if (!sRecordExistSubCat(Doc_SubCatCode, Doc_SubCatName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Doc_SubCategory " +
                                 " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                 " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                    " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }

        public int RecordUpdateSubCatnew(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName, divcode))
            {
                if (!sRecordExistSubCat(Doc_SubCatCode, Doc_SubCatName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Doc_SubCategory " +
                                 " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                 " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                     " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                    " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                     " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }

        public bool RecordExistSubCat(string Doc_SubCatSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName='" + Doc_SubCatSName + "'AND Division_Code='" + divcode + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistSubCat(string Doc_SubCatName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatName='" + Doc_SubCatName + "'AND Division_Code='" + divcode + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int RecordAddSubCat(string divcode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatSName, divcode))
            {
                if (!sRecordExistSubCat(Doc_SubCatName, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Doc_SubCatCode)+1,'1') Doc_SubCatCode from Mas_Doc_SubCategory ";
                        int Doc_SubCatCode = db.Exec_Scalar(strQry);

                        string EffFromdate = Effective_From.Month.ToString() + "-" + Effective_From.Day + "-" + Effective_From.Year;
                        string EffTodate = Effective_To.Month.ToString() + "-" + Effective_To.Day + "-" + Effective_To.Year;

                        strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To)" +
                                 "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "')";


                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public int getDoctorcount_Total(string sf_code, string cat_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in (" + strSf_Code + ") " +
                              " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public int getSpecialcount_Total(string sf_code, string spec_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in(" + strSf_Code + ") " +
                              " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getClasscount_Total(string sf_code, string Doc_ClsCode, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                             " where Sf_Code in(" + strSf_Code + ") " +
                             " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getQualcount_Total(string sf_code, string qual_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in(" + strSf_Code + ") " +
                              " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getDoctorMgr_View(string mgr_code, string type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (mgr_code != "admin"))
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where sf_code in (" + mgr_code + ") ) and a.ListedDr_Active_Flag = 0";
            }
            else if ((type == "0") && (mgr_code == "admin"))
            {
                swhere = swhere + "and a.ListedDr_Active_Flag = 0 ";
            }
            else
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where Sf_Code !='admin' and State_Code = '" + mgr_code + "'  and sf_type = 1)";
            }


            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case isnull(ListedDr_DOB,null)" +
                       " when '1900-01-01 00:00:00.000' then null  else  ListedDr_DOB  end ListedDr_DOB," +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, " +
                       " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and  a.Doc_ClsCode=b.Doc_ClsCode and a.Division_Code='" + div_code + "'  " +
                       swhere +
                       " order by ListedDr_Sl_No ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getDoctorCategory(string sf_code, string cat_code, string type, string strsf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }

            if (sf_code != "-1")
            {
                swhere = swhere + "and a.Sf_Code = '" + sf_code + "' ";
            }

            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB,ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
            //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
            //           " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
            //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
            //           " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and a.Doc_ClsCode=b.Doc_ClsCode " +
            //           swhere +
            //           " order by ListedDr_Name ";

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code," +
                       " case isnull(ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.ListedDr_Active_Flag=0 and  a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode " +
                       swhere +
                       " order by ListedDr_Sl_No ";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDoctorCategory_Chart(string sf_code, string type, string strSf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            if (type == "0")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode) as ListedDrCode,b.Doc_Cat_SName,  a.Doc_Cat_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Category b " +
                               " where a.Sf_Code ='" + sf_code + "' and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                               " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ,b.Doc_Cat_SName";
                }
                else
                {
                    strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode) as ListedDrCode,b.Doc_Cat_SName,  a.Doc_Cat_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Category b " +
                               " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                               " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ,b.Doc_Cat_SName";
                }
            }
            else if (type == "1")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_Special_Name, count(a.ListedDrCode),b.Doc_Special_SName, a.Doc_Special_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Speciality b " +
                               " where a.Sf_Code ='" + sf_code + "' and a.Doc_Special_Code = b.Doc_Special_Code " +
                               " group by a.Doc_Special_Code  , b.Doc_Special_Name,b.Doc_Special_SName ";
                }
                else
                {
                    strQry = " select b.Doc_Special_Name, count(a.ListedDrCode),b.Doc_Special_SName, a.Doc_Special_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Speciality b " +
                               " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_Special_Code = b.Doc_Special_Code " +
                               " group by a.Doc_Special_Code  , b.Doc_Special_Name,b.Doc_Special_SName ";
                }
            }
            else if (type == "2")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_ClsName, count(a.ListedDrCode),b.Doc_ClsSName, a.Doc_ClsCode" +
                               " from Mas_ListedDr a, Mas_Doc_Class b " +
                               " where a.Sf_Code = '" + sf_code + "' and a.Doc_ClsCode = b.Doc_ClsCode " +
                               " group by a.Doc_ClsCode  , b.Doc_ClsName,b.Doc_ClsSName ";
                }
                else
                {
                    strQry = " select b.Doc_ClsName, count(a.ListedDrCode),b.Doc_ClsSName, a.Doc_ClsCode" +
                              " from Mas_ListedDr a, Mas_Doc_Class b " +
                              " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_ClsCode = b.Doc_ClsCode " +
                              " group by a.Doc_ClsCode  , b.Doc_ClsName,b.Doc_ClsSName ";
                }
            }
            else if (type == "3")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_QuaName, count(a.ListedDrCode), b.Doc_QuaName, a.Doc_QuaCode" +
                               " from Mas_ListedDr a, Mas_Doc_Qualification b " +
                               " where a.Sf_Code = '" + sf_code + "' and a.Doc_QuaCode = b.Doc_QuaCode " +
                               " group by a.Doc_QuaCode  , b.Doc_QuaName ";
                }
                else
                {
                    strQry = " select b.Doc_QuaName, count(a.ListedDrCode), b.Doc_QuaName, a.Doc_QuaCode" +
                               " from Mas_ListedDr a, Mas_Doc_Qualification b " +
                               " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_QuaCode = b.Doc_QuaCode " +
                               " group by a.Doc_QuaCode  , b.Doc_QuaName ";
                }
            }

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDr_Pro_Exp(string divcode, string sf_code, int Year, int Month, int Prod, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            //strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, a.listeddr_name,a.Doc_Qua_Name,STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
            //       " and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,a.Doc_Spec_ShortName," +
            //      "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
            //      "WHERE a.Sf_Code='" + sf_code + "' and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
            //    //" and (CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '"+cdate+"' , 126) " +
            //    //" And ((CONVERT(Date, ListedDr_Deactivate_Date) >=  CONVERT(VARCHAR(50), '"+cdate+"' , 126) " +
            //    //" or ListedDr_Deactivate_Date is null))) " +
            //      "and b.Trans_Detail_Info_Code=a.ListedDrCode and CHARINDEX('" + Prod + "', b.Product_Code) > 0 and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' order by Fieldforce_Name ";


            strQry = "EXEC sp_Get_LstDr_Prd_Count_Zoom '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "'";
          
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getDr_Pro_Expall(string divcode, string Sf_Code_multiple, int Year, int Month, int Prod, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, (select Product_Detail_Name  from Mas_Product_Detail where Product_Code_SlNo='" + Prod + "') Product_Name , " +
                      " a.listeddr_name,a.Doc_Qua_Name,STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
                       " and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,a.Doc_Spec_ShortName," +
                      "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
                      "WHERE a.Sf_Code in(" + Sf_Code_multiple + ") and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
                      "and b.Trans_Detail_Info_Code=a.ListedDrCode and charindex('#" + Prod + "~','#'+ b.Product_Code) > 0 " +
                      " and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' order by Fieldforce_Name ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getDocCat_Visit(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
                     " when '' then 1 " +
                     " when 0 then 1 " +
                     " else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                     " FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_Sl_No";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getSpec_Exp(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "Select Doc_Special_Code,Doc_Special_SName " +
                    "FROM Mas_Doctor_Speciality " +
                    "where  Division_Code='" + div_code + "' and Doc_Special_Active_Flag=0 " +
                    "ORDER BY Doc_Spec_Sl_No ";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getCat_Exp(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "Select Doc_Cat_Code,Doc_Cat_SName " +
                    "FROM Mas_Doctor_Category " +
                    "where  Division_Code='" + div_code + "' and Doc_Cat_Active_Flag=0 " +
                    "ORDER BY Doc_Cat_Sl_No ";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDocSpec_ForExpo(string strspec)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_Special_Code,Doc_Special_SName from Mas_Doctor_Speciality where Doc_Special_Active_Flag=0 and  Doc_Special_Code in(" + strspec + ")";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDocCat_ForExpo(string strcat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_Cat_Code,Doc_Cat_SName from Mas_Doctor_Category where Doc_Cat_Active_Flag=0 and  Doc_Cat_Code in(" + strcat + ")";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDr_Pro_Exp_SpeCat(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, int Speciality)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_Get_LstDr_Prd_SpeCat_Zoom '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + Speciality + "'";


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getDr_Pro_Expall_SpeCat(string divcode, string Sf_Code_multiple, int Year, int Month, int Prod, string cdate, int speciality)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, (select Product_Detail_Name  from Mas_Product_Detail where Product_Code_SlNo='" + Prod + "') Product_Name , " +
                      " a.listeddr_name,a.Doc_Qua_Name,STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
                      " and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,a.Doc_Spec_ShortName," +
                     "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
                     "WHERE a.Sf_Code in(" + Sf_Code_multiple + ") and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
                     "and b.Trans_Detail_Info_Code=a.ListedDrCode and charindex('#" + Prod + "~','#'+ b.Product_Code) > 0 " +
                     " and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' and a.Doc_Special_Code='" + speciality + "' order by Fieldforce_Name ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getDr_Pro_Exp_categ(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, int Category)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_Get_LstDr_Prd_Category_Zoom '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + Category + "'";


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getDr_Pro_Expall_categ(string divcode, string Sf_Code_multiple, int Year, int Month, int Prod, string cdate, int category)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, (select Product_Detail_Name  from Mas_Product_Detail where Product_Code_SlNo='" + Prod + "') Product_Name , " +
                      " a.listeddr_name,a.Doc_Qua_Name,STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
                      " and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,a.Doc_Spec_ShortName," +
                     "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
                     "WHERE a.Sf_Code in(" + Sf_Code_multiple + ") and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
                     "and b.Trans_Detail_Info_Code=a.ListedDrCode and (Patindex('" + Prod + "~%', b.Product_Code) > 0 " +
                      " (Patindex('%#" + Prod + "~%', b.Product_Code) > 0 or (Patindex(%~'" + Prod + "%', b.Product_Code) > 0 )" +
                     " and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' and a.Doc_Cat_Code='" + category + "' order by Fieldforce_Name ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getDocCampaign(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT '' as Doc_SubCatCode, '' as Doc_SubCatSName, '---Select---' as Doc_SubCatName " +
                     " UNION " +
                     " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                     " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY Doc_SubCatName";


            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }
        public DataSet LoadWorkwith_camp(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " Select Sf_Code, Sf_Name, sf_Designation_Short_Name , Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' " +
                         " UNION" +
                         " Select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF  from Mas_Salesforce " + // AM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                         " UNION" +
                         " select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF from Mas_Salesforce " + // RM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF from Mas_Salesforce " + // ZM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) ) ) order by Reporting_To_SF Desc ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataTable getCamp_list_doc(string sf_code, string Doc_SubCatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,a.ListedDr_DOB,a.ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, Doc_Cat_ShortName, Doc_Spec_ShortName, Doc_Qua_Name, Doc_Class_ShortName,   " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where cast(t.Territory_Code as varchar)=a.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',a.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') Doc_SubCatName FROM " +
                       " Mas_ListedDr a  " +
                       " where  " +
                       " a.ListedDr_Active_Flag=0  " +
                       " and  (a.Doc_SubCatCode like '" + Doc_SubCatCode + ',' + "%'  or " +
                       " a.Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + "' or" +
                       " a.Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + ',' + "%' or" +
                       " a.Doc_SubCatCode like '%" + Doc_SubCatCode + "%') and a.sf_code = '" + sf_code + "' ";


            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getDoc_MRCamp(string divcode, string Dr_Code, string sf_Code, int Month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select day(a.Activity_Date) as Activity_Date,b.Product_Detail, b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,b.Worked_with_Name from dcrmain_trans a,DCRDetail_lst_trans b " +
                            " where a.trans_slno=b.trans_slno and b.trans_detail_info_code='" + Dr_Code + "'  " +
                            " and a.sf_code='" + sf_Code + "' and MONTH(a.Activity_Date)='" + Month + "' and year(a.Activity_Date) ='" + year + "'  ";


            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }

        public DataSet getDoc_MRCamp_WorkedWith(string divcode, string Dr_Code, string sf_Code, int Month, int day, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select day(a.Activity_Date) as Activity_Date,b.Product_Detail, b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,  " +
                //  " case b.Worked_with_Name when 'SELF,' then ''  else ltrim(rtrim(b.Worked_with_Name)) end as  " +
                       " ltrim(rtrim(b.Worked_with_Name)) as Worked_with_Name from dcrmain_trans a,DCRDetail_lst_trans b " +
                            " where a.trans_slno=b.trans_slno and b.trans_detail_info_code='" + Dr_Code + "'  " +
                            " and a.sf_code='" + sf_Code + "' and MONTH(a.Activity_Date)='" + Month + "' and day(a.Activity_Date) ='" + day + "' and year(a.Activity_Date) ='" + year + "'";


            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }


        public DataSet getPro_Sample_Doctor(string divcode, string sf_code, int Year, int Month, string cdate, int Prod)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;       


            strQry = " select distinct Product_Code_SlNo,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name " +
                       " ,(select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "' )  sf_HQ,  Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name " +
                       " ,sum(cast(substring(c.product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~')-1,charindex('$','#'+ c.Product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code))-(charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~'))) as numeric)) sample " +
                       " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e  where " +
                       " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and " +
                       " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
                       " and  charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  and " +
                       " (charindex('~$','#'+ Product_Code) <= 0 and charindex('~0$','#'+ Product_Code) <= 0)   and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' " +
                       " and  c.sf_code  in ('" + sf_code + "') and b.sf_code in ('" + sf_code + "') and Product_Code_SlNo='" + Prod + "' " +
                       " group by b.Sf_Code ,Product_Code_SlNo,Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name ";


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getDr_Pro_Sample(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;


            strQry = "select distinct Product_Code_SlNo,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' )   Fieldforce_Name,c.sf_code " +
                     ",(select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "' )  sf_HQ,  Product_Detail_Name " +
                     " ,count(distinct Trans_Detail_Info_Code) Sample " +
                     " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e  where  " +
                     " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and  " +
                     " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
                     " and  charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  and " +
                     " (charindex('~$','#'+ Product_Code) <= 0 and charindex('~0$','#'+ Product_Code) <= 0)  and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' " +
                     " and  c.sf_code  in ('" + sf_code + "') and b.sf_code in ('" + sf_code + "') group by c.Sf_Code , " +
                     " Product_Code_SlNo,Product_Detail_Name ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getDr_Prd_Mapped_Name(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT distinct c.Listeddr_Code,d.ListedDr_Name ,d.Doc_Spec_ShortName ,d.Doc_Qua_Name ,d.Doc_Cat_ShortName ,d.Doc_Class_ShortName, " +
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and " +
                    " charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                    " isnull((select Product_Name+' ( '+CAST(Product_Priority as varchar)+' ), ' from " +
                    " Map_LstDrs_Product where d.ListedDrCode=Listeddr_Code for xml path('')),'')  Product_Detail_Name from " +
                    " Mas_ListedDr d ,Map_LstDrs_Product c " +
                    " WHERE c.Sf_Code = '" + sf_code + "' and d.ListedDrCode =c.Listeddr_Code and c.Division_Code ='" + divcode + "' " +
                    " and d.ListedDr_Active_Flag = 0 ";


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getDr_Prd_DCR_Name(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " select distinct Listeddr_Code,e.ListedDr_Name,e.Doc_Spec_ShortName,e.Doc_Qua_Name, " +
                     " e.Doc_Cat_ShortName,e.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = e.Sf_Code and " +
                     " charindex(cast(t.Territory_Code as varchar)+',',e.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                     " isnull((select Product_Name+' ( '+CAST(Product_Priority as varchar)+' ), ' from " +
                     " Map_LstDrs_Product where e.ListedDrCode=Listeddr_Code for xml path('')),'') Product_Detail_Name " +
                     " from DCRMain_Trans b, DCRDetail_Lst_Trans c ,Mas_ListedDr e,Mas_Product_Detail P,Map_LstDrs_Product m where " +
                     " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo  and  " +
                     " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  " +
                     " and c.Trans_Detail_Info_Type=1  and c.Trans_Detail_Info_Code =m.Listeddr_Code and " +
                     " c.Trans_Detail_Info_Code=e.ListedDrCode and e.ListedDr_Active_Flag=0 " +
                     " and month(b.Activity_Date)='" + Month + "' and  YEAR(b.Activity_Date)= '" + Year + "' and  c.sf_code in " +
                     " ('" + sf_code + "') and b.sf_code in ('" + sf_code + "') ";


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getDocCat_View(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name,case c.No_of_visit " +
                     " when isnull(c.No_of_visit,'') then 1 " +
                     " when ISNULL(c.No_of_visit,0) then 1 " +
                     " else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                     " FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_SName";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getPrdCountDoctor(string divcode, string sf_code, int Prod)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_get_PrdCountDoctor_Mapp '" + divcode + "', '" + sf_code + "','" + Prod + "'";


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet Visit_Doctor_DCR(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            //strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

            strQry = " select distinct a.ListedDrCode,a.ListedDr_Name, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and " +
                    " charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                    " a.Doc_Qua_Name,a.Doc_Cat_ShortName, " +
                    " a.Doc_Spec_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a where a.Division_Code ='" + div_code + "'  " +
                    " and a.sf_code in ('" + sf_code + "') and ((CONVERT(Date, ListedDr_Created_Date) < '" + dtcurrent + "') " +
                    " And (CONVERT(Date, ListedDr_Deactivate_Date) >= '" + dtcurrent + "' or " +
                    " ListedDr_Deactivate_Date is null)) or a.ListedDrCode " +
                    " in(select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c " +
                    " where  b.Trans_SlNo = c.Trans_SlNo  and c.Trans_Detail_Info_Type=1 and c.Trans_Detail_Info_Code=a.ListedDrCode " +
                    " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "' and " +
                    " c.sf_code in ('" + sf_code + "')) order by 3,2 ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet Visit_Doctor_DCR_Dates(string sf_code, string div_code, int cmon, int cyear, string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            //strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

            strQry = " select distinct DAY(Activity_Date) Activity_Date ,FieldWork_Indicator,a.Sf_Code ,b.Trans_Detail_Info_Code " +
                    " from DCRMain_Trans a ,DCRDetail_Lst_Trans b ,Mas_ListedDr c where " +
                    " a.Division_Code='" + div_code + "' and a.Sf_Code='" + sf_code + "' " +
                    " and a.FieldWork_Indicator ='F' and b.Trans_Detail_Info_Type =1 and " +
                    " b.Trans_Detail_Info_Code ='" + ListedDrCode + "'  and b.Trans_Detail_Info_Code =c.ListedDrCode and MONTH(a.Activity_Date)='" + cmon + "' and Year(a.Activity_Date)='" + cyear + "' " +
                    " and a.Trans_SlNo = b.Trans_SlNo ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getDCR_Leave_Type(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " select Leave_code,Leave_SName from mas_Leave_Type where Division_Code='" + div_code + "' and Active_Flag =0 ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

		  public DataSet GET_Doc_CLASS(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTerr = null;
            strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName,c.Doc_ClsName,(select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode and ListedDr_Active_Flag=0) as Cls_Count  " +
                     " FROM  Mas_Doc_Class c where c.division_Code = '" + Div_Code + "' AND c.Doc_Cls_ActiveFlag=0  ORDER BY c.Doc_ClsSNo ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public int Add_doc_Change(string Div_Code, string Channel1, string channel2)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_ListedDr set Doc_ClsCode='" + channel2 + "' where division_code='" + Div_Code + "' and Doc_ClsCode='" + Channel1 + "' ";
                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Doc_Class set Doc_Cls_ActiveFlag='1' where  Division_Code='" + Div_Code + "' and Doc_ClsCode='" + Channel1 + "'";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public int RecordAddDocCat(string divcode, string Doc_Special_SName, string Doc_Special_Name)
        {
            int iReturn = -1;
            if (!RecordExistDocCat(Doc_Special_SName, divcode))
            {
                if (!sRecordExistDocCat(Doc_Special_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Doc_Cat_Code)+1,'1') Doc_Cat_Code  from Mas_Doctor_Category ";
                        int Doc_Special_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Doctor_Category(Doc_Cat_Code,Division_Code,Doc_Cat_SName,Doc_Cat_Name,Doc_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Doc_Special_Code + "','" + divcode + "','" + Doc_Special_SName + "', '" + Doc_Special_Name + "',0,getdate(),getdate())";


                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public bool RecordExistDocCat(string Doc_Special_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_SName) FROM Mas_Doctor_Category WHERE Doc_Cat_SName='" + Doc_Special_SName + "'AND Division_Code='" + divcode + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool sRecordExistDocCat(string Doc_Special_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_Name) FROM Mas_Doctor_Category WHERE Doc_Cat_Name='" + Doc_Special_Name + "'AND Division_Code='" + divcode + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int RecordUpdateDocCat(int Doc_Special_Code, string Doc_Special_SName, string Doc_Special_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistDocCat(Doc_Special_Code, Doc_Special_SName, divcode))
            {
                if (!sRecordExistDocCat(Doc_Special_Code, Doc_Special_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();


                        strQry = "UPDATE Mas_ListedDr " +
                               "SET Doc_Cat_ShortName='" + Doc_Special_SName + "' " +
                               "WHERE Doc_Cat_Code= '" + Doc_Special_Code + "' AND Division_Code='" + divcode + "'";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Doctor_Category " +
                                 " SET Doc_Cat_SName = '" + Doc_Special_SName + "', " +
                                 " Doc_Cat_Name = '" + Doc_Special_Name + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_Cat_Code = '" + Doc_Special_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }

        public bool RecordExistDocCat(int Doc_Special_Code, string Doc_Special_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_SName) FROM Mas_Doctor_Speciality WHERE Doc_Special_SName = '" + Doc_Special_SName + "' AND Doc_Special_Code!='" + Doc_Special_Code + "'AND Division_Code='" + divcode + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistDocCat(int Doc_Special_Code, string Doc_Special_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_Name) FROM Mas_Doctor_Speciality WHERE Doc_Special_Name = '" + Doc_Special_Name + "' AND Doc_Special_Code!='" + Doc_Special_Code + "'AND Division_Code='" + divcode + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

       


    }

       
}
