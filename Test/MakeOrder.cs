using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BLL.DTO;
using BLL.Services;

namespace Test
{
    public partial class MakeOrder : Form
    {
        dishService pService = new dishService();
        OrderService oService = new OrderService();
        public MakeOrder()
        {
            InitializeComponent();
            ListBoxPhonesInOrder.DataSource = pService.GetAllDishes();
            ListBoxPhonesInOrder.DisplayMember = "Name";
            ListBoxPhonesInOrder.ValueMember = "Id";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ListBoxPhonesInOrder.CheckedItems.Count == 0)
            {
                MessageBox.Show("Не выбран ни один товар в заказ!");
                return;
            }
            List<int> items = new List<int>();
            foreach (var i in ListBoxPhonesInOrder.CheckedItems)
                items.Add((i as dishDto).id);

            orderDto order = new orderDto()
            {
                //Address = tbAddress.Text,
                stol = (int)Convert.ToInt32(tbCustomer.Text),
                //PhoneNumber = tbPhoneNumber.Text,
                OrderedPhonesIds = items,
            };

            OrderService service = new OrderService();
            bool result = service.MakeOrder(order);
            if (result)
            {
                MessageBox.Show("Success");
            }
            else MessageBox.Show("Failed");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tbCustomer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
