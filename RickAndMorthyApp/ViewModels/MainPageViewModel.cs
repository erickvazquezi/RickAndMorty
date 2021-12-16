using System;
using System.Collections.Generic;
using RickAndMorthyApp.Models;
using RickAndMorthyApp.Managers;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Reflection;

namespace RickAndMorthyApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private bool IsBusy;

        public MainPageViewModel()
        {
            ItemTresholdReachedCommand = new Command(async () => { await LoadNewCharacters(); });
            TapCharacterCommand = new Command(async (e) => { await TapCharacter((Character)e); });
            _ = Task.Run(async () => await LoadData());
        }

        private ObservableCollection<Character> _characterList = new ObservableCollection<Character>();
        ///<summary>
        ///Propiedad que contiene la información del personaje.
        ///</summary>
        public ObservableCollection<Character> CharacterList
        {
            get { return _characterList; }
            set
            {
                _characterList = value;
                OnPropertyChanged();
            }
        }

        private int _itemTreshold;
        ///<summary>
        ///Propiedad que contiene el entero para identificar los elementos faltantes a mostrar.
        ///</summary>
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set
            {
                _itemTreshold = value;
                OnPropertyChanged();
            }
        }

        private int _maxPages;
        ///<summary>
        ///Propiedad que indica el número máximo de páginas disponibles.
        ///</summary>
        public int MaxPages
        {
            get { return _maxPages; }
            set
            {
                _maxPages = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage;
        ///<summary>
        ///Propiedad que indica la página actual.
        ///</summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _totalCharacters;
        ///<summary>
        ///Propiedad que indica el número de personajes disponibles.
        ///</summary>
        public int TotalCharacters
        {
            get { return _totalCharacters; }
            set
            {
                _totalCharacters = value;
                OnPropertyChanged();
            }
        }

        ///<summary>
        ///Commando que se ejecuta al faltar x elementos por mostrarse de acuerdo al ItemTreshold.
        ///</summary>
        public Command ItemTresholdReachedCommand { get; set; }

        ///<summary>
        ///Commando que se ejeuta al dar tap en algún elemento de la colección.
        ///</summary>
        public Command TapCharacterCommand { get; set; }

        ///<summary>
        ///Realiza la carga de información para el Page.
        ///</summary>
        public async Task LoadData()
        {
            try
            {
                var ApiResponse = await GetCharactersData();
                if (ApiResponse.Results?.Count > 0)
                {
                    ApiResponse.Results.ForEach(c => CharacterList.Add(c));
                    MaxPages = ApiResponse.Info.Pages;
                    TotalCharacters = ApiResponse.Info.Count;
                    ItemTreshold = 1;
                    CurrentPage = 1;
                    return;
                }
            }
            catch (Exception ex)
            {
                //loggeo
            }
            Device.BeginInvokeOnMainThread(async () => {
                await Application.Current.MainPage.DisplayAlert("Message",
                "Sorry, an error occurred while retrieving the characters. Try again later",
                "Ok");
            });
        }

        ///<summary>
        ///Realiza petición a la API.
        ///</summary>
        public async Task<CharacterResponse> GetCharactersData(string page = "1")
        {
            var ApiResponse = await new ApiManager().GetCharacters(page);
            return ApiResponse;
        }

        ///<summary>
        ///Realiza la carga de nuevas páginas.
        ///</summary>
        public async Task LoadNewCharacters()
        {
            if (MaxPages >= CurrentPage && !IsBusy)
            {
                IsBusy = true;
                var ApiResponse = await GetCharactersData((CurrentPage + 1).ToString());
                if (ApiResponse.Results?.Count > 0)
                {
                    ApiResponse.Results.ForEach(c => CharacterList.Add(c));
                    CurrentPage++;
                }
                IsBusy = false;
            }
        }

        ///<summary>
        ///Levanta el Page con la información del personaje seleccionado.
        ///</summary>
        public async Task TapCharacter(Character selectedCharacter)
        {
            foreach (PropertyInfo prop in selectedCharacter.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    string value = prop.GetValue(selectedCharacter, null).ToString();
                    if (string.IsNullOrWhiteSpace(value))
                        prop.SetValue(selectedCharacter, "N/A");
                    
                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CharacterInfo(selectedCharacter));
            });
        }
    }
}
