using System;
using MonoTouch.Foundation;
using System.IO;

namespace GeomediaSummit.Core
{
	public partial class MBTilesAccessor
	{
		public void Initialize ()
		{
			this.dbFolder = NSBundle.MainBundle.BundlePath;
			this.dbPath = Path.Combine (this.dbFolder, this.dbFile);
		}
	}
}

