using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PowerPointAddInOne
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            //alligning controls..
            displaylicense_groupBox1.Size = imagesize_group1.Size;
            displaylicense_groupBox1.Top = imagesize_group1.Top;
            displaylicense_groupBox1.Left = imagesize_group1.Left;
        }

        private void hidealloptions()
        {
            imagesize_group1.Hide();
            displaylicense_groupBox1.Hide();
        }

        private void options_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidealloptions();
            if (options_list.SelectedItem.ToString().Equals("Image Size"))
            {
                Debug.WriteLine("image size");
                imagesize_group1.Show();
            }
            else if (options_list.SelectedItem.ToString().Equals( "Display License") )
            {
                Debug.WriteLine("display option");
                displaylicense_groupBox1.Show();
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            //saving settings
            Close();
        }
    }
}
