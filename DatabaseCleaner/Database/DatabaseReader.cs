using DatabaseCleaner.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseCleaner.Database
{
    public class DatabaseReader
    {
        //note that these strings are made for a specific type of database, and should be adjusted somewhere
        private const string DUPLICATE_ONLY_SUFFIX = " HAVING COUNT(*) > 1";
        private const string CONNECTION_PREFIX = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";

        private readonly string FULL_DB_GET = Properties.Resources.GetProjects;

        private Dictionary<int, string> _customerIds = new Dictionary<int, string>();
        private object[] _listEntry = new object[4];//customer id, project title, duplicate estimate

        public DataTable GetDuplicatesAndCount(BackgroundWorker worker, out int duplicateCount, bool duplicatesOnly = false)
        {
            if (UtilMethods.Is32Bit() == false)
            {//OleDb only seems to work in 32 bit mode
                duplicateCount = -1;
                return null;
            }
            DataTable table = GetDuplicates(duplicatesOnly);
            worker.ReportProgress(50);
            duplicateCount = GetDuplicateCount(table);
            worker.ReportProgress(100);
            return table;
        }

        public int GetDuplicateCount(DataTable table)
        {
            if (table == null)
            { return -1; }
            if (table.Columns.Contains("duplicates") == false)
            { return table.Rows.Count; }
            object sumObject = table.Compute("Sum(duplicates)", null);
            return Convert.ToInt32(sumObject);

        }

        /// <summary>Gets duplicate queries from the database</summary>
        /// <param name="path">path to the database</param>
        /// <param name="duplicatesOnly">if only the duplicates need to be gotten</param>
        public DataTable GetDuplicates(bool duplicatesOnly = false)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = Settings.Instance.DbDataSource;
            builder.IntegratedSecurity = Settings.Instance.DbIntegratedSecurity;
            builder.TrustServerCertificate = Settings.Instance.DbTrustServerCertificate;
            builder.InitialCatalog = Settings.Instance.DbInitialCatalog;

            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                string sql = duplicatesOnly ? FULL_DB_GET + DUPLICATE_ONLY_SUFFIX : FULL_DB_GET;
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    try
                    {
                        comm.CommandTimeout = 240;
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(comm);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds.Tables[0];
                    }                                                       //access file uses longtext, so that is not the case
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "SQL Error", MessageBoxButtons.OK);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                return null;
            }
            /*
                        //This seems to give the full length results, look up if you can merge two datatables and then group them
            string comm2 = "SELECT [DESC],[TECHNEW],[TECH],[PROB],[OPLO],[METH],[ZELF],[PRIN],[Opmerkingen],[Vragen Senter] FROM [WBSO_P]";//Asking for just the description, gives it back in full length
            string comm = duplicatesOnly ? FULL_DB_GET + DUPLICATE_ONLY_SUFFIX : FULL_DB_GET;
            using (OleDbConnection conn = new OleDbConnection(CONNECTION_PREFIX + path))
            {
                try
                {
                    conn.Open();
                    OleDbCommand command = new OleDbCommand(comm, conn);//TODO: find source of issue bellow
                    OleDbDataAdapter da = new OleDbDataAdapter(command);//<============================
                    //DataSet ds = new DataSet();                         //|Somewhere in here, Lines are                    
                    //da.Fill(ds);                                        //|limited to ~250 chacters. why??
                    //return ds.Tables[0];                                //<============================
                    DataTable table = new DataTable();
                    table.Load(command.ExecuteReader());
                    command = new OleDbCommand(comm2, conn);
                    DataTable table2 = new DataTable();
                    table2.Load(command.ExecuteReader());
                    table.Merge(table2,false);
                    return table;
                }                                                       //access file uses longtext, so that is not the case
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "SQL Error", MessageBoxButtons.OK);
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;*/
        }

        public Dictionary<int, string> CustomerIds { get => _customerIds; set => _customerIds = value; }
    }
}
