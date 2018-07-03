using System;
using Android.Content;
using Android.Hardware;
using FutureBall;
using FutureBall.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ShakableContentPage), typeof(ShakableContentPageRenderer))]

namespace FutureBall.Droid.Renderers
{
    /// <summary>
    /// A shakable page renderer. Based from the code from the service code from AND210 
    /// Check out https://university.xamarin.com/classes/track/xamarin-android#and210-backgrounding to see more
    /// </summary>
    public class ShakableContentPageRenderer : PageRenderer, ISensorEventListener
    {
        ShakableContentPage _page;

        public ShakableContentPageRenderer(Context context) : base(context)
        {
            MonitorForMotion();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            _page = (ShakableContentPage)e.NewElement;
        }

        SensorManager sensorManager;

        const double ShakeThreshold = 5.0;

        const int SamplingInterval = 100; //ms
        const int gestureTimeout = 500; //ms
        const int ShakesInGesture = 5;

        double lastSampleTime;
        double lastShakeTime;

        float lastX, lastY, lastZ;

        int shakeCount = 0;

        void MonitorForMotion()
        {
            sensorManager = (SensorManager)this.Context.GetSystemService(Context.SensorService);

            var accelerometer = sensorManager.GetDefaultSensor(SensorType.Accelerometer);

            if (accelerometer != null)
            {
                sensorManager.RegisterListener(this, accelerometer, SensorDelay.Normal);
            }
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            var currentTime = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond).TotalMilliseconds;

            var deltaTime = currentTime - lastSampleTime;

            if (deltaTime < SamplingInterval) // only check every 100ms as defined in SamplingInterval
                return;

            //reset shake count if it's been too long since last shake
            if ((currentTime - lastShakeTime) > gestureTimeout)
                shakeCount = 0;

            var currentX = e.Values[0];
            var currentY = e.Values[1];
            var currentZ = e.Values[2];

            bool isShake = IsSignifigantChange(currentX, lastX) | IsSignifigantChange(currentY, lastY) | IsSignifigantChange(currentZ, lastZ);

            if (isShake)
            {
                shakeCount++;
                lastShakeTime = currentTime;
            }

            if (shakeCount >= ShakesInGesture)
            {
                _page.OnShaken(this, EventArgs.Empty);
                shakeCount = 0;
            }

            //save the accel values
            lastX = currentX;
            lastY = currentY;
            lastZ = currentZ;

            lastSampleTime = currentTime;
        }

        bool IsSignifigantChange(double value1, double value2)
        {
            return Math.Abs(value1 - value2) > ShakeThreshold;
        }
    }
}
