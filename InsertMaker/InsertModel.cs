using System;
using System.Collections.Generic;
using System.Text;
using DBHelper;
using System.IO;


namespace InsertMaker
{
    class InsertModel
    {

        public static void InsertModelFoo()
        {

            // string string1 = "insert into lu_vehicle_make(uid, vehicle_make_uid, vehicle_type_uid, combo_display, effective_date, manually_entered, ncic_compliant_code, mf_code, code_name,  expiration_date, code_description, code_comment, indicates_unknown, vehicle_type, vintelligence )values(";

            string s1 = @"C:\github\InsertMaker\InsertMaker" + "\\insert_model.txt";
            string string1 = File.ReadAllText(s1);
            string s2 = string1;

            string[] lines = File.ReadAllLines(@"C:\m\models.csv");
            string[] fields;
            DBHelper.PSDBHelper db = new PSDBHelper();
            db.openConnection();
            RecordModel r;

            StringBuilder sb = new StringBuilder();
            int i = 0;
            string l2;
            List<string> inserted = db.getList("select vehicle_model_uid from retail_in.lu_vehicle_model");
            string s="";
            foreach (string l in lines)
            {


                if (i > 0)// skip the header
                {
                    sb = new StringBuilder();
                    l2 = sq(l);
                    fields = l2.Split(',');
                    r = new RecordModel();
                    r._00id = fields[0];
                    r._01vehicle_model_uid = fields[1];

                    if (inserted.Contains(r._01vehicle_model_uid.Trim()))
                        continue;

                    r._02vehicle_make_uid = fields[2];

                    r._03effective_date = quotes(fields[3]);



                    s = fields[4];
                    if (s == "NULL") s = "nothing";
                    r._04manually_entered = s;



                    s = fields[5];
                    if (s == "NULL") s = "nothing";
                    r._05pcvina_model_code = quotes(s);


                    
                    r._06pcvina_series_code = quotes(replaceNull(fields[6]));
                    r._07code_name = quotes(replaceNull(fields[7]));

                    //db complaining about nulls
                    s = fields[8];
                    if (s == "NULL") s = "nothing";
                    r._08code_description = quotes( s);

                    s = fields[9];
                    if (s == "NULL") s = "nothing";
                    r._09indicates_unknown = quotes(s);
                   

                    sb.Append(string1);
                    sb.Append(r._00id);
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(r._01vehicle_model_uid);
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(r._02vehicle_make_uid);
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append((r._03effective_date));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append((r._04manually_entered));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append((r._05pcvina_model_code));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append((r._06pcvina_series_code));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append((r._07code_name));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);


                    sb.Append((r._08code_description));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);



                    sb.Append((r._09indicates_unknown));
                    sb.Append(")");
                    sb.Append(Environment.NewLine);

                    db.runCommand(sb.ToString());


                }

                Console.WriteLine(i.ToString() + " queries");
                i++;

            }
            db.closeConnection();






        }


        public static string replaceNull (string s)
        {

            if (s == "NULL")
                return "nothing";
            else
                return s;

        }

        public static string sq (string s )
        {
            s = s.Replace("'", @"''");
            return s;

        }
        public static string quotes(string s)
        {
            if (s == "NULL")
                return "NULL";
            s = "'" + s + "'";
            return s;

        }

    }
}
