using System.Data.SqlClient;

namespace NCore.Demo.Install.Helpers
{
    internal class ExecuteResult
    {
        public ExecuteResult()
        {
        }

        public ExecuteResult(SqlException sqlException, string line)
        {
            SqlException = sqlException;
            Line = line;
            Failed = true;
        }

        public bool Failed { get; }
        public SqlException SqlException { get; }
        public string Line { get; }
    }
}