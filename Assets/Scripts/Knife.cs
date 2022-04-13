
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _FlippyKnife
{
    public class Knife : MonoBehaviour {

        public static Knife Instance;

        public Rigidbody rb;
        public float force = 5f;
        public float torque = 20f;
        private float timeStartFlying;

        private Vector2 startSwipe;
        private Vector2 endSwipe;
	    // Use this for initialization
	    void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {
            // lấy tọa độ từ lần nhấn chuột đầu kéo tới điểm thả chuột cuối
		    if (Input.GetMouseButtonDown(0))
            {
                startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);// từ tọa độ màn hình pixel đưa về tọa độ 0 1
            }
            if (Input.GetMouseButtonUp(0))
            {
                endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Swipe();
            }
	    }
        void Swipe()// phương thức vuốt để tung dao
        {
            rb.isKinematic = false;
            timeStartFlying = Time.time;
            Vector2 swipe = endSwipe - startSwipe;
            //Debug.Log(swipe);
            rb.AddForce(swipe * force, ForceMode.Impulse);// đưa lực vào rb 
            rb.AddTorque(0f, 0f, -torque, ForceMode.Impulse);// đưa lực để xoay trục z

        }
        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Block")
            {
                rb.isKinematic = true;
                if (rb.isKinematic)//tính điểm khi bắt va chạm
                {
                    ScoreManager.Instance.AddScore(1);
                    UIManager.Instance.txtScore.GetComponent<Text>().text = ScoreManager.Instance.GetScore().ToString();
                    UIManager.Instance.txtScoreBest.GetComponent<Text>().text = ScoreManager.Instance.GetScoreBest().ToString();
                }
            }
            else
            {
                Restart();
            }
        }
        void OnCollisionEnter()
        {
            float timeInAir = Time.time - timeStartFlying;
            if (!rb.isKinematic && timeInAir >= 0.05f)
            {
                Debug.Log("FAIL");
                Restart();
            }
            
        }
    
        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }

}
