namespace GameJam
{
	public static class AnimationParameterExtensions
	{
		public static string ToAnimationName(this AnimationParameter animationParameter)
		{
			switch (animationParameter)
			{
				#region Triggers

				case AnimationParameter.Attack:
					return AnimationNames.Trigger.Attack;
				case AnimationParameter.Damaged:
					return AnimationNames.Trigger.Damaged;
				case AnimationParameter.Dash:
					return AnimationNames.Trigger.Dash;
				case AnimationParameter.Death:
					return AnimationNames.Trigger.Death;
				case AnimationParameter.DeathIdle:
					return AnimationNames.Trigger.DeathIdle;
				case AnimationParameter.Fire:
					return AnimationNames.Trigger.Fire;
                case AnimationParameter.Open:
                    return AnimationNames.Trigger.Open;

                #endregion

                #region Booleans

                case AnimationParameter.IsAttacking:
					return AnimationNames.Boolean.IsAttacking;
				case AnimationParameter.IsJumping:
					return AnimationNames.Boolean.IsJumping;
				case AnimationParameter.IsMoving:
					return AnimationNames.Boolean.IsMoving;
				case AnimationParameter.IsPlayerDetected:
					return AnimationNames.Boolean.IsPlayerDetected;

				#endregion

				default:
					return string.Empty;
			}
		}
	}
}
