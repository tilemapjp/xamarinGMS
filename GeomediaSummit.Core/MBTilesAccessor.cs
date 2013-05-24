using System;

using System.IO;
using System.Data;
using Mono.Data.Sqlite;

namespace GeomediaSummit.Core
{
	public partial class MBTilesAccessor
	{
		private string dbFile = "suita.mbtiles";
		private string dbFolder;
		private string dbPath;

		public MBTilesAccessor ()
		{
			//DBファイルの準備
			this.Initialize();
		}

		public double[] zoomLatLng()
		{
			double[] zoomLatLng = new double[3];

			//DB接続のオープン
			using (var Conn = new SqliteConnection ("Data Source=" + this.dbPath)) {		
				Conn.Open ();

				//SQLコマンドオブジェクト
				using (var Cmd = Conn.CreateCommand ()) {
					Cmd.CommandText = "SELECT value FROM metadata WHERE name = @name";
					Cmd.Parameters.Clear ();
					Cmd.Parameters.AddWithValue ("@name", "minzoom");

					using (var reader = Cmd.ExecuteReader ()) {
						while (reader.Read()) {
							var value = reader.GetString (0);
							zoomLatLng [0] = double.Parse (value);
						}
					}

					Cmd.Parameters.Clear ();
					Cmd.Parameters.AddWithValue ("@name", "center");

					using (var reader = Cmd.ExecuteReader ()) {
						while (reader.Read()) {
							var value = reader.GetString (0);
							var valArray = value.Split (',');

							for (var i = 0; i < 2; i++) {
								zoomLatLng [i + 1] = double.Parse(valArray [i]);
							}
						}
					}

				}
				Conn.Close ();
			}
			return zoomLatLng;
		}

		public double[] bounds()
		{
			double[] bounds = new double[4];

			//DB接続のオープン
			using (var Conn = new SqliteConnection ("Data Source=" + this.dbPath)) {		
				Conn.Open ();

				//SQLコマンドオブジェクト
				using (var Cmd = Conn.CreateCommand ()) {
					Cmd.CommandText = "SELECT value FROM metadata WHERE name = @name";
					Cmd.Parameters.Clear ();
					Cmd.Parameters.AddWithValue ("@name", "bounds");

					using (var reader = Cmd.ExecuteReader ()) {
						while (reader.Read()) {
							var value = reader.GetString (0);
							var valArray = value.Split (',');

							for (var i = 0; i < 4; i++) {
								bounds [i] = double.Parse(valArray [i]);
							}
						}
					}
				}
				Conn.Close ();
			}
			return bounds;
		}

		public byte[] GetTileImage(int x, int y, int zoom)
		{
			byte[] image = null;

			//DB接続のオープン
			using (var Conn = new SqliteConnection ("Data Source=" + this.dbPath)) {		
				Conn.Open ();

				//SQLコマンドオブジェクト
				using (var Cmd = Conn.CreateCommand ()) {
					Cmd.CommandText = "SELECT tile_data FROM tiles WHERE tile_column = @column AND tile_row = @row AND zoom_level = @zoom";
					Cmd.Parameters.Clear ();
					Cmd.Parameters.AddWithValue ("@column", x);
					var tmsy = System.Math.Pow (2, zoom) - y - 1;
					Cmd.Parameters.AddWithValue ("@row", tmsy);
					Cmd.Parameters.AddWithValue ("@zoom", zoom);

					using (var reader = Cmd.ExecuteReader ()) {
						while (reader.Read()) {
							const int CHUNK_SIZE = 2 * 1024;
							byte[] buffer = new byte[CHUNK_SIZE];
							long bytesRead;
							long fieldOffset = 0;
							using (MemoryStream stream = new MemoryStream()) {
								while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0) {
									stream.Write (buffer, 0, (int)bytesRead);
									fieldOffset += bytesRead;
								}
								image = stream.ToArray ();
							}
						}
						reader.Close ();
					}
				}
				Conn.Close ();
			}
			return image;
		}
	}
}

