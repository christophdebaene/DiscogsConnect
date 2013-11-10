using System;
using System.Linq;
using System.Windows.Forms;

namespace DiscogsConnect.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            var client = new DiscogsClient();
            var result = client.SearchAsync("Beat", ResourceType.Release).Result;
            
            //var vals = result.Results.Select(x => 
            //    string.Format("{0} - {1} ({2})", 
            //        x.Id, 
            //        x.Title, 
            //        x.Formats != null ? string.Join(", ", x.Formats) : string.Empty
            //    )).ToList();

            //comboBox1.DataSource = vals;
        }
    }
}