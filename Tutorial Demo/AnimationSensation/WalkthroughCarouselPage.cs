using System;
using Xamarin.Forms;

namespace AnimationSensation
{
	public class WalkthroughCarouselPage : CarouselPage
	{
        public WalkthroughCarouselPage() : base()
        {
            Title = "Tutorial";
            var doneButton = new ToolbarItem("Done", null, async () =>
            {
                await Navigation.PopModalAsync();
            }, ToolbarItemOrder.Default, 0);

			ToolbarItems.Add (doneButton);

			this.CurrentPageChanged += async (object sender, EventArgs e) => {
				if (CurrentWalkthroughPage != null)
					await CurrentWalkthroughPage.AnimateIn ();
			};

			CurrentWalkthroughPage?.AnimateIn ();
		}

		public WalkthroughCarouselPage Add(string leftSource, string rightSource, string text, Color bg)
		{
			Children.Add (new WalkthroughInfoPage (leftSource, rightSource, text, bg));

			return this;
		}

		public WalkthroughInfoPage CurrentWalkthroughPage
		{
			get {
				return CurrentPage as WalkthroughInfoPage;
			}
		}
	}
}
