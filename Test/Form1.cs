using BLL;
using BLL.DTO;
using BLL.Services;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Test
{
    public partial class Form1 : Form
    {
        IOrderService OrderService;
        IReportService reportService;
        IdishService dishService;
        //PhoneService phoneService = new PhoneService();
        OrderService orderService = new OrderService();
        dishService DishService = new dishService();
        //List<ManufacturerDto> allManufacturers;
        List<orderDto> allOrders;
        List<dishDto> allDishes;
        //Res
        //postgresEntities dbcontext = new postgresEntities();
        ///*Binding*/
        //List<order> allOrders;
        /*        List<order> allManufactures;*/

        public Form1(IdishService phoneservice, IOrderService orderservice, IReportService reportserv)
        {
            OrderService = orderservice;
            reportService = reportserv;
            dishService = phoneservice;
            InitializeComponent();
            loadData();
        }
        private void loadData()
        {
            //dbcontext.orders.Load();
            //allOrders = dbcontext.orders.ToList();
            //dataGridOrder.DataSource = allOrders;
            //allManufacturers = phoneService.GetManufacturers();
            loadDishes();
            loadOrders();

        }
        private void loadOrders()
        {
            dataGridView1.DataSource = orderService.GetAllOrders();
            allOrders = orderService.GetAllOrders();
        }
        private void loadDishes()
        {
            dataGridOrder.DataSource = DishService.GetAllDishes();
            allDishes = DishService.GetAllDishes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private int getSelectedRow(DataGridView dataGridView)
        {
            int index = -1;
            if (dataGridView.SelectedRows.Count > 0 || dataGridView.SelectedCells.Count == 1)
            {
                if (dataGridView.SelectedRows.Count > 0)
                    index = dataGridView.SelectedRows[0].Index;
                else index = dataGridView.SelectedCells[0].RowIndex;
            }
            return index;
        }
        private void dataGridOrder_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonOrderSave_Click(object sender, EventArgs e)
        {
            bool res = Validate();
            if (res)
            {
                orderService.Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Addform f = new Addform();
            //f.comboBoxManuf.DataSource = allManufactures;
            //f.comboBoxManuf.DisplayMember = "Name";
            //f.comboBoxManuf.ValueMember = "Id";

            DialogResult result = f.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            dishDto phone = new dishDto();
            //phone.manufacturerId = (int)f.comboBoxManuf.SelectedValue;
            phone.name = f.textBox8.Text; /*Convert.ToInt32(comboBox2.Text)*/
            phone.id = Convert.ToInt32(f.textBox6.Text);
            //phone.manufacturerId = (int)f.comboBoxManuf.SelectedValue;
            phone.weight = Convert.ToInt32(f.textBox7.Text);
            phone.price = Convert.ToInt32(f.textBox5.Text);
            phone.time_cook = Convert.ToInt32(f.textBox2.Text);
            phone.category = f.textBox1.Text;
            DishService.CreateDish(phone);
            allDishes = DishService.GetAllDishes();
            dataGridOrder.DataSource = allDishes;
            //dbcontext.orders.Add(phone);
            //dbcontext.SaveChanges();

            MessageBox.Show("Новый объект добавлен");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = getSelectedRow(dataGridOrder);
            if (index != -1)
            {
                int id = 0;
                bool converted = Int32.TryParse(dataGridOrder[1, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                /*                order ph = dbcontext.orders.Where(i => i.id_order == id).FirstOrDefault();
                /*                var ph = dbcontext.orders.Local.ToBindingList().Where(i => i.id_order == id).FirstOrDefault();*/
                //orderDto ph = dbcontext.orders.Local.ToBindingList().Where(x => x.id_order == id).FirstOrDefault();
                dishDto ph = allDishes.Where(i => i.id == id).FirstOrDefault();
                if (ph != null)
                {
                    Addform f = new Addform();
                    //f.comboBoxManuf.DataSource = allManufactures;
                    //f.comboBoxManuf.DisplayMember = "Name";
                    //f.comboBoxManuf.ValueMember = "Id";
                    //int id_stol = Convert.ToInt32(f.textBox7.Text); /*Convert.ToInt32(comboBox2.Text)*/
                    f.textBox8.Text = ph.name;
                    f.textBox6.Text = Convert.ToString(ph.id);
                    f.textBox7.Text = Convert.ToString(ph.weight);
                    f.textBox5.Text = Convert.ToString(ph.price);
                    f.textBox1.Text = ph.category;
                    f.textBox2.Text = Convert.ToString(ph.time_cook);
                    int weight;
                    int summ;
                    int id_order;
                    int time_cook;
                    try
                    {
                        weight = (int)Convert.ToInt32(f.textBox7.Text);
                        summ = (int)Convert.ToInt32(f.textBox5.Text);
                        id_order = (int)Convert.ToInt32(f.textBox6.Text);
                        time_cook = (int)Convert.ToInt32(f.textBox2.Text);

                    }
                    catch
                    {
                        //MessageBox.Show("Введите правильные данные");
                    }
                    //MessageBox.Show(order_status);
                    //int id_order = Convert.ToInt32(f.textBox6.Text);
                    //phone.manufacturerId = (int)f.comboBoxManuf.SelectedValue;
                    string name = f.textBox8.Text;
                    string category = f.textBox1.Text;
                    weight = (int)ph.weight;
                    summ = (int)ph.price;
                    id_order = (int)ph.id;
                    time_cook = (int)ph.time_cook;
                    name = ph.name;
                    category = ph.category;

                    DialogResult result = f.ShowDialog(this);

                    if (result == DialogResult.Cancel)
                        return;

                    ph.weight = (int)Convert.ToInt32(f.textBox7.Text);
                    ph.price = (int)Convert.ToInt32(f.textBox5.Text);
                    ph.id = (int)Convert.ToInt32(f.textBox6.Text);
                    ph.time_cook = (int)Convert.ToInt32(f.textBox2.Text);
                    ph.name = f.textBox8.Text;
                    ph.category = f.textBox1.Text;
                    DishService.UpdateDish(ph);
                    //dbcontext.SaveChanges();
                    MessageBox.Show("Объект обновлен");
                }
            }
            else
                MessageBox.Show("Ни один объект не выбран!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = getSelectedRow(dataGridOrder);
            if (index != -1)
            {
                int id = 0;
                bool converted = Int32.TryParse(dataGridOrder[1, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                orderService.DeleteOrder(id);
                dataGridOrder.DataSource = orderService.GetAllOrders();
            }
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            dataGridView3.DataSource = /*ReportServices*/reportService.ReportOrdersWithDelivery((int)numericUpDown1.Value);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            dataGridView4.DataSource = /*ReportServices*/reportService.ExecuteSP(textBox1.Text, (int)numericUpDown2.Value, textBox2.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MakeOrder f = new MakeOrder();
            if (f.ShowDialog() == DialogResult.OK)
                dataGridView1.DataSource = orderService.GetAllOrders();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            int index = getSelectedRow(dataGridOrder);
            if (index != -1)
            {
                int id = 0;
                bool converted = Int32.TryParse(dataGridOrder[1, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                DishService.DeleteDish(id);
                dataGridOrder.DataSource = DishService.GetAllDishes();
            }
        }

        private void buttonOrderSave_Click_1(object sender, EventArgs e)
        {
            bool res = Validate();
            if (res)
            {
                DishService.Save();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool res = Validate();
            if (res)
            {
                orderService.Save();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int index = getSelectedRow(dataGridOrder);
            if (index != -1)
            {
                int id = 0;
                bool converted = Int32.TryParse(dataGridOrder[1, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                /*                order ph = dbcontext.orders.Where(i => i.id_order == id).FirstOrDefault();
                /*                var ph = dbcontext.orders.Local.ToBindingList().Where(i => i.id_order == id).FirstOrDefault();*/
                //orderDto ph = dbcontext.orders.Local.ToBindingList().Where(x => x.id_order == id).FirstOrDefault();
                dishDto ph = allDishes.Where(i => i.id == id).FirstOrDefault();
                if (ph != null)
                {
                    Addform f = new Addform();
                    //f.comboBoxManuf.DataSource = allManufactures;
                    //f.comboBoxManuf.DisplayMember = "Name";
                    //f.comboBoxManuf.ValueMember = "Id";
                    //int id_stol = Convert.ToInt32(f.textBox7.Text); /*Convert.ToInt32(comboBox2.Text)*/
                    f.textBox8.Text = ph.name;
                    f.textBox6.Text = Convert.ToString(ph.id);
                    f.textBox7.Text = Convert.ToString(ph.weight);
                    f.textBox5.Text = Convert.ToString(ph.price);
                    f.textBox1.Text = ph.category;
                    f.textBox2.Text = Convert.ToString(ph.time_cook);
                    int weight;
                    int price;
                    int id_order;
                    int time_cook;
                    try
                    {
                        weight = (int)Convert.ToInt32(f.textBox7.Text);
                        price = (int)Convert.ToInt32(f.textBox5.Text);
                        id_order = (int)Convert.ToInt32(f.textBox6.Text);
                        time_cook = (int)Convert.ToInt32(f.textBox2.Text);

                    }
                    catch
                    {
                        //MessageBox.Show("Введите правильные данные");
                    }
                    //MessageBox.Show(order_status);
                    //int id_order = Convert.ToInt32(f.textBox6.Text);
                    //phone.manufacturerId = (int)f.comboBoxManuf.SelectedValue;
                    string name = f.textBox8.Text;
                    string category = f.textBox1.Text;
                    weight = (int)ph.weight;
                    price = (int)ph.price;
                    id_order = (int)ph.id;
                    time_cook = (int)ph.time_cook;
                    name = ph.name;
                    category = ph.category;

                    DialogResult result = f.ShowDialog(this);

                    if (result == DialogResult.Cancel)
                        return;

                    ph.weight = (int)Convert.ToInt32(f.textBox7.Text);
                    ph.price = (int)Convert.ToInt32(f.textBox5.Text);
                    ph.id = (int)Convert.ToInt32(f.textBox6.Text);
                    ph.time_cook = (int)Convert.ToInt32(f.textBox2.Text);
                    ph.name = f.textBox8.Text;
                    ph.category = f.textBox1.Text;
                    DishService.UpdateDish(ph);
                    //dbcontext.SaveChanges();
                    MessageBox.Show("Объект обновлен");
                }
            }
            else
                MessageBox.Show("Ни один объект не выбран!");
        }
    }
}
