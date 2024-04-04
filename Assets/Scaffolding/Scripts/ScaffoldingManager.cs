using System.Collections.Generic;
using UnityEngine;
using Task;
using System;

#nullable enable

/**
 * TODO
 * 
 * Make it possible to teleport to the top of the scaffold
 * Make the ladder impossible to climb before it has been mounted
 * Adjust the height of the scaffold to make it look like the picture and easier to build
 * Fix the task tablet. It currently does not update with tasks properly
 * Try to make the OUTLINED FixedParts without colliders so they feel like they are not present when placing parts
 * Show a message or something when the scaffold is complete
 */
namespace Scaffolding
{
    public class ScaffoldingManager : MonoBehaviour
    {
        #pragma warning disable CS8618
        private TaskManager _taskManager;
        #pragma warning restore CS8618

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Initializing scaffolding task manager");
            _taskManager = new TaskManager(FindObjectOfType<TaskHolder>());
            _taskManager.CreateBuildTask("FootPiece", "MovableFootPiece", "Plasser bunnskruer");
            _taskManager.CreateBuildTask("LongBeamBottom", "MovableLongBeam", "Fest nedre horisontale bjelker", "Fest lengdebjelker");
            _taskManager.CreateBuildTask("CrossBeamBottom", "MovableCrossBeam", "Fest nedre horisontale bjelker", "Fest tverrbjelker");
            _taskManager.CreateBuildTask("StandardBottom", "MovableStandard", "Sett på bunnspirer");
            _taskManager.CreateBuildTask("LongBeamTop", "MovableLongBeam", "Fest øvre horisontale bjelker", "Fest lengdebjelker");
            _taskManager.CreateBuildTask("CrossBeamTop", "MovableCrossBeam", "Fest øvre horisontale bjelker", "Fest tverrbjelker");
            _taskManager.CreateBuildTask("Bracing", "MovableBracing", "Fest diagonalstag");
            _taskManager.CreateBuildTask("SteelDeck", "MovableSteelDeck", "Monter innplanking");
            _taskManager.CreateBuildTask("LadderBeam", "MovableLadderBeam", "Monter stige", "Fest stigebjelke");
            _taskManager.CreateBuildTask("LadderStandard", "MovableLadderStandard", "Monter stige", "Fest stigespire");
            _taskManager.CreateBuildTask("StandardTop", "MovableStandard", "Sett på toppspirer");
            _taskManager.CreateBuildTask("Railing", "MovableRailing", "Monter rekkverk");
            _taskManager.CreateBuildTask("Kickboard", "MovableKickboard", "Monter sparkebrett");
            _taskManager.CreateBuildTask("RailingFront", "MovableRailing", "Monter framre rekkverk");
            Debug.Log($"Successfully created {_taskManager.count} tasks");
            _taskManager.ActivateFirstTask();
            Debug.Log($"Successfully activated {_taskManager.currentTask.name}");
        }

        // Update is called once per frame
        void Update()
        {
            if (_taskManager.currentTask != null)
            {
                _taskManager.currentTask.AttemptToCompleteAndActivateNext(_taskManager.GetNextTask());
            }
        }

        [System.Serializable]
        public class TaskManager
        {
            private Task.Task _mainTask;
            private List<BuildTask> _buildTasks;

            public TaskManager(TaskHolder taskHolder)
            {
                _mainTask = taskHolder.GetTask("Bygg stillas");
                _buildTasks = new List<BuildTask>();
            }

            public int count { get => _buildTasks.Count; }
            public BuildTask currentTask { get => _buildTasks.Find(bt => bt.status == BuildTask.Status.ACTIVE); }

            public BuildTask? GetNextTask()
            {
                int nextIndex = _buildTasks.IndexOf(currentTask) + 1;

                try
                {
                    return _buildTasks[nextIndex];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
            }

            public void CreateBuildTask(string fixedPartTag, string matchingMovablePartsTag, string subtaskName, string? stepName = null)
            {
                // We find all FixedParts/GameObjects tagged with the given tag so they can be manipulated as a group
                List<FixedPart> fixedParts = new List<FixedPart>();
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(fixedPartTag))
                {
                    fixedParts.Add(new FixedPart(gameObject));
                }

                // We find all MovableParts/GameObjects tagged with the given tag so they can be checked for collision as a group
                List<MovablePart> movableParts = new List<MovablePart>();
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(matchingMovablePartsTag))
                {
                    movableParts.Add(new MovablePart(gameObject));
                }

                // We do not care if we are dealing with a task, subtask or step, so we cast to ICompletable
                ICompletable tabletTask = stepName == null ?
                    _mainTask.GetSubtask(subtaskName) :
                    _mainTask.GetSubtask(subtaskName).GetStep(stepName);
                Debug.Log($"Loaded task '{tabletTask}'");

