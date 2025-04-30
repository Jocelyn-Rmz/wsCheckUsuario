using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsCheckUsuario.Models;

namespace wsCheckUsuario
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Configurar el evento PageIndexChanging del 
            //GridView1
            GridView1.PageIndexChanging += GridView1_PageIndexChanging;

            await cargaDatosApi();
        }

        private void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Actualizar el indice de pagina del GridView1
            //Actualizar los datos del GridView1

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            // throw new NotImplementedException();
        }

        // Método asincrono para ejecutar: vwRptUsuario
        private async Task cargaDatosApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Ejecución de la petición de un endpoint a una webApi
                    string apiUrl = "https://localhost:44335/check/usuario/vwrptusuario?filtro=" + HttpUtility.UrlEncode(TextBox1.Text);
                    HttpResponseMessage respuesta =
                                    await client.GetAsync(apiUrl);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();

                    // Validación de estado de ejecución
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        // ------------------------------------------
                        JArray jsonArray = (JArray)objRespuesta.datos["vwRptUsuario"];
                        // Convertir JArray a DataTable
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                        // -------------------------------------------
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Error de conexión con webapi');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('Error inesperado ...');</script>");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Al escribir texto, recarga los datos con el filtro
            _ = cargaDatosApi(); // Ignorar el await ya que es un evento no async
        }
    }
}