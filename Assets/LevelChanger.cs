using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    
    private int levelToLoad;
    private int startGame = 1;
 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            //FadeToLevel(1);
        }
    }

    public void FadeToLevel (int levelIndex) {
        animator.SetTrigger("FadeOut");
    }

    public void fadeComplete(){
        SceneManager.LoadScene(startGame);
    }
}
