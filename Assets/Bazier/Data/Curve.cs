using UnityEngine;

namespace Lunha
{
	public class Curve : MonoBehaviour, ICurve
	{
		public Transform p0;
		public Transform p1;

		public Transform p2;

		// ~ Cubic
		public Transform p3;

		public Curve4 GetCurve ( )
			=> new Curve4 ( p0.position, p1.position, p2.position, p3.position );

		public Curve4 GetCurve ( bool globalPosition ) =>
			globalPosition
				? GetCurve ( )
				: new Curve4 ( p0.localPosition, p1.localPosition, p2.localPosition, p3.localPosition );
	}
}