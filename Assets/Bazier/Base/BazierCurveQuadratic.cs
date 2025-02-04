using UnityEngine;

namespace Lunha
{
	public class BazierCurveQuadratic: MonoBehaviour
	{
		[SerializeField] [Tooltip("Transform with 3 control points")]
		protected Curve[] curves;

		[Header("Editor")] [SerializeField, Range(0.06f, 1f)]
		private float _radius = 0.12f;

		[SerializeField] private Color _color = Color.cyan;

		[SerializeField] private bool _alignCurves;

		public bool drawGizmos;
		public ICurve[] iCurves;

		public Curve[] Curves
		{
			get => curves;
			set => curves = value;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (!drawGizmos || !CheckData(curves?.Length != iCurves?.Length)) return;

			if (_alignCurves)
			{
				for (var i = 1; i < curves.Length; i++)
				{
					curves[i].p0 = curves[i - 1].p3;

					if (i + 1 < curves.Length)
					{
						curves[i].p3 = curves[i + 1].p0;
					}
				}
			}

			Gizmos.color = _color;

			foreach (var curve in iCurves)
			{
				var curve4 = curve.GetCurve();

				// Draw curve
				for (var t = 0f; t <= 1f; t += 0.05f)
					Gizmos.DrawSphere(BazierCurves.GetQuadraticPosition(curve4, t), _radius);

				var p1Pos = curve4.p1;
				Gizmos.DrawLine(curve4.p0, p1Pos);
				Gizmos.DrawLine(p1Pos, curve4.p2);
			}
		}
#endif

		protected bool CheckData(bool update = false)
		{
			if ((update || iCurves == null) && curves?.Length > 0)
			{
				iCurves = new ICurve [curves.Length];
				for (var i = 0; i < curves.Length; i++)
					iCurves[i] = curves[i];
			}

			return iCurves != null;
		}
	}
}