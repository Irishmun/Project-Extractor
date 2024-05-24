using DatabaseCleaner.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseCleaner
{
    public class Cleaner
    {
        //note that these strings are made for a specific type of database, and should be adjusted somewhere
        private const string DUPLICATE_ONLY_SUFFIX = " HAVING COUNT(*) > 1";
        private const string CONNECTION_PREFIX = @"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=";

        private readonly string GET_ALL_CUSTOMERS_QUERY = Properties.Resources.GetAllCustomers;
        private readonly string FULL_DB_GET = Properties.Resources.GetProjects;

        private Dictionary<int, string> _customerIds = new Dictionary<int, string>();
        private object[] _listEntry = new object[4];//customer id, project title, duplicate estimate
        private int customerColumn = 0, titleColumn = 0, descriptionColumn = 0;

        public DataTable GetDuplicatesAndCount(string path, BackgroundWorker worker, out int duplicateCount, bool duplicatesOnly = false)
        {
            if (UtilMethods.Is32Bit() == false)
            {//Odbc only seems to work in 32 bit mode
                duplicateCount = -1;
                return null;
            }
            DataTable table = GetDuplicates(path, duplicatesOnly);
            worker.ReportProgress(50);
            duplicateCount = GetDuplicateCount(table, worker);
            worker.ReportProgress(100);
            return table;
        }

        public int GetDuplicateCount(DataTable table, BackgroundWorker worker)
        {
            object sumObject = table.Compute("Sum(duplicates)", null);
            return Convert.ToInt32(sumObject);

        }

        /// <summary>Gets duplicate queries from the database</summary>
        /// <param name="path">path to the database</param>
        /// <param name="duplicatesOnly">if only the duplicates need to be gotten</param>
        public DataTable GetDuplicates(string path, bool duplicatesOnly = false)
        {
            string comm = duplicatesOnly ? FULL_DB_GET + DUPLICATE_ONLY_SUFFIX : FULL_DB_GET;
            using (OdbcConnection conn = new OdbcConnection(CONNECTION_PREFIX + path))
            {
                try
                {
                    conn.Open();
                    OdbcCommand command = new OdbcCommand(comm, conn);
                    OdbcDataAdapter da = new OdbcDataAdapter(command);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message, "SQL Error", System.Windows.Forms.MessageBoxButtons.OK);
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        /// <summary>Fills the <see cref="_customerIds"/> Dictionary with entries from the database, returns false if no entries were found</summary>
        /// <param name="path">path to the database file</param>
        /// <returns>Whether any entries were found</returns>
        public bool SetCustomersDict(string path)
        {
            if (File.Exists(path) == false)
            { return false; }
            using (OdbcConnection conn = new OdbcConnection(CONNECTION_PREFIX + path))
            {
                conn.Open();

                OdbcCommand command = new OdbcCommand(GET_ALL_CUSTOMERS_QUERY, conn);
                DataSet ds = new DataSet();
                OdbcDataAdapter da = new OdbcDataAdapter(command);
                da.Fill(ds);
                DataTable col = ds.Tables[0];
                _customerIds.Clear();
                foreach (DataRow item in col.Rows)
                {
                    if (item[0] == null || item[1] == null)
                    { continue; }
                    _customerIds.Add(int.Parse(item[0].ToString()), item[1].ToString());
                }
                if (_customerIds.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public Dictionary<int, string> CustomerIds { get => _customerIds; set => _customerIds = value; }
    }
}
