using System.Windows;

namespace CalculatorWPF
{
    class Calculator
    {
        public List<string> _postfixExpr;
        public List<string> _infixExpr;
        private Stack<string> _stack;
        private double _result;
        private string? _tmp;

        public Calculator()
        {
            this._postfixExpr = new List<string>();
            this._infixExpr = new List<string>();
            this._stack = new Stack<string>();
            this._result = 0;
            this._tmp = null;
        }
        public string GetInfixExprAsString()
        {
            string InfixExpressionAsString = "";
            foreach (string s in _infixExpr)
            {
                InfixExpressionAsString += s;
            }
            return InfixExpressionAsString+=_tmp;
        }
        public double GetResult()
   {
            return this._result;
        }
        public void AddToPrefixExpr(string expr)
        {
            this._tmp += expr;
        }
        public void AddOperator(string op)
        {
            if (this._tmp != null)
            {
                this._infixExpr.Add(this._tmp);
                this._infixExpr.Add(op);
                this._tmp = "";
            }
        }
        public void StackClear()
        {
            this._stack = new Stack<string>();
        }
        public void AddTmp()
        {
            if (this._tmp != null)
            {
                this._infixExpr.Add(_tmp);
            }
        }
        public void Reset()
        {
            this._result = 0;
            this._postfixExpr = new List<string>();
            this._infixExpr = new List<string>();
            this._stack = new Stack<string>();
            this._tmp = null;
        }
        private string add(double a, double b)
        {
            return Convert.ToString(b + a);
        }
        private string subtract(double a, double b)
        {
            return Convert.ToString(b - a); 
        }
        private string multiply(double a, double b)
        {
            return Convert.ToString(b * a);
        }
        private string divide(double a, double b)
        {
            return Convert.ToString(b / a);
        }
        
        private int GetPriority(string op)
        {
            if (op == "^") return 3;
            else if (op == "X" || op == "/") return 2;
            else if (op == "+" || op == "-") return 1;
            else return 0;
        }
        /// <summary>
        /// Gets Priority of the operator, if it returns 0 its a number pushes operators on the stack,
        /// and adds them according to their priority to the postfix expression
        /// </summary>
        /// <param name="infixExpr"></param>
        public void ConvertToPostfix(List<string> infixExpr)
        {
            foreach(string s in infixExpr)
            {
                if (GetPriority(s) == 0)
                {
                    this._postfixExpr.Add(s);
                    continue;
                }
                else if ((this._stack.Count == 0) || GetPriority(s) > GetPriority(this._stack.Peek()))
                {
                    this._stack.Push(s);
                    continue;
                }
                else { 
                    for(int i = 0; i <= this._stack.Count(); i++)
                    {
                        if (GetPriority(this._stack.Peek()) >= GetPriority(s))
                        {
                            this._postfixExpr.Add(this._stack.Pop());
                        }
                    }
                    this._stack.Push(s);
                }
            }
            foreach (string remainingOp in this._stack)
            {
                this._postfixExpr.Add(remainingOp);
            }
        }
        public void EvaluatePostfixExpression(List<string> postfixExpr) { 
            foreach(string s in postfixExpr)
            {
                if (s == "+")
                {
                    this._stack.Push(add(Double.Parse(this._stack.Pop()), Double.Parse(this._stack.Pop())));
                }else if(s == "-")
                {
                    this._stack.Push(subtract(Double.Parse(this._stack.Pop()), Double.Parse(this._stack.Pop())));
                }else if (s == "/")
                {
                    this._stack.Push(divide(Double.Parse(this._stack.Pop()), Double.Parse(this._stack.Pop())));
                }
                else if(s == "X")
                {
                    this._stack.Push(multiply(Double.Parse(this._stack.Pop()), Double.Parse((this._stack.Pop()))));
                }
                else
                {
                    this._stack.Push(s);
                }

            }
            this._result = Double.Parse(this._stack.Pop());
        }

    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculator NewCalculator = new Calculator();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void UpdateDisplay()
        {
            Display_Result.Text = NewCalculator.GetInfixExprAsString();
        }
        
        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("7");
            UpdateDisplay();
        }

        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("8");
            UpdateDisplay();
        }

        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("9");
            UpdateDisplay();
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("4");
            UpdateDisplay();
        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("5");
            UpdateDisplay();
        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("6");
            UpdateDisplay();
        }

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("1");
            UpdateDisplay();
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("2");
            UpdateDisplay();
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("3");
            UpdateDisplay();
        }

        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr("0");
            UpdateDisplay();
        }

        private void Button_Comma_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddToPrefixExpr(",");
            UpdateDisplay();
        }

        private void Button_Equals_Click(object sender, RoutedEventArgs e)
        {
            NewCalculator.AddTmp();
            NewCalculator.ConvertToPostfix(NewCalculator._infixExpr);
            NewCalculator.StackClear();
            NewCalculator.EvaluatePostfixExpression(NewCalculator._postfixExpr);
            Display_Result.Text = Convert.ToString(NewCalculator.GetResult());
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddOperator("+");
            UpdateDisplay();
        }

        private void Button_Subtract_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddOperator("-");
            UpdateDisplay();
        }

        private void Button_Divide_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddOperator("/");
            UpdateDisplay();

        }

        private void Button_Multiply_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
            NewCalculator.AddOperator("X");
            UpdateDisplay();
        }

        private void Button_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            NewCalculator.Reset();
            Display_Result.Text = null;
        }

    }
}