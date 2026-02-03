using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMS
{
    /// <summary>
    /// About box form.
    /// </summary>
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            this.Text = "About";
            this.Size = new Size(300, 150);

            Label lblInfo = new Label { Text = "EcoPark Animal Management System\nDeveloped by: Ibrahim\nVersion: 1.0", Location = new Point(10, 10), AutoSize = true };
            Controls.Add(lblInfo);

        }
        private void AboutForm_Load(object sender, EventArgs e)
        {
         }
    }
}