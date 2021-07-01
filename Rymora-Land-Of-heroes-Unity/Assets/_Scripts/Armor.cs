using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Armor : Equipable{

	public enum ArmorCat {Heavy, Medium, Light};
	public enum ArmorStats {Protection, Evasion};
	public enum ArmorBases {None, Cloth, Robe, Leather, Chainmail, StuddedLeather, Halfplate, Fullplate, Scale, Splitmail};

	public static Dictionary<ArmorCat, Dictionary<ArmorStats, int>> ArmorData = new Dictionary<ArmorCat, Dictionary<ArmorStats, int>>() {
		//{ArmorCat.None, new Dictionary<ArmorStats, int>() {
			//{ArmorStats.Protection, 1},
			//{ArmorStats.Evasion, 5}
		//}},
		{ArmorCat.Light, new Dictionary<ArmorStats, int>() {
			{ArmorStats.Protection, 3},
			{ArmorStats.Evasion, 0}
		}},
		{ArmorCat.Medium, new Dictionary<ArmorStats, int>() {
			{ArmorStats.Protection, 5},
			{ArmorStats.Evasion, 0}
		}},
		{ArmorCat.Heavy, new Dictionary<ArmorStats, int>() {
			{ArmorStats.Protection, 14},
			{ArmorStats.Evasion, 0}
		}},
	};
	public static Dictionary<ArmorCat, Dictionary<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>>> ArmorList = new Dictionary<ArmorCat, Dictionary<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>>>() {
		//{ArmorCat.None, new Dictionary<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>>(){
			//{ArmorBases.None, new Dictionary<string, Weapons.WeaponsDamageCat>() {
				//{"weak", Weapons.WeaponsDamageCat.None},
					//	{"strong", Weapons.WeaponsDamageCat.None}}}}},
		{ArmorCat.Light, new Dictionary<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>>(){
				{ArmorBases.Cloth, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Smashing},
					{"strong", Weapons.WeaponsDamageCat.Piercing}}},
				{ArmorBases.Leather, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Piercing},
					{"strong", Weapons.WeaponsDamageCat.Cutting}}},
				{ArmorBases.Robe, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Cutting},
					{"strong", Weapons.WeaponsDamageCat.Smashing}}}}},
		{ArmorCat.Medium, new Dictionary<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>>(){
				{ArmorBases.Chainmail, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Smashing},
					{"strong", Weapons.WeaponsDamageCat.Piercing}}},
				{ArmorBases.StuddedLeather, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Cutting},
					{"strong", Weapons.WeaponsDamageCat.Smashing}}},
				{ArmorBases.Halfplate, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Piercing},
					{"strong", Weapons.WeaponsDamageCat.Cutting}}}}},
		{ArmorCat.Heavy, new Dictionary<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>>(){
				{ArmorBases.Fullplate, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Smashing},
					{"strong", Weapons.WeaponsDamageCat.Piercing}}},
				{ArmorBases.Scale, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Cutting},
					{"strong", Weapons.WeaponsDamageCat.Smashing}}},
				{ArmorBases.Splitmail, new Dictionary<string, Weapons.WeaponsDamageCat>() {
					{"weak", Weapons.WeaponsDamageCat.Piercing},
					{"strong", Weapons.WeaponsDamageCat.Cutting}}}}}
		
	};

	public ArmorBases baseArmor;
	public ArmorCat category;
	public int lvl;
	public int protection;
	public int evasion;

	public Equipable equipable;
	// Use this for initialization
	public Armor(){
        //equipable = new Equipable("armor");
        this.slot = Slots.Chest;
		getArmorStats(baseArmor, lvl);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void getArmorStats(ArmorBases baseArmor, int lvl) {
		this.baseArmor = baseArmor;
		this.lvl = lvl;
		foreach (KeyValuePair<ArmorCat, Dictionary<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>>> cat in ArmorList ){
			foreach(KeyValuePair<ArmorBases, Dictionary<string, Weapons.WeaponsDamageCat>> armorBase in cat.Value ){
				if ( baseArmor == armorBase.Key) {
					category = cat.Key;
				}
			}
		}
		protection = ArmorData[category][ArmorStats.Protection] * lvl;
		evasion = ArmorData[category][ArmorStats.Evasion];
	}

	public static Armor GetArmorByCat(ArmorCat cat, int lvl) {
		Armor armor = new Armor();
		armor.category = cat;
		armor.baseArmor = ArmorList[cat].Keys.ElementAt((int)UnityEngine.Random.Range(0, ArmorList[cat].Count-1));
		armor.getArmorStats(armor.baseArmor, lvl);

		return armor;
	}
}
