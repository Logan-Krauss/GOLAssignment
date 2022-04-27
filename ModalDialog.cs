using System;
using System.Windows.Forms;

namespace GOLAssignment
{
    public partial class ModalDialog : Form

    {

        
        //Made from double clicking window :: do not touch:
        private void ModalDialog_Load(object sender, EventArgs e)
        {
            //Whatever code that would need to apply when the window pops up would go here
            
        }
        //public event ApplyEventHandler Apply;

        public ModalDialog()
        {
            InitializeComponent();
            
        }
        public int MyInteger
        {
            get { return (int)numericUpDown1.Value; }
            set { numericUpDown1.Value = value; }
        }
        public string MyString { get; set; }


        
        //Accept Button
        private void button1_Click(object sender, EventArgs e)
        {
            //if (Apply != null) Apply(this, new ApplyEventArgs(this.MyInteger, this.MyString));
            //InitializeComponent();

        }



        //Cancel Button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Randomizing the Seed
        public void button1_Click_1(object sender, EventArgs e)
        {
            //Make a NEW random with the purpose of creating a random 10 digit number
            //and store that number in an int.
            Random rand = new Random((int)DateTime.Now.Ticks);
            //Use the stored information and convert to an int 
            numericUpDown1.Value = rand.Next();
            MyInteger = (int)numericUpDown1.Value;

        }


    }


        
}







        


