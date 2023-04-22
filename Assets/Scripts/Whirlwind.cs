using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Whirlwind : BaseSkill
{
    private const string ENEMY_LAYER = "Enemy";

    public float duration = 1.0f;
    public float movementSpeed = 1.0f;
    public VisualEffect whirlwindEffect;

    public bool isBurning = false;
    [ColorUsage(true, true)]
    public Color burnedCoreColor;
    [ColorUsage(true, true)]
    public Color burnedLayer1Color;
    [ColorUsage(true, true)]
    public Color burnedLayer2Color;
    [ColorUsage(true, true)]
    public Color burnedParticlesColor;

    private Burnable burnable;
    private float timer;
    private Vector3 direction;

    // Start is called before the first frame update
    void Awake()
    {
        burnable = GetComponent<Burnable>();
        whirlwindEffect.SetFloat("Duration", duration);

        if(burnable)
        {
            burnable.OnBurnEnter += OnBurnEnter;
            //burnable.OnBurnExit += OnBurnExit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            Destroy(gameObject);
        }

        transform.position += movementSpeed * direction * Time.deltaTime;
    }

    private void OnBurnEnter()
    {
        isBurning = true;
        LeanTween.value(0, 1, 0.3f).setEaseInQuad().setOnUpdate((float value) => {
            if(whirlwindEffect)
            {
                whirlwindEffect.SetVector4("WhirlwindCoreColor", Vector4.Lerp(whirlwindEffect.GetVector4("WhirlwindCoreColor"), burnedCoreColor, value));
                whirlwindEffect.SetVector4("Layer1Color", Vector4.Lerp(whirlwindEffect.GetVector4("Layer1Color"), burnedLayer1Color, value));
                whirlwindEffect.SetVector4("Layer2Color", Vector4.Lerp(whirlwindEffect.GetVector4("Layer2Color"), burnedLayer2Color, value));
                whirlwindEffect.SetVector4("OuterParticleColor", Vector4.Lerp(whirlwindEffect.GetVector4("OuterParticleColor"), burnedParticlesColor, value));
            }
        });
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER)) {
            //Enemy -> Collider -> Body (holds collider component)
            Enemy enemy = other.transform.parent.parent.gameObject.GetComponent<Enemy>();
            if (enemy) {
                enemy.ProjectileHit(gameObject);
            }
        }
    }

    public override void Activate() {
        //Spawn slightly forward
        direction = CharacterManager.Instance.gameObject.transform.forward * 1f;
        Vector3 move = direction * 1.5f;
        transform.Translate(move);
    }

    public override void Deactivate() {
    }

    private void OnBurnExit()
    {

    }
}
