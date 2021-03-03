using System;
namespace PruebaGtk
{
    public class Calculo
    {
        public Calculo(double dblVerdadero,double dblAproximado)
        {
            _dblValorVerdadero = dblVerdadero;
            _dblValorAproximado = dblAproximado;
        }
        private double _dblValorVerdadero;//Valor verdadero
        public double VVerdadero
        {
            get { return _dblValorVerdadero; }
            set { _dblValorVerdadero = value; }
        }
        private double _dblValorAproximado;//Valor Aproximado
        public double VAproximado
        {
            get { return _dblValorAproximado; }
            set { _dblValorAproximado = value; }
        }
        public double CalcularErrorAbsoluto() //Metodo que calcula el error absoluto
        {
            double x = VVerdadero - VAproximado;// Resta el valor verdadero y el valor aproximado
            return Math.Abs(x);//retorna el resultado absoluto
        }
        public double CalcularErrorRelativo() //Metodo que calcula el error relativo
        {
            double x = (CalcularErrorAbsoluto() / VVerdadero);// divide el resultado del metodo anterior y el valor verdadero
            return x;//regresa el resultado de la operacion
        }
        public double CalcularErrorRelativoPorcentual()//Metodo que calcula el error relativo porcentual
        {
            double x = (CalcularErrorRelativo()*100);// el resultado del metodo anterior multiplicado por cien
            return x;//regresa el resultado de la operacion
        }
    }
}
