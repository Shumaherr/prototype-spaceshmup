using UnityEngine;

public class ParallaxScript : MonoBehaviour {
    private float startPos, length;
    public GameObject camera;
    public float paralaxEffect;

    void Start() {
        startPos = transform.position.y;
        length   = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        float temp = camera.transform.position.y * (1 - paralaxEffect);
        float dist = camera.transform.position.y * paralaxEffect;

        // двигаем фон с поправкой на paralaxEffect
        transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

        // если камера перескочила спрайт, то меняем startPos
        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}