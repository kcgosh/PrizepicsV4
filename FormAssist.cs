using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrizepicsV1;

namespace PrizepicsV1
{
    public partial class FormAssist : Form
    {
        List<AssistsB> asB = new List<AssistsB>();
        public FormAssist(List<AssistsB> assb)
        {
            InitializeComponent();
            asB = assb;
        }

        private void FormAssist_Load(object sender, EventArgs e)
        {
            dataAssist.ColumnCount = 5;
            dataAssist.Columns[0].Name = "Name";
            dataAssist.Columns[0].Width = 200;
            dataAssist.Columns[1].Name = "Choice";
            dataAssist.Columns[1].Width = 50;
            dataAssist.Columns[2].Name = "Grade";
            dataAssist.Columns[2].Width = 40;
            dataAssist.Columns[3].Name = "Team";
            dataAssist.Columns[3].Width = 40;
            dataAssist.Columns[4].Name = "Line";
            dataAssist.Columns[4].Width = 40;
            
           
           
            int n = 101;
            while (n != 0)
            {
                n--;
                foreach (AssistsB r2 in asB)
                {
                    if (r2.grade == n)
                    {
                        string[] r = { r2.Name, r2.pick, r2.grade.ToString(), r2.Team, r2.Line.ToString() };
                        dataAssist.Rows.Add(r);
                    }
                }
            }

        }
    }
}
