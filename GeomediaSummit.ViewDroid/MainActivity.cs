using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Support;
using Android.Support.V4.App;

using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace GeomediaSummit.ViewDroid
{
	[Activity (Label = "GeomediaSummit", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private GoogleMap mGoogleMap;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		}

		protected override void OnResume ()
		{
			if (mGoogleMap == null) {
				mGoogleMap = ((MapFragment)FragmentManager.FindFragmentById (Resource.Id.map)).Map;

				if (mGoogleMap != null) {
					mGoogleMap.MyLocationEnabled = true;

					var mbProvider = new MBTilesProvider ();
					var mbOptions  = new TileOverlayOptions();
					mGoogleMap.AddTileOverlay( mbOptions.InvokeTileProvider(mbProvider) );

					//var bounds = mbProvider.bounds ();
					//var update = CameraUpdateFactory.NewLatLngBounds(
					//	new LatLngBounds(new LatLng(bounds[1],bounds[0]),new LatLng(bounds[3],bounds[2])),
					//	600,600,10
					//);

					var zoomLatLng = mbProvider.zoomLatLng ();

					var update = CameraUpdateFactory.NewLatLngZoom (
						new LatLng (zoomLatLng[2], zoomLatLng [1]),
						(float)zoomLatLng [0]
					);

					mGoogleMap.MoveCamera(update);
				}
			}
			base.OnResume();
		}	
	
	}
}


