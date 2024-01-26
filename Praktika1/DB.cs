using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Npgsql.Internal;

namespace Praktika1
{
    internal class DB
    {
        NpgsqlConnection connection = new NpgsqlConnection("User ID = postgres; Password = 12345; Host = localhost; Port = 5432; database = Dokyment;");

       

        public DataTable GetUser(string username, string password)
        {
            connection.Open();

            DataSet dataSet = new DataSet();
            DataTable dataTable= new DataTable();
            string command = $"Select * From Managment Where loginn = '{username}' and passwordd = '{password}' ;";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;
            connection.Close();


        }


        public DataTable GetRights(string username, string password)
        {
           

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Select ID From Managment Where loginn = '{username}' and passwordd = '{password}' ;";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;
            connection.Close();


        }


        public void SetPassword(string password, string username)
        {
        

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"UPDATE managment SET passwordd = '{password}' WHERE loginn = '{username}';";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);


            connection.Close();
        }
        public DataTable CheckUser(string username)
        {
            connection.Open();

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Select * From Managment Where loginn = '{username}'";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;
            connection.Close();


        }

        public DataTable GetProgramms()
        {
            connection.Open();

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Select * From Educational_program";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;
            connection.Close();


        }

        public DataTable GetPerson()
        {
           

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Select * From Fiz_person";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;
            connection.Close();


        }

        public void SetProgramm(string id, string title, string term, string qualification, string costs)
        {


            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Insert Into educational_program Values ({id},'{title}',{term},'{qualification}',{costs})";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

          
            connection.Close();


        }

    }
}
