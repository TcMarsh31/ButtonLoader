using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ButtonLoader
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            ButtonWithSpinner.IsBusy = !ButtonWithSpinner.IsBusy;

            // simulate long time process
            if (ButtonWithSpinner.IsBusy)
            {
                await Task.Delay(3000);
                OnClicked(this, EventArgs.Empty);
            }
        }
        private async void OnClickedRight(object sender, EventArgs e)
        {
            ButtonWithSpinnerRight.IsBusy = !ButtonWithSpinnerRight.IsBusy;

            // simulate long time process
            if (ButtonWithSpinnerRight.IsBusy)
            {
                await Task.Delay(3000);
                OnClickedRight(this, EventArgs.Empty);
            }
        }
        
    }
}
