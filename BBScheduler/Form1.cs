using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;


namespace BBScheduler
{
    public partial class Form1 : Form
    {
        DateTime HHSD, HHSR, HHED, HHER;
        
        public Form1()
        {
            InitializeComponent();
            generateTime();
            lblError.Visible = false;
            lblError.Text = "";
            getDetails(1);
        }

        /**********************************************************************************/
        /*  generateTime() : Generates the Hours and Minutes of the drop down list        */
        /*  comboBox1, comboBox6, comboBox8, comboBox11 => Hours in 12 hour format        */
        /*  comboBox2, comboBox5, comboBox10, comboBox9 => Minutes in 5 minutes increment */
        /**********************************************************************************/

        public void generateTime()
        {
            string hours,mins;

            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                    hours = "0" + i.ToString();
                else
                    hours = i.ToString();

                comboBox1.Items.Add(hours);
                comboBox6.Items.Add(hours);
                comboBox8.Items.Add(hours);
                comboBox11.Items.Add(hours);
            }

            for (int i = 0; i < 60; i+=5)
            {
                if (i < 10)
                    mins = "0" + i.ToString();
                else
                    mins = i.ToString();

                comboBox2.Items.Add(mins);
                comboBox5.Items.Add(mins);
                comboBox10.Items.Add(mins);
                comboBox9.Items.Add(mins);
            }
        }

