using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrizepicsV1
{
    public partial class FormReb : Form
    {
        List<ReboundsB> rb = new List<ReboundsB>();
        public FormReb(List<ReboundsB> rebB)
        {
            InitializeComponent();
            rb = rebB;
        }

        private void FormReb_Load(object sender, EventArgs e)
        {
            dataReb.ColumnCount = 5;
            dataReb.Columns[0].Name = "Name";
            dataReb.Columns[0].Width = 200;
            dataReb.Columns[1].Name = "Choice";
            dataReb.Columns[1].Width = 50;
            dataReb.Columns[2].Name = "Grade";
            dataReb.Columns[2].Width = 40;
            dataReb.Columns[3].Name = "Team";
            dataReb.Columns[3].Width = 40;
            dataReb.Columns[4].Name = "Line";
            dataReb.Columns[4].Width = 40;
           

            int n = 101;
            while (n != 0)
            {
                n--;
                foreach (ReboundsB r2 in rb)
                {
                    if (r2.grade == n)
                    {
                        string[] r = { r2.Name, r2.pick, r2.grade.ToString(), r2.Team, r2.Line.ToString() };
                        dataReb.Rows.Add(r);
                    }
                }
            }
        }
    }
}
