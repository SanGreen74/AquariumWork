using System.Drawing;

namespace AquariumLibrary.Interfaces
{
    public interface IMovable
    {
        /// <summary>
        /// Перемещает объект в точку point
        /// </summary>
        /// <param name="point">Точка</param>
        void MoveTo(PointF point);
    }
}
