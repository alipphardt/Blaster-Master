using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnExit : StateMachineBehaviour
{

    Animator myAnimator;
    AnimatorStateInfo myStateInfo;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myAnimator = animator;
        myStateInfo = stateInfo;
        animator.GetComponent<SophiaMovement>().StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel(){
        yield return new WaitForSeconds(myStateInfo.length);
        myAnimator.GetComponent<SophiaMovement>().gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

}
