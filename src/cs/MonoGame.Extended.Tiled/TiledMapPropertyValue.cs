namespace MonoGame.Extended.Tiled;

public class TiledMapPropertyValue
{
    public string Value { get; }

<<<<<<< HEAD
    public string Type { get; }

    public string PropertyType { get; }

    public TiledMapProperties Properties;

    public TiledMapPropertyValue(string value, string type, string propertyType)
    {
        Value = value;
        Type = type;
        PropertyType = propertyType;
        Properties = new();
    }

=======
    public TiledMapProperties Properties;

    public TiledMapPropertyValue()
    {
        Value = string.Empty;
        Properties = new();
    }

    public TiledMapPropertyValue(string value)
    {
        Value = value;
        Properties = new();
    }

    public TiledMapPropertyValue(TiledMapProperties properties)
    {
        Value = string.Empty;
        Properties = properties;
    }

>>>>>>> bde79b89970010e29e2204042ac717246b20073b
    public override string ToString() => Value;

    //public static implicit operator TiledMapPropertyValue(string value) => new(value);
    public static implicit operator string(TiledMapPropertyValue value) => value.Value;
    public static implicit operator TiledMapProperties(TiledMapPropertyValue value) => value.Properties;
}
