using ConsoleGridSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityGridSystem;

public class CellTest : MonoBehaviour
{
    private const int WIDTH  = 8;
    private const int HEIGHT = 8;

    private EventWatcher ew;
    private CharacterFactory characterFactory;
    private ActionFactory actionFactory;
    private CellFactory cellFactory;
    private List<BattleCell> characters;
    private BattleGrid g;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Качество кода", "IDE0051:Удалите неиспользуемые закрытые члены", Justification = "<Ожидание>")]
    private void Start()
    {
        ew = new EventWatcher();
        characterFactory = new CharacterFactory(ew);
        actionFactory = new ActionFactory(ew);
        cellFactory = new CellFactory();
        g = new BattleGrid(WIDTH, HEIGHT);
        List<BattleCell> actionCells = new List<BattleCell>();
        List<BattleCell> characterCells = new List<BattleCell>();
        characters = AddChars();
        g.PlaceCellsRandom(characters);
        List<BattleCell> targets = AddTargets();
        g.PlaceCellsRandom(targets);
        Object.FindObjectOfType<GridView>().grid = g;
        Object.FindObjectOfType<GridView>().CreateView();
    }
    public void OnClick()
    {
        for (int j = 0; j < characters.Count; j++)
        {
            string s = g._cells.IndexOf(characters[j]).ToString();
            characters[j] = characters[j].MoveTo(g.RandomNeighbour(characters[j]) as BattleCell);
            string s1 = g._cells.IndexOf(characters[j]).ToString();
        }
    }
    //First create items 
    //ALWAYS USE CLONE ASSIGNING ITEM TO CHARACTER
    //Create character by setting his startItems and startSkills
    //Then create cells with the characters
    public List<BattleCell> AddChars()
    {
        //creating items
        Item shovel = new Item("Shovel", 1);
        Item gloves = new Item("Gloves", 1);
        Item knife = new Item("Knife", 1);

        //creating characters
        Character first = characterFactory.CreateCharacter(
            startItems:  new List<Item> {shovel.Clone().SetAmount(10)},
            startSkills: new Skills(med: 0, str: 22));

        Character second = characterFactory.CreateCharacter(
            startItems:  new List<Item> { shovel.Clone(), knife.SetAmount(2) },
            startSkills: new Skills(med: 5, str: 5));

        Character third = characterFactory.CreateCharacter(
            startItems:  new List<Item> {gloves, knife.Clone().SetAmount(1) },
            startSkills: new Skills(med: 10, str: 2));

        //assigning characters to cells
        return new List<BattleCell>() {
            cellFactory.CreateCell(s.holded, first),
            cellFactory.CreateCell(s.holded, second),
            cellFactory.CreateCell(s.holded, third),
        };
    }

    //To create action cell u need to create BAction,
    //then assign it to BSObject 
    //and then assign BSObject to cell
    //and return list with events u created
    //examples are here
    public List<BattleCell> AddTargets()
    {
        //Creating BAction
        BAction easyTarget = actionFactory.CreateAction()
            .AddSkillTest(
                skill: new Skills(str: 5),
                pass: "Soldier pulled out wounded man from the fallen tree",
                fail: "Soldier was too weak to save man");

        BAction hardTarget = actionFactory.CreateAction()
            .AddItemTest(
                item: new Item(name: "Shovel", count: 1),
                pass: "Soldier diged up man after bomb detonation",
                fail: "Soldier has no shovel to help man")
            .AddSkillTest(
                skill: new Skills(med: 1, str: 2),
                pass: "Soldier make a bandaging and bring wounden man to the base",
                fail: "Soldier left one-leged man to die");

        BAction insaneTarget = actionFactory.CreateAction()
            .AddItemTest(
                item: new Item("Knife", 1),
                pass: "Soldier perform field surgery with knife",
                fail: "Soldier has no items to help man")
            .AddSkillTest(
                skill: new Skills(med: 5, str: 0),
                pass: "Soldier retrieves a bullet from wounded man's body",
                fail: "Soldier didn retrieves a bullet and man died with pain")
            .AddAnotherAction((Character ch) => { Debug.Log("Sad soldier goes home"); });

        //creating BSObjects with events
        EventObject event1 = actionFactory.CreateEvent(easyTarget);
        EventObject event2 = actionFactory.CreateEvent(hardTarget);
        EventObject event3 = actionFactory.CreateEvent(insaneTarget);

        //creating EventCells with BSObjects
        return new List<BattleCell>() {
            cellFactory.CreateCell(s.action,event1),
            cellFactory.CreateCell(s.action, event2),
            cellFactory.CreateCell(s.action, event3) };
    }
}
