using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaloLibrary.Geometry.Interfaces
{
    public abstract class Figure2D
    {
        private double area;
        private double perimeter;
        private PointF location;

        protected double Perimeter { get => perimeter; set => perimeter = value; }
        protected PointF Location { get => location; set => location = value; }
        protected double Area { get => area; set => area = value; }
    }

    internal interface IFigure
    {

    }
}
