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
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer revealTimer = new System.Windows.Forms.Timer();

        Button firstClicked = null;
        Button secondClicked = null;
        public Form1()
        {
            InitializeComponent();
            t.Tick += T_tick;
            t.Interval = 3000;
            revealTimer.Tick += RevealTimer_Tick;
            revealTimer.Interval = 1000;
            show();
            t.Start(); // Timer'� ba�lat�n
        }
        private void T_tick(object sender, EventArgs e)
        {
            // Timer tick olay� ger�ekle�ti�inde yap�lacak i�lemler burada
            // Butonlar�n ikonlar�n� gizleyin
            foreach (Control control in Controls)
            {
                if (control is Button item)
                {
                    item.ForeColor = item.BackColor; // Metin rengini arka plan rengiyle ayn� yaparak ikonlar� gizleyin
                    item.Click += Button_Click; // T�klama olay�n� ekleyin
                }
            }
            t.Stop(); // Timer'� durdurun
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // E�er bir timer �al���yorsa, kullan�c� herhangi bir butona t�klamamal�
            if (revealTimer.Enabled)
                return;

            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                // E�er buton zaten a��lm��sa, hi�bir �ey yapma
                if (clickedButton.ForeColor == Color.Black)
                    return;

                // �lk t�klanan kart� belirleyin
                if (firstClicked == null)
                {
                    firstClicked = clickedButton;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // �kinci t�klanan kart� belirleyin
                secondClicked = clickedButton;
                secondClicked.ForeColor = Color.Black;

                // �konlar e�le�iyor mu kontrol edin
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;

                    // T�m kartlar e�le�ti mi kontrol et
                    if (AllCardsMatched())
                    {
                        MessageBox.Show("Tebrikler! Ba�ar�l� bir �ekilde tamamlad�n�z!", "Oyun Tamamland�");
                    }
                    return;
                }

                // E�le�me yoksa, timer ba�lat ve kartlar� gizle
                revealTimer.Start();
            }
        }


        private bool AllCardsMatched()
        {
            foreach (Control control in Controls)
            {
                if (control is Button item && item.ForeColor != Color.Black)
                {
                    // E�er herhangi bir kart hala e�le�tirilmemi�se, false d�nd�r
                    return false;
                }
            }
            // E�er t�m kartlar e�le�tirilmi�se, true d�nd�r
            return true;
        }

        private void RevealTimer_Tick(object sender, EventArgs e)
        {
            revealTimer.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }
        private void ResetGame()
        {
            // T�m kartlar�n rengini resetle
            foreach (Control control in Controls)
            {
                if (control is Button item)
                {
                    item.ForeColor = item.BackColor;
                }
            }

            // Kartlar�n ikonlar�n� yeniden kar��t�r
            show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        
        }
        private void show()
        {
            Button btn;
            List<string> tempIcons = new List<string>(icons); // �konlar� ge�ici bir listeye kopyalay�n
            foreach (Control control in Controls)
            {
                if (control is Button item)
                {
                    btn = item as Button;
                    randomindex = rnd.Next(tempIcons.Count);
                    btn.Text = tempIcons[randomindex];
                    btn.ForeColor = Color.Black;
                    tempIcons.RemoveAt(randomindex);
                }
            }
        }
    }
}
