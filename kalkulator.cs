using System;
using System.ComponentModel;

namespace Zadanie2_Tomasz_Ruszkowski
{
    class Kalkulator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? wynik = "0";
        private string? operacja = null;
        private double? operandLewy = null;
        private double? operandPrawy = null;
        private bool flaga = false;

        public string Wynik
        {
            get => wynik;
            set
            {
                wynik = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs("Wynik")
                    );
            }
        }

        public string Działanie
        {
            get
            {
                if (operandLewy == null)
                    return "";
                else if (operandPrawy == null)
                    return $"{operandLewy} {operacja}";
                else
                    return $"{operandLewy} {operacja} {operandPrawy} = ";
            }
        }

        internal void WprowadźCyfrę(string? cyfra)
        {
            if (flaga)
            {
                Wynik = "0";
                flaga = false;
            }

            if (Wynik == "0")
                Wynik = cyfra;
            else
                Wynik += cyfra;
        }

        internal void WprowadźPrzecinek()
        {
            if (flaga)
                Wynik = "0";
            if (Wynik.Contains(','))
                return;
            else
                Wynik += ",";
        }

        internal void ZmieńZnak()
        {
            if (flaga)
                Wynik = "0";
            if (Wynik == "0")
                return;
            else if (Wynik[0] == '-')
                Wynik = Wynik.Substring(1);
            else
                Wynik = "-" + Wynik;
        }

        internal void KasujZnak()
        {
            if (flaga)
                Wynik = "0";
            if (Wynik == "0")
                return;
            else if (Wynik == "-0," || Wynik.Length == 1 || (Wynik.Length == 2 && Wynik[0] == '-'))
                Wynik = "0";
            else
                Wynik = Wynik.Substring(0, Wynik.Length - 1);
        }

        internal void CzyśćWszystko()
        {
            CzyśćWynik();
            operacja = null;
            operandLewy = operandPrawy = null;
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs("Działanie")
                );
        }

        internal void CzyśćWynik()
        {
            Wynik = "0";
        }

        internal void WprowadźOperacja(string operacja)
        {
            if (this.operacja != null)
            {
                WykonajDziałanie();
                this.operacja = operacja;
            }
            else
            {
                operandLewy = Convert.ToDouble(wynik);
                this.operacja = operacja;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs("Działanie")
                    );
            }

            wynik = "0";
        }

        internal void WprowadźDziałanieDwuargumentowe(string działanie)
        {
            if (operandLewy == null)
            {
                operandLewy = Convert.ToDouble(Wynik);
                operacja = działanie;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));
                wynik = "0";
            }
            else if (operacja == null)
            {
                operacja = działanie;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));
                wynik = "0";
            }
            else
            {
                operandPrawy = Convert.ToDouble(Wynik);
                WykonajDziałanie();
                operacja = działanie;
                operandLewy = Convert.ToDouble(Wynik);
                operandPrawy = null;
                wynik = "0";
                flaga = true;
            }
        }

        internal void WykonajDziałanie()
        {
            if (operandPrawy == null)
                operandPrawy = Convert.ToDouble(Wynik);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));

            switch (operacja)
            {
                case "+":
                    Wynik = $"{operandLewy + operandPrawy}";
                    break;
                case "-":
                    Wynik = $"{operandLewy - operandPrawy}";
                    break;
                case "×":
                    Wynik = $"{operandLewy * operandPrawy}";
                    break;
                case "÷":
                    Wynik = $"{operandLewy / operandPrawy}";
                    break;
                case "x^y":
                    Wynik = $"{Math.Pow((double)operandLewy, operandPrawy.Value)}";
                    break;
                case "%":
                    Wynik = $"{(operandPrawy / 100) * operandLewy}";
                    break;
                case "mod":
                    Wynik = $"{operandLewy % operandPrawy}";
                    break;
            }
        }

        internal void WykonajDziałanieJednoargumentowe(string działanie)
        {
            if (operandLewy == null)
                operandLewy = Convert.ToDouble(Wynik);

            switch (działanie)
            {
                case "√":
                    operandLewy = Math.Sqrt(operandLewy.Value);
                    break;
                case "1/x":
                    operandLewy = 1 / operandLewy;
                    break;
                case "x!":
                    operandLewy = ObliczSilnię(Convert.ToInt32(operandLewy));
                    break;
                case "log":
                    operandLewy = Math.Log10(operandLewy.Value);
                    break;
                case "ln":
                    operandLewy = Math.Log(operandLewy.Value);
                    break;
                case "round down":
                    operandLewy = Math.Floor(operandLewy.Value);
                    break;
                case "round up":
                    operandLewy = Math.Ceiling(operandLewy.Value);
                    break;
            }

            Wynik = $"{operandLewy}";
            flaga = true;
            operacja = null;
            operandPrawy = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));
        }

        private double ObliczSilnię(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * ObliczSilnię(n - 1);
        }
    }
}
