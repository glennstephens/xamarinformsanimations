using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace AnimationSensation
{
	public class WalkthroughInfoPage : ContentPage
	{
		readonly Image leftImage;
		readonly Image rightImage;
		readonly Label coreText;
		readonly Label bottomMessageText;

		public double VerticalStaggerAmount = 60;
		public double HorizontalStaggerAmount = 40;

		public double TextGap = 20;
		public double TextHeight = 60;

		public double ImageWidth = 130;
		public double ImageHeight = 130;

		public WalkthroughInfoPage (string leftSource, string rightSource, string text, Color bg)
		{
			BackgroundColor = bg;

			leftImage = new Image () {
				Source = ImageSource.FromFile (leftSource),
				Opacity = 0
			};
			rightImage = new Image () {
				Source = ImageSource.FromFile (rightSource),
				 Opacity = 0
			};

			coreText = new Label () {
				Text = text,
				XAlign = TextAlignment.Center,
				FontSize = 20,
				Opacity = 0
			};

			bottomMessageText = new Label () {
				Text = "Swipe Left and Right for more instructions",
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.End,
				FontSize = 10,
				Opacity = 0.75
			};

			var totalHeight = ImageHeight + VerticalStaggerAmount + TextGap + TextHeight;
			
			RelativeLayout layout = new RelativeLayout ();
			layout.Children.Add (leftImage, 
				Constraint.RelativeToParent (p => p.Width / 2 - ImageWidth + HorizontalStaggerAmount / 2),
				Constraint.RelativeToParent (p => (p.Height - totalHeight) / 2),
				Constraint.Constant (ImageWidth),
				Constraint.Constant (ImageHeight));
			
			layout.Children.Add (rightImage, 
				Constraint.RelativeToParent (p => p.Width / 2 - HorizontalStaggerAmount / 2),
				Constraint.RelativeToParent (p => (p.Height - totalHeight) / 2 + VerticalStaggerAmount / 2),
				Constraint.Constant (ImageWidth),
				Constraint.Constant (ImageHeight));
			
			layout.Children.Add (coreText, 
				Constraint.Constant (TextGap),
				Constraint.RelativeToParent (p => p.Height - (p.Height - totalHeight) / 2 - TextHeight),
				Constraint.RelativeToParent (p => p.Width - TextGap * 2),
				Constraint.Constant (TextHeight)
			);

			layout.Children.Add (bottomMessageText, 
				Constraint.Constant (TextGap),
				Constraint.RelativeToParent (p => p.Height - TextHeight - 30),
				Constraint.RelativeToParent (p => p.Width - TextGap * 2),
				Constraint.Constant (TextHeight)
			);

			Content = layout;
		}

		public async Task AnimateIn()
		{
			const uint animateTime = 700;
			Easing easing = Easing.Linear;

			await Task.WhenAll (
				leftImage.FadeTo (1, animateTime, easing),
				rightImage.FadeTo (1, animateTime, easing),
				coreText.FadeTo (1, animateTime, easing)
			);
		}
	}
}


