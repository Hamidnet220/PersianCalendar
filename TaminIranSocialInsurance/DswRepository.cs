using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaminIranSocialInsurance
{
    [Guid("5F04FC4F-0B4F-4EF5-9CB3-39BC22D54683")]
    [ClassInterface(ClassInterfaceType.None)]
    public class DswRepository : IDswRepository
    {
        protected string schema;
        protected string tableName;
        private string connectionString=@"Provider = VFPOLEDB.1; Data Source = D:\; Password =; Collating Sequence = MACHINE";
        List<PropertyModel> propertyModels = new List<PropertyModel>();

        public DswRepository()
        {
            var entityType = typeof(DskWor00);
            var tableAttribute = entityType.GetCustomAttributes(typeof(TableAttribute), false).OfType<TableAttribute>().FirstOrDefault();
            if (tableAttribute != null)
            {
                schema = tableAttribute.Schema;
                tableName = tableAttribute.Table;
            }
            else
            {
                schema = "DBF";
                tableName = entityType.Name;
            }

            foreach (var propertyInfo in entityType.GetProperties())
            {
                var propertyModel = new PropertyModel
                {
                    PropertyName = propertyInfo.Name,
                    ColumnName = propertyInfo.Name,
                    IsComputed = propertyInfo.GetCustomAttributes(typeof(ComputedColumnAttribute), false).Any(),
                    IsPrimaryKey = propertyInfo.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Any(),
                    PropertyInfo = propertyInfo,
                    IsNullable = propertyInfo.GetCustomAttributes(typeof(IsNullable), false).Any()
                };

                propertyModels.Add(propertyModel);
            }

        }

        public DswRepository(string connectionString) : this()
        {
            if (connectionString.StartsWith("name="))
                this.connectionString = ConfigurationManager.ConnectionStrings[connectionString.Substring(5)]
                    .ConnectionString;
            else
                this.connectionString = connectionString;

        }

        [ComVisible(true)]
        public int Add(DskWor00 entity)
        {
            StringBuilder insertStatment = new StringBuilder($@"INSERT INTO {tableName}.{schema} (#columns#) VALUES (#values#)");
            var columns = new List<string>();
            var oleDbParameters = new List<OleDbParameter>();
            var valueNames = new List<string>();
            foreach (var property in propertyModels)
            {
                if (property.PropertyInfo.GetValue(entity) == null && property.IsNullable)
                    continue;
                if (property.IsComputed)
                    continue;
                columns.Add(property.ColumnName);
                var propertyValue = property.PropertyInfo.GetValue(entity);
                if (property.PropertyInfo.PropertyType == typeof(string))
                    propertyValue = ((string)property.PropertyInfo.GetValue(entity)).ToIranSystem();

                valueNames.Add("?");
                oleDbParameters.Add(new OleDbParameter(property.ColumnName, propertyValue));
            }

            insertStatment.Replace("#columns#", string.Join(",", columns));
            insertStatment.Replace("#values#", string.Join(",", valueNames));

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = insertStatment.ToString();
                foreach (var dbParameter in oleDbParameters)
                {
                    command.Parameters.Add(dbParameter);
                }

                return command.ExecuteNonQuery();

            }

        }

        public int Remove(DskWor00 entity)
        {
            var primaryKeys = propertyModels.Where(property => property.IsPrimaryKey);
            StringBuilder deleteStatment = new StringBuilder($"DELETE FROM {tableName}");

            List<string> whereParts = new List<string>();
            List<OleDbParameter> oleDbParameters = new List<OleDbParameter>();
            List<string> sqlParameterName = new List<string>();

            foreach (var propety in primaryKeys)
            {
                var parmeterName = propety.ColumnName;
                whereParts.Add(propety.ColumnName + "=?");
                var parameterValue = propety.PropertyInfo.GetValue(entity);
                oleDbParameters.Add(new OleDbParameter(parmeterName, parameterValue));
            }

            deleteStatment.Append(" WHERE " + string.Join(" AND ", whereParts));
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = deleteStatment.ToString();
                foreach (var sqlParameter in oleDbParameters)
                {
                    command.Parameters.Add(sqlParameter);
                }

                return command.ExecuteNonQuery();

            }

        }

        public List<DskWor00> Find(params object[] keys)
        {
            var primaryKeys = propertyModels.Where(property => property.IsPrimaryKey);
            var query = new StringBuilder($"SELECT * FROM {tableName}.{schema}");
            List<string> whereParts = new List<string>();
            List<OleDbParameter> oleDbParameters = new List<OleDbParameter>();
            var keysCounter = 0;
            foreach (var propety in primaryKeys)
            {
                var parmeterName = propety.ColumnName;
                whereParts.Add(propety.ColumnName + "=?");
                var parameterValue = keys[keysCounter++];
                oleDbParameters.Add(new OleDbParameter(parmeterName, parameterValue));
            }
            query.Append(" WHERE " + string.Join(" AND ", whereParts) + $" ORDER BY [ASC]");

            return RunQuery(query.ToString(), oleDbParameters.ToArray());

        }

        public List<DskWor00> GetAll()
        {
            var query = new StringBuilder($"SELECT * FROM {tableName}");
            return  RunQuery(query.ToString());
           
        }

        public int Count()
        {
            var query = new StringBuilder($"SELECT COUNT(*) FROM {tableName}");
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = new OleDbCommand(query.ToString(), connection);
                var resutl = command.ExecuteScalar();
                return int.Parse((resutl).ToString());
            }
        }

        public List<DskWor00> RunQuery(string query, params OleDbParameter[] parameters)
        {
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query.ToString();
                foreach (var oleDbParameter in parameters)
                {
                    command.Parameters.Add(oleDbParameter);
                }


                var reader = command.ExecuteReader();
                var entities = new List<DskWor00>();
                while (reader.Read())
                {

                    DskWor00 entity = Activator.CreateInstance<DskWor00>();

                    foreach (var propertyModel in propertyModels)
                    {
                        propertyModel.PropertyInfo.SetValue(entity, (reader[propertyModel.PropertyName] is DBNull ? null : reader[propertyModel.PropertyName]));
                    }

                    entities.Add(entity);
                }

                return entities;
            }
        }
         

        public int Update(DskWor00 entity)
        {
            var query = new StringBuilder($"UPDATE {tableName} ");
            var nonComputedColumn = propertyModels.Where(p => !p.IsComputed);
            var primaryKeys = propertyModels.Where(p => p.IsPrimaryKey);
            List<string> updateStatment = new List<string>();
            List<OleDbParameter> oleDbParameters = new List<OleDbParameter>();
            foreach (var model in nonComputedColumn)
            {
                var propertyValue = model.PropertyInfo.GetValue(entity);
                if (model.PropertyInfo.PropertyType == typeof(string) && !propertyValue.ToString().Contains("/"))
                    propertyValue = ((string)model.PropertyInfo.GetValue(entity)).ToIranSystem();
                updateStatment.Add(model.PropertyName + "=?");
                oleDbParameters.Add(new OleDbParameter(model.ColumnName, propertyValue));

            }
            query.Append(" SET " + string.Join(",", updateStatment));

            var whereParts = new List<string>();

            foreach (var key in primaryKeys)
            {
                whereParts.Add(key.ColumnName + "=?");
                oleDbParameters.Add(new OleDbParameter(key.ColumnName, key.PropertyInfo.GetValue(entity)));
            }

            query.Append(" WHERE " + String.Join(" AND ", whereParts) + ";");

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query.ToString();
                foreach (var parameter in oleDbParameters)
                {
                    command.Parameters.Add(parameter);

                }

                return command.ExecuteNonQuery();
            }


        }

        public void RefreshConnection(string dataSource)
        {
            var connectionStringBuilder = new OleDbConnectionStringBuilder(connectionString);
            connectionStringBuilder.DataSource = dataSource;
            connectionString = connectionStringBuilder.ConnectionString;
        }

        [ComVisible(true)]
        public void AddStandardDbfFiles(string path, bool create)
        {

            if (!Directory.Exists(path))
            {
                if (!create)
                {
                    MessageBox.Show(@"مسیر مورد نظر یافت نشد");
                    return;
                }
                Directory.CreateDirectory(path);
            }
            var dskDataSource = Path.Combine(path, "DSKKAR00.DBF");
            var dswDataSource = Path.Combine(path, "DSKWOR00.DBF");
            File.WriteAllBytes(dskDataSource, Properties.Resources.DSKWOR00);
            File.WriteAllBytes(dswDataSource, Properties.Resources.DSKWOR00);

        }

        [ComVisible(true)]
        public void FoxproToNetClassGenerator(string path, string connectonString, string className)
        {
            var connectionstringBuilder = new OleDbConnectionStringBuilder(connectonString);

            using (var connection = new OleDbConnection(connectonString))
            {
                connection.Open();
                var classLines = new List<string>();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {connectionstringBuilder.DataSource}";
                command.Connection = connection;
                var reader = command.ExecuteReader();
                classLines.Add("using System;");
                classLines.Add("namespace TaminIranSocialInsurance.Entities");
                classLines.Add("{");
                classLines.Add($"    public class {className}");
                classLines.Add("    {");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var columnType = reader.GetDataTypeName(i);
                    if (columnType == "DBTYPE_CHAR")
                        columnType = "string";
                    if (columnType == "DBTYPE_NUMERIC")
                        columnType = "decimal";

                    classLines.Add($"        public {columnType} {columnName} {{get;set;}}");

                }
                classLines.Add("    }");
                classLines.Add("}");

                File.WriteAllLines(Path.Combine(path, className + ".cs"), classLines);

            }
        }

        public string ConvertToUnicode(string inputString)
        {
            return ConvertTo.Unicode(inputString);
        }

        public string ConvertToIranSystem(string inputString)
        {
            return inputString.ToIranSystem();
        }
    }
}
