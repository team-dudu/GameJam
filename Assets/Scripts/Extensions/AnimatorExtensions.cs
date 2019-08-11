using System;
using System.Linq;
using UnityEngine;

namespace GameJam
{
	public static class AnimatorExtensions
	{
		public static void SetAnimation(this Animator animator, AnimationParameter animation, object value = null)
		{
			var animationToSet = animator.parameters.Where(acp => acp.name == animation.ToAnimationName()).FirstOrDefault();

			if (animationToSet == null)
			{
				Debug.LogWarning("Cannot find animation in the current animator. Defines it in your animator controller.", animator);
				return;
			}

			animator.SetAnimation(animationToSet, value);
		}

		public static void SetAnimation(this Animator animator, AnimatorControllerParameter animationToSet,  object parameter = null)
		{
			switch (animationToSet.type)
			{
				case AnimatorControllerParameterType.Int:
					animator.SetInteger(animationToSet.name, (int)parameter);
					break;
				case AnimatorControllerParameterType.Float:
					animator.SetFloat(animationToSet.name, (float)parameter);
					break;
				case AnimatorControllerParameterType.Bool:
					animator.SetBool(animationToSet.name, (bool)parameter);
					break;
				case AnimatorControllerParameterType.Trigger:
					animator.SetTrigger(animationToSet.name);
					break;
				default:
					throw new System.Exception("Cannot find animation type.");
			}
		}
	}
}
