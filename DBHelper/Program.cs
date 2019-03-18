using System;
using Npgsql;
using System.Data;
using System.Collections.Generic;



namespace DBHelper
{

    public  class PSDBHelper
    {
        public string _constring = "Host=test.db.vitu.com;Username=postgres;Password=postgres;Database=vitu";
        public NpgsqlConnection _connection { get; set; } = new NpgsqlConnection(); 
        

        public DataTable getTable (string query)
        {
       
            NpgsqlDataAdapter a = new NpgsqlDataAdapter(query, _connection);

            DataTable dt = new DataTable();
            a.Fill(dt);



            return dt;

          
        }
       

        public Dictionary<string, string> dt2Diq(DataTable dt)
        {

            Dictionary<string, string> d = new Dictionary<string, string>();
           foreach (DataRow dr in dt.Rows)
            
                d.Add(dr[0].ToString(), dr[1].ToString());


            return d;

        }

        public List<string> getList(string query)
        {
            DataTable dt= getTable(query);

            List<string> l = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                l.Add(dr[0].ToString());

            }

            return l;

        }


        public List<string> dt2List(DataTable dt)
        {
            List<string> l = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                l.Add(dr[0].ToString());

            }

            return l;

        }


        public NpgsqlDataReader  getReader(string query)
        {
           
            string sql = "select * from retail_in.lu_vehicle_make";

            NpgsqlDataReader reader;
            //NpgsqlDataAdapter npgsqlDataAdapter


         
            var conn = new NpgsqlConnection(_constring);
            conn.Open();
            var cmd = new NpgsqlCommand(sql);
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            

            conn.Close();

            return reader;


        }

        public void openConnection()
        {
            _connection.ConnectionString = _constring;
            _connection.Open();
        }

        public void closeConnection()
        { 
            _connection.Close();
          
        }



        public void runCommand(string command)
        {

            
      
            var cmd = new NpgsqlCommand(command);
            cmd.Connection = _connection;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception: ");
                Console.WriteLine(ex.ToString());
            }
            
        }



    }

    class Program
    {
        static void Main(string[] args)
        {

            /*
             
            tables : 
        insert to 
        lu_vehicle_make 
        lu_vehicle_model

------
            schema: Retail_in
            Server: test.db.vitu.com
            Db: vitu
            Schema: retail_in



        

/*

using (var conn = new NpgsqlConnection(connString))
{
    conn.Open();

    // Insert some data
    using (var cmd = new NpgsqlCommand())
    {
        cmd.Connection = conn;
        cmd.CommandText = "INSERT INTO data (some_field) VALUES (@p)";
        cmd.Parameters.AddWithValue("p", "Hello world");
        cmd.ExecuteNonQuery();
    }

    // Retrieve all rows
    using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
    using (var reader = cmd.ExecuteReader())
        while (reader.Read())
            Console.WriteLine(reader.GetString(0));
}

            */
            Console.WriteLine("Hello World!");
        }
    }
}