                // Provide the assembled part tag to the objective so the state can be manipulated correctly
                _buildTasks.Add(new BuildTask(fixedParts, movableParts, tabletTask));
            }

            public void ActivateFirstTask()
            {
                try
                {
                    _buildTasks[0].ActivateTask();
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.Log("Failed to activate first task");
                }
            }
        }

        [System.Serializable]
        public class BuildTask
        {
            private List<FixedPart> _fixedParts;
            private List<MovablePart> _matchingMovableParts;
            private ICompletable _tabletTask;
            private Status _status;

            internal BuildTask(List<FixedPart> fixedParts, List<MovablePart> matchingMovableParts, ICompletable tabletTask)
            {
                _fixedParts = fixedParts;
                _matchingMovableParts = matchingMovableParts;
                _tabletTask = tabletTask;
                _status = Status.INACTIVE;
            }

            public string name { get => _tabletTask.ToString(); }
            public Status status { get => _status; }
            public List<MovablePart> unusedMatchingMovableParts { get => _matchingMovableParts.FindAll(mPart => mPart.gameObject != null); }

            public void AttemptToCompleteAndActivateNext(BuildTask? nextTask)
            {
                if (status == Status.COMPLETED)
                    return;

                foreach (MovablePart mPart in unusedMatchingMovableParts)
                {
                    foreach (FixedPart fPart in _fixedParts)
                    {
                        if (Physics.CheckBox(
                            mPart.gameObject.transform.position,
                            mPart.gameObject.transform.localScale / 2f,
                            mPart.gameObject.transform.rotation,
                            LayerMask.GetMask(fPart.gameObject.tag)
                        ))
                        {
                            Destroy(mPart.gameObject);

                            CompleteTask();
                            Debug.Log($"Completed current task: '{this}'");

                            if (nextTask != null)
                            {
                                nextTask.ActivateTask();
                                Debug.Log($"Activated next task: '{nextTask}'");
                            }
                            else
                            {
                                Debug.Log("Congratulations! Scaffolding is complete.");
                            }

                            return;
                        }
                    }
                }
            }

            internal void ActivateTask()
            {
                _fixedParts.ForEach(part => part.SetState(FixedPart.State.OUTLINED));
                _status = Status.ACTIVE;
            }

            private void CompleteTask()
            {
                _fixedParts.ForEach(part => part.SetState(FixedPart.State.VISIBLE));
                _tabletTask.SetCompleated(true);
                _status = Status.COMPLETED;
            }

            public override string ToString()
            {
                return _tabletTask.ToString();
            }

            public enum Status
            {
                INACTIVE, ACTIVE, COMPLETED
            }
        }

        [System.Serializable]
        public class MovablePart
        {
            private GameObject _gameObject;

            public MovablePart(GameObject gameObject)
            {
                _gameObject = gameObject;
            }

            public GameObject gameObject { get => _gameObject; }
        }

        [System.Serializable]
        public class FixedPart
        {
            private GameObject _gameObject;
            private BlinkingEffect? _blinkingEffect;
            private State _state;
            private GameObject? _ladder;

            public FixedPart(GameObject gameObject)
            {
                _gameObject = gameObject;
                SetState(State.INVISIBLE);

                if (gameObject.tag == "LadderStandard")
                {
                    _ladder = GameObject.FindGameObjectWithTag("Ladder");
                    _ladder.SetActive(false);
                }
            }

            public GameObject gameObject { get => _gameObject; }
            public State state { get => _state; }

            public void SetState(State state)
            {
                if (_gameObject == null)
                    return;

                Debug.Log($"Setting {_gameObject.name} to {state}");

                switch (state)
                {
                    case State.INVISIBLE:
                        _gameObject.SetActive(false);
                        break;
                    case State.OUTLINED:
                        var collider = _gameObject.GetComponent<Collider>();
                        if (collider is MeshCollider)
                        {
                            ((MeshCollider)collider).convex = true;
                        }
                        _gameObject.GetComponent<Collider>().isTrigger = true;
                        _gameObject.SetActive(true);
                        _blinkingEffect = _gameObject.AddComponent<BlinkingEffect>();
                        break;
                    case State.VISIBLE:
                        _gameObject.SetActive(true);
                        if (_ladder != null)
                        {
                            _ladder.SetActive(true);
                        }
                        if (_blinkingEffect != null)
                        {
                            _blinkingEffect.enabled = false;
                        }
                        _gameObject.GetComponent<Collider>().isTrigger = false;
                        break;
                }

                _state = state;
            }

            public enum State
            {
                INVISIBLE, OUTLINED, VISIBLE
            }
        }
    }
}
