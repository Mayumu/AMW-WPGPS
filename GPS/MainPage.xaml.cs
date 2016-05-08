using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GPS.Resources;
using System.Threading.Tasks;

using Microsoft.Phone.Maps.Controls;
using System.Device.Location; // Provides the GeoCoordinate class.
using Windows.Devices.Geolocation; //Provides the Geocoordinate class.
using Windows.UI.Core;

namespace GPS
{
    public partial class MainPage : PhoneApplicationPage
    {
        Geolocator geolocator;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            geolocator = new Geolocator();
            geolocator.MovementThreshold = 50;
            geolocator.ReportInterval = 2000;
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            geolocator.PositionChanged += geolocator_PositionChanged;
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            getpos();
        }

        private void btnGetCoordinates_Click(object sender, RoutedEventArgs e)
        {
            getpos();
        }

        private async void getpos()
        {
            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync();
                Geocoordinate myGeocoordinate = geoposition.Coordinate;
                GeoCoordinate myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);
                
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    textblockAltitude.Text = "Altitude: " + geoposition.Coordinate.Altitude.ToString();
                    textblockLatitude.Text = "Latitude: " + geoposition.Coordinate.Latitude.ToString();
                    textblockLogitude.Text = "Longitude: " + geoposition.Coordinate.Longitude.ToString();
                    map.Center = myGeoCoordinate;
                    map.ZoomLevel = 13;
                });
            }
            catch
            {

            }
        }
    }
}