using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NCore.Demo.Install.Helpers
{
    internal class Runner
    {
        private readonly Assembly _assembly;
        private readonly string _connectionString;
        private readonly string _filePath;
        private readonly string[] _names;

        public Runner()
        {
            _filePath = ConfigurationManager.AppSettings["FilePath"];
            if (!Directory.Exists(_filePath))
                Directory.CreateDirectory(_filePath);
            _assembly = Assembly.GetExecutingAssembly();
            _connectionString = ConfigurationManager.ConnectionStrings["Runner"].ConnectionString;
            _names = _assembly.GetManifestResourceNames()
                .Where(n => n.EndsWith(".sql", StringComparison.InvariantCultureIgnoreCase))
                .ToArray();
        }

        public void Run()
        {
            foreach (var name in _names)
            {
                string sql;
                using (var stream = _assembly.GetManifestResourceStream(name))
                {
                    if (stream == null) continue;
                    var reader = new StreamReader(stream);
                    sql = reader.ReadToEnd();
                }

                ExecuteResult result;
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        Console.WriteLine($"Running: {name}");
                        result = ExecuteSql(connection, string.Format(sql, _filePath));
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ReadException(ex));
                    return;
                }

                if (result.Failed)
                {
                    Console.WriteLine(ReadException(result.SqlException, result.Line));
                }
            }
            Console.WriteLine($"Done running {_names.Length} scripts");
        }

        private ExecuteResult ExecuteSql(SqlConnection connection, string sql)
        {
            var regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var lines = regex.Split(sql);

            using (var cmd = connection.CreateCommand())
            {
                foreach (var line in lines.Where(line => line.Length > 0))
                {
                    cmd.CommandText = line;
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        return new ExecuteResult(ex, line);
                    }
                }
            }
            return new ExecuteResult();
        }

        public string ReadException(Exception ex, string header = "Exception occured:")
        {
            var builder = new StringBuilder(header);
            var exception = ex;
            while (exception != null)
            {
                builder.AppendLine(exception.Message);
                exception = exception.InnerException;
            }
            return builder.ToString();
        }
    }
}