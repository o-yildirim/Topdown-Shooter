using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleCommands : MonoBehaviour
{

    public string executeCommand(string command)
    {
        char seperator = ' ';
        string[] parts = command.Split(seperator);
        string result = "";

        switch (parts[0])
        {
            case "help":
                result = helpCommands();
                break;
            case "give_gun":
                result = giveGun(parts);
                break;
            case "loadlevel":
                result = loadLevel(parts);
                break;

        }



        return result;
    }

    public string helpCommands()
    {
        return "give_gun itemId  ---- Gives the player the gun which has that itemId.\n";
    }

    public string loadLevel(string[] parts)
    {
        int levelIndex;
        if(int.TryParse(parts[1], out levelIndex))
        {
            SceneLoader.instance.loadSceneFadeCall(levelIndex);
        }
        else
        {
            return "Invalid arguments\n";
        }

        return "Level " + levelIndex + " is succesfully loaded.\n";
    }

    public string giveGun(string[] commandParts)
    {

        if (commandParts.Length != 2)
        {
            return "Invalid number of arguments\n";
        }

        GameObject weaponHolster = GunManagement.instance.holster;

        Gun selectedGun;
        int gunId;
        if (int.TryParse(commandParts[1], out gunId))
        {
            selectedGun = UtilityClass.FindGunWithId<Gun>(weaponHolster, gunId); //If console command is called with gun id
        }
        else
        {
            selectedGun = UtilityClass.FindGunWithName<Gun>(weaponHolster, commandParts[1]);   //If console command is called with gun name     
        }

        if (selectedGun == null)
        {
            return "Invalid item id\n";
        }
        else
        {
            GunManagement.instance.switchGun(selectedGun.gunId,9999);
        }


        return "Item " + commandParts[1] + " is successfully added.\n";

    }


}




