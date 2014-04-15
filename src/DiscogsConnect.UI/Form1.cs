namespace DiscogsConnect.UI
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            var client = new DiscogsClient();

            var result = client.Search("Serious Beats 25", ResourceType.Release).Result;

            var release = result.Items
                .Where(x => x.Type == ResourceType.Release)
                .Cast<ReleaseSearchResult>()
                .Where(x => x.Formats.Contains("CD"))
                .Where(x => x.Title.Contains("Serious Beats 25"))
                .FirstOrDefault();


            var releaseDetail = client.GetRelease(release.Id);



            //var result = client.SearchAsync("Beat", ResourceType.Release).Result;
            
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