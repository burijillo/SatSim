using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace Ikriv.Surfaces
{
	public sealed class Arc : Surface
	{
		//private static PropertyHolder<double, Arc> RadiusProperty =
		//	new PropertyHolder<double, Arc>("Radius", 1.0, OnGeometryChanged);

		//public double Radius
		//{
		//	get { return RadiusProperty.Get(this); }
		//	set { RadiusProperty.Set(this, value); }
		//}

		private static PropertyHolder<Point3D, Arc> PositionProperty =
			new PropertyHolder<Point3D, Arc>("Position", new Point3D(0, 0, 0), OnGeometryChanged);

		public Point3D Position
		{
			get { return PositionProperty.Get(this); }
			set { PositionProperty.Set(this, value); }
		}

		private static PropertyHolder<Point3D, Arc> SatPositionProperty =
			new PropertyHolder<Point3D, Arc>("SatPosition", new Point3D(0, 0, 0), OnGeometryChanged);

		public Point3D SatPosition
		{
			get { return SatPositionProperty.Get(this); }
			set { SatPositionProperty.Set(this, value); }
		}

		//private static PropertyHolder<double, Arc> AngleProperty =
		//	new PropertyHolder<double, Arc>("Angle", 1.0, OnGeometryChanged);

		//public double Angle
		//{
		//	get { return AngleProperty.Get(this); }
		//	set { AngleProperty.Set(this, value); }
		//}

		private static PropertyHolder<double, Arc> AngleInclinationProperty =
			new PropertyHolder<double, Arc>("AngleInclination", 1.0, OnGeometryChanged);

		public double AngleInclination
		{
			get { return AngleInclinationProperty.Get(this); }
			set { AngleInclinationProperty.Set(this, value); }
		}

		//private double _radius;
		private Point3D _position;
		private Point3D _SatPosition;
		//private double _angle;
		private double _angleInclination;

        // CHECKEAR ESTAS ECUACIONES PORQUE ESTAN MAL
		private Point3D PointForAngle(double angle_plain, double angle_lat, double radious)
		{
			double x = _position.X + radious * Math.Cos(angle_plain);
			double y = _position.Y + radious * Math.Sin(angle_plain);
			double z = _position.Z + radious * Math.Sin(angle_lat) * Math.Sin(angle_plain);
			return new Point3D(x, y, z);
		}

		private Point3D PointForPosition(double offset, double angle_lat)
		{
			double x0 = _position.X + _SatPosition.X + offset;
			double y0 = _position.Y + _SatPosition.Y + offset;
			double z0 = _position.Z + _SatPosition.Z + offset;

			return new Point3D(x0, y0, z0);
		}

        private bool isInitialized = false;
        Point3D prevPoint = new Point3D(0,0,0);
        Point3D prevPoint_thick = new Point3D(0,0,0);
        Point3D newPoint = new Point3D();
        Point3D newPoint_thick = new Point3D();
        Vector3D normal = new Vector3D(0, 0, 1);
        // TODO CHAPUZA
        int count = 0;

        MeshGeometry3D testGeometry = new MeshGeometry3D();

        protected override Geometry3D CreateMesh()
		{
			//_radius = Radius;
			_position = Position;
			_SatPosition = SatPosition;
			//_angle = Angle;
			_angleInclination = AngleInclination;

			double angleInclination = 2 * Math.PI / 360 * _angleInclination;

			//if (!isInitialized)
			//{
			//	prevPoint = PointForAngle(_angle - 0.01, angleInclination, _radius);
			//	prevPoint_thick = PointForAngle(_angle - 0.01, angleInclination, _radius);
			//}

			////Point3D prevPoint = PointForAngle(0, 0, 0);
			////Point3D prevPoint_thick = PointForAngle(0, 0, 0);
			////Vector3D normal = new Vector3D(0, 0, 1);

			////MeshGeometry3D testGeometry = new MeshGeometry3D();
			//newPoint = PointForAngle(_angle, angleInclination, _radius);
			//newPoint_thick = PointForAngle(_angle, angleInclination, _radius * 0.95);

			//-------------------TEST-------------------------

			if (!isInitialized)
			{
				prevPoint = PointForPosition(0, angleInclination);
				prevPoint_thick = PointForPosition(-10, angleInclination);
			}

			newPoint = PointForPosition(0, angleInclination);
			newPoint_thick = PointForPosition(10, angleInclination);

			//-------------------TEST-------------------------

			if (newPoint.X != 0 && newPoint.Y != 0 && newPoint.Z != 0)
            {
                testGeometry.Positions.Add(prevPoint_thick);
                testGeometry.Positions.Add(newPoint_thick);
                testGeometry.Positions.Add(prevPoint);
                testGeometry.Positions.Add(newPoint);

                testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 4);
                testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 3);
                testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 2);
                testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 3);
                testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 1);
                testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 2); 
            }

			//Debug.WriteLine("NEW: " + newPoint.X + "; " + newPoint.Y + "; " + newPoint.Z);
			//Debug.WriteLine("NEW_THICK: " + newPoint_thick.X + "; " + newPoint_thick.Y + "; " + newPoint_thick.Z);
			//Debug.WriteLine("PREV: " + prevPoint.X + "; " + prevPoint.Y + "; " + prevPoint.Z);
			//Debug.WriteLine("PREV_THICK: " + prevPoint_thick.X + "; " + prevPoint_thick.Y + "; " + prevPoint_thick.Z);

            prevPoint = newPoint;
            prevPoint_thick = newPoint_thick;

            //int div = Convert.ToInt32(_angle / (2 * Math.PI) * 360);
            //for (int i = 1; i <= div; ++i)
            //{
            //	double angle = 2 * Math.PI / 360 * i;

            //	Point3D newPoint = PointForAngle(angle, angleInclination, _radius);
            //	Point3D newPoint_thick = PointForAngle(angle, angleInclination, _radius * 0.95);

            //	// If position is added, it would make a triangle with one point in the origin
            //	testGeometry.Positions.Add(_position);
            //	testGeometry.Positions.Add(prevPoint_thick);
            //	testGeometry.Positions.Add(newPoint_thick);
            //	testGeometry.Positions.Add(prevPoint);
            //	testGeometry.Positions.Add(newPoint);

            //	testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 4);
            //	testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 3);
            //	testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 2);
            //	testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 3);
            //	testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 1);
            //	testGeometry.TriangleIndices.Add(testGeometry.Positions.Count - 2);

            	testGeometry.Normals.Add(normal);

            //	//Debug.WriteLine("NEW: " + newPoint.X + "; " + newPoint.Y + "; " + newPoint.Z);
            //	//Debug.WriteLine("NEW_THICK: " + newPoint_thick.X + "; " + newPoint_thick.Y + "; " + newPoint_thick.Z);
            //	//Debug.WriteLine("PREV: " + prevPoint.X + "; " + prevPoint.Y + "; " + prevPoint.Z);
            //	//Debug.WriteLine("PREV_THICK: " + prevPoint_thick.X + "; " + prevPoint_thick.Y + "; " + prevPoint_thick.Z);

            //	prevPoint = newPoint;
            //	prevPoint_thick = newPoint_thick;

            //}

            //testGeometry.Freeze();
            count++;
            isInitialized = true;
			return testGeometry;
		}
	}
}
