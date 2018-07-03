using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AnimationSensation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void StartTheTutorial(object sender, System.EventArgs e)
        {
            // Using images from http://www.iconarchive.com/show/mobile-icons-by-webiconset.html
            var walkthrough = new WalkthroughCarouselPage()
                .Add("messages.png", "call.png", "Use your phone in awesome ways", Color.FromHex("FF9700"))
                .Add("note.png", "photos.png", "Add notes to your photos", Color.FromHex("FFFA00"))
                .Add("settings.png", "wireless.png", "Easily update your network", Color.FromHex("E1005B"))
                .Add("calendar.png", "phonebook.png", "Link your calendar to your calls", Color.FromHex("0A61B5"));

            await Navigation.PushModalAsync(new NavigationPage(walkthrough));
        }
    }
}
