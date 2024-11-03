using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using static System.Net.Mime.MediaTypeNames;

namespace DBR_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string L_code;
        private string A_code;
        private string Toadd;
        private string currentMode;
        private string currentField;
        private int g_number = 0;

        public Queue<double> QW_Queue = new Queue<double>();
        public Queue<Queue<int>> Q_Queue = new Queue<Queue<int>>();
        public List<int> DBRList = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            currentMode = "Mode1";
            currentField = "Narrow";
        }

        private void G_button_Click(object sender, RoutedEventArgs e)
        {
            Generate_L_Code();
            Show_L_Code();
            Generate_A_Code();
            Show_A_Code();
        }

        public void Generate_L_Code()
        {
            //double yCurrent = 0;
            //int N = 0;
            //Queue<int> DBR_Queue = new Queue<int>();

            int g_count = 0;
            int p_count = 0;
            

            L_code = "";
            L_Add_Line("#Sequence: ");
            L_code += "#";
            for (int l = 0; l < DBRList.Count; l++)
            {
                L_code += DBRList[l] + " ";
            }
            L_Add_Line("\ndeleteall;");
            L_Add_Line("Stimefs = 200;\r\nST = Stimefs * 1e-12;");
            //Toadd = "diameter = " + Diameter_I.Text + ";";
            L_Add_Line("diameter = " + Diameter_I.Text + "e-6;");
            L_Add_Line("radius = 0.5*diameter;");
            //L_Add_Line("Center_Lamda = " + Wavelength_target_I.Text + "e-9;");
            //L_Add_Line("Center_Lamda = Center_Lamda_nm/1000;");
            //L_Add_Line("f = c/Center_Lamda;");
            if (int.TryParse(Materials_Number_I.Text, out int materialsNumber))
            {
                for (int i = 0; i < materialsNumber; i++)
                {
                    L_Add_Line("m" + (i + 1) + " = %m" + (i + 1) + "%;#Need To Be Set");
                }
                /*for (int i = 0; i < materialsNumber; i++)
                {
                    L_Add_Line("n_m" + (i + 1) + " = getfdtdindex(m"+ (i+1) +",f,min(f),max(f));");                  
                }*/
                /*for (int i = 0; i < materialsNumber; i++)
                {
                    L_Add_Line("d_m" + (i + 1) + " = Center_Lamda/(4*real(n_m" + (i + 1) + "));");
                }*/
            }
            L_Add_Line("V_Margin = 750e-9;");
            if(currentField == "Narrow")
            {
                L_Add_Line("H_Margin = -radius*0.42/1.42;");
            }
            else
            {
                L_Add_Line("H_Margin = V_Margin;");
            }
            L_Add_Line("\n # DBR Structure\n");
            L_Add_Line("yCurrent = 0;");
            //if (currentMode == "Mode1")
            //{
            //    L_Add_Line("Stacks_Number = " + Stacks_Number_I.Text + ";");
            //    if (int.TryParse(Stacks_Number_I.Text, out int stackn))
            //    {
            //        N = stackn;
            //    }
            //    L_Add_Line("d_Pair = (d_m1 + d_m2);");
            //    L_Add_Line("addanalysisgroup;\r\nset(\"name\",\"Stack\");\r\nset(\"x\",0);\r\nset(\"z\",0);\n");
            //    for(int i = 1;i <= N; i++)
            //    {
            //        /*L_Add_Line("    addcircle; #------------------" + i + " n1\r\n    set(\"x\",0);\r\n    set(\"radius\",radius);\r\n    set(\"z\",0);\r\n    set(\"first axis\",\"x\");\r\n    set(\"rotation 1\",90);\r\n    set(\"material\",m1);\r\n    set(\"z span\",d_m1);\r\n    set(\"y\",yCurrent + 0.5*d_m1);\r\n    yCurrent = yCurrent + d_m1;\r\n    addtogroup(\"Stack\");");
            //        if (i != N) {
            //            L_Add_Line("    addcircle; #------------------" + i + " n2\r\n    set(\"x\",0);\r\n    set(\"radius\",radius);\r\n    set(\"z\",0);\r\n    set(\"first axis\",\"x\");\r\n    set(\"rotation 1\",90);\r\n    set(\"material\",m2);\r\n    set(\"z span\",d_m2);\r\n    set(\"y\",yCurrent + 0.5*d_m2);\r\n    yCurrent = yCurrent + d_m2;\r\n    addtogroup(\"Stack\");");
            //        }*/
            //        DBR_Queue.Enqueue(1);
            //        if (i != N)
            //        {
            //            DBR_Queue.Enqueue(2);
            //        }
            //    }
            //}else if(currentMode == "Mode2")
            //{
            //    foreach (char c in DBR_Sequence.Text)
            //    {
            //        if (char.IsDigit(c))
            //        {
            //            DBR_Queue.Enqueue(int.Parse(c.ToString()));
            //        }
            //    }
            //}
            
            //while (DBR_Queue.Count > 0)
            //{
            //    int currentItem = DBR_Queue.Peek();
            //    count++;
            //    
            //    L_Add_Line("    addcircle; #------------------" + count + " n" + currentItem + "\r\n    set(\"x\",0);\r\n    set(\"radius\",radius);\r\n    set(\"z\",0);\r\n    set(\"first axis\",\"x\");\r\n    set(\"rotation 1\",90);\r\n    set(\"material\",m" + currentItem + ");\r\n    set(\"z span\",d_m" + currentItem + ");\r\n    set(\"y\",yCurrent + 0.5*d_m" + currentItem + ");\r\n    yCurrent = yCurrent + d_m" + currentItem + ";\r\n    addtogroup(\"Stack\");");
                
            //    DBR_Queue.Dequeue();
            //}

            for(int i = 0; i < g_number; i++)
            {
                
                if(QW_Queue.Count == 0)
                {
                    break;
                }
                //Queue<int> cDBR_dup = cDBR_Queue;
                g_count++;
                p_count = 0;
                double currentW = QW_Queue.Peek();
                if(currentW != 0)
                {
                    L_Add_Line("Center_Lamda_g" + g_count + " = " + currentW + "e-9;");
                    L_Add_Line("f = c/Center_Lamda_g" + g_count + ";");
                    for (int j = 0; j < materialsNumber; j++)
                    {
                        L_Add_Line("n_m" + (j + 1) + " = getfdtdindex(m" + (j + 1) + ",f,min(f),max(f));");
                        L_Add_Line("d_m" + (j + 1) + " = " + currentW + "e-9/(4*real(n_m" + (j + 1) + "));");
                    }
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(currentW);
                    Queue<int> cDBR_Queue = Q_Queue.Peek();
                    int layer_number = (int)QW_Queue.Peek();
                    //Q_Queue.Enqueue(cDBR_dup);
                    for (int j = 0; j < layer_number; j++)
                    {
                        int currentItem = cDBR_Queue.Peek();
                        p_count++;
                        L_Add_Line("    addcircle; #------------------" + g_count + "_" + p_count + " n" + currentItem + "\r\n    set(\"x\",0);\r\n    set(\"radius\",radius);\r\n    set(\"z\",0);\r\n    set(\"first axis\",\"x\");\r\n    set(\"rotation 1\",90);\r\n    set(\"material\",m" + currentItem + ");\r\n    set(\"z span\",d_m" + currentItem + ");\r\n    set(\"y\",yCurrent + 0.5*d_m" + currentItem + ");\r\n    yCurrent = yCurrent + d_m" + currentItem + ";\r\n    addtogroup(\"Stack\");");
                        cDBR_Queue.Dequeue();
                        cDBR_Queue.Enqueue(currentItem);
                    }
                    Q_Queue.Dequeue();
                    Q_Queue.Enqueue(cDBR_Queue);
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(layer_number);
                }
                else
                {
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(currentW);
                    double space_d = QW_Queue.Peek();
                    Queue<int> cDBR_Queue = Q_Queue.Peek();
                    
                    L_Add_Line("    addcircle; #------------------Space_ n" + cDBR_Queue.Peek() + "\r\n    set(\"x\",0);\r\n    set(\"radius\",radius);\r\n    set(\"z\",0);\r\n    set(\"first axis\",\"x\");\r\n    set(\"rotation 1\",90);\r\n    set(\"material\",m" + cDBR_Queue.Peek() + ");\r\n    set(\"z span\"," + space_d + "e-6);\r\n    set(\"y\",yCurrent + 0.5*" + space_d + "e-6);\r\n    yCurrent = yCurrent + " + space_d + "e-6;\r\n    addtogroup(\"Stack\");");
                    
                    Q_Queue.Enqueue(cDBR_Queue);
                    Q_Queue.Dequeue();
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(space_d);
                }
                L_Add_Line("");
            }
            
            
            L_Add_Line("\n # DFTD Setting\n");
            L_Add_Line("SimX = 2e-6; \r\nSimZ = SimX;\r\nSimY = (yCurrent + 2*V_Margin);\n");
            L_Add_Line("addfdtd;");
            L_Add_Line("MeshAccuracy = " + Mesh_I.Text + ";");
            L_Add_Line("dt = 0.95;");
            L_Add_Line("set(\"index\",1);\r\nset(\"dt stability factor\",dt);\r\nset(\"dimension\",\"2D\");\r\nset(\"simulation time\",ST);\r\nset(\"mesh accuracy\",MeshAccuracy);\r\nset(\"mesh refinement\",\"conformal variant 0\");");
            L_Add_Line("set(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"z\",0);\r\nset(\"Z span\",SimZ);");
            L_Add_Line("set(\"y min\",-3e-7);\r\nset(\"y max\",yCurrent + V_Margin);");
            L_Add_Line("SimXMin = get(\"x min\");\r\nSimXMax = get(\"x max\");\r\nSimZMin = get(\"z min\");\r\nSimZMax = get(\"z max\");\r\nSimYPos = get(\"y\");");
            L_Add_Line("set(\"x min bc\",\"periodic\");\r\nset(\"x max bc\",\"periodic\");");
            L_Add_Line("\n # Source Setting\n");
            L_Add_Line("addplane;\r\nset(\"injection axis\",\"y\");\r\nset(\"direction\",\"forward\");\r\nset(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",-1e-7);");
            L_Add_Line("set(\"wavelength start\"," + LambdaStart_I.Text + "e-9);");
            L_Add_Line("set(\"wavelength stop\"," + LambdaStop_I.Text + "e-9);");

            L_Add_Line("\n # Monitor Setting\n");
            //L_Add_Line("addpower;\r\nset(\"name\",\"field_start\");\r\nset(\"monitor type\",6);  \r\nset(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",-1e-7);\n");
            //L_Add_Line("addpower;\r\nset(\"name\",\"field_end\");\r\nset(\"monitor type\",6);  \r\nset(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",yCurrent+1e-7);\n");
            L_Add_Line("addpower;\r\nset(\"name\",\"field_profile\");\r\nset(\"monitor type\",7);  # 2D z-normal\r\nset(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"y\",yCurrent/2-1e-7);\r\nset(\"y span\",yCurrent+2e-7);\r\nset(\"z\",0);\n");
            //L_Add_Line("addtime;\r\nset(\"name\",\"time_1\");\r\nset(\"monitor type\",\"point\");\r\nset(\"x\",0);\r\nset(\"z\",0);\r\nset(\"y\",-2e-7);\r\n");
            //L_Add_Line("addtime;\r\nset(\"name\",\"time_2\");\r\nset(\"monitor type\",\"point\");\r\nset(\"x\",0);\r\nset(\"z\",0);\r\nset(\"y\",yCurrent+1e-7);\n");
            L_Add_Line("addpower;\r\nset(\"name\",\"power_1\");\r\nset(\"monitor type\",6);\r\nset(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",-2e-7);\r\n\r\naddpower;\r\nset(\"name\",\"power_2\");\r\nset(\"monitor type\",6);\r\nset(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",yCurrent+1e-7);\r\n");
            L_Add_Line("addpower;\r\nset(\"name\",\"power_3\");\r\nset(\"monitor type\",5);\r\nset(\"x\",SimX/2);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",yCurrent/2-0.5e-7);\r\nset(\"y span\",yCurrent+3e-7);\r\n\r\n\r\naddpower;\r\nset(\"name\",\"power_4\");\r\nset(\"monitor type\",5);\r\nset(\"x\",-SimX/2);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",yCurrent/2-0.5e-7);\r\nset(\"y span\",yCurrent+3e-7);\r\n");
            if (movie_ls.IsChecked == true)
            {
                L_Add_Line("addmovie;\r\nset(\"name\",\"Moviels\");\r\nset(\"monitor type\",\"2D Z-normal\");\r\nset(\"x min\",SimXMin);\r\nset(\"x max\",SimXMax);\r\nset(\"y min\",-2e-7);\r\nset(\"y max\",yCurrent + V_Margin);\r\nset(\"z\",0);\r\nset(\"scale\", 0.05);\r\nset(\"horizontal resolution\",round(SimX/1e-9));\r\nset(\"vertical resolution\",round(SimY/1e-9));\n");
            }
            if (movie_cs.IsChecked == true)
            {
                L_Add_Line("addmovie;\r\nset(\"name\",\"Moviecs\");\r\nset(\"monitor type\",\"2D Y-normal\");\r\nset(\"x\",0);\r\nset(\"x span\",SimX);\r\nset(\"z\",0);\r\nset(\"z span\",SimZ);\r\nset(\"y\",yCurrent + V_Margin);\r\nset(\"scale\", 0.05);\r\nset(\"horizontal resolution\",round(SimX/1e-9));\r\nset(\"vertical resolution\",round(SimY/1e-9));\n");
            }
        }

        public void Show_L_Code()
        {
            L_Code.Text = L_code;
        }

        private void L_Add_Line(string to_add)
        {
            L_code += to_add;
            L_code += '\n';
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                if (radioButton == Mode1)
                {
                    currentMode = "Mode1";
                    //Materials_Number_I.IsReadOnly = true;  
                    //Materials_Number_I.Text = "2";
                    //Materials_Number_I.Background = new SolidColorBrush(Colors.LightGray);
                    Mode1Content.Visibility = Visibility.Visible;
                    Mode2Content.Visibility = Visibility.Collapsed;
                    Mode3Content.Visibility = Visibility.Collapsed;
                    Sub_M1.Items.Clear();
                    Sub_M2.Items.Clear();
                    if (int.TryParse(Materials_Number_I.Text, out int number) && number > 0)
                    {
                        for (int i = 1; i <= number; i++)
                        {
                            Sub_M1.Items.Add(i);
                            Sub_M2.Items.Add(i);
                        }
                    }
                }
                else if (radioButton == Mode2)
                {
                    currentMode = "Mode2";
                    //Materials_Number_I.IsReadOnly = false;
                    //Materials_Number_I.Background = new SolidColorBrush(Colors.White);
                    Mode1Content.Visibility = Visibility.Collapsed;
                    Mode2Content.Visibility = Visibility.Visible;
                    Mode3Content.Visibility = Visibility.Collapsed;
                }
                else if(radioButton == Mode3)
                {
                    currentMode = "Mode3";
                    Mode1Content.Visibility = Visibility.Collapsed;
                    Mode2Content.Visibility = Visibility.Collapsed;
                    Mode3Content.Visibility = Visibility.Visible;
                    Space_M.Items.Clear();
                    Space_M.Items.Add(0);
                    if (int.TryParse(Materials_Number_I.Text, out int number) && number > 0)
                    {
                        for (int i = 1; i <= number; i++)
                        {
                            Space_M.Items.Add(i);
                        }
                    }
                }
            }
        }

        private void Field_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                if (radioButton == Narrow_Mode)
                {
                    currentField = "Narrow";
                }
                else if (radioButton == Wide_Mode)
                {
                    currentField = "Wide";
                }
            }
        }

        private void Addsubgroup_button_Click(object sender, RoutedEventArgs e)
        {
            Queue<int> subDBR_Queue = new Queue<int>();
            if (currentMode == "Mode1")
            {
                if (Wavelength_target_I1.Text != null)
                {
                    int.TryParse(Stacks_Number_I.Text, out int stackn);
                    int m1 = 0, m2 = 0;

                    if (Sub_M1.SelectedItem != null)
                    {
                        m1 = (int)Sub_M1.SelectedItem;
                    }
                    if (Sub_M2.SelectedItem != null)
                    {
                        m2 = (int)Sub_M2.SelectedItem;
                    }
                    for (int i = 1; i <= stackn+1; i++)
                    {
                        subDBR_Queue.Enqueue(m1);
                        if (i != stackn+1)
                        {
                            subDBR_Queue.Enqueue(m2);
                        }
                    }
                    double.TryParse(Wavelength_target_I1.Text, out double wavelength);
                    QW_Queue.Enqueue(wavelength);
                    QW_Queue.Enqueue(stackn * 2 + 1);
                    Q_Queue.Enqueue(subDBR_Queue);
                }
                
            }
            else if(currentMode == "Mode2")
            {
                int layer_n = 0;
                if(Wavelength_target_I2.Text != null)
                {
                    foreach (char c in DBR_Sequence.Text)
                    {
                        if (char.IsDigit(c))
                        {
                            subDBR_Queue.Enqueue(int.Parse(c.ToString()));
                            layer_n++;
                        }
                    }
                    double.TryParse(Wavelength_target_I2.Text, out double wavelength);
                    QW_Queue.Enqueue(wavelength);
                    QW_Queue.Enqueue(layer_n);
                    Q_Queue.Enqueue(subDBR_Queue);
                }
            }
            else if(currentMode == "Mode3")
            {
                if(Space_dist.Text != null)
                {
                    double.TryParse(Space_dist.Text, out double spacedist);
                    QW_Queue.Enqueue(0);
                    QW_Queue.Enqueue(spacedist);
                    int m0 = 0;
                    if (Space_M.SelectedItem != null)
                    {
                        m0 = (int)Space_M.SelectedItem;
                    }
                    subDBR_Queue.Enqueue(m0);
                    Q_Queue.Enqueue(subDBR_Queue);
                }
                
            }
            g_number++;
            DBR_queue2list();
            AddRectanglesToGrid();
        }

        private void clearall_button_Click(object sender, RoutedEventArgs e)
        {
            g_number = 0;
            QW_Queue.Clear();
            Q_Queue.Clear();
            DBR_queue2list();
            AddRectanglesToGrid();
        }

        public void Generate_A_Code()
        {
            A_code = "";
            A_Add_Line("# Practical Reflectivity\r\n\r\nspecR = getresult(\"power_1\",\"E\");\r\nspecO = getresult(\"power_2\",\"E\");\r\nspecs1 = getresult(\"power_3\",\"E\");\r\nspecs2 = getresult(\"power_4\",\"E\");\r\n\r\nspo = getresult(\"source\",\"spectrum\");\r\nlambda_R = specR.getparameter(\"lambda\");\r\nxx = specR.getparameter(\"x\");\r\nxc = find(xx,0);\r\n\r\nspec_RE = abs(sum(pinch(specR.getattribute(\"E\")),1));\r\nlambda_O = specO.getparameter(\"lambda\");\r\nspec_OE = abs(sum(pinch(specO.getattribute(\"E\")),1));\r\nspec_sE1 = abs(sum(pinch(specs1.getattribute(\"E\")),1));\r\nspec_sE2 = abs(sum(pinch(specs2.getattribute(\"E\")),1));\r\nlambda_S = spo.getparameter(\"lambda\");\r\nsp = spo.getattribute(\"spectrum\");\r\n\r\nd1 = [1;0;0];\r\nd2 = [0;1;0];\r\nd3 = [0;0;1];\r\n\r\nspec_R1 = sum(spec_RE,2);\r\nspec_O1 = sum(spec_OE,2);\r\nspec_S1 = sum(spec_sE1,2);\r\nspec_S2 = sum(spec_sE2,2);\r\nspec_R = matrixdataset(\"spectrum I\");\r\nspec_R.addparameter(\"lambda\",lambda_R);\r\nspec_R.addattribute(\"E\",spec_R1);\r\n\r\nspec_O = matrixdataset(\"spectrum O\");\r\nspec_O.addparameter(\"lambda\",lambda_O);\r\nspec_O.addattribute(\"E\",spec_O1);");
            A_Add_Line("R_s1 = find(lambda_S," + LambdaStart_I.Text + "e-9);");
            A_Add_Line("R_s2 = find(lambda_S," + LambdaStop_I.Text + "e-9);");
            A_Add_Line("R_s = linspace(R_s2,R_s1,500);");
            A_Add_Line("spm = abs(sp);\r\n#eff = (spec_R1+spec_O1+spec_S1+spec_S2)/spm(R_s);\r\nmall = spec_R1+spec_O1+spec_S1+spec_S2;\r\nmtd = spec_R1+spec_O1;\r\neff = mtd/mall;\r\n#RR = spec_R1/(spec_R1+spec_O1);\r\n#TT = spec_O1/(spec_R1+spec_O1);\r\nRR = spec_R1/mtd;\r\nTT = spec_O1/mtd;\r\n\r\nspec_sp = matrixdataset(\"spectrum sp\");\r\nspec_sp.addparameter(\"lambda\",lambda_S(R_s));\r\nspec_sp.addattribute(\"E\",spm(R_s));\r\nspec_RR = matrixdataset(\"spectrum R\");\r\nspec_RR.addparameter(\"lambda\",lambda_R);\r\nspec_RR.addattribute(\"E\",RR);\r\nspec_TT = matrixdataset(\"spectrum T\");\r\nspec_TT.addparameter(\"lambda\",lambda_R);\r\nspec_TT.addattribute(\"E\",TT);\r\n\r\nspec_Ef = matrixdataset(\"spectrum Ef\");\r\nspec_Ef.addparameter(\"lambda\",lambda_R);\r\nspec_Ef.addattribute(\"E\",eff);\r\n\r\nspec_m = matrixdataset(\"spectrum m\");\r\nspec_m.addparameter(\"lambda\",lambda_R);\r\nspec_m.addattribute(\"R\",spec_R1);\r\nspec_m.addattribute(\"O\",spec_O1);\r\nspec_m.addattribute(\"s1\",spec_S1);\r\nspec_m.addattribute(\"s2\",spec_S2);\r\nspec_m.addattribute(\"all\",spec_R1+spec_O1+spec_S1+spec_S2);\r\n");

            A_Add_Line("\n# Theoretical Reflectivity\n");//stackrt
            A_Add_Line("f = linspace(c/" + LambdaStart_I.Text + "e-9, c/" + LambdaStop_I.Text + "e-9,200);");
            A_Add_Line("d = [0; 0.105e-6; 0.146e-6; 0.105e-6; 0]; # Distance-Sequence Change by yourself");
            A_Add_Line("n = [1; 2.022; 1.452; 2.022; 1]; # n-Sequence Change by yourself");
            A_Add_Line("?length(d);\r\n?length(n); # To check");
            A_Add_Line("RT = stackrt(n, d, f);");
            A_Add_Line("spec_TH = matrixdataset(\"spectrum TH\");\r\nspec_TH.addparameter(\"lambda\",RT.lambda);\r\nspec_TH.addattribute(\"R\",RT.Rp);\r\n#visualize(RT);");
        }
          
        public void Show_A_Code()
        {
            A_Code.Text = A_code;
        }

        private void A_Add_Line(string to_add)
        {
            A_code += to_add;
            A_code += '\n';
        }

        private void DBR_queue2list()
        {
            DBRList.Clear();
            for (int i = 0; i < g_number; i++)
            {
                if (QW_Queue.Count == 0)
                {
                    break;
                }
                double currentW = QW_Queue.Peek();
                if (currentW != 0)
                {
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(currentW);
                    Queue<int> cDBR_Queue = Q_Queue.Peek();
                    int layer_number = (int)QW_Queue.Peek();
                    for (int j = 0; j < layer_number; j++)
                    {
                        int currentItem = cDBR_Queue.Peek();
                        DBRList.Add(currentItem);
                        cDBR_Queue.Dequeue();
                        cDBR_Queue.Enqueue(currentItem);
                    }
                    Q_Queue.Dequeue();
                    Q_Queue.Enqueue(cDBR_Queue);
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(layer_number);
                }
                else
                {
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(currentW);
                    double space_d = QW_Queue.Peek();
                    Queue<int> cDBR_Queue = Q_Queue.Peek();
                    Q_Queue.Enqueue(cDBR_Queue);
                    Q_Queue.Dequeue();
                    DBRList.Add(cDBR_Queue.Peek());
                    QW_Queue.Dequeue();
                    QW_Queue.Enqueue(space_d);
                }
            }
        }

        private void AddRectanglesToGrid()
        {
            Grid mainGrid = this.DBRGrid;
            mainGrid.RowDefinitions.Clear();
            mainGrid.Children.Clear();

            for (int i = 0; i < DBRList.Count; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < DBRList.Count; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Fill = new SolidColorBrush(GetColorFromHex(GetHexColorFromValue(DBRList[i])));
                Grid.SetRow(rect, DBRList.Count - 1 - i);
                mainGrid.Children.Add(rect);
            }
        }

        private string GetHexColorFromValue(int value)
        {
            switch (value)
            {
                case 1: return "#72F2EB";
                case 2: return "#1B7F79";
                case 3: return "#00CCC0";
                case 4: return "#BDE038";
                case 5: return "#F2CB05";
                case 6: return "#F29F05";
                case 7: return "#F28705";
                case 8: return "#F23030";
                case 9: return "#F28585";
                default: return "#CCCCCC";
            }
        }

        private Color GetColorFromHex(string hex)
        {
            return (Color)ColorConverter.ConvertFromString(hex);
        }
    }
}
