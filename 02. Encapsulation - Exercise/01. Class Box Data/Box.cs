using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public double Length
        {
            get { return length; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length cannot be zero or negative.");
                }
                else
                {
                    length = value;
                }
            }
        }
        public double Width
        {
            get { return width; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width cannot be zero or negative.");
                }
                else
                {
                    width = value;
                }
            }
        }
        public double Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height cannot be zero or negative.");
                }
                else
                {
                    height = value;
                }
            }
        }

        public double SurfaceArea() // Surface Area = 2lw + 2lh + 2wh
        {
            double result = 2 * length * width + 2 * length * height + 2 * width * height;
            return result;
        }

        public double LateralSurfaceArea() // Lateral Surface Area = 2lh + 2wh
        {
            double result = 2 * length * height + 2 * width * height;
            return result;
        }

        public double Volume() // Volume = lwh
        {
            double result = length * width * height;
            return result;
        }
    }
}
