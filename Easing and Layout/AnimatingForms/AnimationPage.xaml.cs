using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AnimatingForms
{
    public partial class AnimationPage : ContentPage
    {
        public AnimationPage()
        {
            InitializeComponent();

            DisplayEasing();
        }

        async void PerformAnimation(object sender, System.EventArgs e)
        {
            // Setup the initial bounds
            image.Layout(new Rectangle(80, 80, image.Width, image.Height));

            await image.LayoutTo(new Rectangle(180, 80, image.Width, image.Height),
                2000, GetCurrentEasing());
        }

        void DisplayEasing()
        {
            stepDisplay.Text = GetCurrentEasingName();
        }

        void AnimationChoiceChange(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            DisplayEasing();
            image.Layout(new Rectangle(80, 80, image.Width, image.Height));
        }

        Easing GetCurrentEasing()
        {
            int value = Convert.ToInt32(step.Value);

            Easing[] entries = new Easing[] {
                Easing.CubicInOut,
                Easing.SpringOut,
                Easing.SpringIn,
                Easing.BounceIn,
                Easing.BounceOut,
                Easing.CubicOut,
                Easing.Linear,
                Easing.SinOut,
                Easing.SinIn,
                Easing.SinInOut,
                Easing.CubicIn
            };

            return entries[value];
        }

        string GetCurrentEasingName()
        {
            int value = Convert.ToInt32(step.Value);

            string[] entries = new string[] {
                "CubicInOut",
                "SpringOut",
                "SpringIn",
                "BounceIn",
                "BounceOut",
                "CubicOut",
                "Linear",
                "SinOut",
                "SinIn",
                "SinInOut",
                "CubicIn"
            };

            return entries[value];
        }
    }
}
