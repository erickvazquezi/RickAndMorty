using System;
using System.Collections.Generic;
using RickAndMorthyApp.Models;
using Xamarin.Forms;

namespace RickAndMorthyApp
{
    public partial class CharacterInfo : ContentPage
    {
        public CharacterInfo(Character currentCharacter)
        {
            InitializeComponent();
            this.BindingContext = currentCharacter;
        }
    }
}
