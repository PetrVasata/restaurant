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
    public partial class Form1 : Form
    {
        public List<Table> tableData = new List<Table>();

        private PictureBox[,] pictureGrid = new PictureBox[40, 40];
        private const int boxSize = 20; // Size of One grid
        private const int spacing = 0;  // Spacing between boxes

        private List<Customer> customer;
        private List<CustomerDataTS> CustomerData = new List<CustomerDataTS>();
        
        int[,] room;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            General general = new General();
       
            RootData jsonInit = general.DeseriliazeInit("init.JSON");

            tableData = general.CreateTables(jsonInit);
            FillRoom(jsonInit);
            room = jsonInit.room;

            CustomerData = jsonInit.customerDataTS;

            customer = new List<Customer>();
            InitCustomer(ref customer, CustomerData);

            RefreshRoom(ref room);
      
        }
        private void InitializeGrid()
        {
            for (int row = 0; row < 40; row++)
            {
                for (int col = 0; col < 40; col++)
                {
                    PictureBox pb = new PictureBox
                    {
                        Width = boxSize,
                        Height = boxSize,
                        BorderStyle = BorderStyle.None,
                        BackColor = Color.White,
                        Location = new Point(col * (boxSize + spacing), row * (boxSize + spacing)),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Visible = true
                    };

                    pictureGrid[row, col] = pb;
                    this.Controls.Add(pb);
                }
            }

            // Nastaví velikost formuláře podle mřížky
            this.ClientSize = new Size(40 * (boxSize + spacing), 40 * (boxSize + spacing) + 80);
        }
        public void SetEndPos(int x,int y, int index)
        {
            customer[index].EndPoint.X = x;
            customer[index].EndPoint.Y = y;
        }
        private void InitCustomer(ref List<Customer> customrers, List<CustomerDataTS> CustomerData)
        {
            foreach (CustomerDataTS LocalPerson in CustomerData)
            {
                customrers.Add(new Customer());
                Points pointSt = new Points(LocalPerson.xStart, LocalPerson.yStart);
                Points pointen = new Points(LocalPerson.xEnd, LocalPerson.yEnd);
                customrers[customrers.Count - 1].ActualPoint = pointSt;
                customrers[customrers.Count - 1].EndPoint = pointen;

            }
        }
        private void FillRoom(RootData jsonInit)
        {
            this.SuspendLayout();
            //Image brickImage = Image.FromFile("briks.jpg"); // Nahraj obrázek jednou

            for (int row = 0; row < 40; row++)
            {
                for (int col = 0; col < 40; col++)
                {
                    if (jsonInit.room[col, row] == 1) // Nebo true, podle typu pole
                    {
                        pictureGrid[col, row].Image = imageList1.Images[1]; 
                    }
                }
            }
            this.ResumeLayout(); // Obnoví překreslování
        }

        private void RefreshRoom(ref int [,] room)
        {
            this.SuspendLayout();
            //Image sandImage = Image.FromFile("sand.jpg"); // Nahraj obrázek jednou
            //Image robImage = Image.FromFile("rob.jpg"); // Nahraj obrázek jednou
            //Image emptyImage = Image.FromFile("empty.jpg"); // Nahraj obrázek jednou

            for (int row = 0; row < 40; row++)
            {
                for (int col = 0; col < 40; col++)
                {
                    if ((room[row, col] == 100) && pictureGrid[row, col].Image != imageList1.Images[2])
                        pictureGrid[row, col].Image = imageList1.Images[2];
                    if (room[row, col] == 0 && pictureGrid[row, col].Image != imageList1.Images[0])
                        pictureGrid[row, col].Image = imageList1.Images[0]; 
                }
            }
            this.ResumeLayout(); // Obnoví překreslování
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            for (int i=0; i < customer.Count; i++)
            {
                customer[i].Step(ref room);
            }
            RefreshRoom(ref room);
            timer1.Enabled = true; 
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            Edit editForm = new Edit(customer);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Můžeš zde provést další akce, například přepočítat cestu nebo obnovit grid
                //RefreshRoom(ref room);
            }
        }
    }
}
