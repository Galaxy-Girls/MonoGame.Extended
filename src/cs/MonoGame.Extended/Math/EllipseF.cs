using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace MonoGame.Extended
{
    [DataContract]
    public struct EllipseF : IEquatable<EllipseF>, IEquatableByRef<EllipseF>, IShapeF
    {
        [DataMember] public Point2 Center { get; set; }
        [DataMember] public float RadiusX { get; set; }
        [DataMember] public float RadiusY { get; set; }
        [DataMember] public float Angle { get; set; }

        public Point2 Position
        {
            get => Center;
            set => Center = value;
        }

        public EllipseF(Vector2 center, float radiusX, float radiusY)
        {
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
            Angle = 0f;
        }

        public EllipseF(Vector2 center, float radiusX, float radiusY, float angle)
        {
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
            Angle = angle;
        }

        /// <inheritdoc cref="IShapeF.WithPosition(Point2)"/>
        public IShapeF WithPosition(Point2 newPosition) => this with { Position = newPosition };

        public float Left => BoundingRectangle.Left;
        public float Top => BoundingRectangle.Top;
        public float Right => BoundingRectangle.Right;
        public float Bottom => BoundingRectangle.Bottom;

        [field: AllowNull]
        public RectangleF BoundingRectangle
        {
            get
            {
                float x = Math.Abs(RadiusX * (float)Math.Cos(Angle)) + Math.Abs(RadiusY * (float)Math.Sin(Angle));
                float y = Math.Abs(RadiusX * (float)Math.Sin(Angle)) + Math.Abs(RadiusY * (float)Math.Cos(Angle));
                return new RectangleF(Center.X - x, Center.Y - y, x * 2, y * 2);
            }
        }

        [Pure]
        public bool Contains(float x, float y)
        {
            float dx = x - Center.X;
            float dy = y - Center.Y;
            float cos = (float)Math.Cos(Angle);
            float sin = (float)Math.Sin(Angle);
            float a = (dx * cos + dy * sin) / RadiusX;
            float b = (dy * cos - dx * sin) / RadiusY;
            return a * a + b * b <= 1f;
        }

        [Pure]
        public bool Contains(Point2 point) => Contains(point.X, point.Y);

        [Pure]
        public bool Contains(Vector2 point) => Contains(point.X, point.Y);

        [Pure]
        public Point2 ClosestPointTo(Point2 point)
        {
            float dx = point.X - Center.X;
            float dy = point.Y - Center.Y;
            float cos = (float)Math.Cos(Angle);
            float sin = (float)Math.Sin(Angle);
            float a = (dx * cos + dy * sin) / RadiusX;
            float b = (dy * cos - dx * sin) / RadiusY;
            float length = (float)Math.Sqrt(a * a + b * b);
            if (length == 0f)
                return Center;

            a /= length;
            b /= length;

            return new Point2(Center.X + a * RadiusX, Center.Y + b * RadiusY);
        }

        [Pure]
        public bool Intersects(EllipseF ellipse)
        {
            var closestPoint = ClosestPointTo(ellipse.Center);
            return ellipse.Contains(closestPoint);
        }

        [Pure]
        public float RadiusAtAngle(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            return (RadiusX * RadiusY) / (float)Math.Sqrt((RadiusY * RadiusY * cos * cos) + (RadiusX * RadiusX * sin * sin));
        }

        [Pure]
        public float AngleAtPoint(float x, float y)
        {
            float dx = x - Center.X;
            float dy = y - Center.Y;
            float cos = (float)Math.Cos(Angle);
            float sin = (float)Math.Sin(Angle);
            float a = (dx * cos + dy * sin) / RadiusX;
            float b = (dy * cos - dx * sin) / RadiusY;
            return (float)Math.Atan2(b, a);
        }

        [Pure]
        public float RadiusAtPoint(float x, float y) => RadiusAtAngle(AngleAtPoint(x, y));

        [Pure]
        public EllipseF Rotate(float angle) => new(Center, RadiusX, RadiusY, Angle + angle);

        [Pure]
        public EllipseF Reflect(Vector2 angle) => Reflect(angle.ToAngle());

        [Pure]
        public EllipseF Reflect(float angle)
        {
            Point2 center = Center;
            EllipseF rotation = Rotate(-angle);
            EllipseF conj = rotation.Conjugate;
            EllipseF result = conj.Rotate(angle);
            result.Position = center;
            return result;
        }

        [Pure]
        private EllipseF Conjugate => new(Center, -RadiusX, RadiusY);

        public bool Equals(EllipseF ellispse) => Equals(ref ellispse);

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(ref EllipseF ellispse)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return ellispse.Center == Center
                   && ellispse.RadiusX == RadiusX
                   && ellispse.RadiusY == RadiusY
                   && ellispse.Angle == Angle;
        }

        public override bool Equals(object obj) => (obj is EllipseF ellipse) && Equals(ellipse);

        public override int GetHashCode() => HashCode.Combine(Center, RadiusX, RadiusY, Angle);

        public override string ToString() => $"Center: {Center}, RadiusX: {RadiusX}, RadiusY: {RadiusY}, Angle: {Angle}";

        public static bool operator ==(EllipseF first, EllipseF second) => first.Equals(ref second);

        public static bool operator !=(EllipseF first, EllipseF second) => !(first == second);
    }
}
