using System;
using Gtk;
using Calculus;

public partial class MainWindow : Gtk.Window
{
    Gtk.ListStore musicListStore;
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
#pragma warning disable RECS0021 // Avisa sobre las llamadas a funciones de miembros virtuales que tienen lugar en el constructor
        Build();
#pragma warning restore RECS0021 // Avisa sobre las llamadas a funciones de miembros virtuales que tienen lugar en el constructor
        Gtk.TreeViewColumn ColumnaIt = new TreeViewColumn
        {
            Title = "Numero",
            //MinWidth = 20,
            //MaxWidth = 50,

            Expand = true,
            Sizing = TreeViewColumnSizing.Fixed

        };
        Gtk.CellRendererPixbuf activeCell = new Gtk.CellRendererPixbuf();
        ColumnaIt.PackStart(activeCell, true);
        ColumnaIt.AddAttribute(activeCell, "pixbuf", 6);
        Gtk.TreeViewColumn Columnaxl = new TreeViewColumn
        {
            Title = "Xl",
            //MinWidth = 20,
            //MaxWidth = 50,

            Expand = true,
            Sizing = TreeViewColumnSizing.Fixed

        };
        Columnaxl.PackStart(activeCell, true);
        Columnaxl.AddAttribute(activeCell, "pixbuf", 6);
        Gtk.TreeViewColumn Columnaxu = new TreeViewColumn
        {
            Title = "Xu",

            Expand = true,            
            Sizing = TreeViewColumnSizing.Fixed
        };
        Columnaxu.PackStart(activeCell, true);
        Columnaxu.AddAttribute(activeCell, "pixbuf", 6);
        Gtk.TreeViewColumn Columnaxr = new TreeViewColumn
        {
            Title = "Xr",

            Expand = true,
            Sizing = TreeViewColumnSizing.Fixed
        };
        Columnaxr.PackStart(activeCell, true);
        Columnaxr.AddAttribute(activeCell, "pixbuf", 6);
        Gtk.TreeViewColumn Columnaet = new TreeViewColumn
        {
            Title = "Et",

            Expand = true,
            Sizing = TreeViewColumnSizing.Fixed
        };
        Columnaet.PackStart(activeCell, true);
        Columnaet.AddAttribute(activeCell, "pixbuf", 6);
        Gtk.TreeViewColumn Columnaea = new TreeViewColumn
        {
            Title = "Ea",

            Expand = true,
            Sizing = TreeViewColumnSizing.Fixed
        };
        Columnaea.PackStart(activeCell, true);
        Columnaea.AddAttribute(activeCell, "pixbuf", 6);
        Tabla.AppendColumn(ColumnaIt);
        Tabla.AppendColumn(Columnaxl);
        Tabla.AppendColumn(Columnaxu);
        Tabla.AppendColumn(Columnaxr);
        Tabla.AppendColumn(Columnaea);
        Tabla.AppendColumn(Columnaet);
        Gtk.CellRendererText xlNameCell = new Gtk.CellRendererText();
        Columnaxl.PackStart(xlNameCell, true);
        Gtk.CellRendererText xuNameCell = new Gtk.CellRendererText();
        Columnaxu.PackStart(xuNameCell, true);
        Gtk.CellRendererText ItNameCell = new Gtk.CellRendererText();
        ColumnaIt.PackStart(ItNameCell, true);
        Gtk.CellRendererText xrNameCell = new Gtk.CellRendererText();
        Columnaxr.PackStart(xrNameCell, true);
        Gtk.CellRendererText eaNameCell = new Gtk.CellRendererText();
        Columnaea.PackStart(eaNameCell, true);
        Gtk.CellRendererText etNameCell = new Gtk.CellRendererText();
        Columnaet.PackStart(etNameCell, true);
        ColumnaIt.AddAttribute(ItNameCell, "text", 0);
        Columnaxl.AddAttribute(xlNameCell, "text", 1);
        Columnaxu.AddAttribute(xuNameCell, "text", 2);
        Columnaxr.AddAttribute(xrNameCell, "text", 3);
        Columnaea.AddAttribute(eaNameCell, "text", 4);
        Columnaet.AddAttribute(etNameCell, "text", 5);
        musicListStore = new Gtk.ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
        //musicListStore.AppendValues("1", "Garbage", "Dog","xxxx","x");
        Tabla.Model = musicListStore;
        this.ShowAll();

    }


    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
    private void CalcularFPosicion()
    {
        try
        {
            if(AnalizarFuncion()==false)
            {
                //si la funcion no es valida saldra un messagedialog para decir que habia un error
                MessageDialog mensaje = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "La funcion esta mal escrita");
                mensaje.Run();
                mensaje.Destroy();
            }
            else if (txtXu.Text != "" && txtXl.Text != ""&& txtVVerdadero.Text != "" && (txtEs.Text!=""||txtLimite.Text!=""))
            {
                musicListStore.Clear();//Se limpia la tabla si se cumplen las condiciones
                //Si cumple la condicion de los textbox de Xu, xl y alguno de los dos textbox de limite o  Es de no estar vacios continua.
            if (rdbEs.Active)//Si el radiobutton de la condicion del Es esta activada se hara lo siguiente:
                {

                    double es = double.Parse(txtEs.Text);//Se obtiene el valor del textbox de la condicion Es
                    MetodoFalsaPosicion(es);
                    //Se ejecutara el metodo Calculo(es) que hara la operacion si Ea < Es 
                }
                if (rdbLimite.Active)//Si el radiobutton de la condicion del Es esta activada se hara lo siguiente:
                {
                    int limite = int.Parse(txtLimite.Text);//Se obtiene el entero del textbox del limite de iteraciones
                    MetodoFalsaPosicion(limite);
                    //Se ejecutara el metodo Calculo(es) que hara la operacion hasta el limite introducido
                }
            }
            else
            {
                MessageDialog mensajes = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor introduce los datos en las casillas");
                mensajes.Run();
                mensajes.Destroy();
            }

        }
        catch (Exception ex)
        {
            throw new Exception("" + ex);
        }

    }
    private void Calculo(double Es)
    {
        try
        {
            double Xr = 0; double Xl = double.Parse(txtXl.Text);//Estos valores son los valores Xl y Xr
            double Xu = double.Parse(txtXu.Text);//Este valor contiene a Xu
            double Ea = 0;//Este es el valor que se comprbara con la condicion establecida de este metodo
            int It = 1;//El numero de iteraciones
            double XX = 0;//El valor de la multiplicacion de las funciones con sustituyendo x con Xl y Xr
            bool blnContinua = true;
            do
            {
                Xr = (Xl + Xu) / 2;
                double XrViejo = Xr;
                if (Math.Abs(Xu + Xl) > 0)//Se comprara si no se divide entre cero
                {
                    Ea = Math.Abs((Xu - Xl) / (Xu + Xl)) * 100;//La formula de Ea
                    musicListStore.AppendValues($"{It}", $"{Xl}", $"{Xu}", $"{Xr}", $"{Ea}");
                }
                else
                {
                    musicListStore.AppendValues($"{Xl}", $"{Xu}", $"{Xr}", "Indeterminada");
                    //Si no se puede dividir mostrara en la tabla la frase de "indeterminada"
                }
                XX = AnalizarFuncion(Xl) * AnalizarFuncion(Xr);//La formula para comprobar si se sustituira Xu o Xl
                if (XX < 0)
                {
                    Xu = Xr;
                }
                else if (XX > 0)
                {
                    Xl = Xr;
                }
                else
                {
                    blnContinua = false;
                    //Si llegara a suceder que fuera un cero exacto se terminaria las iteracionees
                }

                //Tabla.Model = musicListStore;
                It++;
            } while (blnContinua && Ea>=Es);
            //Si es cero o si se cumple la condicion se rompe el ciclo

        }
        catch (Exception ex)
        {
            throw new Exception("" + ex);
        }
    }
    private void Calculo(int Numero_iteraciones)
    {
        try
        {
            double Xr = 0; double Xl = double.Parse(txtXl.Text);
            double Xu = double.Parse(txtXu.Text);
            double Ea = 0;
            int Iteracion = 1;
            double XX = 0;//El valor de la multiplicacion de las funciones con sustituyendo x con Xl y Xr
            bool blnContinua = true;
            do
            {
                Xr = (Xl + Xu) / 2;
                double XrViejo = Xr;
                if (Xr < 0 && Xr > 0)
                {
                    Ea = Math.Abs((Xr - XrViejo) / Xr) * 100;
                    musicListStore.AppendValues($"{Iteracion}", $"{Xl}", $"{Xu}", $"{Xr}", $"{Ea}");
                }
                else
                {
                    musicListStore.AppendValues($"{Xl}", $"{Xu}", $"{Xr}", "Indeterminada");
                }
                XX = AnalizarFuncion(Xl) * AnalizarFuncion(Xr);//La formula para comprobar si se sustituira Xu o Xl
                if (XX < 0)
                {
                    Xu = Xr;
                }
                else if (XX > 0)
                {
                    Xl = Xr;
                }
                else
                {
                    blnContinua = false;
                }

                Iteracion++;
            } while (blnContinua && Iteracion==Numero_iteraciones);
            //Si es cero o si es igual el numero de iteraciones al introducido como parametro se rompe el ciclo
            //Este es el metodo que limita el numero de iteraciones
        }
        catch (Exception ex)
        {
            throw new Exception("" + ex);
        }
    }
    protected void SalirClick(object sender, EventArgs e)
    {
        Application.Quit();
    }

    protected void CalcularSalir(object sender, EventArgs e)
    {
        try
        {
            //Este metodo se utiliza cuando se pulsa el boton Calcular
            //Se llama al metodo CalcularFPosicion()
            CalcularFPosicion();
        }
        catch (Exception ex)
        {
            MessageDialog mensaje = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Error: " + ex);
            mensaje.Run();
            mensaje.Destroy();
        }
    }

    protected void LimiteChanged(object sender, EventArgs e)
    {
        if(rdbLimite.Active)
        {
            txtLimite.IsEditable = true;
            txtLimite.Text = "";
        }else
        {
            txtLimite.IsEditable = false;
            txtLimite.Text = "";
        }
    }

    protected void EsChanged(object sender, EventArgs e)
    {
        if (rdbEs.Active)
        {
            txtEs.IsEditable = true;
            txtEs.Text = "";
        }
        else
        {
            txtEs.IsEditable = false;
            txtEs.Text = "";
        }
    }

    protected void Prueba(object sender, EventArgs e)
    {
        //Este boton puede ser usada si quieres comprobar antes de calcular si tu funcion es correcta
        try
        {
            if (AnalizarFuncion())
            {
                MessageDialog mensaje = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Salio el resultado: "+fxx);
                mensaje.Run();
                mensaje.Destroy();
            }
            else
            {
                MessageDialog mensaje = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Salio error ");
                mensaje.Run();
                mensaje.Destroy();
            }
        }
        catch(Exception ex)
        {
            MessageDialog mensaje = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Error: " + ex);
            mensaje.Run();
            mensaje.Destroy();
        }

    }
    private double fxx=0;
    private bool AnalizarFuncion()//Metod si comprueba si esta correctamente escrita la funcion
    {
        try
        {
            Calculo pc = new Calculo();
            if (pc.Sintaxis(txtFuncion.Text, 'x'))//Se introduce en el metodo Sintaxis los parametros que son la funcion y la letra que se cambia
                //en este caso es x
            {
                double fx = pc.EvaluaFx(1);//Evalua la funcion sustituyendo x por 1
                fxx = fx;
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("" + ex);
        }
    }
    private double AnalizarFuncion(double _dblValorX)//Este metodo analiza y devuelve el resultado de la funcion escrita
    {
        try
        {
            Calculo pc = new Calculo();//Se llama a la clase Calculo
            if (pc.Sintaxis(txtFuncion.Text, 'x'))//Se llama al metodo de la clase para comprobar si la funcion funciona
            {
                double fx = pc.EvaluaFx(_dblValorX);//Realiza la operacion sustituyendo x por le valor del parametro

                return fx;
            }else
            {
                MessageDialog mensaje = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Error ");
                mensaje.Run();
                mensaje.Destroy();
                throw new Exception("Ocurrio un error: la funcion no es correcta");

            }
        }
        catch (Exception ex)
        {
            throw new Exception("" + ex);
        }
    }

    protected void LimpiarButton(object sender, EventArgs e)
    {
        txtFuncion.Text = "";
        txtEs.Text = "";
        txtXl.Text = "";
        txtXu.Text = "";
        txtLimite.Text = "";
        txtVVerdadero.Text = "";
        musicListStore.Clear();
    }

    protected void AgregarSigno1(object sender, EventArgs e)
    {
        string var = txtFuncion.Text;
        txtFuncion.Text = $"{var}^";//Se agrega al textbox de la funcion el caracter '^'
    }
    protected void Signoe(object sender, EventArgs e)
    {
        string var = txtFuncion.Text;
        txtFuncion.Text = $"{var}exp(x)";//Se agrega al textbox de la funcion la forma que interpreta e
    }

    private void MetodoFalsaPosicion(double Es)
    {
        try
        { 
            //Se declaran los datos
            double Xl = double.Parse(txtXl.Text);
            double Xu = double.Parse(txtXu.Text);
            double Xr, Et, Ea, XrViejo;
            int It = 1;//el numero de iteraciones
            bool blnContinuar = true;/*el valor booleano es para continuar la iteracion*/
            double Fxl = AnalizarFuncion(Xl);/*Se llama el metodo AnalizarFuncion(Xl) para obtener el resultado de la funcion dada
            por el usuario reemplazando la variable por el parametro dado*/
            double Fxu = AnalizarFuncion(Xu);
            //Se aplica la formula para xr
            Xr = Xu - (Fxu *(Xl   - Xu )  / (Fxl - Fxu));
            double Vverdadero = double.Parse(txtVVerdadero.Text);
            Et = (Math.Abs((Vverdadero - Xr) / Vverdadero) * 100);
            musicListStore.AppendValues($"{It}", $"{Xl}", $"{Xu}", $"{Xr}", $"0", $"{Et}");//Se introducen los datos en la tabla
            double xx = Fxl * AnalizarFuncion(Xr);
            /*xx es la mutiplicacion de la funcion con el valor xl y con la funcion con el valor xr*/
            if(xx>0)
            {
                Xl = Xr;//Si la variable xx es positiva Xl sera igual a Xr
                do
                {
                    It++;
                    Fxl = AnalizarFuncion(Xl);//Se reemplaza la variable por el valor del parametro xl de esta iteracion
                    Fxu = AnalizarFuncion(Xu);//Se reemplaza la variable por el valor del parametro xu de esta iteracion
                    XrViejo = Xr;//Se guarda el Xr viejo
                    Xr = Xu - ((Fxu * (Xl - Xu) / (Fxl - Fxu)));//Se usa la formula para obtener el nuevo xr
                    Et = (Math.Abs((Vverdadero - Xr) / Vverdadero) * 100);//La formula del error verdadero
                    Ea = (Math.Abs((Xr - XrViejo) / Xr)) * 100;//La formula de Error absoluto
                    xx = Fxl * Fxu;
                    if(xx>0)
                    {
                        Xl = Xr;//Si la variable xx es positiva Xl sera igual a Xr
                    }
                    else if (xx < 0)
                    {
                        Xu = Xr;//Si la variable xx es positiva Xu sera igual a Xr
                    }
                    else
                    {
                        blnContinuar = false;//Si llega a ser 0 es termina la iteracion
                    }
                    musicListStore.AppendValues($"{It}", $"{Xl}", $"{Xu}", $"{Xr}", $"{Ea}", $"{Et}");
                    //Se introducen los datos en la tabla
                } while (blnContinuar && Ea>=Es);
                //si la variable blncontinuar es verdadera y la condicion Ea es mayor o igual a Es dado como parametro
            }
            else if(xx<0)
            {
                Xu = Xr;//Si la variable xx es positiva Xu sera igual a Xr
                do
                {
                    It++;
                    Fxl = AnalizarFuncion(Xl);
                    Fxu = AnalizarFuncion(Xu);
                    XrViejo = Xr;
                    Xr = Xu - ((Fxu * (Xl - Xu) / (Fxl - Fxu)));
                    Et = (Math.Abs((Vverdadero - Xr) / Vverdadero) * 100);
                    Ea = (Math.Abs((Xr - XrViejo) / Xr)) * 100;
                    xx = Fxl * Fxu;
                    if (xx > 0)
                    {
                        Xl = Xr;
                    }
                    else if (xx < 0)
                    {
                        Xu = Xr;
                    }
                    else
                    {
                        blnContinuar = false;
                    }
                    musicListStore.AppendValues($"{It}", $"{Xl}", $"{Xu}", $"{Xr}", $"{Ea}", $"{Et}");
                } while (blnContinuar && Ea >= Es);
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw new Exception("" + ex);
        }
    }
    private void MetodoFalsaPosicion(int Limite)
    {
        try
        {
            double Xl = double.Parse(txtXl.Text);
            double Xu = double.Parse(txtXu.Text);
            double Xr, Et, Ea, XrViejo;
            int It = 1;
            bool blnContinuar = true;
            double Fxl = AnalizarFuncion(Xl);
            double Fxu = AnalizarFuncion(Xu);
            Xr = Xu - (Fxu * (Xl - Xu) / (Fxl - Fxu));
            double Vverdadero = double.Parse(txtVVerdadero.Text);
            Et = (Math.Abs((Vverdadero - Xr) / Vverdadero) * 100);
            musicListStore.AppendValues($"{It}", $"{Xl}", $"{Xu}", $"{Xr}", $"0", $"{Et}");
            double xx = Fxl * AnalizarFuncion(Xr);
            if (xx > 0)
            {
                Xl = Xr;
                do
                {
                    It++;
                    Fxl = AnalizarFuncion(Xl);
                    Fxu = AnalizarFuncion(Xu);
                    XrViejo = Xr;
                    Xr = Xu - ((Fxu * (Xl - Xu) / (Fxl - Fxu)));
                    Et = (Math.Abs((Vverdadero - Xr) / Vverdadero) * 100);
                    Ea = (Math.Abs((Xr - XrViejo) / Xr)) * 100;
                    xx = Fxl * Fxu;
                    if (xx > 0)
                    {
                        Xl = Xr;
                    }
                    else if (xx < 0)
                    {
                        Xu = Xr;
                    }
                    else
                    {
                        blnContinuar = false;
                    }
                    musicListStore.AppendValues($"{It}", $"{Xl}", $"{Xu}", $"{Xr}", $"{Ea}", $"{Et}");
                } while (blnContinuar && It < Limite);
                //Igual que el metodo de la condicion Ea>=Es pero ahora la iteracion pide que 
                //blnContinuar sea verdadero y la variable iteracion sea menor al limite dado como parametro
                }
            else if (xx < 0)
            {
                Xu = Xr;
                do
                {
                    It++;
                    Fxl = AnalizarFuncion(Xl);
                    Fxu = AnalizarFuncion(Xu);
                    XrViejo = Xr;
                    Xr = Xu - ((Fxu * (Xl - Xu) / (Fxl - Fxu)));
                    Et = (Math.Abs((Vverdadero - Xr) / Vverdadero) * 100);
                    Ea = (Math.Abs((Xr - XrViejo) / Xr)) * 100;
                    xx = Fxl * Fxu;
                    if (xx > 0)
                    {
                        Xl = Xr;
                    }
                    else if (xx < 0)
                    {
                        Xu = Xr;
                    }
                    else
                    {
                        blnContinuar = false;
                    }
                    musicListStore.AppendValues($"{It}", $"{Xl}", $"{Xu}", $"{Xr}", $"{Ea}", $"{Et}");
                } while (blnContinuar && It < Limite);
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw new Exception("" + ex);
        }
    }
    private void MensajeBox(string mss)
    {
        MessageDialog mensaje = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, $"{mss}");
        mensaje.Run();
        mensaje.Destroy();
    }
}
