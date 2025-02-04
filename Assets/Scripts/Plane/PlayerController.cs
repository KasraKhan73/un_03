using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;
using Lunha;
using Prototype.AudioCore;
using SBabchuk;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    #region Actions
    public static event Action<int> _OnTrashCountChanged;
    
    public static event Action<Transform> OnPlayerisStoped;
    
    public static event Action<TMultiplier> OnFinishFlaying;
    
    public static event Action<float> OnClownshot;
    
    public static event Action OnFlying;
    
    public static event Action OnDropedPlane;
    
    public static event Action<float, int> OnShowResult;
    
    public static event Action<float> OnShowFail;
    
    public static event Action<int> OnChangeSlider;
    
    public static event Action OnCreateParticles;
    #endregion

    #region Vars
    [Header("Параметри літака")]
    public PlaneSettings settings;
    
    [Header("Кількість зібраних предметів")]
    public static int trashCount = 0;
    
    [Header("Елементи планера префаби")]
    public List<GameObject> elementsPrefabs;
    
    [Header("Елементи планера (вже на ньому)")]
    public List<GameObject
    > parts;
    
    [Header("Рама (вже на ньому)")]
    public GameObject frame;

    [Header("Позиція куди будуть добавлятись частинки")]
    public Transform lowPolyPlane;
    
    [Header("Позиція куди будуть добавлятись частинки")]
    public Transform lowPolyPlaneNew;
    
    [Header("Ідентифікатор літака")]
    public PlaneColors planeID;

    public Rigidbody Hull;

    public static float kof;

    public Collider collider;
    
    public Collider colliderTrigger;

    public BazierCurveCubic _moveCurve;
    
    [Header("Елементи планера")]
    private List<DropElements> elements = new List<DropElements>();
    
    private bool isMove = false;
    
    private bool stoped = false;
    
    private Rigidbody _rigidbody;
    
    private bool isFly;

    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    private PlayerMovement _movement;

    private bool isDroped;

    private Vector3 startFlyingPosition;

    public static int distanceOfFly;

    public static bool isFailed = true;

    public static bool canFly;
    #endregion

    #region Subscribe
    private void OnEnable()
    {
        Trash._OnTrashCollected += OnTrashCollected;

        RoadPoint._OnCheckRoad += CheckRoad;

        MaterialController.OnDropedPlane += CheckDrop;

        TrashElemet.OnTrashCollected += TrashCollected;
    }

    private void OnDisable()
    {
        Trash._OnTrashCollected -= OnTrashCollected;
        
        RoadPoint._OnCheckRoad -= CheckRoad;
        
        MaterialController.OnDropedPlane -= CheckDrop;
        
        TrashElemet.OnTrashCollected -= TrashCollected;
    }
    #endregion

    public List<int> ids = new List<int>();
    private void TrashCollected(int newID)
    {
        if (ids.Any(id => id == newID))
        {
            return;
        }
        
        ids.Add(newID);

        if (elements.Count <= 0) return;
        if(elements[elements.Count - 1].name == PLaneParts.Engine && AudioController.IsSoundPlaying("TakeEngine"))
            AudioController.StopSound("TakeEngine");
                
        elements[elements.Count - 1].PlayDropAnimation();
                
        elements.Remove(elements[elements.Count - 1]);
                
        OnChangeSlider?.Invoke(elements.Count);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        if(planeID == PlaneColors.None)
            planeID = (PlaneColors)(PlayerPrefs.GetInt("planeID", 0) + 1);

        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        isFailed = true;
        
        MovementController.Instance.speed = MovementController.Instance.defaultSpeed;
    }

    private void LateUpdate()
    {
        if (isDroped)
            return;
        
        if (isFly)
            //distanceOfFly = (int)Vector3.Distance(startFlyingPosition, gameObject.transform.position);
            distanceOfFly = (int) (gameObject.transform.position.z - startFlyingPosition.z);
        else
            distanceOfFly = 0;
    }

    private float CheckPoints()
    {
        return elements.Count == 0 ? 0 : elements.Sum(element => element.color == planeID ? 1 : 1f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Finish"))
        {
            PlayerMovement.speed = 28;

            MovementController.Instance.speed = 28;

            _rigidbody.constraints = RigidbodyConstraints.None;

            kof = CheckPoints() / settings.detailsCount;

            DOVirtual.Float(RenderSettings.fogDensity, 0.0015f, 2f, f =>
            {
                RenderSettings.fogDensity = f;
            });
        }
        else if (collider.CompareTag("Trampline"))
        {
            if(isFly)
                return;
            
            AudioController.Release(false);
            
            AudioController.PlaySound("start_fly");

            canFly = CheckCanFly();
            
            OnClownshot?.Invoke(1);

            isFly = true;
            
            OnFlying?.Invoke();
            
            startFlyingPosition = gameObject.transform.position;
            
            colliderTrigger.isTrigger = true;
        }  
        else if (collider.CompareTag("Earth"))
        {
            collider.enabled = false;
            
            colliderTrigger.enabled = false;
            
            isFailed = true;

            Hull.GetComponent<Collider>().enabled = true;
            
            CheckDrop();
        }
    }

    public void EndFlying()
    {
        isFailed = false;
        
        collider.enabled = false;

        Hull.GetComponent<Collider>().enabled = true;
        
        CheckDrop();
        
        OnCreateParticles?.Invoke();
    } 
    
    private void CheckRecord()
    {
        var best = PlayerPrefs.GetInt("BestDistance", 0);

        Debug.Log(best + " ? " + distanceOfFly);
        
        if (best < distanceOfFly)
        {
            PlayerPrefs.SetInt("IsRecord", 1);
            PlayerPrefs.SetInt("BestDistance", distanceOfFly);
        }
        else
        {
            PlayerPrefs.SetInt("IsRecord", 0);
        }
      
        PlayerPrefs.Save();
    }

    private void CheckDrop()
    {
        if(isDroped)
            return;

        GetComponent<MovementController>().state = States.None;

        CheckRecord();

        Debug.Log("canFly: " + canFly);
        
        if (canFly)
            OnShowResult?.Invoke(3.5f, distanceOfFly);
        else
            OnShowFail?.Invoke(3.5f);
        
        isDroped = true;
        
        //GetComponent<CustomGravity>().enabled = false;
            
        _rigidbody.velocity = Vector3.zero;
            
        Drop();

        if (PlayerPrefs.GetInt("IsRecord", 1) == 1)
        {
            PlayerPrefs.SetInt("IsRaycast", (PlayerPrefs.GetInt("Level", 1) == 2? 1 : 0));
            PlayerPrefs.SetFloat("_X", transform.position.x);
            PlayerPrefs.SetFloat("_Y", transform.position.y);
            PlayerPrefs.SetFloat("_Z", transform.position.z);
            PlayerPrefs.Save();
        }
    }

    private void CheckRoad(TRoad road)
    {
        if(stoped) return;
        
        stoped = road == TRoad.Final;

        if (stoped) CreatePlane();
    }

    public bool CheckCanFly()
    {
        return elements.Count >= 5;

        //return elements.Any(element => element.name == PLaneParts.Wings);
    }
    
    private void CreatePlane()
    {
        _rigidbody.velocity = Vector3.zero;
     
        _rigidbodies.Clear();
        
        transform.DOMoveX(0, 0.3f).OnComplete(() =>
        {
            OnPlayerisStoped?.Invoke(this.gameObject.transform);
        });
    }

    private void OnTrashCollected(PLaneParts value, PlaneColors color)
    {
        if(CheckPart(value, color))
            return;

        AudioController.PlaySound("get_details", StreamGroup.FX, 0.45f);

        if (value != PLaneParts.OneDesign)
        {
            var go = Instantiate(elementsPrefabs[(int) value - 1], lowPolyPlane).GetComponent<DropElements>();
            elements.Add(go);
            go.Init(color);
            //go.PlayAnimation(_moveCurve);
            
            trashCount = elements.Count;
        }
        else
        {
            if(elements.Count >= parts.Count)
                return;
            
            var go = Instantiate(parts[elements.Count], lowPolyPlaneNew).GetComponent<DropElements>();
            elements.Add(go);
            go.Init(color);
            //go.PlayAnimation(_moveCurve);
            
            frame.SetActive(elements.Count == 0);
            
            trashCount = elements.Count;
        }
       

        OnChangeSlider?.Invoke(elements.Count);
    }
    
    private bool CheckPart(PLaneParts value, PlaneColors color)
    {
        if (value == PLaneParts.None)
        {
            if (elements.Count > 0)
            {
                if(elements[0].name == PLaneParts.Engine && AudioController.IsSoundPlaying("TakeEngine"))
                    AudioController.StopSound("TakeEngine");
                
                elements[elements.Count - 1].PlayDropAnimation();
                
                elements.Remove(elements[elements.Count - 1]);
                
                OnChangeSlider?.Invoke(elements.Count);
            }

            return true;
        }
        else
        {
            if (value == PLaneParts.OneDesign)
                return false;
            
            if(value == PLaneParts.Engine)
                AudioController.PlaySound("TakeEngine", StreamGroup.FX, 0.3f,true);
            
            if (elements.Count == 0)
                return false;
            
            foreach (var el in elements.Where(el => el.name == value))
            {
                
                if (el.color != color)
                {
                    el.PlayDropAnimation();

                    elements.Remove(el);

                    return false;
                }
                else
                {
                    return true;
                }
            }
            
            return false;
        }
    }

    private void Drop()
    {
        AudioController.Release();
        
        OnDropedPlane?.Invoke();
        
        Hull.isKinematic = false;
         
        Hull.useGravity = true;

        Hull.GetComponent<Collider>().enabled = true;
        
        foreach (var element in elements)
        {
            element.DropWithoutAnimation();
        }
        
        elements.Clear();
    }
}