using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.IO;
using System.Text;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string vid =TextBox1.Text.Substring(TextBox1.Text.IndexOf("=")+1,11);
        HttpWebRequest request = WebRequest.Create("http://gdata.youtube.com/feeds/api/videos/"+vid) as HttpWebRequest;
        // Get response 
        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        {
            // Load data into a dataset 
            DataSet ds = new DataSet();
            ds.ReadXml(response.GetResponseStream());  //now you have all the details
            DataTable dtTitle = new DataTable();
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.TableName.ToLower() == "title")
                {
                    dtTitle = dt;
                    break;
                }
            }
            //Response.Write("<object width='480' height='385'><param name='movie' value='http://www.youtube.com/v/" + vid + "?fs=1&amp;hl=en_GB'></param><param name='allowFullScreen' value='true'></param><param name='allowscriptaccess' value='always'></param><embed src='http://www.youtube.com/v/" + vid + "?fs=1&amp;hl=en_GB' type='application/x-shockwave-flash' allowscriptaccess='always' allowfullscreen='true' width='480' height='385'></embed></object>");
            StringBuilder sbr = new StringBuilder();
            sbr.Append("<table><tr><td valign='top'><img src='" + ds.Tables["thumbnail"].Rows[0][0] + "' /></td>");
            sbr.Append("<td valign='top'><b>" + dtTitle.Rows[0][1] + "</b><br/>");
            sbr.Append(ds.Tables["description"].Rows[0][1] + "</td></tr></table>");
            Label1.Text = sbr.ToString();
          }
    }
}
