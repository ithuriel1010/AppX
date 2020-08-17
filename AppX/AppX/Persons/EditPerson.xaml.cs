using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX.Persons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPerson : ContentPage
    {
        public EditPerson()
        {
            InitializeComponent();
        }

        public async void OnNextPageButtonClicked()
        {
            await Navigation.PushAsync(new MainPage());
        }

        public async void OnRootPageButtonClicked()
        {
            await Navigation.PopToRootAsync();
        }
    }
}