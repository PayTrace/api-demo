using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetClientEncryptionExample
{
    public partial class ClientSideEncryptionDemo : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                writeResults(FormSubmit());
            }
        }

        /*public void BtnSubmitClicked(object sender, EventArgs args)
        {
            if(this.IsPostBack)
            {
                writeResults(FormSubmit());
            }
        }*/

        protected Boolean FormSubmit()
        {
            string encrypted_ccNumber = Request.Form["ccNumber"];
            string encrypted_ccCSC = Request.Form["ccCSC"];

            // to see the encrypted data in the response,prepending encrypted_ to the name of the property. 
            Response.Write("encypted_ccNumber :" + encrypted_ccNumber + "<br>" + "<br>");
            Response.Write("encrypted_ccCSC :" + encrypted_ccCSC + "<br>" + "<br>");

            // pass the encrypted data onto the API here
            // return true if the call to the API succeeds
            return true;

        }

        protected void writeResults(Boolean results)
        {
            if (results == true)
            {
                Response.Write("Success" + "<br>");
            }
            else
            {
                Response.Write("Failed" + "<br>");
            }
        }
    }
}