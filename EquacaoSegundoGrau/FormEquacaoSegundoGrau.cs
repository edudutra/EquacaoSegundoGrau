using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquacaoSegundoGrau
{
    public partial class FormEquacaoSegundoGrau : Form
    {
        public FormEquacaoSegundoGrau()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";

            try
            {
                var a = Double.Parse(textBoxA.Text);
                var b = Double.Parse(textBoxB.Text);
                var c = Double.Parse(textBoxC.Text);

                var eq = new EquacaoSegundoGrau(a, b, c);

                eq.Resolver();

                textBoxX1.Text = eq.X1.ToString();
                textBoxX2.Text = eq.X2.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Valores inválidos. Todos os valores devem ser numéricos", "Equação 2º Grau", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DeltaLessThanZeroException ex)
            {
                MessageBox.Show(ex.Message, "Equação 2º Grau", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            
        }
    }

    public class EquacaoSegundoGrau
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }

        public EquacaoSegundoGrau(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public void Resolver ()
        {
            var delta = Math.Pow(B, 2) - 4 * A * C;

            if (delta < 0)
            {
                throw new DeltaLessThanZeroException("Equação sem raízes reais, pois o descriminante é < 0");
            }

            X1 = (-B + Math.Sqrt(delta)) / (2 * A);
            X2 = (-B - Math.Sqrt(delta)) / (2 * A);
        }

    }

    [Serializable]
    internal class DeltaLessThanZeroException : Exception
    {
        public DeltaLessThanZeroException()
        {
        }

        public DeltaLessThanZeroException(string message) : base(message)
        {
        }

        public DeltaLessThanZeroException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeltaLessThanZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
