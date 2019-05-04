using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoissonUstelDagılım
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            //POISSON İÇİN:
            if(radioButtonPoisson.Checked)
            {
                double lambda = Convert.ToDouble(txtLambdaPoisson.Text);
                double a1 = Convert.ToDouble(txtA1Poisson.Text);
                double a2 = Convert.ToDouble(txtA2Poisson.Text);
                                if (a1 >= a2)
                {
                        MessageBox.Show("A1 değeri A2 değerine eşit veya büyük olamaz! ", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(a1<a2)
                {
                    double numerator = 0;
                    double p = 0;

                    double Factorial(double x)
                    {
                        double count = x;
                        double factorial = 1;
                        while (count >= 1)
                        {
                            factorial = factorial * count;
                            count--;
                        }
                        return factorial;
                    }
                    p = 0;
                    chart1.Series["λ"].Points.Clear();

                    //P(a1<X<a2) için 
                    for (double x = a1+1; x <a2; x++)
                    {
                        double xFactorial = Factorial(x);
                        numerator = Math.Pow(Math.E, -(double)lambda) * Math.Pow((double)lambda, (double)x);
                        p = p + (double)numerator / xFactorial;
                        this.chart1.Series["λ"].Points.AddXY(x, p);
                    }
                    txtSonuc.Text = p.ToString();
                    p = 0;

                    // P(X<=a2) için
                    for (double x = 0.0; x <= a2; x++)
                    {
                        double xFactorial = Factorial(x);
                        numerator = Math.Pow(Math.E, -(double)lambda) * Math.Pow((double)lambda, (double)x);
                        p = p + (double)numerator / xFactorial;
                    }
                    txtSonuc2.Text = p.ToString();
                    p = 0;

                    // P(a1<=X) için
                    for (double x = 0.0; x < a1; x++)
                    {
                        double xFactorial = Factorial(x);
                        numerator = Math.Pow(Math.E, -(double)lambda) * Math.Pow((double)lambda, (double)x);
                        p = p + (double)numerator / xFactorial;
                        p = 1 - p;
                    }
                    txtSonuc3.Text = p.ToString();
                    p = 0;
                    
                    //P(X<a1 | X<a2) için
                    for (double x = 0.0; x < a1; x++)
                    {
                        double xFactorial = Factorial(x);
                        numerator = Math.Pow(Math.E, -(double)lambda) * Math.Pow((double)lambda, (double)x);
                        p = p + (double)numerator / xFactorial;
                    }
                    double k = p;

                    p = 0;
                    for (double x = 0.0; x < a2; x++)
                    {
                        double xFactorial = Factorial(x);
                        numerator = Math.Pow(Math.E, -(double)lambda) * Math.Pow((double)lambda, (double)x);
                        p = p + (double)numerator / xFactorial;
                    }
                    double m = p;

                    double sonuc = k / m;
                    txtSonuc4.Text = sonuc.ToString();
                }
            }
            //ÜSTEL İÇİN
            else if(radioButtonUstel.Checked)
            {
                double lambda = Convert.ToDouble(txtLambdaUstel.Text);
                double a1 = Convert.ToDouble(txtA1Ustel.Text);
                double a2 = Convert.ToDouble(txtA2Ustel.Text);
                if (a1>=a2)
                {
                    MessageBox.Show("A1 değeri A2 değerine eşit veya büyük olamaz! ", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if(a1<a2)
                {
                double p = 0;
                double Fonksiyon(double a)
                    {
                        double eUstu = (-(double)a * (1 / lambda));
                        double num = Math.Pow(Math.E, (double)eUstu);
                        return num;
                    }

                    chart1.Series["λ"].Points.Clear();
                    for(double k = a1; k <= a2; k = k + 1)
                    {
                      double sonuc = (Fonksiyon(k - 0.1)) - (Fonksiyon(k));
                      p = sonuc;
                      this.chart1.Series["λ"].Points.AddXY(k, p);
                    }

                    txtSonuc.Text = ((1 - Fonksiyon(a2)) - (1 - Fonksiyon(a1))).ToString();
                    txtSonuc2.Text = (1 - Fonksiyon(a2)).ToString();
                    txtSonuc3.Text = (Fonksiyon(a1)).ToString();
                    txtSonuc4.Text = ((1 - Fonksiyon(a1)) / (1 - Fonksiyon(a2))).ToString();

                }
                
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtA1Poisson.Clear();
            txtA1Ustel.Clear();
            txtA2Poisson.Clear();
            txtA2Ustel.Clear();
            txtLambdaPoisson.Clear();
            txtLambdaUstel.Clear();
            txtSonuc.Clear();
            txtSonuc2.Clear();
            txtSonuc3.Clear();
            txtSonuc4.Clear();
            chart1.Series["λ"].Points.Clear();
        }
    }
}
