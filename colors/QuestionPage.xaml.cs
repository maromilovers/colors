using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace colors
{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        int cntQuiz = 0;
        int rightQuiz = 0;
        int[] ary;
        Color[] exArray = new Color[] { Color.Yellow, Color.Blue };
        Color bfColor;
        int _mode = 0;
        Boolean flgFirst = true;

        ISoundEffect soundEffect = DependencyService.Get<ISoundEffect>();

        public QuestionPage(int mode)
        {
            InitializeComponent();

            _mode = mode;

            // 画面セット
            setDisplay();

            // 問題表示
            setQuestion();
        }

        // コントロールサイズ調整
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // メイングリッド
            grdMain.WidthRequest = width - 20;
            grdMain.HeightRequest = height - 20;
            // 見本
            frmExample.WidthRequest = width / 5;
            frmExample.HeightRequest = width / 5;
            // 作成色
            frmDraw.WidthRequest = width / 5;
            frmDraw.HeightRequest = width / 5;
            imgResult.WidthRequest = width - 20;
            imgResult.HeightRequest = height - 20;
        }

        // 画面セット
        private void setDisplay()
        {
            switch (_mode)
            {
                // 3色のみ
                case 0:
                    imgColors2.BackgroundColor = Color.Red;
                    imgColors3.BackgroundColor = Color.Yellow;
                    imgColors4.BackgroundColor = Color.Blue;

                    ary = new int[] { 2, 3, 4 };

                    break;
                // 5色
                case 1:
                    imgColors1.BackgroundColor = Color.White;
                    imgColors2.BackgroundColor = Color.Red;
                    imgColors3.BackgroundColor = Color.Yellow;
                    imgColors4.BackgroundColor = Color.Blue;
                    imgColors5.BackgroundColor = Color.Black;

                    ary = new int[] { 1, 2, 3, 4, 5 };

                    break;
                // 10色
                case 2:
                    imgColors1.BackgroundColor = Color.White;
                    imgColors2.BackgroundColor = Color.Red;
                    imgColors3.BackgroundColor = Color.Yellow;
                    imgColors4.BackgroundColor = Color.Blue;
                    imgColors5.BackgroundColor = Color.Black;
                    imgColors6.BackgroundColor = Color.Green;
                    imgColors7.BackgroundColor = Color.Orange;
                    imgColors8.BackgroundColor = Color.Purple;
                    imgColors9.BackgroundColor = Color.Pink;
                    imgColors10.BackgroundColor = Color.Gray;

                    ary = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                    break;

            }

            flgFirst = true;

            //frmAnime.Opacity = 0;
            
            for (int i = 1; i <= 10; i++)
            {
                ImageButton ib = NameScopeExtensions.FindByName<ImageButton>(this, string.Concat("imgColors", (i).ToString()));
                Image img = NameScopeExtensions.FindByName<Image>(this, string.Concat("imgCName", (i).ToString()));

                ib.IsEnabled = true;

                if (ary.Contains(i))
                {
                    ib.IsVisible = true;
                    img.IsVisible = true;
                }
                else
                { 
                    ib.IsVisible = false;
                    img.IsVisible = false;
                }
            }
        }

        // 問題出題
        private void setQuestion()
        {
            imgResult.Source = null;
            // 問題作成

            Color colQes = createQuestion();

            if (bfColor != null)
            {
                while (colQes == bfColor)
                {
                    colQes = createQuestion();
                }
            }
 
            frmExample.BackgroundColor = colQes;
            bfColor = colQes;
            frmDraw.IsVisible = false;
            flgFirst = true;
            
        }

        // 色作成
        private Color createColor(Color color1, Color color2)
        {
            Color retColor = Color.White;
            Boolean flgGreen = false;
            double val = 0;

            // 例外パターン
            if (color1 == Color.Blue)
            {
                if(Math.Abs(color2.R - color2.G) < 0.1 && color2.R - color2.B > 0.3)
                {
                    flgGreen = true;
                    val = color2.R - color2.B;
                }
            }
            else if (color1 == Color.Yellow)
            {
                if (Math.Abs(color2.R - color2.G) < 0.1 && color2.R - color2.B < 0.3)
                {
                    flgGreen = true;
                    val = color2.B - color2.R;
                }
            }
            
            double R = color1.R * (0.5) + color2.R * (0.5);
            double G = color1.G * (0.5) + color2.G * (0.5);
            double B = color1.B * (0.5) + color2.B * (0.5);

            if (flgGreen)
            {
                R = R / 2;
                B = B / 2;
                G = (G + val) / 2;

            }

            retColor = Color.FromRgb(R, G, B);

            return retColor;

        }

        // 問題作成
        private Color createQuestion()
        {
            // 問題色作成
            Random rnd = new Random();
            int n = ary.Length;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                int tmp = ary[k];
                ary[k] = ary[n];
                ary[n] = tmp;
            }

            int col1 = ary[0];
            int col2 = ary[1];

            ImageButton img = NameScopeExtensions.FindByName<ImageButton>(this, string.Concat("imgColors", col1.ToString()));
            Color color1 = img.BackgroundColor;
            img = NameScopeExtensions.FindByName<ImageButton>(this, string.Concat("imgColors", col2.ToString()));
            Color color2 = img.BackgroundColor;

            return createColor(color1, color2);
        }

        private bool checkLimit(int num, int max, int min)
        {
            if (num > max) return false;
            if (num < min) return false;
            return true;
        }

        // 画像表示切替
        private void dispImages(string str, int num, string strImage)
        {
            int cnt = 0;
            int cntMax = 9;

            // 表示
            while (cnt < num )
            {
                Image img = NameScopeExtensions.FindByName<Image>(this, string.Concat("img" , str, "_", (cnt + 1).ToString()));
                img.Source = strImage;
                img.IsVisible = true;
                cnt = cnt + 1;
            }

            // 非表示
            for (int i = cnt; i <= cntMax; i++)
            {
                Image img = NameScopeExtensions.FindByName<Image>(this, string.Concat("img", str, "_", (i + 1).ToString()));
                img.Source = strImage; 
                img.IsVisible = false;
            }

        }

        private async void checkAnswer(Boolean flg)
        {
            grdMain.IsEnabled = false;

            await Task.Delay(1000);

            if (flg)
            {
                imgResult.Source = "maru.png";
                using (soundEffect as IDisposable)
                {
                    soundEffect.CorrectSound();
                }
                rightQuiz++;
            }
            else
            {
                imgResult.Source = "batu.png";
                using (soundEffect as IDisposable)
                {
                    soundEffect.IncorrectSound();
                }

            }

            imgResult.IsVisible = true;

            cntQuiz++;

            await Task.Delay(1000);

            imgResult.IsVisible = false;

            //if (cntQuiz >= maxQuiz)
            //{
            //    Navigation.PushAsync(new ResultPage(rightQuiz, "plus"));
            //}
            //else
            //{
            //    setDisplay();
            //    setQuestion();
            //}

            setDisplay();
            setQuestion();

            grdMain.IsEnabled = true;
        }

        private void imgBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void imgColors_Clicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;
            Color color = new Color();
            Boolean flgIncorrect = false;

            ib.IsVisible = false;

            if (flgFirst)
            {
                color = ib.BackgroundColor;
                flgFirst = false;
                frmDraw.BackgroundColor = color;
                frmDraw.IsVisible = true;
            }
            else
            {
                color = createColor(ib.BackgroundColor, frmDraw.BackgroundColor);
                frmDraw.BackgroundColor = color;

                flgIncorrect = frmExample.BackgroundColor == frmDraw.BackgroundColor ? true : false;
                checkAnswer(flgIncorrect);
            }
        }

        // クリアボタン押下
        private void imgClear_Clicked(object sender, EventArgs e)
        {
            // 画面を初期状態に戻す
            setDisplay();

            frmDraw.IsVisible = false;
            flgFirst = true;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}