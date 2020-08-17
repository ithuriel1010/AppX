using AppX.Classes;
using AppX.LocalizationFiles;
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
    public partial class PersonDetails : ContentPage
    {
        public static PersonsDB person;
        public PersonDetails()
        {
            InitializeComponent();
        }
        public async void Edit(PersonsDB personDetails)
        {
            person = personDetails;
            var editPersonVM = new EditPersonViewModel(person);
            var esitPersonPage = new EditPerson();

            esitPersonPage.BindingContext = editPersonVM;
            await Application.Current.MainPage.Navigation.PushAsync(esitPersonPage);
        }
    }
}