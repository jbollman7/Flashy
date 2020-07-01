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
       
        private bool poweron;
        private string _BackGroundColor;
        private string _BackGroundImage;
        public  MainViewViewModel()
        {
            BackGroundImage = "power_logo_dark";
            BackGroundColor = "Black";
            poweron = false; 
           PowerButton();
           
        }

        public string BackGroundColor
        {
            get => _BackGroundColor;
            set
            {
                _BackGroundColor = value;
                var args = new PropertyChangedEventArgs(nameof(BackGroundColor));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public string BackGroundImage
        {
            get => _BackGroundImage;
            set
            {
                _BackGroundImage = value;
                var args = new PropertyChangedEventArgs(nameof(BackGroundImage));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public void PowerButton()
        {
            PowerButtonCommand = new Command(async () =>
            {
                SwapBackground();
                try
                {
                    if (poweron == false)
                    {
                        await Flashlight.TurnOnAsync();
                        poweron = true;
                        SwapBackground();
                    }
                       
                    else
                    {
                        await Flashlight.TurnOffAsync();
                        poweron = false;
                        SwapBackground();
                    }
                       
                }
                catch (FeatureNotSupportedException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");
                }
                catch (PermissionException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");
                }
                catch (NullReferenceException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"Unable toggle flashlight: {ex.Message}", "OK");                    
                }
            });
        }

        public void SwapBackground()
        {
            if (poweron == true)
            {
                BackGroundImage = "power_logo";
                BackGroundColor = "White";
            }
            else
            {
                BackGroundImage = "power_logo_dark";
                BackGroundColor = "Black";
            }
        }

    }
}
