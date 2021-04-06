using Controllers;
using Data;
using Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        private static GridModel gridModel;

        public Button Reload;
        public Button Plus;
        
        public enum Stages
        {
            None,
            Odinakovie,
            Desyat,
            FirstNLast,
            Repeat,
            FirstNlastRow,
            End
        }

        public Stages stage;

        private bool once = true;

        public static TutorialController Instance = null;
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void Start()
        {
            stage = PlayerPrefsController.Tutorial == 0 ? Stages.Odinakovie : Stages.None;
        }

        public static GridModel CreateTutorGridModel()
        {
            gridModel = new GridModel(0, 0);
            gridModel.AddCell(2);
            gridModel.AddCell(2);
            gridModel.AddCell(1);
            gridModel.AddCell(9);
            gridModel.AddCell(4);
            gridModel.AddCell(5);
            gridModel.AddCell(5);
            gridModel.AddCell(1);
            gridModel.AddCell(1);
            gridModel.AddCell(1);
            gridModel.AddCell(3);
            gridModel.AddCell(4);
            gridModel.AddCell(6);
            gridModel.AddCell(7);
            gridModel.AddCell(8);
            gridModel.AddCell(2);
            gridModel.AddCell(2);
            gridModel.AddCell(2);
            gridModel.AddCell(1);
            gridModel.AddCell(5);
            gridModel.AddCell(5);
            gridModel.AddCell(9);
            gridModel.AddCell(1);
            gridModel.AddCell(1);
            gridModel.AddCell(1);
            gridModel.AddCell(9);
            gridModel.AddCell(4);

            return gridModel;
        }

        private void Update()
        {
            if (stage == Stages.None)
            {

            }
            else if (stage == Stages.Odinakovie)
            {
                Odinakovie();
            }            
            else if(stage == Stages.Desyat)
            {
                Desyat();
            }
            else if(stage == Stages.FirstNLast)
            {
                FirstNLast();
            }
            else if(stage == Stages.Repeat)
            {
                Repeat();
            }
            else if(stage == Stages.FirstNlastRow)
            {
                FirstNLastRow();
            }
            else if(stage == Stages.End)
            {
                End();
            }
        }

        private void End()
        {
            Reload.interactable = true;
            Plus.interactable = true;
            PlayerPrefsController.Tutorial = 1;
            Alerts.AlertCall.CallWithText(null, null, Res.lang.Tutorial[5], Res.lang.Confirmation[0]);

            
            foreach (var model in gridModel.Grid)
            {
                if (model.Value != -1)
                {
                    model.GetView().Enable();
                }
            }

            GAController.TutorialStageChange(99);
            stage = Stages.None;
        }

        public void Desyat()
        {
            if (once)
            {
                for (int i = 0; i < gridModel.Grid.Count; i++)
                {
                    gridModel.Grid[i].GetView().Disable();
                }
                gridModel.Grid[2].GetView().Enable();
                gridModel.Grid[3].GetView().Enable();
            
                Alerts.AlertCall.CallWithText(null, null, Res.lang.Tutorial[1], Res.lang.Confirmation[1]);
                
                once = false;
                
            }
            
            if (gridModel.Grid[2].Value == -1 &&
                gridModel.Grid[3].Value == -1)
            {
                GAController.TutorialStageChange(2);
                once = true;
                stage = Stages.FirstNLast;
            }
        }
        
        public void Odinakovie()
        {
            if (once)
            {
                Reload.interactable = false;
                Plus.interactable = false;
                
                for (int i = 0; i < gridModel.Grid.Count; i++)
                {
                    gridModel.Grid[i].GetView().Disable();
                }
                gridModel.Grid[0].GetView().Enable();
                gridModel.Grid[1].GetView().Enable();
            
                Alerts.AlertCall.CallWithText(null, null, Res.lang.Tutorial[0], Res.lang.Confirmation[2]);
                
                once = false;
                GAController.TutorialStageChange(0);
            }
            
            if (gridModel.Grid[0].Value == -1 &&
                gridModel.Grid[1].Value == -1)
            {
                GAController.TutorialStageChange(1);
                once = true;
                stage = Stages.Desyat;
            }
        }

        public void FirstNLast()
        {
            if (once)
            {
                for (int i = 0; i < gridModel.Grid.Count; i++)
                {
                    gridModel.Grid[i].GetView().Disable();
                }
                gridModel.Grid[4].GetView().Enable();
                gridModel.Grid[26].GetView().Enable();
            
                Alerts.AlertCall.CallWithText(null, null, Res.lang.Tutorial[2], Res.lang.Confirmation[3]);
                
                once = false;
                
            }
            
            if (gridModel.Grid[4].Value == -1 &&
                gridModel.Grid[26].Value == -1)
            {
                GAController.TutorialStageChange(3);
                once = true;
                stage = Stages.Repeat;
            }
        }
        public void Repeat()
        {
            if (once)
            {
                for (int i = 0; i < gridModel.Grid.Count; i++)
                {
                    gridModel.Grid[i].GetView().Disable();
                }
                gridModel.Grid[5].GetView().Enable();
                gridModel.Grid[6].GetView().Enable();
                gridModel.Grid[7].GetView().Enable();
                gridModel.Grid[25].GetView().Enable();
            
                Alerts.AlertCall.CallWithText(null, null, Res.lang.Tutorial[3], Res.lang.Confirmation[1]);
                
                once = false;
                
            }
            
            if (gridModel.Grid[5].Value == -1 &&
                gridModel.Grid[6].Value == -1 &&
                gridModel.Grid[7].Value == -1 &&
                gridModel.Grid[25].Value == -1)
            {
                GAController.TutorialStageChange(4);
                once = true;
                stage = Stages.FirstNlastRow;
            }
        }
        
        public void FirstNLastRow()
        {
            if (once)
            {
                for (int i = 0; i < gridModel.Grid.Count; i++)
                {
                    gridModel.Grid[i].GetView().Disable();
                }
                gridModel.Grid[8].GetView().Enable();
                gridModel.Grid[9].GetView().Enable();
            
                Alerts.AlertCall.CallWithText(null, null, Res.lang.Tutorial[4], Res.lang.Confirmation[4]);
                
                once = false;
                
            }
            
            if (gridModel.Grid.Count <= 18)
            {
                GAController.TutorialStageChange(5);
                once = true;
                stage = Stages.End;
            }
        }
    }
}