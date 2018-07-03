using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FutureBall
{
	public class EightBallContentPage : ShakableContentPage
	{
		readonly Image _eightBall;
		readonly Label _answer;

		const string eightBallImageFileName = "FutureBall.png";

		public EightBallContentPage () : base()
		{
            BackgroundColor = Color.Black;

			var layout = new AbsoluteLayout ();

			_eightBall = new Image () {
				Aspect = Aspect.AspectFit,
				AnchorX = 0.5,
				AnchorY = 0.5,
				Source = ImageSource.FromFile(eightBallImageFileName)
			};

			// Add the items with their locations
			layout.Children.Add (_eightBall, new Rectangle (0, 0, 1, 1), 
				AbsoluteLayoutFlags.All);

			_answer = new Label () {
				TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
				AnchorX = 0.5,
				AnchorY = 0.5,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			layout.Children.Add (_answer, new Rectangle (0.5, 0.50, 0.3, 0.2),
				AbsoluteLayoutFlags.All);

            var tap = new TapGestureRecognizer();
            tap.Command = new Command<View>(async (View obj) => await DisplayANewAnswer());

            _eightBall.GestureRecognizers.Add(tap);
			_answer.GestureRecognizers.Add (tap);

			Content = layout;
		}

		public async override void OnShaken (object sender, EventArgs e)
		{
			base.OnShaken (sender, e);

			await DisplayANewAnswer ();
		}

		bool isFlipped = true;
		const int animSpeed = 250;

		async Task DisplayANewAnswer()
		{
            await _answer.FadeTo(0, 10);
			_answer.Text = CommonAnswers.GetAnswer ();

			await Task.WhenAll(
				_answer.RotateTo (isFlipped ? 720 : 0, animSpeed),
                _answer.FadeTo(1, animSpeed * 5),
				_eightBall.RotateTo (isFlipped ? -720 : 0, animSpeed)
			);

			isFlipped = !isFlipped;
		}
	}
}

