using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AquariumLibrary.BaseClasses
{
    public class VectorF
    {
        public PointF Start { get; }
        public PointF End { get; }
        public float X { get; }
        public float Y { get; }

        public VectorF(PointF start, PointF end)
        {
            Start = start;
            End = end;
            X = end.X - start.X;
            Y = end.Y - start.Y;
        }

        public VectorF(float x, float y)
        {
            Start = new PointF(0f, 0f);
            End = new PointF(x, y);
        }

        public static VectorF operator +(VectorF vector1, VectorF vector2)
        {
            return new VectorF(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static VectorF operator -(VectorF vector1, VectorF vector2)
        {
            return new VectorF(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        public static PointF operator +(PointF point, VectorF vector2)
        {
            return new PointF(point.X + vector2.X, point.Y + vector2.Y);
        }

        public static VectorF operator *(float mult, VectorF vector)
        {
            return new VectorF(vector.X * mult, vector.Y * mult);
        }

        public static VectorF operator *(VectorF vector, float mult)
        {
            return mult * vector;
        }

        public static double operator *(VectorF vector1, VectorF vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y;
        }

        /// <summary>
        /// Возвращает длину вектора
        /// </summary>
        /// <returns></returns>
        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Возвращает угол между двумя векторами
        /// </summary>
        /// <param name="vector1">Первый вектор</param>
        /// <param name="vector2">Второй вектор</param>
        /// <returns></returns>
        public static double operator ^(VectorF vector1, VectorF vector2)
        {
            return Math.Acos(vector1 * vector2 / (vector1.GetLength() * vector2.GetLength()));
        }

        public bool IsVectorBetween(VectorF v1, VectorF v2)
        {
            var v1v2 = v1 ^ v2;
            var v1p = v1 ^ this;
            var pv2 = this ^ v2;
            return Math.Abs(v1v2 - (v1p + pv2)) < 0.0001;
        }

        /// <summary>
        /// Возвращает нормализованный еденичный вектор
        /// </summary>
        /// <returns></returns>
        public VectorF Normalize()
        {
            var length = (float)GetLength();
            return new VectorF(X /length, Y /length);
        }

        /// <summary>
        /// Возвращает нормализованный еденичный вектор
        /// </summary>
        /// <returns></returns>
        public VectorF Normalized => new VectorF(X / (float)GetLength(), Y / (float)GetLength());
    }
}
