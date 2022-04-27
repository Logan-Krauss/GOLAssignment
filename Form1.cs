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
    public partial class Form1 : Form
    {
        int xx = Properties.Settings.Default.xx;
        int yy = Properties.Settings.Default.yy;
        

        // The universe array
        bool[,] universe = new bool[Properties.Settings.Default.xx, Properties.Settings.Default.yy];
        //Universe for altering neighbors
        bool[,] newUniverse = new bool[Properties.Settings.Default.xx, Properties.Settings.Default.yy];
        //Bool to chane toridal and finite
        bool universeSwap = true;
        //bool determining grid being drawn or not
        bool grid = true;
        //bool deciding neighbor settings
        bool neighbors = true;
        //current seed
        int seed;
        //Milliseconds
        int MIntervals = Properties.Settings.Default.MIntervals;
        //Bool to turn HUD ON/OFF
        bool HUDcheck = false;

        // Drawing colors
        Color gridColor = Properties.Settings.Default.gridColor;
        Color cellColor = Properties.Settings.Default.cellColor;
        Color backcolor = Properties.Settings.Default.backcolor;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = MIntervals; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running

        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            CopyUniverse();
            int CountNey = 0;

            for(int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    if (universeSwap == true)
                    {
                        CountNey = CountNeighborsFinite(i, j);
                    }
                    else
                    {
                        CountNey = CountNeighborsToroidal(i, j);
                    }

                    //Living cells with less than 2 living neighbors die in the next generation
                    if (CountNey < 2 && universe[i,j] == true)
                    {
                        newUniverse[i, j] = false;
                    }

                    //Living cells with more than 3 living neighbors die in the next generation.
                    if (CountNey > 3 && universe[i, j] == true)
                    {
                        newUniverse[i, j] = false;
                    }

                    //Living cells with 2 or 3 living neighbors live in the next generations

                    if ((CountNey == 2 || CountNey == 3) && universe[i,j] == true)
                    {
                        newUniverse[i, j] = true;
                    }

                    //Dead cells with exactly 3 living neighbors live in the next generation
                    if (CountNey == 3 && universe[i, j] == false)
                    {
                        newUniverse[i, j] = true;
                    }
                    
                }
            }
            

            // Increment generation count
            generations++;
            

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            graphicsPanel1.Invalidate();
            SetUniverse();

        }

        

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            LivingCellStatus();
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);
            Brush DeadBrush = new SolidBrush(backcolor);

            // Iterate through the universe in the y, top to bottom
            for (float y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (float x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[(int)x, (int)y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(DeadBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    if(grid == true)
                    {
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
                    else
                    {
                        continue;
                    }
                    //This Code is to count the neighbors, and put the number of neighbors in a cell...

                    Font font = new Font("Arial", 20f);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    RectangleF rect = new RectangleF(cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    int CountNey = 0;
                    if (universeSwap == true)
                    {
                        CountNey = CountNeighborsFinite((int)x, (int)y);
                    }
                    else if (universeSwap == false)
                    {
                        CountNey = CountNeighborsToroidal((int)x, (int)y);
                    }
                    if (CountNey != 0 && neighbors == true)
                    {
                        //Chainging Neighbor Colors

                        if (CountNey >= 5)
                        {
                            e.Graphics.DrawString(CountNey.ToString(), font, Brushes.DarkBlue, rect, stringFormat);
                        }
                        else if (CountNey <= 4 && CountNey > 2)
                        {
                            e.Graphics.DrawString(CountNey.ToString(), font, Brushes.DarkGreen, rect, stringFormat);
                        }
                        else if (CountNey == 2)
                        {
                            e.Graphics.DrawString(CountNey.ToString(), font, Brushes.DarkGoldenrod, rect, stringFormat);
                        }
                        else if (CountNey == 1)
                        {
                            e.Graphics.DrawString(CountNey.ToString(), font, Brushes.Red, rect, stringFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString(CountNey.ToString(), font, Brushes.Black, rect, stringFormat);
                        }
                    }
                }
            }
            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }
        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = (float)graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                float cellHeight = (float)graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                float x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                float y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[(int)x, (int)y] = !universe[(int)x, (int)y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }      

        //-FUNCTIONS-

        //Checks rules for finite logic.
        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0)
                    {
                        continue;
                    }
                    // if xCheck is less than 0 then continue
                    if (xCheck < 0)
                    {
                        continue;
                    }
                    // if yCheck is less than 0 then continue
                    if (yCheck < 0)
                    {
                        continue;
                    }
                    // if xCheck is greater than or equal too xLen then continue
                    if (xCheck >= xLen)
                    {
                        continue;
                    }
                    // if yCheck is greater than or equal too yLen then continue
                    if (yCheck >= yLen)
                    {
                        continue;
                    }
                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }
        //Checks rules for Toroidal logic.
        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0)
                    {
                        continue;
                    }
                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0)
                    {
                        xCheck = xLen - 1;
                    }
                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0)
                    {
                        yCheck = yLen - 1;
                    }
                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen)
                    {
                        xCheck = 0;
                    }
                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen)
                    {
                        yCheck = 0;
                    }

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }
        //Clearing/Ressetting the Screen but not settings in the Toolstrip.
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    universe[i, j] = false;
                }
            }
            graphicsPanel1.Invalidate();
        }
        //Pressing PLAY button
        private void Play_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }
        //Pressing 'X'
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Pressing "Pause"
        private void Pause_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;   
        }
        //Pressing "Next"
        private void Next_Click(object sender, EventArgs e)
        {
            NextGeneration();
        }
        //Tudorial button Logic
        private void todorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            universeSwap = false;
            finiteToolStripMenuItem.Checked = false;
            todorialToolStripMenuItem.Checked = true;
            graphicsPanel1.Invalidate();
        }
        //Finite Button Logic
        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            universeSwap = true;
            todorialToolStripMenuItem.Checked = false;
            finiteToolStripMenuItem.Checked = true;
            graphicsPanel1.Invalidate();

        }
        //Clearing/Ressetting the Screen but not settings in the Toolstrip.
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    universe[i, j] = false;
                }
            }
            graphicsPanel1.Invalidate();
        }
        //Reused Randomize function
        public void randomize(int timeOrSeed)
        {
            var randInt = new Random(timeOrSeed);
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    int result = randInt.Next(0, 2);
                    if(result == 0)
                    {
                        universe[i, j] = false;
                    }
                    else if(result > 0)
                    {
                        universe[i, j] = true;
                    }
                    
                }
            }
            toolStripStatusLabel1.Text = "Current Seed: " + timeOrSeed.ToString();
            seed = timeOrSeed;

        }
        //Randomize By Seed Input
        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModalDialog dlg = new ModalDialog();

            //dlg.Apply += new ApplyEventHandler(dlg_Apply);
            dlg.MyInteger = seed;
            
            
            if (DialogResult.OK == dlg.ShowDialog())
            {
                randomize(dlg.MyInteger);
                graphicsPanel1.Invalidate();
            }   
        }
        //Extra Code Unneeded from ModalBox PDF
        void dlg_Apply(object sender, ApplyEventArgs e)
        {
            //retrieve event arguements
            int x = e.MyInteger;
            string s = e.MyString;
        }
        //Randomize based on Time
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
             randomize((int)DateTime.Now.Ticks);
             graphicsPanel1.Invalidate();
        }
        //Bringing Up the Color Select Menu
        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog CellColorPicker = new ColorDialog();
            CellColorPicker.Color = cellColor;
            if (DialogResult.OK == CellColorPicker.ShowDialog())
            {
                cellColor = CellColorPicker.Color;
                graphicsPanel1.Invalidate();
            }
        }
        //Saving the form's settings
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.cellColor = cellColor;
            Properties.Settings.Default.backcolor = backcolor;
            Properties.Settings.Default.gridColor = gridColor;
            Properties.Settings.Default.MIntervals = MIntervals;
            Properties.Settings.Default.xx = xx;
            Properties.Settings.Default.yy = yy;
            Properties.Settings.Default.Save();
        }
        //Grid Toggle Setting Check Mark functionality done
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(grid == true)
            {
                //code here
                grid = false;
                gridToolStripMenuItem.Checked = false;
                
            }
            else
            {
                //code here
                grid = true;
                gridToolStripMenuItem.Checked = true;
                
            }
            graphicsPanel1.Invalidate();
        }
        //Keeping track of living cells in display counter
        private void LivingCellStatus()
        {
            int counter = 0;
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    if (universe[i, j] == true)
                    {
                        counter++;
                    }
                    LivingCellCount.Text = "Living Cell Count =" + counter.ToString();
                }
            }
        }
        //Randomizing from CURRENT seed
        private void fromCurrentSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            randomize(seed);
            graphicsPanel1.Invalidate();
        }
        //Background Color
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog BackcolorPicker = new ColorDialog();
            BackcolorPicker.Color = backcolor;
            if (DialogResult.OK == BackcolorPicker.ShowDialog())
            {
                backcolor = BackcolorPicker.Color;
                graphicsPanel1.Invalidate();
            }
        }
        //Toggle Neighbors
        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(neighbors == true)
            {
                neighborCountToolStripMenuItem.Checked = false;
                neighbors = false;    
            }
            else
            {
                neighborCountToolStripMenuItem.Checked = true;
                neighbors = true;
            }
            graphicsPanel1.Invalidate();
        }
        //Grid Color Changer
        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog gColor = new ColorDialog();
            gColor.Color = gridColor;
            if (DialogResult.OK == gColor.ShowDialog())
            {
                gridColor = gColor.Color;
                graphicsPanel1.Invalidate();
            }
        }
        //Change Milliseconds and Array Size width and Height. 
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyUniverse();
            //Edit_Tick_Rate was the old name when I thought the box would just be used for milliseconds It is now ALL OPTIONS. 
            Edit_Tick_Rate Milli = new Edit_Tick_Rate();
            Milli.MillisecondInt = MIntervals;
            Milli.Xaxissize = xx;
            Milli.Yaxissize = yy;

            if (DialogResult.OK == Milli.ShowDialog())
            {
                MIntervals = Milli.MillisecondInt;
                timer.Interval = MIntervals;

                xx = Milli.Xaxissize;

                yy = Milli.Yaxissize;

                //if(xx == Milli.Xaxissize && yy == Milli.Yaxissize)
                //{
                    
                //}
                //else
                //{
                    universe = new bool[Milli.Xaxissize, Milli.Yaxissize];
                    newUniverse = new bool[Milli.Xaxissize, Milli.Yaxissize];
                //}
                graphicsPanel1.Invalidate();
            }
            //Scratchpad: New array to change size and settings



            //Do I need this scratchpad?
            //bool[,] scratchPad = new bool[5, 5];

            //bool[,] temp = universe;
            //universe = scratchPad;
            //scratchPad = temp;
            SetUniverse();
        }
        //Universe Copy OG to Scratchpad
        private void CopyUniverse()
        {
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    newUniverse[i,j] = universe[i, j];
                }
            }
        }
        //Set the Scratchpad to Universe
        private void SetUniverse()
        {
            for (int i = 0; i < newUniverse.GetLength(0); i++)
            {
                for (int j = 0; j < newUniverse.GetLength(1); j++)
                {
                    universe[i, j] = newUniverse[i, j];
                }
            }
        }

        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
    
}
