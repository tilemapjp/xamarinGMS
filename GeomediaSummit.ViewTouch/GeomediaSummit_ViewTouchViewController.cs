using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Google.Maps;
using MonoTouch.CoreLocation;

namespace GeomediaSummit.ViewTouch
{
	public partial class GeomediaSummit_ViewTouchViewController : UIViewController
	{
		private MapView       mapView;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public GeomediaSummit_ViewTouchViewController ()
			: base (UserInterfaceIdiomIsPhone ? "GeomediaSummit_ViewTouchViewController_iPhone" : "GeomediaSummit_ViewTouchViewController_iPad", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var mbProvider = new MBTilesProvider ();
			var zoomLatLng = mbProvider.zoomLatLng ();

			//var point  = new CLLocationCoordinate2D (0, 0);
			//var camera = CameraPosition.FromCamera (point, 12, 0, 0);
			//mapView    = MapView.FromCamera (RectangleF.Empty, camera);
			mapView = new MapView ();

			View = mapView;

			mbProvider.Map = mapView; 

			//CameraUpdate update = CameraUpdate.FitBounds (
			//	new CoordinateBounds(
			//		new CLLocationCoordinate2D(bounds[1],bounds[0]),
			//		new CLLocationCoordinate2D(bounds[3],bounds[2])
			//	)
			//);

			CameraUpdate update = CameraUpdate.SetCamera (
				new CameraPosition(new CLLocationCoordinate2D(zoomLatLng[2],zoomLatLng[1]),
			                   (float)zoomLatLng[0],0.0,0.0)
			);

			mapView.MoveCamera(update);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}
	}
}

