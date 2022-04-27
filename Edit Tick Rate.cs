using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLAssignment
{
    public partial class Edit_Tick_Rate : Form
    {
        public Edit_Tick_Rate()
        {
            InitializeComponent();
        }
        public int MilisecondInt { get; set; }

        public int MillisecondInt
        {
            get { return (int)numericUpDown1.Value; }
            set { numericUpDown1.Value = value; }
        }

        private void AcceptMilisecond_Click(object sender, EventArgs e)
        {

        }

        private void CancelMiliseconds_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    

    
}
