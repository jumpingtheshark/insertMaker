// reading files, writing files, parsing csv's


using DBHelper;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace InsertMaker
{
    class Program
    {


        static void Main(string[] args)
        {

            InsertModel.InsertModelFoo();

        }









            static void MainOld(string[] args)
        {

            Console.WriteLine("starting");

            // string string1 = "insert into lu_vehicle_make(uid, vehicle_make_uid, vehicle_type_uid, combo_display, effective_date, manually_entered, ncic_compliant_code, mf_code, code_name,  expiration_date, code_description, code_comment, indicates_unknown, vehicle_type, vintelligence )values(";

            string s1 = @"C:\github\InsertMaker\InsertMaker" + "\\insert.txt";
            string string1 = File.ReadAllText(s1);
            string s2 = string1;

            string[] lines = File.ReadAllLines(@"C:\m\file.csv");
            string[] fields;
            DBHelper.PSDBHelper db = new PSDBHelper();
            db.openConnection();
            Record r;

            StringBuilder sb = new StringBuilder();
            int i = 0;

            List<string> inserted = db.getList("select vehicle_make_uid from retail_in.lu_vehicle_make");

            foreach (string l in lines)
            {


                if (i > 0)// skip the header
                {
                    sb = new StringBuilder();
                    fields = l.Split(',');
                    r = new Record();
                    r._00uid = fields[0];
                    r._01vehicle_make_uid = fields[1];

                    if (inserted.Contains(r._01vehicle_make_uid.Trim()))
                        continue; 

                    r._02vehicle_type_uid = fields[2];
                    r._03combo_display = fields[3];
                    r._04effective_date = fields[4];
                    r._05manually_entered = fields[5];
                    r._06ncic_compliant_code = fields[6];
                    r._07mf_code = fields[7];
                    r._08code_name = fields[8];
                    r._10code_description = fields[10];
                    r._11code_comment = fields[11];
                    r._12indicates_unknown = fields[12];
                    r._13vehicle_type = fields[13];
                    r._14vintelligence = fields[14];

                    
                    sb.Append(string1);
                    sb.Append(r._00uid);
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(r._01vehicle_make_uid);
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(r._02vehicle_type_uid);
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._03combo_display));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._04effective_date));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._05manually_entered));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._06ncic_compliant_code));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._07mf_code));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);



                    sb.Append(quotes(r._08code_name));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);



                    sb.Append(quotes(r._10code_description));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._11code_comment));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._12indicates_unknown));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._13vehicle_type));
                    sb.Append(",");
                    sb.Append(Environment.NewLine);

                    sb.Append(quotes(r._14vintelligence));
                    sb.Append(")");
                    sb.Append(Environment.NewLine);
                    db.runCommand(sb.ToString());


                }

                Console.WriteLine(i.ToString() + " queries");
                i++;

            }
            db.closeConnection();
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
