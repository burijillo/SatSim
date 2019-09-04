using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace Ikriv.Surfaces
{
	public sealed class Line : Surface
	{
		private static PropertyHolder<double, Line> RadiusProperty =
			new PropertyHolder<double, Line>("Radius", 1.0, OnGeometryChanged);

		public double Radius
		{
			get { return RadiusProperty.Get(this); }
			set { RadiusProperty.Set(this, value); }
		}

		private static PropertyHolder<Point3D, Line> PositionProperty =
			new PropertyHolder<Point3D, Line>("Position", new Point3D(0, 0, 0), OnGeometryChanged);

		public Point3D Position
		{
			get { return PositionProperty.Get(this); }
			set { PositionProperty.Set(this, value); }
		}

		private static PropertyHolder<double, Line> AngleLongitudeProperty =
			new PropertyHolder<double, Line>("AngleInclination", 1.0, OnGeometryChanged);

		public double AngleLongitude
		{
			get { return AngleLongitudeProperty.Get(this); }
			set { AngleLongitudeProperty.Set(this, value); }
		}

		private static PropertyHolder<double, Line> AngleLatitudeProperty =
			new PropertyHolder<double, Line>("AngleInclination", 1.0, OnGeometryChanged);

		public double AngleLatitude
		{
			get { return AngleLatitudeProperty.Get(this); }
			set { AngleLatitudeProperty.Set(this, value); }
		}

		private double _radius;
		private Point3D _position;
		private double _angleLongitude;
		private double _angleLatitude;

		protected override Geometry3D CreateMesh()
		{
			_radius = Radius;
			_position = Position;
			_angleLongitude = AngleLongitude;
			_angleLatitude = AngleLatitude;

			double angleLongitude = 2 * Math.PI / 360 * _angleLongitude;
			double angleLatitude = 2 * Math.PI / 360 * _angleLatitude;

			MeshGeometry3D mesh = new MeshGeometry3D();
			Vector3D normal = new Vector3D(0, 0, 1);

			Point3D point0 = new Point3D(_position.X - 5 + _radius * Math.Cos(angleLatitude) * Math.Cos(angleLongitude), _position.Y - 5 + _radius * Math.Cos(angleLatitude) * Math.Sin(angleLongitude), _position.Z + _radius * Math.Sin(angleLatitude));
			Point3D point1 = new Point3D(_position.X + 5 + _radius * Math.Cos(angleLatitude) * Math.Cos(angleLongitude), _position.Y + 5 + _radius * Math.Cos(angleLatitude) * Math.Sin(angleLongitude), _position.Z + _radius * Math.Sin(angleLatitude));
			Point3D point2 = new Point3D(_position.X - 5, _position.Y - 5, _position.Z);
			Point3D point3 = new Point3D(_position.X + 5, _position.Y + 5, _position.Z);

			mesh.TriangleIndices.Add(mesh.Positions.Count - 4);
			mesh.TriangleIndices.Add(mesh.Positions.Count - 3);
			mesh.TriangleIndices.Add(mesh.Positions.Count - 2);
			mesh.TriangleIndices.Add(mesh.Positions.Count - 3);
			mesh.TriangleIndices.Add(mesh.Positions.Count - 1);
			mesh.TriangleIndices.Add(mesh.Positions.Count - 2); 

			mesh.Freeze();
			return mesh;
		}
	}
}
