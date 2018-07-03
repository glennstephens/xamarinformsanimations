using System;
using FutureBall;
using FutureBall.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShakableContentPage), typeof(ShakableContentPageRenderer))]

namespace FutureBall.iOS.Renderers
{
    /// <summary>
    /// A handler that exposes the ability to detect when a shake operation has occurred
    /// Based on the work from Bryan Costanich at: 
    /// https://github.com/xamarin/monotouch-samples/blob/master/SharedResources/SharedResources/Screens/iPhone/Accelerometer/ShakeScreen.xib.cs
    /// </summary>
    public class ShakableContentPageRenderer : PageRenderer
    {
        ShakableContentPage _page;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            _page = (ShakableContentPage)e.NewElement;
        }

        public override void MotionEnded(UIEventSubtype motion, UIEvent evt)
        {
            base.MotionEnded(motion, evt);
        
            if (motion == UIEventSubtype.MotionShake)
                _page.OnShaken(this.Element, EventArgs.Empty);
        }

        public override bool CanBecomeFirstResponder
        {
            get
            {
                return true;
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            BecomeFirstResponder();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ResignFirstResponder();
        }
    }
}

