using System;
using Tech.CodeGeneration;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Media;

namespace NotificationCenter
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<double> answ;
        List<double> crit;
        string s;
        public Window1()
        {
            InitializeComponent();
        }

         private double Count(double minValue, double maxValue)
         {
            double a= minValue;
            double b = maxValue;
            double esp = 0.0001;
            double cen=0;
            while ((b-a>esp) &&( f(a) * f(b) < 0))
            {
                cen = (a + b) / 2;
                if (f(a) * f(cen) < 0)
                {
                    b = cen;
                }
                else 
                {
                    a = cen;
                }
            }
            if (f(cen) < 1)
            {
                return Math.Round(cen,3);
            }
            return double.NaN;
        }
        private double f(double x)
        {
            return CodeGenerator.ExecuteCode<double>(s, CodeParameter.Create("x", x));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input.Text += "9";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Input.Text += "8";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Input.Text += "7";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Input.Text += "6";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Input.Text += "5";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Input.Text += "4";
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Input.Text += "3";
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Input.Text += "2";
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Input.Text += "1";
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Input.Text += "0";
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Input.Text += ".";
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Input.Text += "=";
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Input.Text += "/";
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            Input.Text += "*";
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            Input.Text += "-";
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            Input.Text += "+";
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            try
            {
                Input.Text = Input.Text.Remove(Input.Text.Length - 1);
            }
            catch { }
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            Input.Text += "x";
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            Output.Background = Brushes.White;
            string[] tmp = Input.Text.Split('=');
            try
            {
                if (tmp[1] != "0") throw new Exception();
                s = tmp[0];         
                if (s.Contains("^") || s.Contains("√"))
                {
                    while (s.Contains("^"))
                    {
                        int x = s.IndexOf("^");
                        string str="";
                        string col="";
                        for(int i = x; i < s.Length; i++)
                        {
                            if (s[i] == '*' || s[i] == '/' || s[i] == '+' || s[i] == '-' || s[i] == ' ' || s[i] == '=')
                            {
                                col = s.Substring(x+1,i-x-1);
                                break;
                            }
                            if (i == s.Length - 1) { col = s.Substring(x + 1, i - x); break; }
                        }
                        for (int i = x; i >=0; i--)
                        {
                            if (s[i] == '*' || s[i] == '/' || s[i] == '+' || s[i] == '-' || s[i] == ' ' || s[i] == '=' || s[i] == '(')
                            {
                                str = s.Substring(i+1,x-i-1);
                                break;
                            }
                            if (i == 0)
                            {
                                str = s.Substring(i, x - i);
                                break;
                            }
                        }
                        string o = "";
                        for (int i = 0; i < int.Parse(col); i++)
                            o += str+"*";
                        o = o.Remove(o.Length - 1);
                        s = s.Replace(str + "^" + col, o);
                    }
                    while (s.Contains("√"))
                    {
                        int x = s.IndexOf("√");
                        string col = "";
                        for (int i = x; i < s.Length; i++)
                        {
                            if (s[i] == '*' || s[i] == '/' || s[i] == '+' || s[i] == '-' || s[i] == ' ' || s[i] == '=' )
                            {
                                col = s.Substring(x + 1, i - x - 1);
                                break;
                            }
                            if (i == s.Length - 1) { col = s.Substring(x + 1, i - x); break; }
                        }
                        s = s.Remove(x,col.Length+1);
                        s=s.Insert(x,"Math.Sqrt("+col+")");
                    }
                    
                }
                string proisv = FindProisv(s,0);
                s = s.Insert(0, "return ");
                s = s.Insert(s.Length, " ;");
                CodeGenerator.ExecuteCode<double>(s, CodeParameter.Create("x", 8));
                StartObr(proisv);

            }
            catch { Output.Content = "Неверный формат!"; }
        }

        private static string FindProisv(string stri,int p)
        {
            string proisv = "";
            string str = stri;
            if(p==0)str = str.Replace("-", "+(-1)*");
            string[] parts = str.Split('+');
            foreach (string part in parts)
            {
                if (part.Contains("x"))
                {
                    str = part;
                    int xes = 0;
                    for (int i = 0; i < part.Length; i++)
                    {
                        if (part[i] == 'x') xes++;
                    }
                    if (xes == 1) str = part.Replace("x", "");
                    else str = str.Remove(str.IndexOf("*x"), 2);
                    proisv += xes.ToString() + "*" + str + "+";
                }
            }
            for (int i = 1; i < proisv.Length; i++)
            {
                if (proisv[i - 1] == '*' || proisv[i - 1] == '+' || proisv[i - 1] == '/')
                {
                    if (proisv[i] == '*' || proisv[i] == '+' || proisv[i] == '/') { proisv = proisv.Remove(i - 1, 1); i--; }
                }
            }
            proisv = proisv.Remove(proisv.Length - 1, 1);
            return proisv;
        }

        private void StartObr(string proisv)
        {            
            crit = new List<double>();
            crit.Add(int.MinValue/2);
            crit.Add(int.MaxValue/2);
            SolveProisv(proisv);
            answ = new List<double>();
            for (int i = 1; i < crit.Count; i++)
            {
                if (f(crit[i - 1]) != 0 && f(crit[i]) != 0 && !double.IsNaN(crit[i-1]) && !double.IsNaN(crit[i]))
                {
                     answ.Add(Count(crit[i - 1], crit[i]));
                }
                else if (f(crit[i - 1]) == 0 && !answ.Contains(crit[i-1])) answ.Add(crit[i - 1]);
                else if(!answ.Contains(crit[i])&& f(crit[i]) == 0) answ.Add(crit[i]);
            }
            Output.Content = "";
            foreach (double answer in answ)
            {
                if(double.IsNaN(answer)) Output.Content += "--  ";
                else Output.Content += answer.ToString() + " ";
            }

        }

        private void SolveProisv(string proisv)
        {
            string[] parts = proisv.Split('+','-');
            List<string> vs = new List<string>();
            int maxst = 0;
            string tmp = s;
            foreach (string part in parts)
            {  
                 if(maxst< part.ToCharArray().Where(x => x == 'x').Count()) maxst = part.ToCharArray().Where(x => x == 'x').Count();
            }
            while (true)
            {
                if (maxst > 1)
                {
                    vs.Add(proisv);
                    proisv=FindProisv(proisv, 1);
                    maxst--;
                }
                if (maxst == 1)
                {
                    vs.Add(proisv);
                    
                    for (int i = vs.Count - 1;i >= 0; i--)
                    {
                        vs[i] = vs[i].Insert(0, "return ");
                        vs[i] = vs[i].Insert(vs[i].Length, " ;");
                        s = vs[i];
                        List<double> newCrit = new List<double>();
                        newCrit.Add(double.MinValue / 2);
                        newCrit.Add(double.MaxValue / 2);
                        for (int j = 1; j < crit.Count; j++)
                        {
                            if (f(crit[j - 1]) != 0 && f(crit[j]) != 0)
                            {
                               double n= Count(crit[j - 1], crit[j]);
                               if (!newCrit.Contains(n)) newCrit.Add(n);
                            }
                            else if (f(crit[j - 1]) == 0 && !newCrit.Contains(crit[j - 1])) newCrit.Add(crit[j - 1]);
                            else if (f(crit[j]) == 0 && !newCrit.Contains(crit[j])) newCrit.Add(crit[j - 1]);
                        }
                        newCrit.Sort();
                        crit = new List<double>(newCrit);
                    }
                    break;
                }
                if (maxst == 0)
                {
                    crit = new List<double>();
                    crit.Add(double.MinValue/2);
                    crit.Add(double.MaxValue / 2);
                    break;
                }
            }
            s = tmp;
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            Input.Text += "(";
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            Input.Text += ")";
        }
        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            Close();
        }
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            urav.Width = grid.ActualWidth * 0.23;
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            if (Polosa.Visibility == Visibility.Collapsed)
            {
                Polosa.Visibility = Visibility.Visible;
            }
            else
            {
                Polosa.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            Input.Text += "√";
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            Input.Text += "^";
        }
    }
}
