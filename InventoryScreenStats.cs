using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreenStats : MonoBehaviour
{
    [SerializeField]
    private GameObject playerInventoryUI;
    [SerializeField]
    private GameObject playerStatsUI;
    [SerializeField]
    private GameObject dialogWindow;
    [SerializeField]
    private GameObject sealInventoryUI;
    [SerializeField]
    private GameObject menuInGame;
    [SerializeField]
    private GameObject otherBuildInventoryUI;
    [SerializeField]
    private GameObject buildingSystemUI;
    [SerializeField]
    private GameObject viewSystemUI;
    [SerializeField]
    private GameObject dialogViewError;
    [SerializeField]
    private GameObject infoWindowIsOpen;

    [SerializeField]
    PlayerInventory p_invent;
    [SerializeField]
    BuildSeal b_seal;
    [SerializeField]
    StateGamePlayer stateWindow;

    void Start()
    {
        playerStatsUI.SetActive(true);
        playerInventoryUI.SetActive(false);
        dialogWindow.SetActive(false);
        sealInventoryUI.SetActive(false);
        infoWindowIsOpen.SetActive(false);
        menuInGame.SetActive(false);
        otherBuildInventoryUI.SetActive(false);
        buildingSystemUI.SetActive(false);
        viewSystemUI.SetActive(false);
        dialogViewError.SetActive(false);
    }
    void Update()
    {
        if (stateWindow.GetStateFreeGame() || stateWindow.GetStatePlayerInventory())
        {
            if (Input.GetButtonDown("Inventory"))
            {
                if (dialogWindow.activeSelf)
                {
                    dialogWindow.SetActive(!dialogWindow.activeSelf);
                }
                if (!stateWindow.GetStatePlayerInventory())
                {
                    stateWindow.SetState("openInventory", true);
                    stateWindow.SetState("freeGame", false);
                    playerInventoryUI.SetActive(true);
                    playerStatsUI.SetActive(false);
                }
                else
                {
                    stateWindow.SetState("openInventory", false);
                    stateWindow.SetState("freeGame", true);
                    playerInventoryUI.SetActive(false);
                    playerStatsUI.SetActive(true);
                }
            }
        }

        if (stateWindow.GetStateFreeGame() || stateWindow.GetStateGameMenu())
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (!stateWindow.GetStateGameMenu())
                {
                    stateWindow.SetState("openInGameMenu", true);
                    stateWindow.SetState("freeGame", false);
                    playerStatsUI.SetActive(false);
                    menuInGame.SetActive(true);
                }
                else
                {
                    stateWindow.SetState("openInGameMenu", false);
                    stateWindow.SetState("freeGame", true);
                    playerStatsUI.SetActive(true);
                    menuInGame.SetActive(false);
                }
            }
        }
        if (stateWindow.GetStatePlayerInventory() || stateWindow.GetStateSealInventory()
            || (stateWindow.GetStateOtherBuildInventory() && !stateWindow.GetStateViewBase()))
        {
            if (Input.GetButtonDown("Cancel"))
            {
                stateWindow.SetState("freeGame", true);
                stateWindow.SetState("openInventory", false);
                stateWindow.SetState("openSealInventory", false);
                stateWindow.SetState("openOtherBuilds", false);

                playerStatsUI.SetActive(true);
                playerInventoryUI.SetActive(false);
                dialogWindow.SetActive(false);
                sealInventoryUI.SetActive(false);
                infoWindowIsOpen.SetActive(false);
                otherBuildInventoryUI.SetActive(false);
                
            }
        }
        if (stateWindow.GetStateViewBase() && !stateWindow.GetStateOtherBuildInventory())
        {
            if (Input.GetButtonDown("Cancel"))
            {
                stateWindow.SetState("openSealInventory", true);
                stateWindow.SetState("openViewBase", false);
                sealInventoryUI.SetActive(true);
                viewSystemUI.SetActive(false);
            }
        }
        if (stateWindow.GetStateViewBase() && stateWindow.GetStateOtherBuildInventory())
        {
            if (Input.GetButtonDown("Cancel"))
            {
                stateWindow.SetState("openOtherBuilds", false);
                otherBuildInventoryUI.SetActive(false);
                viewSystemUI.SetActive(true);
            }
        }

    }
    public bool GetOpenInfoForBuilding()
    {
        return infoWindowIsOpen.activeSelf;
    }
    public void SetOpenInfoForBuilding(bool active)
    {
        infoWindowIsOpen.SetActive(active);
    }
    public void OpenDialogScreen()
    {
        dialogWindow.SetActive(true);
    }
    public void TrueAnswerOnBotton()
    {
        stateWindow.SetState("freeGame", true);
        stateWindow.SetState("openInventory", false);
        dialogWindow.SetActive(false);
        playerInventoryUI.SetActive(false);
        playerStatsUI.SetActive(true);
        //Запускается катсцена постройки;
        b_seal.BuildsSealInGame();
    }
    public void FalseAnswerOnBotton()
    {
        dialogWindow.SetActive(false);
        p_invent.ReturnSeal();
    }
        public void CloseMenuInButton()
    {
        stateWindow.SetState("openSealInventory", false);
        stateWindow.SetState("freeGame", true);
        sealInventoryUI.SetActive(false);
        playerStatsUI.SetActive(true);
    }
    public void OpenMenuSeal()
    {
        stateWindow.SetState("openSealInventory", true);
        stateWindow.SetState("freeGame", false);
        sealInventoryUI.SetActive(true);
        playerStatsUI.SetActive(false);
    }
    public void OpenMenuOtherBuilds()
    {
        if (stateWindow.GetStateViewBase())
        {
            stateWindow.SetState("openOtherBuilds", true);
            otherBuildInventoryUI.SetActive(true);
            viewSystemUI.SetActive(false);
        }
        else if (stateWindow.GetStateFreeGame())
        {
            stateWindow.SetState("openOtherBuilds", true);
            stateWindow.SetState("freeGame", false);
            otherBuildInventoryUI.SetActive(true);
            playerStatsUI.SetActive(false);
        }
    }
    public void BuildModeWindow()
    {
        stateWindow.SetState("openBuildsMode", true);
        stateWindow.SetState("openSealInventory", false);
        buildingSystemUI.SetActive(true);
        sealInventoryUI.SetActive(false);
    }
    public void FinishedBuildings()
    {
        stateWindow.SetState("openBuildsMode", false);
        stateWindow.SetState("openSealInventory", true);
        buildingSystemUI.SetActive(false);
        sealInventoryUI.SetActive(true);
    }
    public void ViewModeWindow()
    {
        stateWindow.SetState("openViewBase", true);
        stateWindow.SetState("openSealInventory", false);
        viewSystemUI.SetActive(true);
        sealInventoryUI.SetActive(false);
    }
    public void OpenInventoryOtherBuilds()
    {
        stateWindow.SetState("openOtherBuilds", true);
    }
    public IEnumerator DialogView()
    {
        dialogViewError.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        dialogViewError.SetActive(false);   
    }
    public void RemoveBuildsFromViewMode()
    {
        stateWindow.SetState("openOtherBuilds", false);
        otherBuildInventoryUI.SetActive(false);
        viewSystemUI.SetActive(true);
    }
    public void RemoveBuildsFromFreeGame()
    {
        stateWindow.SetState("openOtherBuilds", false);
        stateWindow.SetState("freeGame", true);
        otherBuildInventoryUI.SetActive(false);
        playerStatsUI.SetActive(true);
    }
}
