using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{

    public int X { get; set; }
    public int Y { get; set; }

    public  Point(int X,int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public static bool operator ==(Point first, Point second) => first.X == second.X && first.Y == second.Y;

    public static bool operator !=(Point first, Point second) => first.X != second.X || first.Y != second.Y;

    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

    public override bool Equals(object obj)
    {
        return obj is Point point &&
               X == point.X &&
               Y == point.Y;
    }

    public override int GetHashCode()
    {
        int hashCode = 1861411795;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Y.GetHashCode();
        return hashCode;
    }
}
