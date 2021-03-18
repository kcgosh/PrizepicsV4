using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PrizepicsV1
{

    public partial class FormMain : Form
    {
        //intialize all the list 
        private static List<Offense> mainO = new List<Offense>();
        private static List<Defense> mainD = new List<Defense>();
        private static List<Line> mainL = new List<Line>();
        private List<PointsB> Pbets = new List<PointsB>();
        private List<AssistsB> Abets = new List<AssistsB>();
        private List<ReboundsB> Rbets = new List<ReboundsB>();
        private static List<Advanced> mainA  = new List<Advanced>();
        private static List<Daily> dailyL = new List<Daily>();
        private static List<Last5> lastf = new List<Last5>();
        private static List<Last10> lastt = new List<Last10>();


        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Create the table
            mainGrid.ColumnCount = 7;
            mainGrid.Columns[0].Name = "Name";
            mainGrid.Columns[0].Width = 200;
            mainGrid.Columns[1].Name = "Choice";
            mainGrid.Columns[1].Width = 50;
            mainGrid.Columns[2].Name = "Grade";
            mainGrid.Columns[2].Width = 40;
            mainGrid.Columns[3].Name = "Team";
            mainGrid.Columns[3].Width = 40;
            mainGrid.Columns[4].Name = "vs Team";
            mainGrid.Columns[4].Width = 40;
            mainGrid.Columns[5].Name = "Line";
            mainGrid.Columns[5].Width = 40;
            mainGrid.Columns[6].Name = "Hit";
            mainGrid.Columns[6].Width = 40;
        }

        private void doDeedButton(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;

            int h = 0;
            int m = 0;
            int dailyFlag = 0;

            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                fileExt = Path.GetExtension(filePath); //get the file extension  
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        //Convert excel tables to data tables
                        DataTable dtStats = new DataTable();
                        DataTable dtDstat = new DataTable();
                        DataTable dtLine = new DataTable();
                        DataTable dtAdv = new DataTable();
                        DataTable dtDaily = new DataTable();
                        DataTable dtlastfive = new DataTable();
                        DataTable dtlastten = new DataTable();

                        dtStats = ReadExcel1(filePath, fileExt);
                        dtDstat = ReadExcel2(filePath, fileExt);
                        dtLine = ReadExcel3(filePath, fileExt);
                        dtAdv = ReadExcel4(filePath, fileExt);
                        dtDaily = ReadExcel5(filePath, fileExt);
                        dtlastfive = ReadExcel6(filePath, fileExt);
                        dtlastten = ReadExcel7(filePath, fileExt);


                        //add Offensive Stats
                        foreach (var row in dtStats.AsEnumerable())
                        {

                            mainO.Add(new Offense
                            {
                                //strings
                                Team = row.Field<string>("F5"),
                                Name = row.Field<string>("F2"),
                                Pos = row.Field<string>("F3"),

                                Ppg = row.Field<double?>("F30"),
                                Apg = row.Field<double?>("F25"),
                                Age = row.Field<double?>("F4"),
                                Games = row.Field<double?>("F6"),
                                GameStart = row.Field<double?>("F7"),
                                min = row.Field<double?>("F8"),
                                Fgm = row.Field<double?>("F9"),
                                Fga = row.Field<double?>("F10"),
                                Fgp = row.Field<double?>("F11"),
                                Threepm = row.Field<double?>("F12"),
                                Threepa = row.Field<double?>("F13"),
                                Threepp = row.Field<double?>("F14"),
                                Twopm = row.Field<double?>("F15"),
                                Twopa = row.Field<double?>("F16"),
                                Twopp = row.Field<double?>("F17"),
                                EffFgp = row.Field<double?>("F18"),
                                Ftm = row.Field<double?>("F19"),
                                Fta = row.Field<double?>("F20"),
                                Ftp = row.Field<double?>("F21"),
                                Orb = row.Field<double?>("F22"),
                                Drb = row.Field<double?>("F23"),
                                Rpg = row.Field<double?>("F24")
                            });

                        }

                        //add Defensive Stats
                        foreach (var row1 in dtDstat.AsEnumerable())
                        {
                            mainD.Add(new Defense
                            {
                                //strings

                                Team = row1.Field<string>("F1"),
                                PointAllow = row1.Field<double?>("F2"),
                                RebAllow = row1.Field<double?>("F3"),
                                OffRebAllow = row1.Field<double?>("F4"),
                                DefRebAllow = row1.Field<double?>("F7"),
                                AssAllow = row1.Field<double?>("F5"),
                                FgAllow = row1.Field<double?>("F8"),
                                ThreeAllow = row1.Field<double?>("F9"),
                                Blk = row1.Field<double?>("F10"),
                                OppFgp = row1.Field<double?>("F12"),
                                Oppthreep = row1.Field<double?>("F13"),
                                Rtg = row1.Field<double?>("F14")


                            });
                        }

                        //add Advanced Stats
                        foreach (var row3 in dtAdv.AsEnumerable())
                        {
                            mainA.Add(new Advanced
                            {
                                //strings

                                Name = row3.Field<string>("F2"),
                                Team = row3.Field<string>("F5"),
                                Pos = row3.Field<string>("F3"),

                                PlayEffRate = row3.Field<double?>("F8"),
                                Truesp = row3.Field<double?>("F9"),
                                ThreeAttRate = row3.Field<double?>("F10"),
                                FtRate = row3.Field<double?>("F11"),
                                Offrp = row3.Field<double?>("F12"),
                                Deffrp = row3.Field<double?>("F13"),
                                Totalrp = row3.Field<double?>("F14"),
                                Assistp = row3.Field<double?>("F15"),
                                Usg = row3.Field<double?>("F19"),
                                Obpm = row3.Field<double?>("F24")


                            });
                        }

                        //add Line
                      

                        foreach (var row5 in dtlastfive.AsEnumerable())
                        {
                            lastf.Add(new Last5
                            {
                                Name = row5.Field<string>("F2"),
                                Team = row5.Field<string>("F3"),

                                Ppg = row5.Field<double?>("F9"),
                                Apg = row5.Field<double?>("F22"),
                                min = row5.Field<double?>("F8"),
                                Fgm = row5.Field<double?>("F10"),
                                Fga = row5.Field<double?>("F11"),
                                Fgp = row5.Field<double?>("F12"),
                                Threepm = row5.Field<double?>("F13"),
                                Threepa = row5.Field<double?>("F14"),
                                Threepp = row5.Field<double?>("F15"),
                                Ftm = row5.Field<double?>("F16"),
                                Fta = row5.Field<double?>("F17"),
                                Ftp = row5.Field<double?>("F18"),
                                Trb = row5.Field<double?>("F21"),
                                pm = row5.Field<double?>("F30")

                            });

                        }

                        foreach (var row6 in dtlastten.AsEnumerable())
                        {
                            lastt.Add(new Last10
                            {
                                Name = row6.Field<string>("F2"),
                                Team = row6.Field<string>("F3"),

                                Ppg = row6.Field<double?>("F9"),
                                Apg = row6.Field<double?>("F22"),
                                min = row6.Field<double?>("F8"),
                                Fgm = row6.Field<double?>("F10"),
                                Fga = row6.Field<double?>("F11"),
                                Fgp = row6.Field<double?>("F12"),
                                Threepm = row6.Field<double?>("F13"),
                                Threepa = row6.Field<double?>("F14"),
                                Threepp = row6.Field<double?>("F15"),
                                Ftm = row6.Field<double?>("F16"),
                                Fta = row6.Field<double?>("F17"),
                                Ftp = row6.Field<double?>("F18"),
                                Trb = row6.Field<double?>("F21"),
                                pm = row6.Field<double?>("F30")

                            });
                        }

                        foreach (var row2 in dtLine.AsEnumerable())
                        {
                            mainL.Add(new Line
                            {
                                //strings

                                Name = row2.Field<string>("F1"),
                                Pts = row2.Field<double?>("F2"),
                                Ast = row2.Field<double?>("F3"),
                                Rbs = row2.Field<double?>("F4"),
                                Team = row2.Field<string>("F5"),
                                OTeam = row2.Field<string>("F6"),
                                N = row2.Field<double?>("F7")

                            });
                        }

                        DialogResult dialogResult = MessageBox.Show("Yes for results, no for regular", "Choice", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            
                            //add that days points
                            foreach (var row4 in dtDaily.AsEnumerable())
                            {
                                dailyL.Add(new Daily
                                {
                                    //strings

                                    Name = row4.Field<string>("F2"),
                                    pts = row4.Field<double?>("F9"),
                                    Team = row4.Field<string>("F3"),

                                });
                            }
                            dailyFlag = 1;
                        }
                        else
                        {
                           
                        }


                        PointsB z = new PointsB();

                        foreach (Line L in mainL)
                        {
                            z.Name = L.Name;
                            int p = 0;
                            int ass = 0;
                            int reb = 0;
                            foreach (Offense i in mainO)
                            {
                                if (string.IsNullOrEmpty(i.Name))
                                { }
                                else
                                {
                                    if (i.Name == L.Name && i.Team == L.Team)
                                    {
                                        //MessageBox.Show("Off" +L.Name + " " + L.N);

                                        int p1 = AvgPointsline(i.Ppg, L.Pts);
                                        int p2 = Minutes(i.min);
                                        int p3 = FgAtempt(i.Fgm, i.Fga);
                                        int p4 = FgPercentage(i.Fgp);
                                        int p5 = twopoint(i.Twopm, i.Twopa);
                                        int p7 = Fgeffect(i.EffFgp, i.Games);
                                        int p8 = freethrowa(i.Fta);
                                        int p9 = freethrowp(i.Ftp);
                                        int p10 = threepointRatio(i.Threepa);
                                        int p11 = threePP(i.Threepp);

                                        int r1 = AvgRebline(i.Rpg, L.Rbs);
                                        int a1 = AvgAssistline(i.Rpg, L.Rbs);

                                        p = p1 + p2 + p3 + p4 + p5 +  p7 + p8 + p9 + p10 + p11;
                                        ass = a1;
                                        reb = r1;
                                    }
                                }
                            }

                            foreach (Advanced av in mainA)
                            {
                                if (L.Name == av.Name && L.Team == av.Team)
                                {
                                    //MessageBox.Show("Adv" +L.Name + " " + L.N);

                                    int p19 = PlayerEff(av.PlayEffRate);
                                    int p20 = TrueScore(av.Truesp);
                                    int p21 = Ftrate(av.FtRate);
                                    int p22 = usagep(av.Usg);
                                    //int p23 = OffBox(av.Obpm);

                                    int a3 = Assp(av.Assistp);
                                    int a4 = usagea(av.Usg);
                                    //int a5 = OffBox(av.Obpm);

                                    int r3 = Rebp(av.Totalrp);
                                    int r4 = usager(av.Usg);
                                    //int r5 = OffBox(av.Obpm);

                                    p = p + p19 + p20 + p21 + p22;// + p23;
                                    ass = ass + a3 + a4;// + a5;
                                    reb = reb + r3 + r4;// + r5;
                                }
                            }

                            foreach (Defense d in mainD)
                            {
                                if (d.Team == L.OTeam)
                                {
                                    //MessageBox.Show("Def" +L.Name + " " + L.N);
                                    //pts
                                    int p12 = DPA(d.PointAllow);
                                    int p13 = DFGA(d.FgAllow);
                                    int p14 = DTA(d.ThreeAllow);
                                    int p15 = Doppp(d.OppFgp);
                                    int p16 = Doppt(d.Oppthreep);
                                    int p17 = Drate(d.Rtg);

                                    //Assists
                                    int a2 = DAA(d.AssAllow);


                                    //Rebounds
                                    int r2 = DRA(d.RebAllow);


                                    p = p + p12 + p13 + p14 + p15 + p16 + p17;
                                    ass = ass + a2;
                                    reb = reb + r2;
                                }
                            }

                            foreach(Last5 lf in lastf)
                            {
                                if(L.Name == lf.Name && L.Team == lf.Team)
                                {
                                    //MessageBox.Show("L5"+L.Name + " " + L.N);
                                    int l1 = FAvgPointsline(lf.Ppg, L.Pts);
                                    int l2 = FMinutes(lf.min);
                                    int l3 = FFgPercentage(lf.Fgp);
                                    int l4 = FFgAtempt(lf.Fgm, lf.Fga);
                                    int l5 = FthreepointRatio(lf.Threepa);
                                    int l6 = FthreePP(lf.Threepp);
                                    int l7 = Ffreethrowp(lf.Ftp);
                                    int l8 = Ffreethrowa(lf.Fta);

                                    int a3 = FAvgAssistline(lf.Apg,L.Ast);

                                    int r3 = FAvgRebline(lf.Trb, L.Rbs);
                                    p = p + l1 + l2 + l3 + l4 + l5 +l6 + l7 + l8;
                                    ass = ass + a3;
                                    reb = reb + r3;
                                }
                            }

                            foreach (Last10 lt in lastt)
                            {
                                if (L.Name == lt.Name && L.Team == lt.Team)
                                {
                                    //MessageBox.Show("L10"+L.Name + " " + L.N);
                                    int t1 = TAvgPointsline(lt.Ppg, L.Pts);
                                    int t2 = TMinutes(lt.min);
                                    int t3 = TFgPercentage(lt.Fgp);
                                    int t4 = TFgAtempt(lt.Fgm, lt.Fga);
                                    int t5 = TthreepointRatio(lt.Threepa);
                                    int t6 = TthreePP(lt.Threepp);
                                    int t7 = Tfreethrowp(lt.Ftp);
                                    int t8 = Tfreethrowa(lt.Fta);

                                    int a4 = TAvgAssistline(lt.Apg, L.Ast);

                                    int r4 = TAvgRebline(lt.Trb, L.Rbs);
                                    
                                    p = p + t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;
                                    ass = ass + a4;
                                    reb = reb + r4;
                                }
                            }

                            //MessageBox.Show(L.Name + " " + p + " " + L.N);
                            if (dailyFlag == 1)
                            {

                                foreach (Daily d in dailyL)
                                {
                                    if (L.Name == d.Name)
                                    {
                                        if (d.pts >= L.Pts)
                                        {
                                            Pbets.Add(new PointsB
                                            {
                                                Name = L.Name,
                                                pick = "Points",
                                                grade = p,
                                                Team = L.Team,
                                                Nums = L.Pts,
                                                OTeam = L.OTeam,
                                                Hit = "Hit :)"
                                            });
                                            h++;
                                        }
                                        else
                                        {
                                            Pbets.Add(new PointsB
                                            {
                                                Name = L.Name,
                                                pick = "Points",
                                                grade = p,
                                                Team = L.Team,
                                                Nums = L.Pts,
                                                OTeam = L.OTeam,
                                                Hit = "Miss :("
                                            });
                                            m++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //MessageBox.Show(L.Name + " " + p + " " + L.N);
                                Pbets.Add(new PointsB
                                {
                                    Name = L.Name,
                                    pick = "Points",
                                    grade = p,
                                    Team = L.Team,
                                    Nums = L.Pts,
                                    OTeam = L.OTeam,
                                    Hit = L.N.ToString()
                                });
                            }

                            Abets.Add(new AssistsB
                            {
                                Name = L.Name,
                                pick = "Assists",
                                grade = ass,
                                Team = L.Team,
                                Line = L.Ast
                            });
                            Rbets.Add(new ReboundsB
                            {
                                Name = L.Name,
                                pick = "Rebounds",
                                grade = reb,
                                Team = L.Team,
                                Line = L.Rbs
                            });

                        }
                            int n = 201;
                            while (n != 0)
                            {
                                n--;
                                foreach (PointsB p2 in Pbets)
                                {
                                    if (p2.grade == n)
                                    {
                                        string[] r = { p2.Name, p2.pick, p2.grade.ToString(), p2.Team, p2.OTeam, p2.Nums.ToString(), p2.Hit.ToString() };
                                        mainGrid.Rows.Add(r);
                                    }
                                }
                            }

                            if(dailyFlag == 1)
                            {
                                MessageBox.Show("Hit: " + h + "   Miss: " + m);
                            }


                        DialogResult dialogResult2 = MessageBox.Show("Want assists and reb", "Choice", MessageBoxButtons.YesNo);
                        if (dialogResult2 == DialogResult.Yes)
                        {
                            FormAssist As = new FormAssist(Abets);
                            As.Show();

                            FormReb Rb = new FormReb(Rbets);
                            Rb.Show();
                        }
                        else { }
                    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }


        }



        ///Adding excel
        public DataTable ReadExcel1(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }
        public DataTable ReadExcel2(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet2$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }
        public DataTable ReadExcel3(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet3$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }
        public DataTable ReadExcel4(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet4$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }
        public DataTable ReadExcel5(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet5$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }
        public DataTable ReadExcel6(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet6$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }
        public DataTable ReadExcel7(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet7$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }



        public List<AssistsB> AgetAll()
        {
            List<AssistsB> assB = new List<AssistsB>();
            foreach (AssistsB asB in Abets)
            {
                
                assB.Add(asB);
            }
            return assB;
        }
        public List<ReboundsB> RgetAll()
        {
            List<ReboundsB> RebB = new List<ReboundsB>();
            foreach (ReboundsB R in Rbets)
            {
                RebB.Add(R);
            }
            return RebB;
        }



        //Checks
        private int AvgPointsline(double? avgp, double? lp)
        {
            if (avgp >= lp)
                return 4;
            else if (avgp >= (lp - 1))
                return 2;
            else
                return -1;
        }
        private int Minutes(double? time)
        {
            if (time >= 30.0)
                return 3;
            else if (time >= 20.0)
                return 2;
            else if (time >= 10.0)
                return 1;
            else
                return 0;
        }
        private int FgPercentage(double? fg)
        {
            if (fg >= .5)
                return 5;
            else if (fg >= .4)
                return 4;
            else if (fg >= .3)
                return 1;
            else
                return 0;
        }
        private int FgAtempt(double? fg, double? fga)
        {
            if (fg > (fga / 2.0))
                return 3;
            if (fg > (fga / 3.0))
                return 1;
            else
                return 0;
        }
        private int twopoint(double? tp, double? tpa)
        {
            if ((2.0 * tp) > (tpa+2))
                return 4;
            if ((2.0 * tp) > tpa)
                return 3;
            else
                return 1;
        }
        private int threepointRatio(double? tpa)
        {
            if (tpa >= 8.0)
                return 5;
            else if (tpa >= 5.0)
                return 3;
            else
                return 1;
        }
        private int threePP(double? tpp)
        {
            if (tpp >= .5)
                return 6;
            else if (tpp >= .4)
                return 4;
            else if (tpp >= .3)
                return 2;
            else
                return 0;
        }
        private int Fgeffect(double? efg, double? gp)
        {
            if (efg >= .6 && gp <= 10)
                return -2;
            else if (efg >= .6)
                return 5;
            else if (efg >= .5)
                return 4;
            else if (efg >= .4)
                return 2;
            else
                return 0;

        }
        private int freethrowa(double? Fta)
        {
            if (Fta >= 10.0)
                return 5;
            else if (Fta >= 5.0)
                return 3;
            else if (Fta >= 0.0)
                return 1;
            else
                return 0;

        }
        private int freethrowp(double? Ftp)
        {
            if (Ftp >= 90.0)
                return 5;
            else if (Ftp >=80.0)
                return 4;
            else if (Ftp >= 70.0)
                return 3;
            else if (Ftp == 60.0)
                return 1;
            else
                return 0;

        }
        private int DPA(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int DFGA(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int DTA(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int Doppp(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int Doppt(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int Drate(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int PlayerEff(double? x)
        {
            if (x >= 25.0)
                return 5;
            else if (x >= 20.0)
                return 4;
            else if (x >= 15.0)
                return 3;
            else if (x >= 10.0)
                return 2;
            else
                return 0;
        }
        private int TrueScore(double? x)
        {
            if (x >= .65)
                return 4;
            else if (x >= .5)
                return 3;
            else if (x >= .3)
                return 1;
            else
                return 0;
        }
        private int Ftrate(double? x)
        {
            if (x >= .5)
                return 3;
            else if (x >= .3)
                return 2;
            else if (x >= 1)
                return 1;
            else
                return 0;
        }
        private int usagep(double? x)
        {
            if (x >= 30.0)
                return 5;
            else if (x >= 25.0)
                return 4;
            else if (x >= 20)
                return 3;
            else if (x >= 15)
                return 2;
            else if (x >= 10)
                return 1;
            else
                return 0;
        }



        private int FAvgPointsline(double? avgp, double? lp)
        {
            if (avgp >= lp)
                return 3;
            else if (avgp >= (lp - 1))
                return 2;
            else
                return -2;
        }
        private int FMinutes(double? time)
        {
            if (time >= 30.0)
                return 3;
            else if (time >= 20.0)
                return 2;
            else if (time >= 10.0)
                return 1;
            else
                return 0;
        }
        private int FFgPercentage(double? fg)
        {
            if (fg >= .5)
                return 5;
            else if (fg >= .4)
                return 4;
            else if (fg >= .3)
                return 1;
            else
                return 0;
        }
        private int FFgAtempt(double? fgm, double? fga)
        {
            if (fgm > (fga / 2.0))
                return 3;
            if (fgm > (fga / 3.0))
                return 1;
            else
                return 0;
        }
        private int FthreepointRatio(double? tpa)
        {
            if (tpa >= 8.0)
                return 5;
            else if (tpa >= 5.0)
                return 3;
            else
                return 1;
        }
        private int FthreePP(double? tpp)
        {
            if (tpp >= .5)
                return 6;
            else if (tpp >= .4)
                return 4;
            else if (tpp >= .3)
                return 2;
            else
                return 0;
        }
        private int Ffreethrowa(double? Fta)
        {
            if (Fta >= 10.0)
                return 5;
            else if (Fta >= 5.0)
                return 3;
            else if (Fta >= 3.0)
                return 2;
            else if (Fta == 0.0)
                return 0;
            else
                return 1;

        }
        private int Ffreethrowp(double? Ftp)
        {
            if (Ftp >= 90.0)
                return 5;
            else if (Ftp >= 80.0)
                return 3;
            else if (Ftp >= 70.0)
                return 2;
            else
                return 0;

        }

        private int TAvgPointsline(double? avgp, double? lp)
        {
            if (avgp >= lp)
                return 3;
            else if (avgp >= (lp - 1))
                return 2;
            else
                return -2;
        }
        private int TMinutes(double? time)
        {
            if (time >= 30.0)
                return 4;
            else if (time >= 20.0)
                return 3;
            else if (time >= 10.0)
                return 1;
            else
                return 0;
        }
        private int TFgPercentage(double? fg)
        {
            if (fg >= .5)
                return 5;
            else if (fg >= .4)
                return 4;
            else if (fg >= .3)
                return 1;
            else
                return 0;
        }
        private int TFgAtempt(double? fgm, double? fga)
        {
            if (fgm > (fga / 2.0))
                return 3;
            if (fgm > (fga / 3.0))
                return 1;
            else
                return 0;
        }
        private int TthreepointRatio(double? tpa)
        {
            if (tpa >= 8.0)
                return 5;
            else if (tpa >= 5.0)
                return 3;
            else
                return 1;
        }
        private int TthreePP(double? tpp)
        {
            if (tpp >= .5)
                return 6;
            else if (tpp >= .4)
                return 4;
            else if (tpp >= .3)
                return 2;
            else
                return 0;
        }
        private int Tfreethrowa(double? Fta)
        {
            if (Fta >= 10.0)
                return 5;
            else if (Fta >= 5.0)
                return 3;
            else if (Fta >= 3.0)
                return 2;
            else if (Fta == 0.0)
                return 0;
            else
                return 1;

        }
        private int Tfreethrowp(double? Ftp)
        {
            if (Ftp >= 90.0)
                return 5;
            else if (Ftp >= 80.0)
                return 3;
            else if (Ftp >= 70.0)
                return 2;
            else
                return 0;

        }





        private int AvgAssistline(double? avga, double? lp)
        {
            if (avga >= lp)
                return 4;
            else if (avga >= (lp - 1.0))
                return 3;
            else
                return 1;
        }
        private int DAA(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int Assp(double? x)
        {
            if (x >= 30.0)
                return 5;
            else if (x >= 20.0)
                return 4;
            else if (x >= 15.0)
                return 3;
            else if (x >= 10.0)
                return 2;
            else
                return 0;

        }
        private int usagea(double? x)
        {
            if (x >= 30.0)
                return 4;
            else if (x >= 25.0)
                return 3;
            else if (x >= 20.0)
                return 3;
            else if (x >= 15.0)
                return 2;
            else if (x >= 10.0)
                return 1;
            else
                return 0;
        }
        private int FAvgAssistline(double? avga, double? lp)
        {
            if (avga >= lp)
                return 4;
            else if (avga >= (lp - 1.0))
                return 3;
            else
                return 1;
        }
        private int TAvgAssistline(double? avga, double? lp)
        {
            if (avga >= lp)
                return 4;
            else if (avga >= (lp - 1.0))
                return 3;
            else
                return 1;
        }


        private int AvgRebline(double? avgr, double? lp)
        {
            if (avgr >= lp)
                return 4;
            else if (avgr >= (lp - 1.0))
                return 3;
            else
                return 1;
        }
        private int DRA(double? x)
        {
            if (x >= 25)
                return 5;
            else if (x >= 20)
                return 4;
            else if (x >= 15)
                return 4;
            else if (x >= 10)
                return 2;
            else if (x >= 5)
                return 1;
            else
                return 0;
        }
        private int Rebp(double? x)
        {
            if (x >= 20.0)
                return 4;
            else if (x >= 15.0)
                return 3;
            else if (x >= 10.0)
                return 2;
            else if (x >= 5.0)
                return 1;
            else
                return 0;

        }
        private int usager(double? x)
        {
            if (x >= 30.0)
                return 4;
            else if (x >= 25.0)
                return 3;
            else if (x >= 20.0)
                return 3;
            else if (x >= 15.0)
                return 2;
            else if (x >= 10.0)
                return 1;
            else
                return 0;
        }
        private int FAvgRebline(double? avgr, double? lp)
        {
            if (avgr >= lp)
                return 4;
            else if (avgr >= (lp - 1.0))
                return 3;
            else
                return 1;
        }
        private int TAvgRebline(double? avgr, double? lp)
        {
            if (avgr >= lp)
                return 4;
            else if (avgr >= (lp - 1.0))
                return 3;
            else
                return 1;
        }




        private int OffBox(double? x)
        {
            int c = 0;
            c = Convert.ToInt32(x / 2.0);
            return c;
        }
        private int pm(double? x)
        {
            if (x >= 15.0)
                return 4;
            else if (x >= 10.0)
                return 3;
            else if (x >= 5.0)
                return 1;
            else if (x >= 0.0)
                return 0;
            else if (x >= -5.0)
                return -1;
            else if (x >= -10.0)
                return -2;
            else if (x >= -15.00)
                return -3;
            else
                return -4;
        }



       



        private void buttonClear_Click(object sender, EventArgs e)
        {
            mainGrid.Rows.Clear();
            mainO.Clear();
            mainD.Clear();
            mainL.Clear();
            Pbets.Clear();
            Abets.Clear();
            Rbets.Clear();
            mainA.Clear();
            dailyL.Clear();
            lastf.Clear();
            lastt.Clear();
    }
    }
}
 