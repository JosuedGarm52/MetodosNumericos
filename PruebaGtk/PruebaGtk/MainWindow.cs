using System;
using Gtk;
using PruebaGtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnSalirClicked(object sender, EventArgs e)
    {
        Application.Quit();
    }

    protected void OnBtnCalcularClicked(object sender, EventArgs e)
    {
        try
        {
            if (txtVtrue.Text != "" && txtVaprox.Text != "")//Pregunta si los campos de valor verdadero y aproximado no estan vacios
            {
                    Calcular();
            }
            else
            {
                //Si lo estan se abre un MessageDialog que le indica que debe rellenar los campos
                MessageDialog mdd = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor introduce los valores en las casillas requeridas.");
                mdd.Run();
                mdd.Destroy();
            }

        }
        catch(Exception ex)
        {
            //Si algo falla en el programa salta una excepcion
            MessageDialog mddd = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Sucedio el error:"+ex);
            mddd.Run();
            mddd.Destroy();
        }
        //Una vez calcula todas las operaciones pregunta si desea realizar otra operacion
        MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, "¿Desea volver a calcular?");
        md.SetPosition(WindowPosition.Mouse);
        ResponseType result = (ResponseType)md.Run();
        if (result == ResponseType.Yes)
        {
            //Si responde que si los campos se limpian
            txtErp.Text = "";// se limpia el textbox del error relativo porcentual
            txtEabs.Text = "";// se limpia el textbox del error absoluto
            txtErel.Text = "";// se limpia el textbox del error relativo 
            txtVtrue.Text = "";// se limpia el textbox del valor verdadero
            txtVaprox.Text = "";// se limpia el textbox del valor aproximado
        }
        else
        {
            //Si no, cierra el MessageDialog y la aplicacion
            md.Destroy();
            Application.Quit();
        }
        md.Destroy();
    }
    private void Calcular()
    {
        try
        {
            double V = double.Parse(txtVtrue.Text);//Esta variable guarda el valor del textbox del valor verdadero
            double A = double.Parse(txtVaprox.Text);//Esta variable guarda el valor del textbox del valor aproximado
            Calculo error = new Calculo(V, A);/*Se declara el objeto calculo con los parametros de las variables anteriores */
            txtEabs.Text = $"{error.CalcularErrorAbsoluto()}";
            //Se introduce el reslutado del metodo que calcula el error absoluto en el textbox del error absoluto
            txtErel.Text = $"{error.CalcularErrorRelativo()}";
            //Se introduce el reslutado del metodo que calcula el error relativo en el textbox del error relativo
            txtErp.Text = $"{error.CalcularErrorRelativoPorcentual()}" + " %";
            //Se introduce el reslutado del metodo que calcula el error relativo porcentual en el textbox del 
            //error relativo porcentual 
        }
        catch (Exception ex)
        {
            throw new Exception("Error" + ex);
        }
    }
}
