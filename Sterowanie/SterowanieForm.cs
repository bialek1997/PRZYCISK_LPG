using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Sterowanie
{
    public partial class SterowanieForm : Form
    {
        Thread aktualizujPolaDlaNowejPredkosci;
        int counter = 0;
        ushort zadanaPredkosc;
        ulong xSeries = 0;

        public SterowanieForm()
        {
            InitializeComponent();

            aktualizujPolaDlaNowejPredkosci = new Thread(AktualizujPolaDlaNowejPredkosci);
            aktualizujPolaDlaNowejPredkosci.Start();

            Wykres.ChartAreas[0].AxisX.Minimum = 0;
            Wykres.ChartAreas[0].AxisX.Maximum = 500;
            Wykres.ChartAreas[0].AxisX2.Minimum = 0;
            Wykres.ChartAreas[0].AxisX2.Maximum = 500;
        }

        private void WyslijButton_Click(object sender, EventArgs e)
        {
            bool czyZadanaPredkoscPrawidlowa = ushort.TryParse(ZadanaPredkoscValue.Text, out zadanaPredkosc);
            if (czyZadanaPredkoscPrawidlowa)
            {
                CAN.DoObiektu(zadanaPredkosc);
            }
        }

        private void ZadanaPredkoscValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(System.Text.RegularExpressions.Regex.IsMatch(ZadanaPredkoscValue.Text, "[^[0-9]"))
            {
                MessageBox.Show("Zadana Prędkość może być tylko liczbą naturalną!");
                ZadanaPredkoscValue.Text = ZadanaPredkoscValue.Text.Remove(ZadanaPredkoscValue.Text.Length - 1);
            }
        }

        public void AktualizujPolaDlaNowejPredkosci()
        {
            string aktualnaPredkosc;
            while (true)
            {
                aktualnaPredkosc = CAN.DoSterowania();
                if (aktualnaPredkosc != string.Empty)
                {
                    try
                    {
                        AktualizujTextBox(aktualnaPredkosc);
                        AktualizujGauge(aktualnaPredkosc);
                        AktualizujWykres(aktualnaPredkosc);
                    }
                    catch (InvalidOperationException)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
        }

        public void AktualizujTextBox(string aktualnaPredkosc)
        {
            AktualnaPredkoscValue.BeginInvoke(new Action(() => {
                AktualnaPredkoscValue.Text = Math.Round(Double.Parse(aktualnaPredkosc)).ToString();
            }));
        }

        public void AktualizujGauge(string aktualnaPredkosc)
        {
            Gauge.BeginInvoke(new Action(() =>
            {
                Gauge.Value = float.Parse(aktualnaPredkosc);
            }));
        }

        public void AktualizujWykres(string aktualnaPredkosc)
        {
            if(counter > 500)
            {
                counter = 0;
                xSeries += 1;
                Wykres.BeginInvoke(new Action(() =>
                {
                    if (xSeries > 500)
                    {
                        Wykres.Series["ZadanaPredkoscSeries"].Points.Clear();
                        Wykres.Series["AktualnaPredkoscSeries"].Points.Clear();
                        xSeries = 0;
                    }
                    Wykres.Series["ZadanaPredkoscSeries"].Points.AddXY(xSeries, zadanaPredkosc);
                    Wykres.Series["AktualnaPredkoscSeries"].Points.AddXY(xSeries, double.Parse(aktualnaPredkosc));
                }));
            }
            else
            {
                counter++;
            }
        }

        private void SterowanieForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            aktualizujPolaDlaNowejPredkosci.Abort();
        }
    }

}
