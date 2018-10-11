using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;


public partial class MainTestTask : System.Web.UI.Page
{
    // imya faily
    private string filename = null;
    string filename2 = null;
    StreamReader s;


    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Visible = false;
        Label2.Visible = false;
        Label3.Text = "";
        Label4.Text = "";
    }

    // get file and save
    protected void ButtonBrowse_Click(object sender, EventArgs e)
    {
        if (FileUploadControl.HasFile)
        {
            try
            {
                filename = Path.GetFileName(FileUploadControl.FileName);
                FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                string path = Server.MapPath("~/" + filename);
                Label1.Text = filename;
                Label2.Text = path;
                filename2 = filename;
                Label3.Text = "OK <br> Filename - " + Label1.Text ;

            }
            catch (Exception)
            {
                Response.Write("Error.Cannot get file");
            }
        }
    }


    // searches words
    protected void ButtonSearchWord_Click(object sender, EventArgs e)
    {

        // SQLCONNECTION
        // SQL OPEN
        // SQL QUERY
        // SQL COMMAND
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestTaskConnectionString"].ConnectionString);
        sqlConnection.Open();
        string query = "insert into TestTaskTable (label, sentence, count) values (@label, @sentence, @count)";
        SqlCommand command = new SqlCommand(query, sqlConnection);


        // WORD - slovo yake treba shykatu
        string word = TextBoxWord.Text;
        
 
        
            //string path = Server.MapPath("~/" + filename);
            StreamReader s = new StreamReader(Label2.Text);
         
           
            string text = s.ReadToEnd();


            // split text + search if contain word
            string[] sentences = text.Split('.');
            string reversed;
            int count = 0;
            foreach (var sentence in sentences)
            {
                count = 0;
                if (sentence.Contains(word))
                {
                    // kilkist' vxodzhen' slova v 1 rechennya
                    foreach (Match match in Regex.Matches(sentence, word))
                    {
                        count++;
                    }


                    // zvorotni bykvu
                    reversed = ReverseLetter(sentence);


                    // zapus v db
                    command.Parameters.AddWithValue("@label", Label1.Text);
                    command.Parameters.AddWithValue("@sentence", reversed);
                    command.Parameters.AddWithValue("@count", count);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
            string query1 = "SELECT * FROM TestTaskTable";
            SqlCommand command1 = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader;
            reader = command1.ExecuteReader();

            s.Close();
        Label4.Text = "Reboot program to see changes.";
            sqlConnection.Close();

        

    }
    // zvorotnij napryamok bykv
    string ReverseLetter(string text)
    {
        // dlya ostann'ogo slova
        text += " ";


        string reversed = string.Empty;
        // POS - current posucia kyrsora v rechenni
        int pos = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == ' ')
            {
                string tmp = text.Substring(pos, i - pos);
                reversed += new string(tmp.Reverse().ToArray()) + " ";
                pos = i + 1;
            }
        }

        

        return reversed;
    }
}