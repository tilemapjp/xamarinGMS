using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Gms;
using Android.Gms.Maps.Model;
using Android.Support.V4.App;

using GeomediaSummit.Core;

namespace GeomediaSummit.ViewDroid
{
	class MBTilesProvider : Java.Lang.Object, Android.Runtime.IJavaObject, IDisposable, ITileProvider
	{
		private MBTilesAccessor accessor;

		private static int TILE_WIDTH  = 256;
		private static int TILE_HEIGHT = 256;

		public MBTilesProvider () : base ()
		{
			accessor = new MBTilesAccessor ();
		}

		public double[] bounds()
		{
			return accessor.bounds ();
		}

		public double[] zoomLatLng()
		{
			return accessor.zoomLatLng ();
		}

		public Tile GetTile(int x, int y, int zoom)
		{
			var image = accessor.GetTileImage (x, y, zoom);

			if (image == null) {
				return TileProvider.NoTile;
			} else {
				return new Tile(TILE_WIDTH, TILE_HEIGHT, image);
			}
		}
	}
}

