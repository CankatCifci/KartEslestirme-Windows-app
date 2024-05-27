namespace KartEslestirme_Windows_app
{
    public partial class Form1 : Form
    {
        List<string> icons = new List<string>(){

            "!",",","b","k","v","w","z","N",
             "!",",","b","k","v","w","z","N"
        };
        Random rnd = new Random();
        int randomindex;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button btn;
            foreach(Button item in Controls)
            {
                btn = item as Button;
                randomindex = rnd.Next(icons.Count);
                btn.Text =icons[randomindex];
                btn.ForeColor= Color.Black;
                icons.RemoveAt(randomindex);
            }
        }
    }
}
