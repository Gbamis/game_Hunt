using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace HT
{
    public class AnimalController : MonoBehaviour, ICommandable
    {
        private int idle_shuffle;
        private AnimalStat animalStat;
        private AnimalBehaviour animalBehaviour;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        public Gene gene { set; get; }
        public bool isCommand;
        public GameEvent gameEvent;
        public GameObject selectedIcon;
        public LayerMask collisionMask;
        public float debugDis;

        private Vector3 rayOrigin = Vector3.zero;
        public float radius = 0;


        public void Init(AnimalStat stat, Gene m_gene)
        {
            animator = GetComponent<Animator>();
            animalBehaviour = GetComponent<AnimalBehaviour>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            Random.InitState(System.DateTime.Now.Millisecond);
            navMeshAgent.speed = Random.Range(1, 1.3f);
            navMeshAgent.angularSpeed = Random.Range(600, 800);
            navMeshAgent.acceleration = Random.Range(4, 7);
            animalStat = stat;
            idle_shuffle = 0;
            gene = m_gene;
            StopAllCoroutines();

            animalBehaviour.DefaultAction().Execute(this);
        }

        public void Die() { StopAllCoroutines(); animator.SetTrigger("die"); }

        public void OnPointerClick(PointerEventData ped) => OnSelect();

        public void OnSelect()
        {
            bool isSelected = selectedIcon.activeSelf;
            if (gameEvent.OnGet_CommandSystem().iscOmmandMode == true) { return; }

            if (isSelected)
            {
                gameEvent.OnGet_CommandSystem().RemoveSelected(this);
                selectedIcon.SetActive(false);
            }
            else
            {
                gameEvent.OnGet_CommandSystem().AddToSelection(this, transform.position);
                selectedIcon.SetActive(true);
            }
        }
        public void OnDeSelect() => selectedIcon.SetActive(false);

        public void OnExecuteCommandable(AnimalAction command)
        {
            if (animalStat.GetStamina() > 0.3f && !isCommand)
            {
                StopAllCoroutines();
                command.Execute(this);
            }
        }

        private void OnFinhsedExecutingAction()
        {
            AnimalAction animalAction = animalBehaviour.DecideBestAction();
            if (animalAction != null && !isCommand) { animalAction.Execute(this); }
        }

        private void MotionValue(float value) => animator.SetFloat("velocity", value);
        private void EatValue(bool value) => animator.SetBool("eating", value);
        private void RestValue(bool value) => animator.SetBool("rest", value);

        public void Execute_Idle_Action(float duration)
        {
            StopAllCoroutines();

            MotionValue(0); EatValue(false); RestValue(false);

            StartCoroutine(Run());

            IEnumerator Run()
            {
                float total = duration;
                while (total > 0 && !isCommand)
                {
                    total -= Time.deltaTime;
                    animalStat.ReduceStamina(gene.tirednessRate);
                    yield return null;
                }

                OnFinhsedExecutingAction();
            }
        }

        public void Execute_Rest_Action(float duration)
        {
            MotionValue(0); EatValue(false); RestValue(true);

            StartCoroutine(Run());
            IEnumerator Run()
            {
                float total = duration;
                while (total > 0)
                {
                    total -= Time.deltaTime;
                    yield return null;
                }
                OnFinhsedExecutingAction();
            }
        }

        public void Execute_Feed_Action(float duration)
        {
            bool foundBlocker = false;
            if (animalStat.place_of_action != null)
            {
                //foundBlocker = CheckBlocker(animalStat.place_of_action.position);

                RestValue(false);

                StartCoroutine(GotoFood(animalStat.place_of_action.position));
                IEnumerator GotoFood(Vector3 pos)
                {
                    MotionValue(1);
                    Vector3 dir = pos - transform.position;
                    while (Vector3.Distance(transform.position, pos) > 0.8f && !isCommand && !foundBlocker)
                    {
                        SetPositionRotation(pos, dir, 0);

                        debugDis = Vector3.Distance(transform.position, pos);
                        if (debugDis < 1.28f) { MotionValue(0); }
                        //animalStat.ReduceStamina(0.04f);
                        yield return null;
                    }
                    MotionValue(0);
                    StartCoroutine(Eat());
                }

                IEnumerator Eat()
                {
                    if (animalStat.place_of_action != null)
                    {
                        IFood food = animalStat.place_of_action.GetComponent<IFood>();

                        EatValue(true);
                        float total = duration;
                        while (total > 0 && animalStat.GetStamina() != 1
                        && animalStat.place_of_action != null && !isCommand && !foundBlocker)
                        {
                            animalStat.AddStamina(food.Consume(0.01f));
                            total -= Time.deltaTime;
                            yield return null;
                        }
                    }

                    EatValue(false);
                    OnFinhsedExecutingAction();
                }
            }


        }

        public void Execute__Roam_Action()
        {
            Vector3 endPos = GetRandomPositionFromSource(transform.position);

            bool foundBlocker = CheckBlocker(endPos);

            MotionValue(1); EatValue(false); RestValue(false);

            StartCoroutine(Move(endPos));

            IEnumerator Move(Vector3 pos)
            {

                Vector3 dir = pos - transform.position;
                while (Vector3.Distance(transform.position, pos) > 0.02f && !foundBlocker)
                {
                    SetPositionRotation(pos, dir, 0.002f);
                    animalStat.ReduceStamina(gene.tirednessRate);

                    yield return null;
                }
                MotionValue(0); EatValue(true);
                yield return new WaitForSeconds(1);
                EatValue(false);

                OnFinhsedExecutingAction();
            }
        }

        public void Execute_Flee_Action(float duration)
        {
            StartCoroutine(Move());

            IEnumerator Move()
            {
                navMeshAgent.speed += 2;
                navMeshAgent.enabled = true;
                MotionValue(2);

                Vector3 pos = animalStat.place_of_action.position;
                Vector3 dir = transform.position - (pos - transform.position);
                float step = 0;
                while (step < duration)
                {
                    navMeshAgent.destination = dir;
                    animalStat.ReduceStamina(gene.tirednessRate * 20);
                    step += Time.deltaTime;
                    yield return null;
                }
                MotionValue(0);
                navMeshAgent.speed -= 2;
                navMeshAgent.enabled = false;
                OnFinhsedExecutingAction();
            }
        }

        public void Execute__Move_Command(Transform location)
        {
            isCommand = true;

            bool should_run = Vector3.Distance(transform.position, location.position) > 5;

            if (should_run)
            {
                navMeshAgent.speed += 2;
                MotionValue(2);
            }
            else { MotionValue(1); }

            navMeshAgent.enabled = true;

            EatValue(false); RestValue(false);

            StartCoroutine(Move(location.position));

            IEnumerator Move(Vector3 pos)
            {
                Vector3 dir = pos - transform.position;
                while (Vector3.Distance(transform.position, pos) > 0.9f)
                {
                    navMeshAgent.destination = pos;
                    animalStat.ReduceStamina(gene.tirednessRate * 20);
                    yield return null;
                }
                MotionValue(0);
                if (should_run) { navMeshAgent.speed -= 2; }
                navMeshAgent.enabled = false;

                isCommand = false;
                gameEvent.OnGet_CommandSystem().iscOmmandMode = false;
                navMeshAgent.enabled = false;
                OnFinhsedExecutingAction();
            }

        }

        public void Execute__Mate_Command(Transform location)
        {
            isCommand = true;
            MotionValue(1);
            EatValue(false); RestValue(false);

            StartCoroutine(Move(location.position));

            IEnumerator Move(Vector3 pos)
            {
                Vector3 dir = pos - transform.position;
                while (Vector3.Distance(transform.position, pos) > 1f)
                {
                    SetPositionRotation(pos, dir, 0.002f);
                    animalStat.ReduceStamina(0.06f);
                    yield return null;
                }
                MotionValue(0);

                isCommand = false;
                gameEvent.OnGet_CommandSystem().iscOmmandMode = false;
                OnFinhsedExecutingAction();
            }
        }

        public void Execute_FollowPlayer_Command()
        {
            float defSpeed = navMeshAgent.speed;

            navMeshAgent.enabled = true;
            isCommand = true;
            MotionValue(1); EatValue(false); RestValue(false);
            Transform player = gameEvent.OnGet_CommandSystem().player;

            StartCoroutine(Move());

            IEnumerator Move()
            {
                Vector3 dir = player.position - transform.position;
                Vector3 pos = player.position;
                Vector2 rand = Random.insideUnitCircle;
                pos.x += rand.x;
                pos.z += rand.y;
                while (Vector3.Distance(transform.position, player.position) > 3f)
                {
                    if (Vector3.Distance(transform.position, player.position) > 5)
                    {
                        navMeshAgent.speed = defSpeed + 2;
                        MotionValue(2);
                    }
                    else
                    {
                        navMeshAgent.speed = defSpeed;
                        MotionValue(1);
                    }
                    navMeshAgent.destination = player.position;
                    yield return null;
                }
                MotionValue(0);

                isCommand = false;
                gameEvent.OnGet_CommandSystem().iscOmmandMode = false;
                navMeshAgent.enabled = false;
                OnFinhsedExecutingAction();
            }
        }

        public void Execute_Reproduce_Action(IGoat male, IGoat female, float reproductionTime)
        {
            isCommand = true;
            RestValue(true);

            StartCoroutine(Reproduce());

            IEnumerator Reproduce()
            {
                Debug.Log("started reproduction");
                animalStat.ResetHeatLevel();
                float time = 0;
                while (time < reproductionTime * 1000)
                {
                    time += Time.deltaTime;
                    yield return null;
                }

                RestValue(false);

                gameEvent.OnGet_BreedingSystem().BreedGoat(male, female, transform);
                animalStat.GetNextProductionCycle(gene.reproductionRate);

                Debug.Log("completed reproduction at: " + time);
                isCommand = false;
                OnFinhsedExecutingAction();
            }
        }

        private Vector3 GetRandomPositionFromSource(Vector3 source)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Vector2 pos = Random.insideUnitCircle * 2;
            Vector3 endPos = new Vector3(pos.x + source.x, source.y, source.z + pos.y);
            return endPos;
        }

        private void SetPositionRotation(Vector3 pos, Vector3 dir, float add = 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, (Time.deltaTime * 1f) + add);
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * 3f);
        }

        private bool CheckBlocker(Vector3 endPos)
        {
            rayOrigin = endPos;
            Collider[] colliders = new Collider[7];
            Physics.OverlapSphereNonAlloc(rayOrigin, radius, colliders, collisionMask);
            foreach (Collider col in colliders)
            {
                if (col != null && col.transform != transform)
                {
                    return true;
                }
            }


            Vector3 origin = transform.position;
            Vector3 dir = endPos - transform.position;
            origin.y += 0.3f;

            Color green = Color.green;
            green.a = 0.3f;
            Routine.Instance.AddInfo(new DebugCheck()
            {
                radius = radius,
                debugColor = green,
                center = rayOrigin
            });

            Debug.DrawRay(origin, dir, Color.yellow, 2);

            Ray ray = new Ray(origin, dir);
            if (Physics.Raycast(ray, out RaycastHit info, 1, collisionMask))
            {
                return info.collider != null && info.collider.transform != transform;
            }


            return false;
        }

    }
}