        /**********************************************************************/
        /*  checkBox1_Click() : Saves the Dialer Name, User Name and Password */
        /*  to the registry. In-case of blank entry the user is warned        */
        /**********************************************************************/

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (tbDialer.Text != "" && tbUser.Text != "" && tbPass.Text != "")
                {
                    lblError.Text = "";
                    getDetails(2);
                }
                else
                    lblError.Visible = true;
                    lblError.Text = "*Provide all the relelvant details";

            }
            else
            {
                lblError.Text = "";
                getDetails(3);
            }                
        }

        /**********************************************************************************************/
        /*  getDetails(int state) : Saves the details of the form to HKEY_CURRENT_USER\Software\BBHH  */
        /*  state = 1 : Used when the application starts in the initialzation starts                  */
        /*  state = 2 : Used when the Save Details check box is checked                               */
        /*  state = 3 : Used when the Save Details check box is un-checked after checking it          */
        /**************************************     Xtra Info    **************************************/
        /*  state = 1 : Used to get the saved info from the registry during start-up                  */
        /*  state = 2 : Used to save the details to the registry when the CheckBox is checked         */
        /*  state = 3 : Used to delete the registry key when the CheckBox is un-checked               */
        /**********************************************************************************************/

        public void getDetails(int state)
        {
            int getstate = state;

            string keyLoc = "Software\\BBHH";

            try
            {
                if (getstate == 1)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(keyLoc);

                    if (key == null)
                    {
                        //Do nothing since the user
                        //didn't save/wish to save anything
                    }
                    else
                    {
                        tbDialer.Text = key.GetValue("Dailer").ToString();
                        tbUser.Text = key.GetValue("User").ToString();
                        tbPass.Text = key.GetValue("Pass").ToString();

                        comboBox1.SelectedItem = key.GetValue("HHSDhr");
                        comboBox2.SelectedItem = key.GetValue("HHSDmin");
                        comboBox3.SelectedItem = key.GetValue("HHSD12");
                        comboBox6.SelectedItem = key.GetValue("HHSRhr");
                        comboBox5.SelectedItem = key.GetValue("HHSRmin");
                        comboBox4.SelectedItem = key.GetValue("HHSR12");
                        comboBox8.SelectedItem = key.GetValue("HHEDhr");
                        comboBox10.SelectedItem = key.GetValue("HHEDmin");
                        comboBox12.SelectedItem = key.GetValue("HHED12");
                        comboBox11.SelectedItem = key.GetValue("HHERhr");
                        comboBox9.SelectedItem = key.GetValue("HHERmin");
                        comboBox7.SelectedItem = key.GetValue("HHER12");

                        checkBox1.Checked = true;

                    }
                }

                if (getstate == 2)
                {
                    RegistryKey key;
                    key = Registry.CurrentUser.CreateSubKey(keyLoc);

                    key.SetValue("Dailer", tbDialer.Text);
                    key.SetValue("User", tbUser.Text);
                    key.SetValue("Pass", tbPass.Text);

                    if (comboBox1.SelectedItem == null)
                        key.SetValue("HHSDhr", "");
                    else
                        key.SetValue("HHSDhr", comboBox1.SelectedItem);

                    if (comboBox2.SelectedItem == null)
                        key.SetValue("HHSDmin", "");
                    else
                        key.SetValue("HHSDmin", comboBox2.SelectedItem);

                    if (comboBox3.SelectedItem == null)
                        key.SetValue("HHSD12", "");
                    else
                        key.SetValue("HHSD12", comboBox3.SelectedItem);

                    if (comboBox6.SelectedItem == null)
                        key.SetValue("HHSRhr","");
                    else
                        key.SetValue("HHSRhr", comboBox6.SelectedItem);

                    if (comboBox5.SelectedItem == null)
                        key.SetValue("HHSRmin", "");
                    else
                        key.SetValue("HHSRmin", comboBox5.SelectedItem);

                    if (comboBox4.SelectedItem == null)
                        key.SetValue("HHSR12", "");
                    else
                        key.SetValue("HHSR12", comboBox4.SelectedItem);

                    if (comboBox8.SelectedItem == null)
                        key.SetValue("HHEDhr", "");
                    else
                        key.SetValue("HHEDhr", comboBox8.SelectedItem);

                    if (comboBox10.SelectedItem == null)
                        key.SetValue("HHEDmin", "");
                    else
                        key.SetValue("HHEDmin", comboBox10.SelectedItem);

                    if (comboBox12.SelectedItem == null)
                        key.SetValue("HHED12", "");
                    else
                        key.SetValue("HHED12", comboBox12.SelectedItem);

                    if (comboBox11.SelectedItem == null)
                        key.SetValue("HHERhr", "");
                    else
                        key.SetValue("HHERhr", comboBox11.SelectedItem);

                    if (comboBox9.SelectedItem == null)
                        key.SetValue("HHERmin", "");
                    else
                        key.SetValue("HHERmin", comboBox9.SelectedItem);

                    if (comboBox7.SelectedItem == null)
                        key.SetValue("HHER12", "");
                    else
                        key.SetValue("HHER12", comboBox7.SelectedItem);
                }

                if (getstate == 3)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(keyLoc);

                    if (key != null)
                        Registry.CurrentUser.DeleteSubKey(keyLoc);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSchJob_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            doJob();
        }

        /***********************************************************/
        /*  Form1_Resize() : Minimize to tray with icon visibility */
        /***********************************************************/

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }            
        }
        
        /***************************************************************************/
        /*  notifyIcon1_MouseDoubleClick() : Restores the application to maximized */
        /*  state on Double Clicking the System Tray Icon                          */
        /***************************************************************************/

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        /*****************************************************************************/
        /*  toolStripMenuItem1_Click() : Restores the application to maximized state */
        /*  on Right Clicking the System Tray Icon anmd selecting Restore            */
        /*****************************************************************************/

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        /***************************************************************/
        /*  toolStripMenuItem2_Click() : Closes/Quits application      */
        /*  on Right Clicking the System Tray Icon anmd selecting Exit */
        /***************************************************************/

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        /***********************************************/
        /*  doJob() : The scheduling job is done here  */
        /***********************************************/

        public void doJob()
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
            {
                //Do nothing
            }
            else
            {
                HHSD = DateTime.Parse(comboBox1.SelectedItem.ToString() + ":" + comboBox2.SelectedItem.ToString() + " " + comboBox3.SelectedItem.ToString());
            }

            if (comboBox6.SelectedItem == null || comboBox5.SelectedItem == null || comboBox4.SelectedItem == null)
            {
                //Do nothing
            }
            else
            {
                HHSR = Convert.ToDateTime(comboBox6.SelectedItem.ToString() + ":" + comboBox5.SelectedItem.ToString() + " " + comboBox4.SelectedItem.ToString());
            }

            if (comboBox8.SelectedItem == null || comboBox10.SelectedItem == null || comboBox12.SelectedItem == null)
            {
                //Do nothing
            }
            else
            {
                HHED = Convert.ToDateTime(comboBox8.SelectedItem.ToString() + ":" + comboBox10.SelectedItem.ToString() + " " + comboBox12.SelectedItem.ToString());
            }

            if (comboBox11.SelectedItem == null || comboBox9.SelectedItem == null || comboBox7.SelectedItem == null)
            {
                //Do nothing
            }
            else
            {
                HHER = Convert.ToDateTime(comboBox11.SelectedItem.ToString() + ":" + comboBox9.SelectedItem.ToString() + " " + comboBox7.SelectedItem.ToString());
            }

            /*
            for (; ; )
            {
                string sys_time = DateTime.Now.ToString("h:mm tt");

                if (sys_time == HHSD)
                {
                    string args = "RASDIAL \"" + tbDialer.Text + "\" /disconnect";
                    dis_connect(args);
                }

                if (sys_time == HHSR)
                {
                    string args = "RASDIAL \"" + tbDialer.Text + "\" " + tbUser.Text + " " + tbPass.Text;
                    dis_connect(args);
                }

                if (sys_time == HHED)
                {
                    string args = "RASDIAL \"" + tbDialer.Text + "\" /disconnect";
                    dis_connect(args);
                }

                if (sys_time == HHER)
                {
                    string args = "RASDIAL \"" + tbDialer.Text + "\" " + tbUser.Text + " " + tbPass.Text;
                    dis_connect(args);
                }
            }*/
        }

        /*********************************************************************/
        /*  dis_connect() : Connect/Disconnect to the Internet is done here  */
        /*********************************************************************/

        public void dis_connect(string argu)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C" + argu;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
            }

            catch (Exception ex)
            {

            }
        }

        private void HHTimer_Tick(object sender, EventArgs e)
        {
            doJob();

        }
    }
}