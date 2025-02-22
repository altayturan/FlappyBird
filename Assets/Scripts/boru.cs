using UnityEngine;

public class Boru : MonoBehaviour
{
    [Header("Boru Ayarları")]
    public float minY = -2.5f; // En düşük boru yüksekliği
    public float maxY = -1.3f; // En yüksek boru yüksekliği
    public float hareketHizi = 2f; // Borunun hareket hızı
    public float yokOlmaX = -1f; // Borunun silineceği X konumu

    void Start()
    {
        float rastgeleYukseklik = Random.Range(minY, maxY);
        transform.position = new Vector3(transform.position.x, rastgeleYukseklik, transform.position.z);
    }

    void FixedUpdate()
    {
        // Boruyu sola hareket ettir
        transform.Translate(Vector3.left * hareketHizi * Time.fixedDeltaTime);

        // Eğer boru ekran dışına çıkarsa yok et
        if (transform.position.x <= yokOlmaX)
        {
            DestroyBoru();
        }
    }

    void DestroyBoru()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
