using System;
using UnityEngine;

namespace Lunha
{
	public class BazierCurveCubic : MonoBehaviour
	{
		[SerializeField] [Tooltip ( "Transform with 4 control points" )]
		public Curve [ ] curves;

		[SerializeField] protected bool _globalSpacePosition = true;

		[Header ( "Editor" )] [SerializeField, Range ( 0.06f, 1f )]
		private float _radius = 0.12f;

		[SerializeField] private Color _color = Color.cyan;

		[SerializeField] private bool _alignCurves;

		public bool drawGizmos;
		public ICurve [ ] iCurves;

		public Curve [ ] Curves
		{
			set => curves = value;
			get => curves;
		}

		public bool UseGlobalSpace => _globalSpacePosition;

#if UNITY_EDITOR
		private void OnDrawGizmos ( )
		{
			if ( !drawGizmos || !CheckData ( curves?.Length != iCurves?.Length ) ) return;

			if ( _alignCurves )
			{
				for ( var i = 1; i < curves.Length; i++ )
				{
					curves [ i ].p0.position = curves [ i - 1 ].p3.position;

					if ( i + 1 < curves.Length )
					{
						curves [ i ].p3.position = curves [ i + 1 ].p0.position;
					}
				}
			}

			Gizmos.color = _color;

			foreach ( var curve in iCurves )
			{
				var curve4 = curve.GetCurve ( );

				// Draw curve
				for ( var t = 0f; t <= 1f; t += 0.05f )
					Gizmos.DrawSphere ( BazierCurves.GetCubicPosition ( curve4, t ), _radius );

				// Draw control points directions
				Gizmos.DrawLine ( curve4.p0, curve4.p1 );
				Gizmos.DrawLine ( curve4.p2, curve4.p3 );
			}
		}
#endif

		public bool CheckData ( bool update = false )
		{
			if ( ( update || iCurves == null ) && curves?.Length > 0 )
			{
				iCurves = new ICurve [ curves.Length ];
				for ( var i = 0; i < curves.Length; i++ )
					iCurves [ i ] = curves [ i ];
			}

			return iCurves != null;
		}

		public virtual void Copy ( BazierCurveCubic copy )
		{
			copy._color = _color;
			copy._radius = _radius;
			copy.curves = curves;
		}
	}
}