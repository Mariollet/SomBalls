using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{
    Renderer m_Renderer;
    public List<Texture> m_MainTexture;
    public List<Material> m_TrailTexture;
    public ParticleSystem playerParticleSystem;
    public List<Color> trailColor;
    private int playerColor;
    private int playerTrail;
    private AudioManager audioManager;
    private ShopController shopController;
    


    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        shopController = FindObjectOfType<ShopController>();
        m_Renderer = GetComponent<Renderer>();

        if (!PlayerPrefs.HasKey("PlayerColor"))
        {
            playerColor = 0;
            m_Renderer.material.SetTexture("_MainTex", m_MainTexture[playerColor]);
        }
        else
        {
            playerColor = PlayerPrefs.GetInt("PlayerColor");
            m_Renderer.material.SetTexture("_MainTex", m_MainTexture[playerColor]);
        }

        if (!PlayerPrefs.HasKey("PlayerTrail"))
        {
            playerTrail = 0;
            playerParticleSystem.GetComponent<ParticleSystemRenderer>().material = m_TrailTexture[playerTrail];

            ParticleSystem.MainModule ma = playerParticleSystem.main;
            ma.startColor = trailColor[playerTrail];

            var col = playerParticleSystem.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(trailColor[playerTrail], 0.0f), new GradientColorKey(Color.white, 0.5f) },
                            new GradientAlphaKey[] { new GradientAlphaKey(0.9f, 0.0f), new GradientAlphaKey(0.6f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });
            col.color = grad;
        }
        else
        {
            playerTrail = PlayerPrefs.GetInt("PlayerTrail");
            playerParticleSystem.GetComponent<ParticleSystemRenderer>().material = m_TrailTexture[playerTrail];

            ParticleSystem.MainModule ma = playerParticleSystem.main;
            ma.startColor = trailColor[playerTrail];

            var col = playerParticleSystem.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(trailColor[playerTrail], 0.0f), new GradientColorKey(Color.white, 0.5f) },
                            new GradientAlphaKey[] { new GradientAlphaKey(0.9f, 0.0f), new GradientAlphaKey(0.6f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });
            col.color = grad;
        }
    }

    public void Initialize()
    {
        shopController.Initialize();
        if (!PlayerPrefs.HasKey("PlayerColor"))
        {
            m_Renderer.material.SetTexture("_MainTex", m_MainTexture[0]);
            playerColor = 0;
        }
        else
        {
            playerColor = PlayerPrefs.GetInt("PlayerColor");
            m_Renderer.material.SetTexture("_MainTex", m_MainTexture[playerColor]);
        }

        if (!PlayerPrefs.HasKey("PlayerTrail"))
        {
            playerTrail = 0;
            playerParticleSystem.GetComponent<ParticleSystemRenderer>().material = m_TrailTexture[playerTrail];

            ParticleSystem.MainModule ma = playerParticleSystem.main;
            ma.startColor = trailColor[playerTrail];

            var col = playerParticleSystem.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(trailColor[playerTrail], 0.0f), new GradientColorKey(Color.white, 0.5f) },
                            new GradientAlphaKey[] { new GradientAlphaKey(0.9f, 0.0f), new GradientAlphaKey(0.6f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });
            col.color = grad;
        }
        else
        {
            playerTrail = PlayerPrefs.GetInt("PlayerTrail");
            playerParticleSystem.GetComponent<ParticleSystemRenderer>().material = m_TrailTexture[playerTrail];

            ParticleSystem.MainModule ma = playerParticleSystem.main;
            ma.startColor = trailColor[playerTrail];

            var col = playerParticleSystem.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(trailColor[playerTrail], 0.0f), new GradientColorKey(Color.white, 0.5f) },
                            new GradientAlphaKey[] { new GradientAlphaKey(0.9f, 0.0f), new GradientAlphaKey(0.6f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });
            col.color = grad;
        }
    }

    public void SetColor(int color)
    {
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture[color]);
        PlayerPrefs.SetInt("PlayerColor", color);
        playerColor = color;
        shopController.Initialize();
    }
    public void SetTrail(int trail)
    {
        playerParticleSystem.GetComponent<ParticleSystemRenderer>().material = m_TrailTexture[trail];
        PlayerPrefs.SetInt("PlayerTrail", trail);
        playerTrail = trail;
        shopController.Initialize();

        ParticleSystem.MainModule ma = playerParticleSystem.main;
        ma.startColor = trailColor[playerTrail];

        var col = playerParticleSystem.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(trailColor[playerTrail], 0.0f), new GradientColorKey(Color.white, 0.5f) },
                        new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(0.7f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });
        col.color = grad;
    }
}