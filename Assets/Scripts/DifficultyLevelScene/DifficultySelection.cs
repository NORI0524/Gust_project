using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DifficultySelection : BaseCompornent, IPointerClickHandler
{
    [SerializeField] private GameObject clickedGameObject = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    //オブジェクトをクリック
    public void OnPointerClick(PointerEventData eventData)
    {
        clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
            }

            //オブジェクトをちょっと拡大(選択したことが分かるように)
            clickedGameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

            //ここにフェードイン(場面転換)開始を書く



        //選択された難易度のゲームシーンに移動
        if (clickedGameObject.name == "Item_2") //難易度：梅をクリックしていたら
        {
            Debug.Log("梅");
            //SceneManager.LoadScene("TitleScene");
        }
        if (clickedGameObject.name == "Item_1") //難易度：竹をクリックしていたら
        {
            Debug.Log("竹");
            //SceneManager.LoadScene("GameScene");
        }
        if (clickedGameObject.name == "Item_0") //難易度：松をクリックしていたら
        {
            Debug.Log("松");
            //SceneManager.LoadScene("ResultScene");
        }
    }
}
