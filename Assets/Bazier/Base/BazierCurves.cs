using System;
using UnityEngine;

namespace Lunha
{
	public static class BazierCurves
	{
		/// <summary>
		///     Return the quadratic curve position
		/// </summary>
		/// <param name="curve"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static Vector3 GetQuadraticPosition ( Curve4 curve, float t )
		{
			// P0
			return Mathf.Pow ( 1f - t, 2f ) * curve.p0 +
				   // P1
				   2f * ( 1f - t ) * t * curve.p1 +
				   // P2
				   Mathf.Pow ( t, 2f ) * curve.p2;
		}

		public static Vector3 GetQuadraticPosition ( ref Curve [ ] curves, float t, bool globalSpace = true )
		{
			var curveNTime = GetCurveNTime ( ref curves, t, globalSpace );
			return GetQuadraticPosition ( curveNTime.curve4, curveNTime.time );
		}

		/// <summary>
		///     Return the cubic curve position
		/// </summary>
		/// <param name="curve"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static Vector3 GetCubicPosition ( Curve4 curve, float t )
		{
			// P0
			return Mathf.Pow ( 1f - t, 3f ) * curve.p0 +
				   // P1
				   3f * Mathf.Pow ( 1f - t, 2f ) * t * curve.p1 +
				   // P2
				   3f * ( 1f - t ) * Mathf.Pow ( t, 2f ) * curve.p2 +
				   // P3
				   Mathf.Pow ( t, 3f ) * curve.p3;
		}

		public static Vector3 GetCubicPosition ( Curve[] curves, float t, bool globalSpace = true )
		{
			var curveNTime = GetCurveNTime ( curves, t, globalSpace );
			return GetCubicPosition ( curveNTime.curve4, curveNTime.time );
		}

		private static (Curve4 curve4, float time) GetCurveNTime ( Curve[] curves, float t, bool globalSpace )
		{
			var p = 100f / curves.Length;
			var v = t * 100f;

			var c = v / p;

			if ( c > curves.Length )
				c = curves.Length;

			var i = 0;

			if ( c > 1f )
			{
				while ( true )
				{
					i++;
					c -= 1f;

					if ( c <= 1f )
					{
						break;
					}
				}
			}

			return (curves[i].GetCurve ( globalSpace ), c);
		}

		public static Vector3 GetCubicPosition ( ref Curve [ ] curves, float t, bool globalSpace = true )
		{
			var curveNTime = GetCurveNTime ( ref curves, t, globalSpace );
			return GetCubicPosition ( curveNTime.curve4, curveNTime.time );
		}

		private static (Curve4 curve4, float time) GetCurveNTime ( ref Curve [ ] curves, float t, bool globalSpace )
		{
			var p = 100f / curves.Length;
			var v = t * 100f;

			var c = v / p;

			if ( c > curves.Length )
				c = curves.Length;

			var i = 0;

			if ( c > 1f )
			{
				while ( true )
				{
					i++;
					c -= 1f;

					if ( c <= 1f )
					{
						break;
					}
				}
			}

			return ( curves [ i ].GetCurve ( globalSpace ), c );
		}

		public static Vector3 GetPositionByAxis ( Vector3 position, Vector3 targetPosition, MoveAxis moveAxis )
		{
			switch ( moveAxis )
			{
				case MoveAxis.XYZ :
					return targetPosition;
				case MoveAxis.XZ :
					return new Vector3 ( targetPosition.x, position.y, targetPosition.z );
				case MoveAxis.XY :
					return new Vector3 ( targetPosition.x, targetPosition.y, position.z );
				default :
					return targetPosition;
			}
		}
	}

	public enum CurveType
	{
		Cubic,
		Quadratic
	}

	public enum MoveAxis
	{
		XYZ,
		XZ,
		XY
	}
}