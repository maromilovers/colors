﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace colors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        ISoundEffect soundEffect = DependencyService.Get<ISoundEffect>();

        string strMode = "";

        public ResultPage(int num, string mode)
        {
            strMode = mode;

            InitializeComponent();

            dispResult(num);
        }

        // コントロールサイズ調整
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            grdMain.WidthRequest = width;
            grdMain.HeightRequest = height;

        }

        // 結果画面表示
        private async void dispResult(int num)
        {
            await Task.Delay(800);

            int cnt = 0;

            // 表示
            while (cnt < num)
            {
                Image img = NameScopeExtensions.FindByName<Image>(this, string.Concat("imgScore", (cnt + 1).ToString()));
                img.Source = "hosi.png";
                using (soundEffect as IDisposable)
                {
                    soundEffect.ResultSound();
                }
                await img.RotateTo(360, 250);
                cnt = cnt + 1;
            }

            await Task.Delay(800);

            switch (num)
            {
                case 0:
                case 1:
                    imgResult.Source = "res1.png";
                    break;
                case 2:
                    imgResult.Source = "res2.png";
                    break;
                case 3:
                    imgResult.Source = "res3.png";
                    break;
                case 4:
                    imgResult.Source = "res4.png";
                    break;
                case 5:
                    imgResult.Source = "res5.png";
                    break;
                default:
                    imgResult.Source = "res5.png";
                    break;
            }

            imgResult.IsVisible = true;
            btnRetry.IsVisible = true;
            btnBack.IsVisible = true;
        }

        private void btnRetry_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new QuestionPage(0));
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new MainPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}