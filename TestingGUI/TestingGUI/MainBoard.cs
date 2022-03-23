using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestingGUI
{
    public partial class MainBoard : Form
    {
        private int RowIndex = 0, ColIndex = 0;
        private ToolStripItem LastClicked;
        private BoardLogic boardLogic;
        
        public MainBoard()
        {
            this.CenterToParent();
            InitializeComponent();
            LoadBoard(10, 10);


            dataGridView1.AutoSize = true;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            
            
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;

            
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;


            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ShowCellToolTips = false;
           

            LastClicked = toolStrip1.Items.Add("10 x 10");
            LastClicked.BackColor = Color.LightBlue;
            toolStrip1.Items.Add("20 x 20");
            toolStrip1.Items.Add("30 x 30");
            toolStrip1.Items.Add("Solve");
            
            toolStrip1.MaximumSize = toolStrip1.Size;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised;



        }



        private void LoadBoard(int rows, int columns)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            boardLogic = new BoardLogic(rows, columns);
            
            for (int i = 0; i < columns; i++)
            {

                dataGridView1.Columns.Add(i.ToString(), String.Join("\n", boardLogic.ColumnsNumbers[i]));
               dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if ((i + 1) % 5 == 0)
                {

                }


            }
            for (int i = 0; i < rows; i++)
            {

                
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = String.Join("  ", boardLogic.RowsNumbers[i]);
                dataGridView1.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if ((i + 1) % 5 == 0)
                {
                        
                }


            }
            
            this.Size = dataGridView1.Size;
            
            
             
        }



        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverCell = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;


                if (currentMouseOverRow >= 0 && currentMouseOverCell >= 0)
                {
                    var cell = dataGridView1.Rows[currentMouseOverRow].Cells[currentMouseOverCell];
                    var val = cell.Value;
                    cell.Value = val ==  null || val.ToString() == "" ? "X" : "";
                    cell.Style.BackColor = Color.White;
                }
                return;

            }
            foreach (DataGridViewTextBoxCell cell in dataGridView1.SelectedCells)
                {
                
                
                    Color CurrentColor = cell.Style.BackColor;
                    if (CurrentColor.Equals(Color.Black))
                    {
                        cell.Style.BackColor = Color.White;
                    }
                    else
                    {
                        cell.Style.BackColor = Color.Black;
                    }
                   cell.Selected = false;
                    cell.Value = "";
                
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int num = int.Parse(LastClicked.Text.Split(" x ")[0]);
            if (!e.ClickedItem.Text.Equals("Solve"))
            {
                LastClicked.BackColor = DefaultBackColor;
                LastClicked = e.ClickedItem;
                LastClicked.BackColor = Color.LightBlue;
                num = int.Parse(e.ClickedItem.Text.Split(" x ")[0]);
                LoadBoard(num, num);
            }
            else
            {
                if (boardLogic.CheckBoard(dataGridView1))
                {
                    MessageBox.Show("Solved!");
                    LoadBoard(num, num);
                }
                else
                {
                    MessageBox.Show("Not solved yet!");
                }

            }
          
        }
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if(e.RowIndex != RowIndex)
            {
                dataGridView1.Rows[e.RowIndex].HeaderCell.Style.BackColor = Color.LightBlue;
                dataGridView1.Rows[RowIndex].HeaderCell.Style.BackColor = DefaultBackColor;
                RowIndex = e.RowIndex;
            }
            if (e.ColumnIndex != ColIndex)
            {
                dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.LightBlue;
                dataGridView1.Columns[ColIndex].HeaderCell.Style.BackColor = DefaultBackColor;
                ColIndex = e.ColumnIndex;
            }

        }
    }
}
