using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Newton_Method
{
   // public class MyStack : Stack<T>
   // {
       // T get_element(int index)
       // {
         //   T [] temp  = this.ToArray();
         //   if (index > 0 && index < temp.Length)
        //        return temp[index];
        //    else
        //        throw new Exception();
      //  }
   // }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string tmp1 = null;
        bool x;
        List<string> tmpO;
        List<string> tmpD;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tmpO = new List<string>();
                tmpD = new List<string>();
                double epsillon = Double.Epsilon;
                double x0;
                double root = 0;
                Stack<string> Operation = new Stack<string>();
                Stack<string> Oprand = new Stack<string>();
                int err = -1;
                int iter = 0;
                textBox6.Text = string.Concat(textBox6.Text, "O: Check if all necessary field is full.\n");
                if (textBox1.Text.Length != 0 && textBox3.Text.Length != 0 && textBox4.Text.Length != 0 && (Xor(textBox2.Text.Length != 0, textBox5.Text.Length != 0)))
                {
                    textBox6.Text = string.Concat(textBox6.Text, "O: Try to read the function.\n");
                    if (read(ref Oprand, ref Operation) == true)
                    {
                        textBox6.Text = string.Concat(textBox6.Text, "O: See if the function is recognisable.\n");
                        if (Checker(ref Oprand, ref Operation, epsillon) == true)
                        {
                            textBox6.Text = string.Concat(textBox6.Text, "O: Detect which function must be called.\n");
                            if (textBox2.Text.Length != 0)
                                epsillon = double.Parse(textBox2.Text);
                            if (epsillon > 0.025 || epsillon < Double.Epsilon)
                            {
                                textBox6.Text = string.Concat(textBox6.Text, "E: Epsillon must be in this domain ]0,0.025].\n");
                                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                                MessageBox.Show("Epsillon must be in this domain!\n]0,0.025]", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else if (Math.Abs(double.Parse(textBox3.Text) - double.Parse(textBox4.Text)) < epsillon)
                            {
                                textBox6.Text = string.Concat(textBox6.Text, "E: Domain bound mustn't be identical.\n");
                                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                                MessageBox.Show("Domain bound mustn't be identical!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else if (textBox5.Text.Length != 0 && (int.Parse(textBox5.Text) < 1 || int.Parse(textBox5.Text) > 100))
                            {
                                textBox6.Text = string.Concat(textBox6.Text, "E: Iteration max can not be less than 1 or more than 100.\n");
                                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                                MessageBox.Show("Iteration max can not be less than 1 or more than 100!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                double x1 = f(double.Parse(textBox3.Text), Oprand, Operation);
                                double x2 = f(double.Parse(textBox4.Text), Oprand, Operation);
                                textBox6.Text = string.Concat(textBox6.Text, "O: Identifying whether there is solution or not.\n");
                                textBox6.Text = string.Concat(textBox6.Text, "O: a = ", textBox3.Text, "\n");
                                textBox6.Text = string.Concat(textBox6.Text, "O: b = ", textBox4.Text, "\n");
                                textBox6.Text = string.Concat(textBox6.Text, "O: f(a) = ", x1, "\n");
                                textBox6.Text = string.Concat(textBox6.Text, "O: f(b) = ", x2, "\n");
                                textBox6.Text = string.Concat(textBox6.Text, "O: f(a) * f(b) = ", x1 * x2);
                                if (x1 * x2 < 0)
                                {
                                    double d2a = d2f(double.Parse(textBox3.Text), Oprand, Operation);
                                    double d2b = d2f(double.Parse(textBox4.Text), Oprand, Operation);
                                    textBox6.Text = string.Concat(textBox6.Text, " < 0 there is a solution.\n");
                                    textBox6.Text = string.Concat(textBox6.Text, "O: f''(a) = ", d2a, "\n");
                                    textBox6.Text = string.Concat(textBox6.Text, "O: f''(b) = ", d2b, "\n");
                                    textBox6.Text = string.Concat(textBox6.Text, "O: f(a) * f''(a) = ", x1 * d2a);
                                    if (x1 * d2a > 0)
                                    {
                                        textBox6.Text = string.Concat(textBox6.Text, " >= 0 so x0 = ", textBox3.Text, "\n");
                                        x0 = double.Parse(textBox3.Text);
                                    }
                                    else
                                    {
                                        textBox6.Text = string.Concat(textBox6.Text, " <= 0 so x0 = ", textBox4.Text, "\n");
                                        x0 = double.Parse(textBox4.Text);
                                    }
                                    if (textBox2.Text.Length == 0)
                                    {
                                        epsillon = Double.Epsilon;
                                        Solve(x0, ref err, iter, int.Parse(textBox5.Text), ref root, Oprand, Operation);
                                    }
                                    else
                                    {
                                        epsillon = double.Parse(textBox2.Text);
                                        Solve(x0, epsillon, ref err, iter, ref root, Oprand, Operation);
                                    }
                                    if (err == 0)
                                    {
                                        MessageBox.Show("Done.", "Newton Method", MessageBoxButton.OK, MessageBoxImage.Information);
                                        textBox6.Text = string.Concat(textBox6.Text, "Done.\n****************************************\n");
                                    }
                                    else if (err == 2)
                                    {
                                        textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                                        MessageBox.Show("Can't Solve this equation!", "Newton Method", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    else if (err == 1)
                                    {
                                        textBox6.Text = string.Concat(textBox6.Text, "Done.\n****************************************\n");
                                        MessageBox.Show("Done.", "Newton Method", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }
                                else
                                {
                                    textBox6.Text = string.Concat(textBox6.Text, " > 0 there is not a solution\n");
                                    textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                                    MessageBox.Show("No solution for this function in this domain!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                    }
                    else
                    {
                        textBox6.Text = string.Concat(textBox6.Text, "E: Function Can not be read.\n");
                        textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                        MessageBox.Show("Function was not recognise!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    textBox6.Text = string.Concat(textBox6.Text, "E: there is some necessary fields is empty or you wrote ε and max iteration in same time.\n");
                    textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                    MessageBox.Show("Please complete all necessary fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception er)
            {
                textBox6.Text = string.Concat(textBox6.Text, "E: there is some thing error.\n", er, "\n");
                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                MessageBox.Show("Unexpected error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int Solve(double x0, double epsillon, ref int err, int iter, ref double root, Stack<string> Oprand, Stack<string> Operation)
        {
            double x1 = 0;
            iter = 1;
            do
            {
                double d1x0 = Math.Abs(df(x0, Oprand, Operation));
                if (d1x0 < epsillon)
                {
                    textBox6.Text = string.Concat(textBox6.Text, "O: |f'(x0)| = ", d1x0, " this equation can not be solved in Newton Method.\n");
                    err = 2;
                    return -100;
                }
                if (iter == 1)
                    textBox6.Text = string.Concat(textBox6.Text, "O: |f'(x0)| = ", d1x0, " > 0 this equation can be solved in Newton Method.\n");
                x1 = x0 - (f(x0, Oprand, Operation) / df(x0, Oprand, Operation));
                textBox6.Text = string.Concat(textBox6.Text, "O: x", iter, " = x", iter - 1, " -[f(x", iter - 1, ") / f'(x", iter - 1, ")] = ", x1, "\n");
                if (double.IsNaN(x1) || double.IsInfinity(x1))
                {
                    textBox6.Text = string.Concat(textBox6.Text, "O: x", iter, " is not a number, this equation can not be solved in Newton Method.\n");
                    err = 2;
                    return -100;
                }
                if (Math.Abs(x1 - x0) <= epsillon)
                {
                    textBox6.Text = string.Concat(textBox6.Text, "O: |x", iter, " - x", iter - 1, "| = ", Math.Abs(x1 - x0), " < ε.", "\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: x", iter, " = ", x1, " is a root of the function.\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: ε = ", epsillon, "\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: Number of iteration = ", iter, ".\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: |f(x", iter, ")| = ", Math.Abs(f(x1, Oprand, Operation)), "\n");
                    root = x1;
                    err = 1;
                    return iter;
                }
                textBox6.Text = string.Concat(textBox6.Text, "O: f(x", iter, ") = ", f(x1, Oprand, Operation), " .\n");
                textBox6.Text = string.Concat(textBox6.Text, "O: |x", iter, " - x", iter - 1, "| = ", Math.Abs(x1 - x0), " > ε so we well do another iteration.\n");
                iter++;
                x0 = x1;
            } while (iter <= 100);
            textBox6.Text = string.Concat(textBox6.Text, "O: |x", iter - 1, " - x", iter - 2, "| is still > ε after 100 iterations, this equation can not be solved in Newton Method.\n");
            err = 2;
            return -100;
        }

        private double Solve(double x0, ref int err, int iter, int iter_Max, ref double root, Stack<string> Oprand, Stack<string> Operation)
        {
            double epsillon = Double.Epsilon;
            double x1 = 0;
            iter = 1;
            do
            {
                double d1x0 = Math.Abs(df(x0, Oprand, Operation));
                if (d1x0 < epsillon)
                {
                    textBox6.Text = string.Concat(textBox6.Text, "O: |f'(x0)| = ", d1x0, " this equation can not be solved in Newton Method.\n");
                    err = 2;
                    return -100;
                }
                if (iter == 1)
                    textBox6.Text = string.Concat(textBox6.Text, "O: |f'(x0)| = ", d1x0, " > 0 this equation can be solved in Newton Method.\n");
                x1 = x0 - (f(x0, Oprand, Operation) / df(x0, Oprand, Operation));
                textBox6.Text = string.Concat(textBox6.Text, "O: x", iter, " = x", iter - 1, " -[f(x", iter - 1, ") / f'(x", iter - 1, ")] = ", x1, "\n");
                if (double.IsNaN(x1) || double.IsInfinity(x1))
                {
                    textBox6.Text = string.Concat(textBox6.Text, "O: x", iter, " is not a number, this equation can not be solved in Newton Method.\n");
                    err = 2;
                    return -100;
                }
                if (iter == iter_Max)
                {
                    textBox6.Text = string.Concat(textBox6.Text, "O: iteration = ", iter, " = iteration max.\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: x", iter, " = ", x1, " is a root of the function.\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: Number of iteration = ", iter, ".\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: ε = ", Math.Abs(x1 - x0), "\n");
                    textBox6.Text = string.Concat(textBox6.Text, "O: |f(x", iter, ")| = ", Math.Abs(f(x1, Oprand, Operation)), "\n");
                    root = x1;
                    err = 0;
                    return Math.Abs(x1 - x0);
                }
                textBox6.Text = string.Concat(textBox6.Text, "O: f(x", iter, ") = ", f(x1, Oprand, Operation), " .\n");
                textBox6.Text = string.Concat(textBox6.Text, "O: iteration = ", iter, " < iteration max so we well do another iteration.\n");
                iter++;
                x0 = x1;
            } while (err != 3);
            return -100;
        }

        private double f(double x0, Stack<string> Oprand, Stack<string> Operation)
        {
            List<string> D = new List<string>(tmpD);
            List<string> O = new List<string>(tmpO);
            D.Reverse();
            O.Reverse();
            for (int i = O.Count - 1; i >= 0; i--)
                if (O[i] == "^")
                    Findfx(i, x0, D, O);
            for (int i = 0; i < O.Count; )
                if (O[i] == "*" || O[i] == "/")
                    Findfx(i, x0, D, O);
                else
                    i++;
            while (O.Count > 0)
                Findfx(0, x0, D, O);
            return Assignment(D[0], x0);
        }

        private double df(double x0, Stack<string> Oprand, Stack<string> Operation)
        {
            double h = 1e-6 * Math.Max(1, Math.Abs(x0));
            return (f(x0 + h, Oprand, Operation) - f(x0 - h, Oprand, Operation)) / (2 * h);
        }

        private double d2f(double x0, Stack<string> Oprand, Stack<string> Operation)
        {
            double h = 1e-4 * Math.Max(1, Math.Abs(x0));
            return (f(x0 + h, Oprand, Operation) - 2 * f(x0, Oprand, Operation) + f(x0 - h, Oprand, Operation)) / (h * h);
        }

        private bool read(ref Stack<string> Oprand, ref Stack<string> Operation)
        {
            string line = textBox1.Text;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] >= 48 && line[i] <= 57)
                {
                    x = true;
                    tmp1 = tmp1 + line[i];
                    if (i < line.Length - 1 && (line[i + 1] >= 48 && line[i + 1] <= 57))
                        continue;
                    else if (i < line.Length - 1 && line[i + 1] == '.')
                    {
                        i++;
                        tmp1 = tmp1 + line[i];
                        continue;
                    }
                }
                else if (line[i] == '+' || line[i] == '/' || line[i] == '*' || line[i] == '-')
                {
                    tmp1 = line[i].ToString();
                    x = false;
                }
                else if (line[i] == '^')
                {
                    tmp1 = line[i].ToString();
                    x = false;
                }
                else if (line[i] == 'x')
                {
                    tmp1 = line[i].ToString();
                    x = true;
                }
                else
                    return false;
                if (x == false)
                    Operation.Push(tmp1);
                else
                    Oprand.Push(tmp1);
                tmp1 = null;
            }
            return true;
        }

        private void Findfx(int i, double x0, List<string> D, List<string> O)
        {
            string a = D[i + 1];
            string b = D[i];
            double result = 0;
            switch (O[i])
            {
                case "^":
                    result = Power(a, b, x0);
                    break;
                case "*":
                    result = Multiple(a, b, x0);
                    break;
                case "/":
                    result = Division(a, b, x0);
                    break;
                case "+":
                    result = Plus(a, b, x0);
                    break;
                case "-":
                    result = Minus(a, b, x0);
                    break;
            }
            D[i] = result.ToString("R");
            D.RemoveAt(i + 1);
            O.RemoveAt(i);
        }

        private double Power(string a, string b, double x0)
        {
            if (a == "x")
                if (b == "x")
                    return Math.Pow(x0, x0);
                else
                    return Math.Pow(double.Parse(b), x0);
            else
                if (b == "x")
                    return Math.Pow(x0, double.Parse(a));
                else
                    return Math.Pow(double.Parse(b), double.Parse(a));
        }

        private double Multiple(string a, string b, double x0)
        {
            if (a == "x")
                if (b == "x")
                    return x0 * x0;
                else
                    return double.Parse(b) * x0;
            else
                if (b == "x")
                    return x0 * double.Parse(a);
                else
                    return double.Parse(b) * double.Parse(a);
        }

        private double Division(string a, string b, double x0)
        {
            if (a == "x")
                if (b == "x")
                    return x0 / x0;
                else
                    return double.Parse(b) / x0;
            else
                if (b == "x")
                    return x0 / double.Parse(a);
                else
                    return double.Parse(b) / double.Parse(a);
        }

        private double Plus(string a, string b, double x0)
        {
            if (a == "x")
                if (b == "x")
                    return x0 + x0;
                else
                    return double.Parse(b) + x0;
            else
                if (b == "x")
                    return x0 + double.Parse(a);
                else
                    return double.Parse(b) + double.Parse(a);
        }

        private double Minus(string a, string b, double x0)
        {
            if (a == "x")
                if (b == "x")
                    return x0 - x0;
                else
                    return double.Parse(b) - x0;
            else
                if (b == "x")
                    return x0 - double.Parse(a);
                else
                    return double.Parse(b) - double.Parse(a);
        }

        private double Assignment(string a, double x0)
        {
            if (a == "x")
                return x0;
            else
                return double.Parse(a);
        }

        private bool Xor(bool ex1, bool ex2)
        {
            if (ex1 == true && ex2 == false)
                return true;
            else if (ex1 == false && ex2 == true)
                return true;
            else if (ex1 == true && ex2 == true)
                return false;
            else
                return false;
        }

        private void CopyStack(ref Stack<string> D, ref Stack<string> O)
        {
            D = new Stack<string>();
            O = new Stack<string>();
            for (int i = tmpO.Count - 1; i >= 0; i--)
                O.Push(tmpO[i]);
            for (int i = tmpD.Count - 1; i >= 0; i--)
                D.Push(tmpD[i]);
        }

        private bool Checker(ref Stack<string> D, ref Stack<string> O, double epsillon)
        {
            while (O.Count != 0)
                tmpO.Add(O.Pop());
            while (D.Count != 0)
                tmpD.Add(D.Pop());
            for (int i = tmpO.Count - 1; i >= 0; i--)
                O.Push(tmpO[i]);
            for (int i = tmpD.Count - 1; i >= 0; i--)
                D.Push(tmpD[i]);
            string[] tmp1 = new string[99999];
            if (D.Count != O.Count + 1)
            {
                textBox6.Text = string.Concat(textBox6.Text, "E Function type is ununderstandable Please retype the function.\n");
                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                MessageBox.Show("Function type is ununderstandable!\nPlease retype the function.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            for (int j = 0; j < D.Count; j++)
            {
                tmp1[j] = D.Pop();
                if (tmp1[j] == "x") { }
                else if (Math.Abs(double.Parse(tmp1[j])) < epsillon)
                {
                    textBox6.Text = string.Concat(textBox6.Text, "E: Zero causes problem in calculation, please remove all zero from the function.\n");
                    textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                    MessageBox.Show("Please remove all zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            if (textBox1.Text.Contains("x")) { }
            else
            {
                textBox6.Text = string.Concat(textBox6.Text, "E: The function must related to x only,.\n");
                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
                MessageBox.Show("This is not a function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            CopyStack(ref D, ref O);
            return true;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBox6.Text = null;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = string.Concat(textBox1.Text, "2.71828182846");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = string.Concat(textBox1.Text, "^");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = string.Concat(textBox1.Text, "^0.5");
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = string.Concat(textBox1.Text, "x");
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = string.Concat(textBox1.Text, "3.14159265359");
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = string.Concat(textBox1.Text, "^0.33333333333");
        }

        //private void button9_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        tmpO = new List<string>();
        //        tmpD = new List<string>();
        //        double epsillon = Double.Epsilon;
        //        double a;
        //        double b;
        //        double root = 0;
        //        Stack<string> Operation = new Stack<string>();
        //        Stack<string> Oprand = new Stack<string>();
        //        int err = -1;
        //        int iter = 0;
        //        textBox6.Text = string.Concat(textBox6.Text, "O: Check if all necessary field is full.\n");
        //        if (textBox1.Text.Length != 0 && textBox3.Text.Length != 0 && textBox4.Text.Length != 0 && (Xor(textBox2.Text.Length != 0, textBox5.Text.Length != 0)))
        //        {
        //            textBox6.Text = string.Concat(textBox6.Text, "O: Try to read the function.\n");
        //            if (read(ref Oprand, ref Operation) == true)
        //            {
        //                textBox6.Text = string.Concat(textBox6.Text, "O: See if the function is recognisable.\n");
        //                if (Checker(ref Oprand, ref Operation, epsillon) == true)
        //                {
        //                    textBox6.Text = string.Concat(textBox6.Text, "O: Detect which function must be called.\n");
        //                    if (textBox2.Text.Length != 0)
        //                        epsillon = double.Parse(textBox2.Text);
        //                    if (epsillon > 0.025 || epsillon < Double.Epsilon)
        //                    {
        //                        textBox6.Text = string.Concat(textBox6.Text, "E: Epsillon must be in this domain ]0,0.025].\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //                        MessageBox.Show("Epsillon must be in this domain!\n]0,0.025]", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                    }
        //                    else if (Math.Abs(double.Parse(textBox3.Text) - double.Parse(textBox4.Text)) < epsillon)
        //                    {
        //                        textBox6.Text = string.Concat(textBox6.Text, "E: Domain bound mustn't be identical.\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //                        MessageBox.Show("Domain bound mustn't be identical!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                    }
        //                    else if (textBox5.Text.Length != 0 && (int.Parse(textBox5.Text) < 1 || int.Parse(textBox5.Text) > 200))
        //                    {
        //                        textBox6.Text = string.Concat(textBox6.Text, "E: Iteration max can not be less than 1 or more than 200.\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //                        MessageBox.Show("Iteration max can not be less than 1 or more than 200!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                    }
        //                    else
        //                    {
        //                        a = double.Parse(textBox3.Text);
        //                        b = double.Parse(textBox4.Text);
        //                        double fa = f(a, Oprand, Operation);
        //                        double fb = f(b, Oprand, Operation);
        //                        textBox6.Text = string.Concat(textBox6.Text, "O: Identifying whether there is solution or not.\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "O: a = ", textBox3.Text, "\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "O: b = ", textBox4.Text, "\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "O: f(a) = ", fa, "\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "O: f(b) = ", fb, "\n");
        //                        textBox6.Text = string.Concat(textBox6.Text, "O: f(a) * f(b) = ", fa * fb);
        //                        if (fa * fb > 0)
        //                        {
        //                            textBox6.Text = string.Concat(textBox6.Text, " < 0 there is a solution.\n");
        //                            if (textBox2.Text.Length == 0)
        //                            {
        //                                epsillon = Double.Epsilon;
        //                                Mid(a,b, ref err, iter, int.Parse(textBox5.Text), ref root, Oprand, Operation);
        //                            }
        //                            else
        //                            {
        //                                epsillon = double.Parse(textBox2.Text);
        //                                Mid(a,b, epsillon, ref err, iter, ref root, Oprand, Operation);
        //                            }
        //                            if (err == 0)
        //                            {
        //                                MessageBox.Show("Done.", "Mid Method", MessageBoxButton.OK, MessageBoxImage.Information);
        //                                textBox6.Text = string.Concat(textBox6.Text, "Done.\n****************************************\n");
        //                            }
        //                            else if (err == 2)
        //                            {
        //                                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //                                MessageBox.Show("Can't Solve this equation!", "Mid Method", MessageBoxButton.OK, MessageBoxImage.Information);
        //                            }
        //                            else if (err == 1)
        //                            {
        //                                textBox6.Text = string.Concat(textBox6.Text, "Done.\n****************************************\n");
        //                                MessageBox.Show("Done.", "Mid Method", MessageBoxButton.OK, MessageBoxImage.Information);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            textBox6.Text = string.Concat(textBox6.Text, " > 0 there is not a solution\n");
        //                            textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //                            MessageBox.Show("No solution for this function in this domain!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                textBox6.Text = string.Concat(textBox6.Text, "E: Function Can not be read.\n");
        //                textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //                MessageBox.Show("Function was not recognise!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        }
        //        else
        //        {
        //            textBox6.Text = string.Concat(textBox6.Text, "E: there is some necessary fields is empty or you wrote ε and max iteration in same time.\n");
        //            textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //            MessageBox.Show("Please complete all necessary fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        textBox6.Text = string.Concat(textBox6.Text, "E: there is some thing error.\n", er, "\n");
        //        textBox6.Text = string.Concat(textBox6.Text, "\n****************************************\n");
        //        MessageBox.Show("Unexpected error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private int Mid(double a, double b, double epsillon, ref int err, int iter, ref double root, Stack<string> Oprand, Stack<string> Operation)
        //{
        //    iter = 1;
        //    double x0 = (a + b) / 2;
        //    FindAB(ref a, ref b, x0, Oprand, Operation);
        //    while (f(x0, Oprand, Operation)>epsillon)
        //    {
        //        x0 = (a + b) / 2;
        //        FindAB(ref a, ref b, x0, Oprand, Operation);
        //        iter++;
        //    }
        //    root = x0;
        //    MessageBox.Show(root.ToString()+"\n/n"+iter.ToString()+"/n\n"+epsillon.ToString());
        //    return iter;
        //}

        //private double Mid(double a, double b, ref int err, int iter, int iter_Max, ref double root, Stack<string> Oprand, Stack<string> Operation)
        //{
        //    iter = 1;
        //    double x0 = (a + b) / 2;
        //    FindAB(ref a, ref b, x0, Oprand, Operation);
        //    while (iter < iter_Max)
        //    {
        //        x0 = (a + b) / 2;
        //        FindAB(ref a, ref b, x0, Oprand, Operation);
        //        iter++;
        //    }
        //    root = x0;
        //    MessageBox.Show(root.ToString());
        //    return f(x0, Oprand, Operation);
        //}

        //private void FindAB(ref double a, ref double b,double x,Stack<string> Oprand, Stack<string> Operation)
        //{
        //        if (f(a, Oprand, Operation) * f(x, Oprand, Operation) > 0)
        //            a = x;
        //        else
        //            b = x;
        //}

    }
}
