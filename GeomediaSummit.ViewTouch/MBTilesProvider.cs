using System;

using Google.Maps;
using MonoTouch.UIKit;

using GeomediaSummit.Core;
using MonoTouch.Foundation;

namespace GeomediaSummit.ViewTouch
{
	public class MBTilesProvider : SyncTileLayer
	{
		private MBTilesAccessor accessor;

		//private static int TILE_WIDTH  = 256;
		//private static int TILE_HEIGHT = 256;

		public MBTilesProvider () : base()
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

		public override UIImage Tile (uint x, uint y, uint zoom)
		{
			var image = accessor.GetTileImage ((int)x, (int)y, (int)zoom);

			if (image == null) {
				return Constants.TileLayerNoTile;
			} else {
				return UIImage.LoadFromData (NSData.FromArray(image));
			}
		}
	}
}

