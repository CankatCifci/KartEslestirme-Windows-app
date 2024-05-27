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

        DateTime startTime; // Oyunun baþladýðý zamaný saklamak için

        System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer(); // Zamanlayýcýyý tanýmla


        public Form1()
        {
            InitializeComponent();
            t.Tick += T_tick;
            t.Interval = 3000;
            revealTimer.Tick += RevealTimer_Tick;
            revealTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick; // Zamanlayýcý tick olayýna GameTimer_Tick metodunu baðla
            gameTimer.Interval = 1000; // Her saniyede bir iþlem yapmasý için interval'ý 1000 ms olarak ayarla
            show();
            t.Start(); // Timer'ý baþlatýn
            startTime = DateTime.Now; // Oyunun baþladýðý zamaný kaydet
            gameTimer.Start(); // Zamanlayýcýyý baþlat

        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime; // Geçen süreyi hesapla
            lblTimer.Text = $"Geçen Zaman: {elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}"; // Geçen süreyi etikete yazdýr
        }
        private void T_tick(object sender, EventArgs e)
        {
            // Timer tick olayý gerçekleþtiðinde yapýlacak iþlemler burada
            // Butonlarýn ikonlarýný gizleyin
            foreach (Control control in Controls)
            {
                if (control is Button item)
                {
                    item.ForeColor = item.BackColor; // Metin rengini arka plan rengiyle ayný yaparak ikonlarý gizleyin
                    item.Click += Button_Click; // Týklama olayýný ekleyin
                }
            }
            t.Stop(); // Timer'ý durdurun
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // Eðer bir timer çalýþýyorsa, kullanýcý herhangi bir butona týklamamalý
            if (revealTimer.Enabled)
                return;

            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                // Eðer buton zaten açýlmýþsa, hiçbir þey yapma
                if (clickedButton.ForeColor == Color.Black)
                    return;

                // Ýlk týklanan kartý belirleyin
                if (firstClicked == null)
                {
                    firstClicked = clickedButton;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // Ýkinci týklanan kartý belirleyin
                secondClicked = clickedButton;
                secondClicked.ForeColor = Color.Black;

                // Ýkonlar eþleþiyor mu kontrol edin
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;

                    // Tüm kartlar eþleþti mi kontrol et
                    if (AllCardsMatched())
                    {
                        TimeSpan elapsedTime = DateTime.Now - startTime; // Geçen süreyi hesapla
                        MessageBox.Show($"Tebrikler! Baþarýlý bir þekilde tamamladýnýz!\nTamamlama Süresi: {elapsedTime.TotalSeconds:F2} saniye", "Oyun Tamamlandý"); // Tamamlama süresini tebrikler mesajýna ekle
                    }
                    return;
                }

                // Eþleþme yoksa, timer baþlat ve kartlarý gizle
                revealTimer.Start();
            }
        }


        private bool AllCardsMatched()
        {
            foreach (Control control in Controls)
            {
                if (control is Button item && item.ForeColor != Color.Black)
                {
                    // Eðer herhangi bir kart hala eþleþtirilmemiþse, false döndür
                    return false;
                }
            }
            // Eðer tüm kartlar eþleþtirilmiþse, true döndür
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
            // Tüm kartlarýn rengini resetle
            foreach (Control control in Controls)
            {
                if (control is Button item)
                {
                    item.ForeColor = item.BackColor;
                }
            }

            // Kartlarýn ikonlarýný yeniden karýþtýr
            show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void show()
        {
            Button btn;
            List<string> tempIcons = new List<string>(icons); // Ýkonlarý geçici bir listeye kopyalayýn
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
