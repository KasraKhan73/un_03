using UnityEngine;

namespace Lunha
{
	public class Curve3Last : ICurve
	{
		public Curve4 curve4;
		public Transform p2;

		public Curve3Last ( Vector3 p0, Vector3 p1, Transform p2 )
		{
			this.p2 = p2;
			curve4 = new Curve4 ( p0, p1, p2.position, Vector3.zero );
		}

		public Curve4 GetCurve ( )
		{
			curve4.p2 = p2.position;
			return curve4;
		}

		public Curve4 GetCurve ( bool globalPosition = true )
		{
			return GetCurve ( );
		}
	}
}