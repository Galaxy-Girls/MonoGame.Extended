using Microsoft.Xna.Framework.Content;

namespace MonoGame.Extended.Tiled
{
    public static class ContentReaderExtensions
    {
        public static void ReadTiledMapProperties(this ContentReader reader, TiledMapProperties properties)
        {
            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++)
            {
                var key = reader.ReadString();
<<<<<<< HEAD
                var value = new TiledMapPropertyValue(
                    reader.ReadString(),
                    reader.ReadString(),
                    reader.ReadString());
=======
                var value = new TiledMapPropertyValue(reader.ReadString());
>>>>>>> bde79b89970010e29e2204042ac717246b20073b
                ReadTiledMapProperties(reader, value.Properties);
                properties[key] = value;
            }
        }
    }
}
