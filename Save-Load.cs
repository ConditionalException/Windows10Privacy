using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Save_Load : Form
    {
        public string label
        {
            get
            {
                return label;
            }
            set
            {
                lblSaveLoad.Text = value;
            }
        }

        public Save_Load()
        {
            InitializeComponent();
            lblSaveLoad.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
