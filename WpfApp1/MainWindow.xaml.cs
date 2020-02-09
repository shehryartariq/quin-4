using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;



namespace Quine_McCluskey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int stage = 0;
        string group;
        string data;
        bool status = false;
        DataTable algo = new DataTable();

        string[] temp1 = new string[1000];
        int length = 0;
        public MainWindow()
        {
            InitializeComponent();
            //      datagrid2.DataContext = logic.DefaultView;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            { 

            algo.Rows.Clear();
            algo.Columns.Clear();
            algo.Columns.Add("Stage", typeof(int));
            algo.Columns.Add("Group", typeof(string));
            algo.Columns.Add("value1", typeof(string));
            algo.Columns.Add("Data", typeof(string));
            algo.Columns.Add("Status", typeof(bool));
            algo.EndInit();
            datagrid.DataContext = algo;
            datagrid.ColumnWidth = DataGridLength.SizeToHeader;
          
            datagrid.UpdateDefaultStyle();


            string dataset2 = min.Text;
   
            string[] data2 = dataset2.Split(',');
            string[] data1 = dataset2.Split(',');

            int dataqnty = data1.Count();
            int[] zerocount = new int[dataqnty];
            int[] data3 = new int[dataqnty];

            for (int temp0 = 0; temp0 < dataqnty; temp0++)
            {
                data3[temp0] = Int32.Parse(data1[temp0]);
            }
            int maxValue = data3.Max();
            var binary2 = Convert.ToString(maxValue, 2);
            maxValue = binary2.Length;
    
            for (int temp0 = 0; temp0 < dataqnty; temp0++)
            {
                data3[temp0] = Int32.Parse(data1[temp0]);
                int temp1 = Int32.Parse(data1[temp0]);
                var binary = Convert.ToString(temp1, 2); 

                binary = binary.PadLeft(maxValue, '0');
                data1[temp0] = binary;
                zerocount[temp0] = data1[temp0].Split('0').Length - 1;

            }

            Array.Sort(zerocount, data1);
            foreach (int key in zerocount)
            {
                Console.Write(key);
                Console.Write(' ');
            }
            Console.WriteLine();
            foreach (string value in data1)
            {
                Console.Write(value);
                Console.Write(' ');
            }
            Console.WriteLine();

            //////////////////////////////////////////////////
            int tempgp11 = 0;
            for (int tempz = 0; tempz < dataqnty; tempz++)
            {


                if (tempz != 0)
                {

                    if (zerocount[tempz] != zerocount[tempz - 1])
                    {
                        tempgp11++;

                    }
                    else
                    {

                    }
                }
                else
                {
                    tempgp11++;


                }

            }




            /////////////////////////////////
            int tempgp = 0;
            for (int tempz = 0; tempz < dataqnty; tempz++)
            {
                DataRow dtr1 = algo.NewRow();
                dtr1[0] = 0;
                if (tempz != 0)
                {

                    if (zerocount[tempz] != zerocount[tempz - 1])
                    {
                        tempgp11--;
                        dtr1[1] = tempgp11;
                    }
                    else
                    {
                        dtr1[1] = tempgp11;
                    }
                }
                else
                {
                    tempgp11--;
                    dtr1[1] = tempgp11;

                }
                dtr1[3] = data1[tempz];
                dtr1[2] = Convert.ToInt32(data1[tempz], 2);
                dtr1[4] = false;
                dtr1.EndEdit();
                algo.Rows.InsertAt(dtr1, 0);

                datagrid.UpdateDefaultStyle();
                datagrid.Items.Refresh();
            }
            int tempa1 = 0, index = 0;
            int comparestatus = 0;
            String substring, substring2;
            DataTable dtemp1, dtemp2;

            int anychange = 0, laststage = 0;
            ///////////////////////////////////////////////////////     
            do
            {
                anychange = 0;
                dtemp1 = algo.Copy();
                dtemp2 = algo.Copy();
                index = 0;
                foreach (DataRow row1 in dtemp2.Rows)
                {

                    if (row1.ItemArray[0].Equals(laststage))
                    {
                    
                        tempa1 = Convert.ToInt32(row1.ItemArray[1]);
                        int compareresult = 0, internalindex = 0, comp = 0;
                        foreach (DataRow row2 in dtemp1.Rows)
                        {
                            if (row2.ItemArray[0].Equals(laststage))
                            {
                                if (row2.ItemArray[1].Equals((tempa1 + 1).ToString()))
                                {

                                    char[] charArr1, charArr2;
                                    charArr1 = row1.ItemArray[3].ToString().ToCharArray();
                                    charArr2 = row2.ItemArray[3].ToString().ToCharArray();

                                    compareresult = 0;
                                    for (int tk = 0; tk < maxValue; tk++)
                                    {
                 
                                        if (charArr1[tk] == charArr2[tk])
                                        {


                                        }
                                        else
                                        {
                                            charArr1[tk] = 'x';
                                            comp = internalindex;
                                            compareresult++;
                                        }
                                    }
                                    if (compareresult == 1)
                                    {
                                        int[] dataalrdyprstindex = new int[100];
                                        int dataalrdyprst = 0, tempindex = 0;
                                        /////////////////
                                        foreach (DataRow row3 in algo.Rows)
                                        {

                                            if (row3.ItemArray[3].Equals(new string(charArr1)))
                                            {
                                                dataalrdyprstindex[dataalrdyprst] = tempindex;
                                                dataalrdyprst++;
                     
                                            }
                                            else
                                            {

                                            }
                                            tempindex++;
                                        }
                                        if (dataalrdyprst == 0)
                                        {
                                            /////////////////
                                            DataRow dtr1 = algo.NewRow();
                                            dtr1[0] = laststage + 1;
                                            dtr1[1] = tempa1;
                                            dtr1[2] = row1.ItemArray[2].ToString() + ',' + row2.ItemArray[2].ToString();
                                            dtr1[3] = new string(charArr1);
                                            comparestatus = 1;
                                            algo.Rows[index][4] = true;
                                            algo.Rows[internalindex][4] = true;
                                            anychange = 1;
              
                                            algo.Rows.InsertAt(dtr1, algo.Rows.Count);
                                            datagrid.UpdateDefaultStyle();
                                            datagrid.Items.Refresh();
                                        }
                                        else
                                        {
                                            algo.Rows[index][4] = true;
                                            algo.Rows[internalindex][4] = true;
                                            for (int tempqnt = 0; tempqnt < dataalrdyprst; tempqnt++)
                                            {
                                  
                                            }
                                        }
                                    }
                                    else
                                    {
                  
                                    }

                                }

                            }
                            internalindex++;
                            /////////////////////////////////////////////////////
                        }
                        if (comparestatus == 0)
                        {
                            if (algo.Rows[index][4].Equals(false))
                            {
                              
                                comparestatus = 0;
                            
                                datagrid.UpdateDefaultStyle();
                                datagrid.Items.Refresh();
                            }
                        }

                        comparestatus = 0;
                        compareresult = 0;
                    }

                    index++;

                }
                laststage++;
            } while (anychange == 1);

           

            DataTable logic = new DataTable();
            logic.Clear();
            logic.Rows.Clear();
            logic.Columns.Clear();
            logic.Dispose();


            logic.Columns.Add("Equation", typeof(string));


            datagrid2.UpdateDefaultStyle();
            datagrid2.Items.Refresh();
            for (int temp0 = 0; temp0 < dataqnty; temp0++)
            {


                logic.Columns.Add(data3[temp0].ToString(), typeof(bool));
                logic.EndInit();
            }



            int dataqnty1 = 0;
            datagrid2.ItemsSource = logic.DefaultView;
            foreach (DataRow row3 in algo.Rows)
            {

                if (row3.ItemArray[4].Equals(true))
                {
                }
                else
                {
                    string[] data21 = row3.ItemArray[2].ToString().Split(',');
                    dataqnty1 = data21.Count();
                    DataRow dtr1 = logic.NewRow();
                    dtr1["Equation"] = row3.ItemArray[3].ToString();
                    for (int rp = 0; rp < dataqnty1; rp++)
                    {

                        dtr1[data21[rp].ToString()] = true;



                    }





                    logic.Rows.InsertAt(dtr1, logic.Rows.Count);
                    datagrid2.UpdateDefaultStyle();
                    datagrid2.Items.Refresh();

                }
            }





            string[] row = new string[] { "1", "Product 1", "1000" };
           
            datagrid2.UpdateDefaultStyle();
       
            int tq1 = 0;



            }

   catch (Exception ex)
 {}
}
     
           

    }
}
