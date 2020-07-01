using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;



namespace Flashy.ViewModels
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       
        public ICommand PowerButtonCommand { get; set;  }
        bool poweron;

        public  MainViewViewModel()
        {
            poweron = false; 
           PowerButton();
           
        }

        public void PowerButton()
        {
            PowerButtonCommand = new Command(async () =>
            {
                try
                {
                    if (poweron == false)
                    {
                        await Flashlight.TurnOnAsync();
                        poweron = true;
                    }
                       
                    else
                    {
                        await Flashlight.TurnOffAsync();
                        poweron = false;
                    }
                       
                }
                catch (FeatureNotSupportedException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");

                    // Handle not supported on device exception
                }
                catch (PermissionException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");
                    // Handle permission exception
                }
                catch (NullReferenceException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");
                    // Unable to turn on/off flashlight
                    
                }
            });
        }

    }
}
