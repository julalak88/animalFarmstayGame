using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : AILerp
{

    public Character character;

	public override void OnTargetReached() {
        character.OnReached();
    }
}
