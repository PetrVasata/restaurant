using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurant
{
    public partial class Edit : Form
    {
        private List<Customer> _customer;
        public Edit(List<Customer> CustomerEdit)
        {
            InitializeComponent();
            _customer = CustomerEdit;
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            for (int i=0; i<_customer.Count;i++)
            {
                string name = "Customer" + i.ToString();
                comboBox1.Items.Add(name);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            int.TryParse(textBoxX.Text, out int posx);
            _customer[comboBox1.SelectedIndex].EndPoint.X = posx;
            int.TryParse(textBoxX.Text, out int posy);
            _customer[comboBox1.SelectedIndex].EndPoint.Y = posy;
            this.Close();
        }
    }
}
