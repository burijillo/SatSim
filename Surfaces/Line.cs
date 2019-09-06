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

		private static PropertyHolder<double, Line> ThicknessProperty =
			new PropertyHolder<double, Line>("Thickness", 1, OnGeometryChanged);

		public double Thickness
		{
			get { return ThicknessProperty.Get(this); }
			set { ThicknessProperty.Set(this, value); }
		}

		private static PropertyHolder<Point3D, Line> PositionProperty =
			new PropertyHolder<Point3D, Line>("Position", new Point3D(0, 0, 0), OnGeometryChanged);

		public Point3D Position
		{
			get { return PositionProperty.Get(this); }
			set { PositionProperty.Set(this, value); }
		}

		private static PropertyHolder<double, Line> AngleLongitudeProperty =
			new PropertyHolder<double, Line>("AngleLongitude", 1.0, OnGeometryChanged);

		public double AngleLongitude
		{
			get { return AngleLongitudeProperty.Get(this); }
			set { AngleLongitudeProperty.Set(this, value); }
		}

		private static PropertyHolder<double, Line> AngleLatitudeProperty =
			new PropertyHolder<double, Line>("AngleLatitude", 1.0, OnGeometryChanged);

		public double AngleLatitude
		{
			get { return AngleLatitudeProperty.Get(this); }
			set { AngleLatitudeProperty.Set(this, value); }
		}

		private double _radius;
		private double _thickness;
		private Point3D _position;
		private double _angleLongitude;
		private double _angleLatitude;

		protected override Geometry3D CreateMesh()
		{
			_radius = Radius;
			_thickness = Thickness;
			_position = Position;
			_angleLongitude = AngleLongitude;
			_angleLatitude = AngleLatitude;

			double angleLongitude = 2 * Math.PI / 360 * _angleLongitude;
			double angleLatitude = 2 * Math.PI / 360 * _angleLatitude;

			MeshGeometry3D mesh = new MeshGeometry3D();
			Vector3D normal = new Vector3D(-1, -1, 1);

			// TODO: SE ESTA PLANTEANDO COMO UN CUBO EN VEZ DE COMO UN RECTANGULO
			Point3D point0 = new Point3D(
				_position.X + _thickness * Math.Cos(Math.PI / 2 - angleLongitude) + _radius * Math.Cos(angleLatitude) * Math.Cos(angleLongitude),
				_position.Y - _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) - _thickness * Math.Cos(Math.PI / 2 - angleLatitude) + _radius * Math.Cos(angleLatitude) * Math.Sin(angleLongitude),
				_position.Z + _thickness * Math.Sin(Math.PI / 2 - angleLatitude) + _radius * Math.Sin(angleLatitude));
			Point3D point1 = new Point3D(
				_position.X - _thickness * Math.Cos(Math.PI / 2 - angleLongitude) + _radius * Math.Cos(angleLatitude) * Math.Cos(angleLongitude),
				_position.Y + _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) - _thickness * Math.Cos(Math.PI / 2 - angleLatitude) + _radius * Math.Cos(angleLatitude) * Math.Sin(angleLongitude),
				_position.Z + _thickness * Math.Sin(Math.PI / 2 - angleLatitude) + _radius * Math.Sin(angleLatitude));
			Point3D point2 = new Point3D(
				_position.X + _thickness * Math.Cos(Math.PI / 2 - angleLongitude),
				_position.Y - _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) - _thickness * Math.Cos(Math.PI / 2 - angleLatitude),
				_position.Z + _thickness * Math.Sin(Math.PI / 2 - angleLatitude));
			Point3D point3 = new Point3D(
				_position.X - _thickness * Math.Cos(Math.PI / 2 - angleLongitude),
				_position.Y + _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) - _thickness * Math.Cos(Math.PI / 2 - angleLatitude),
				_position.Z + _thickness * Math.Sin(Math.PI / 2 - angleLatitude));
			Point3D point4 = new Point3D(
				_position.X + _thickness * Math.Cos(angleLatitude) * Math.Cos(Math.PI / 2 - angleLongitude) + _radius * Math.Cos(angleLatitude) * Math.Cos(angleLongitude),
				_position.Y - _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) + _thickness * Math.Cos(Math.PI / 2 - angleLatitude) + _radius * Math.Cos(angleLatitude) * Math.Sin(angleLongitude),
				_position.Z - 5 * Math.Sin(Math.PI / 2 - angleLatitude) + _radius * Math.Sin(angleLatitude));
			Point3D point5 = new Point3D(
				_position.X - _thickness * Math.Cos(angleLatitude) * Math.Cos(Math.PI / 2 - angleLongitude) + _radius * Math.Cos(angleLatitude) * Math.Cos(angleLongitude),
				_position.Y + _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) + _thickness * Math.Cos(Math.PI / 2 - angleLatitude) + _radius * Math.Cos(angleLatitude) * Math.Sin(angleLongitude),
				_position.Z - _thickness * Math.Sin(Math.PI / 2 - angleLatitude) + _radius * Math.Sin(angleLatitude));
			Point3D point6 = new Point3D(
				_position.X + _thickness * Math.Cos(angleLatitude) * Math.Cos(Math.PI / 2 - angleLongitude),
				_position.Y - _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) + _thickness * Math.Cos(Math.PI / 2 - angleLatitude),
				_position.Z - _thickness * Math.Sin(Math.PI / 2 - angleLatitude));
			Point3D point7 = new Point3D(
				_position.X - _thickness * Math.Cos(angleLatitude) * Math.Cos(Math.PI / 2 - angleLongitude),
				_position.Y + _thickness * Math.Cos(angleLatitude) * Math.Sin(Math.PI / 2 - angleLongitude) + _thickness * Math.Cos(Math.PI / 2 - angleLatitude),
				_position.Z - _thickness * Math.Sin(Math.PI / 2 - angleLatitude));

			if (_angleLatitude <= 90 && _angleLatitude > 85)
			{
				point0 = new Point3D(_thickness, -_thickness, _radius);
				point1 = new Point3D(-_thickness, -_thickness, _radius);
				point2 = new Point3D(_thickness, -_thickness, 0);
				point3 = new Point3D(-_thickness, -_thickness, 0);
				point4 = new Point3D(_thickness, _thickness, _radius);
				point5 = new Point3D(-_thickness, _thickness, _radius);
				point6 = new Point3D(_thickness, _thickness, 0);
				point7 = new Point3D(-_thickness, _thickness, 0);
			}

			mesh.Positions.Add(point0);
			mesh.Positions.Add(point1);
			mesh.Positions.Add(point2);
			mesh.Positions.Add(point3);
			mesh.Positions.Add(point4);
			mesh.Positions.Add(point5);
			mesh.Positions.Add(point6);
			mesh.Positions.Add(point7);

			//Debug.WriteLine("Latitud: " + _angleLatitude + " Longitud: " + _angleLongitude + " punto 0: " + point0.X + "; " + point0.Y + "; " + point0.Z);
			//Debug.WriteLine("Latitud: " + _angleLatitude + " Longitud: " + _angleLongitude + " punto 1: " + point1.X + "; " + point1.Y + "; " + point1.Z);
			//Debug.WriteLine("Latitud: " + _angleLatitude + " Longitud: " + _angleLongitude + " punto 2: " + point2.X + "; " + point2.Y + "; " + point2.Z);
			//Debug.WriteLine("Latitud: " + _angleLatitude + " Longitud: " + _angleLongitude + " punto 3: " + point3.X + "; " + point3.Y + "; " + point3.Z);

			mesh.TriangleIndices.Add(0);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(7);
			mesh.TriangleIndices.Add(6);

			mesh.TriangleIndices.Add(0);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(7);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(7);
			mesh.TriangleIndices.Add(3);

			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(7);
			mesh.TriangleIndices.Add(0);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(5);

			mesh.Normals.Add(normal);

			mesh.Freeze();
			return mesh;
		}
	}
}
