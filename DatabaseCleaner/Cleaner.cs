using DatabaseCleaner.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;

namespace DatabaseCleaner
{
    public class Cleaner
    {
        //note that these strings are made for a specific type of database, and should really be adjustable somewhere
        private const string GET_ALL_PROJECTS_QUERY = "SELECT * FROM WBSO_P";
        private const string GET_ALL_CUSTOMER_QUERY = "SELECT \"CustomerID\", \"CompanyName\" FROM Customers";
        private const string DUPLICATE_BASE_QUERY = "SELECT \"CompanyName\", \"TITLE\",\"DESC\", COUNT(*) AS duplicates FROM WBSO_P INNER JOIN Customers ON WBSO_P.CustomerID = Customers.CustomerID GROUP BY \"CompanyName\", \"TITLE\",\"DESC\"";
        private const string DUPLICATE_ONLY_QUERY = " HAVING COUNT(*) > 1";

        //private const string CONNECTION_PREFIX = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        private const string CONNECTION_PREFIX = @"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=";

        private Dictionary<int, string> _customerIds = new Dictionary<int, string>();
        private object[] _listEntry = new object[4];//customer id, project title, duplicate estimate
        private int customerColumn = 0, titleColumn = 0, descriptionColumn = 0;

        

        public void FindDuplicates(string path, BackgroundWorker worker)
        {
            if (UtilMethods.Is32Bit() == false)
            {
                //can't get access database to work when running in anything but 32 bit mode
                //stop for now
                worker.ReportProgress(100);
                return;
            }

            //get projects from database, use those for progress
            DataTable dt = GetDataTable(path, GET_ALL_PROJECTS_QUERY);
            DataTable col;
            using (OdbcConnection conn = new OdbcConnection(CONNECTION_PREFIX + path))
            {
                conn.Open();
                OdbcCommand command = new OdbcCommand(DUPLICATE_BASE_QUERY, conn);
                OdbcDataAdapter da = new OdbcDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                col = ds.Tables[0];
                //return col;
            }
            double rowCount = (double)col.Rows.Count;
            for (int i = 0; i < col.Rows.Count; i++)
            {
                Array.Clear(_listEntry);
                DataRow row = col.Rows[i];
                _listEntry[0] = row[0];
                _listEntry[1] = row[1];
                _listEntry[2] = row[2];
                _listEntry[3] = row[3];
                worker.ReportProgress((int)(((double)i + 1d) * 100d / rowCount), _listEntry);
            }
            /*
            for (int i = 0; i < dt.Columns.Count; i++)
            {
#if DEBUG
                Debug.WriteLine(dt.Columns[i].ColumnName);
#endif
                if (dt.Columns[i].ColumnName.Equals("CustomerID", StringComparison.OrdinalIgnoreCase))
                {
                    customerColumn = i;
                    continue;
                }
                if (dt.Columns[i].ColumnName.Equals("TITLE", StringComparison.OrdinalIgnoreCase))
                {
                    titleColumn = i;
                    continue;
                }
                if (dt.Columns[i].ColumnName.Equals("DESC", StringComparison.OrdinalIgnoreCase))
                {
                    descriptionColumn = i;
                    continue;
                }
            }

            bool useCostumerName = SetCustomersDict(path);//if false, use id number instead

            List<int> duplicateEntries = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (duplicateEntries.Contains(i))//no need to look over this one, is already in the table
                { continue; }
                Array.Clear(_listEntry);
                DataRow row = dt.Rows[i];

                if (string.IsNullOrWhiteSpace(row[customerColumn].ToString()) == false)
                {
                    _listEntry[0] = useCostumerName ? _customerIds[int.Parse(row[customerColumn].ToString())] : row[customerColumn].ToString();
                }
                else
                {
                    _listEntry[0] = string.Empty;
                }
                //TODO: het werkt niet ;-;
                _listEntry[1] = row[titleColumn];
                _listEntry[2] = FindPossibleDuplicates(row, dt, duplicateEntries, i, out int[] findings);
                _listEntry[3] = row[descriptionColumn];
                duplicateEntries.AddRange(findings);

                worker.ReportProgress((int)(((double)i + 1d) * 100d / (double)dt.Rows.Count), _listEntry);
            }
            */
        }

        public DataTable GetDuplicates(string path)
        {
            using (OdbcConnection conn = new OdbcConnection(CONNECTION_PREFIX + path))
            {
                conn.Open();
                OdbcCommand command = new OdbcCommand(DUPLICATE_BASE_QUERY, conn);
                OdbcDataAdapter da = new OdbcDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
                //return col;
            }
        }

        public void CleanDuplicates()
        {
            throw new NotImplementedException();
        }

        private int FindPossibleDuplicates(DataRow checkRow, DataTable table, List<int> alreadyFound, int start, out int[] newEntries)
        {
            List<int> duplicateEntries = new List<int>();
            string checkCustomer = checkRow[customerColumn]?.ToString();
            string checkTitle = checkRow[titleColumn]?.ToString();
            string checkDescription = checkRow[descriptionColumn]?.ToString();
            if (string.IsNullOrWhiteSpace(checkDescription) == false && checkDescription.Contains('.'))
            {
                checkDescription = checkDescription.Substring(checkDescription.IndexOf('.'));
            }
            for (int i = start; i < table.Rows.Count; i++)
            {
                if (table.Rows[i].Equals(checkRow))
                { continue; }

                DataRow row = table.Rows[i];
                string title = row[titleColumn]?.ToString();
                string description = row[descriptionColumn]?.ToString();
                string customer = row[customerColumn]?.ToString();
                if (alreadyFound.Contains(i))
                { continue; }
                //check title, then first line of description, then title
                if (string.IsNullOrWhiteSpace(description) == false)
                {
                    if (description.Contains('.'))
                    {
                        description = description.Substring(0, description.IndexOf('.')).Trim();//should result in first sentence
                    }
                    if (description.StartsWith(checkDescription.Trim(), StringComparison.OrdinalIgnoreCase))
                    {//description matches first sentence, should be the same
                        duplicateEntries.Add(i);
                        continue;
                    }
                }
                if (string.IsNullOrWhiteSpace(title) == false && title.Equals(checkTitle, StringComparison.OrdinalIgnoreCase))
                {
                    duplicateEntries.Add(i);
                    continue;
                }
            }
            newEntries = duplicateEntries.ToArray();
            return duplicateEntries.Count;
        }


        private DataTable GetDataTable(string path, string query)
        {
            if (File.Exists(path) == false)
            { return null; }
            using (OdbcConnection conn = new OdbcConnection(CONNECTION_PREFIX + path))
            {
                conn.Open();
                OdbcCommand command = new OdbcCommand(query, conn);
                OdbcDataAdapter da = new OdbcDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable col = ds.Tables[0];
                return col;
            }
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

                OdbcCommand command = new OdbcCommand(GET_ALL_CUSTOMER_QUERY, conn);
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
