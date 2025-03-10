using UnityEngine;

public class CarAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isFolded = false;
   

    
    private Fly_to_Drive_Transision fly_To_Drive_Transision;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        fly_To_Drive_Transision = gameObject.GetComponent<Fly_to_Drive_Transision>();

    }

    void Update()
    {
        
            if (fly_To_Drive_Transision.flyingMode)
            {
                // Fold the tires
                animator.SetBool("FoldTires", true);
                animator.SetBool("UnfoldTires", false);
                isFolded = true;
               // Debug.Log("folding");
                Invoke("ThrusterOn", 1f);
            }
            else
            {
                // Unfold the tires
                animator.SetBool("FoldTires", false);
                animator.SetBool("UnfoldTires", true);
                isFolded = false;
             //   Debug.Log("unfolding");
            }
        
    }
    
}
