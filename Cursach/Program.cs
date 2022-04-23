using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var dbConnect = new dbConnect(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\Learn\курсовой\Cursach\Cursach\dbcursovoy.mdf; Integrated Security = True");

            Application.Run(new FormPremier());



        }
    }
}
