/* Developer: Jorge Garcia
 * Ask your questions on github: https://github.com/Jorest
 */

using System.Collections.Generic;
using UnityEngine;

namespace Task
{

    /// <summary>
    /// This script is meant to hold the relevant task data (task and skills) 
    /// this works an interface for the data to be modified on runtime
    /// </summary>
    public class TaskHolder : MonoBehaviour
    {
        public static TaskHolder Instance;
        private List<TaskxTarget> _taskAndTargerts = new List<TaskxTarget>();

        [Header("Profession Tasks")]
        [SerializeField] public List<Skill> skillList = new List<Skill>();

        [SerializeField] public List<Task> taskList = new List<Task>();

        //making the task holder a singleton
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public Task GetTask(string taskName)
        {
            // This method could be simplified to: return taskList.FindLast(task => task.TaskName == taskName);
            Task returnTask = null;

            foreach (Task task in taskList)
            {
                if (task.TaskName == taskName)
                {
                    returnTask = task;
                    break;
                }
            }

            return returnTask;
        }

        public Skill GetSkill(string skillName)
        {
            // This method could be simplified to: return skillList.FindLast(task => task.Name == skillname);
            Skill returnSkill = null;

            foreach (Skill task in skillList)
            {
                if (task.Name == skillName)
                {
                    returnSkill = task;
                    break;
                }
            }

            return returnSkill;
        }

    }
    //this sub-class can be usefull in the future to set targets 
    [System.Serializable]
    public class TaskxTarget
    {
        public Subtask subtask;
        public GameObject target;
    }

    public interface ICompletable
    {
        bool Compleated();
        void SetCompleated(bool isCompleated);
    }
}