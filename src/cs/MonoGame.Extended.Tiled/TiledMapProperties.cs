using System.Collections.Generic;

namespace MonoGame.Extended.Tiled
{
    public class TiledMapProperties : Dictionary<string, TiledMapPropertyValue>
    {
        public bool TryGetValue(string key, out string value)
        {
            bool result = TryGetValue(key, out TiledMapPropertyValue tmpVal);
<<<<<<< HEAD
            value = result ? tmpVal.Value : null;
=======
            value = result ? null : tmpVal.Value;
>>>>>>> bde79b89970010e29e2204042ac717246b20073b
            return result;
        }
    }
}
