using DG.Tweening;

public static class TweenHelper
{
	public static void SequenceKill ( Sequence sequence )
	{
		if ( sequence != null && sequence.IsActive ( ) )
			sequence.Kill ( );
	}

	public static void SequencePause ( Sequence sequence )
	{
		if ( sequence != null && sequence.IsActive ( ) )
			sequence.Pause ( );
	}

	public static void SequencePlay ( Sequence sequence )
	{
		if ( sequence != null && sequence.IsActive ( ) && !sequence.IsPlaying ( ) )
			sequence.Play ( );
	}

	public static void TweenKill ( Tween tween )
	{
		if ( tween != null && tween.IsActive ( ) )
			tween.Kill ( );
	}

	public static void TweenPause ( Tween tween )
	{
		if ( tween != null && tween.IsActive ( ) && tween.IsPlaying ( ) )
			tween.Pause ( );
	}

	public static void TweenPlay ( Tween tween )
	{
		if ( tween != null && tween.IsActive ( ) && !tween.IsPlaying ( ) )
			tween.Play ( );
	}

	public static void TweenRestart ( Tween tween )
	{
		if ( tween != null && tween.IsActive ( ) && tween.IsPlaying ( ) )
			tween.Restart ( );
	}

	public static bool TryTweenRestart ( Tween tween )
	{
		if ( tween != null && tween.IsActive ( ) && tween.IsPlaying ( ) )
		{
			tween.Restart ( );
			return true;
		}

		return false;
	}

}
