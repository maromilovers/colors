using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace colors
{
    public partial class MainPage : ContentPage
    {

        ISoundEffect soundEffect = DependencyService.Get<ISoundEffect>();

        public MainPage()
        {
            InitializeComponent();
        }

        // コントロールサイズ調整
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            grdMain.WidthRequest = width;
            grdMain.HeightRequest = height;
            
        }

        private void btnEasy_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new QuestionPage(0));
        }

        private void btnNormal_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new QuestionPage(1));
        }

        private void btnHard_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new QuestionPage(2));
        }

        private void btnFree_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new FreePage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}
