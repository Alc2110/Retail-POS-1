using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
//https://www.dotnetcurry.com/linq/564/linq-to-xml-tutorials-examples

namespace ConfigTool
{
    public partial class ConfigForm : Form
    {
        private bool applied;

        private string configFilePath = "POS.exe.config";

        public ConfigForm()
        {
            InitializeComponent();

            // disable ability to resize this form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            applied = false;

            load();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (applied)
            {
                this.Close();
            }
            else
            {
                applyChanges();
            }
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            button_apply.Enabled = false;
            applyChanges();
        }

        private void applyChanges()
        {
            applied = true;
        }

        private void load()
        {
            // extract XML from config file
            string xml = File.ReadAllText(configFilePath);

            // parse XML data
            XDocument doc = XDocument.Parse(xml);
            IEnumerable<XElement> elements = doc.Elements().Descendants();
            foreach (var element in elements)
            {
                Debug.WriteLine("\n" + element);
            }
        }
    }
}
