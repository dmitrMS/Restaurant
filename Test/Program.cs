using Interfaces.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.Utils;

namespace Test
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("Model1"));

            IdishService phoneServ = kernel.Get<IdishService>();
            IOrderService orderServ = kernel.Get<IOrderService>();
            IReportService reportServ = kernel.Get<IReportService>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(phoneServ, orderServ, reportServ));
        }
    }
}
