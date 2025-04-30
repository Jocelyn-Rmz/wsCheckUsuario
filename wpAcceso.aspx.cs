using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//------------------------------
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using wsCheckUsuario.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wsCheckUsuario
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //------------------------------------------
        //Proceso asincrono para ejecucion del método (acceso)

        private async Task cargaDatosApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Contenido para enviarse al endpoint
                    String datos = @"{
                                    ""usuario"":""" + TextBox1.Text + "\"," +
                                    "\"contrasena\":\"" + TextBox2.Text + "\"" +
                                    "}";
                    // Configurar el envío del contenido
                    HttpContent contenido =
                            new StringContent(datos, Encoding.UTF8, "application/json");
                    string urlApi = "https://localhost:44335/check/usuario/spvalidaracceso";
                    // Ejecución del endpoint
                    HttpResponseMessage respuesta =
                            await client.PostAsync(urlApi, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // Se debe importar el modelo de salida clsApiStatus!
                    // ---------------------------------------------------
                    if (respuesta.IsSuccessStatusCode)
                    {
                        //--------------------------------
                        string resultado =
                                        await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        //---------------------------------

                        if (objRespuesta.ban == 1)
                        {
                            // Usuario válido, actualización de la Sesión 

                            Session["nomUsuario"] = objRespuesta.datos["usu_nombre_completo"].ToString();
                            Session["urlUsuario"] = objRespuesta.datos["usu_ruta"].ToString();
                            Session["usuUsuario"] = objRespuesta.datos["usu_usuario"].ToString();
                            Session["rolUsuario"] = objRespuesta.datos["tip_descripcion"].ToString();

                            Response.Write("<script language='javascript'>" +
                                "alert ('Bienvenido (a): " + 
                                    Session["nomUsuario"].ToString()
                                         + "');"+
                                "</script>");

                            Response.Write("<script language='javascript'>" +
                                "document.location.href='WebForm2.aspx'; " +
                                "</script>");
                        }
                        else
                        {

                            // Usuario NO valido resetear la Sesión

                            Session["nomUsuario"] = "";
                            Session["urlUsuario"] = "";
                            Session["usuUsuario"] = "";
                            Session["rolUsuario"] = "";

                            Response.Write("<script language='javascript'>" +
                               "alert ('Acceso Denegado....');" +
                               "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                               "alert ('Falló la conexión con el servidor, "+
                               "        intentar mas tarde ');" +
                               "</script>");
                    }

                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
                Response.Write(ex.InnerException.ToString());

                Response.Write("<script language='javascript'>" +
                               "alert ('Sucedio un error en el acceso a la aplicación," +
                               " contacte al administrador del sistema.');" +
                               "</script>");
            }

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            //Ejecucion asicrona del método cargaDatosApi()
            await cargaDatosApi();
        }
    }
}