namespace Calculator_winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double num1) &&
                double.TryParse(textBox2.Text, out double num2))
            {
                double result = num1 + num2;
                textBox3.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("请输入有效的数字");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double num1) &&
                double.TryParse(textBox2.Text, out double num2))
            {
                double result = num1 - num2;
                textBox3.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("请输入有效的数字");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double num1) &&
                double.TryParse(textBox2.Text, out double num2))
            {
                double result = num1 * num2;
                textBox3.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("请输入有效的数字");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double num1) &&
                double.TryParse(textBox2.Text, out double num2))
            {
                double result = num1 / num2;
                textBox3.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("请输入有效的数字");
            }
        }
    }
}
