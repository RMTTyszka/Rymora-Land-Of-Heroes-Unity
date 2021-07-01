using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Equipable {

	public enum WeaponsSize {Light, Medium, Heavy};
	public enum WeaponsDamageCat {Smashing, Piercing, Cutting, Catalyst, None, Ranged, Thrown};
	public enum WeaponsBase {GreatAxe, Longsword, Sickle, Maul, Morningstar, Hammer, Dagger, Rapier, Spear, Staff, Rod, Orb, Wand, LongBow, Crossbow, ShortBow, ThrowingKnife, Javalin, ThrowingAxe, Unarmed };


	public class WeaponsDataClass {
		public float minDamage;
		public float maxDamage;
		public float attackSpeed;
		public float hitMod;
		public float armorPen;
		public float multDamage;
		public float multAttr;
		public float counterRating;
		public float Range;

		public WeaponsDataClass(float min, float max, float speed, float hit, float pen, float damage, float attr, float counter) {
			minDamage = min;
			maxDamage = max;
			attackSpeed = speed;
			hitMod = hit;
			armorPen = pen;
			multDamage = damage;
			multAttr = attr;
			counterRating = counter;
		}
	}

	public static WeaponsDataClass LightWeapon = new WeaponsDataClass(8f, 18f, 5f, 5f, 0f, 1.2f, 1f, 15f );

	public static Dictionary<WeaponsSize, Dictionary<string, float>> WeaponsData = new Dictionary<WeaponsSize,  Dictionary<string, float>>(){
		{WeaponsSize.Light, new Dictionary<string, float>(){
			{"minDamage", 8f},
			{"maxDamage", 18f},
			{"attackSpeed", 5f}, // 2.5
			{"hitMod", 0f}, // -25 2W
			{"armorPen", 0f},
			{"multDamage", 1.0f},
			{"multAttr", 1f},
			{"counterRating", 15f}}},
		{WeaponsSize.Medium, new Dictionary<string, float>(){
			{"minDamage", 8f},
			{"maxDamage", 18f},
			{"attackSpeed", 10f}, // 5
			{"hitMod", 0f}, // -25
			{"armorPen", 1f},
			{"multDamage", 2f},
			{"multAttr", 2f},
			{"counterRating", 10f}}},
		{WeaponsSize.Heavy, new Dictionary<string, float>(){
			{"minDamage", 8f},
			{"maxDamage", 18f},
			{"attackSpeed", 13f},
			{"hitMod", 10f},
			{"armorPen", 8f},
			{"multDamage", 3f},
			{"multAttr", 3f},
			{"counterRating", 5f}}}
		};

	public static Dictionary<WeaponsDamageCat, Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>> WeaponsList = new Dictionary<WeaponsDamageCat,  Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>>(){
		{WeaponsDamageCat.Catalyst, new Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>(){
			{WeaponsSize.Heavy, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Staff, null}}},
			{WeaponsSize.Medium, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Rod, null}}},
			{WeaponsSize.Light, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Wand, null},
				{WeaponsBase.Orb, null}}}
		}},
		{WeaponsDamageCat.Cutting, new Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>(){
			{WeaponsSize.Heavy, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.GreatAxe, null}}},
			{WeaponsSize.Medium, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Longsword, null}}},
			{WeaponsSize.Light, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Sickle, null}}}
		}},
		{WeaponsDamageCat.Smashing, new Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>(){
			{WeaponsSize.Heavy, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Maul, null}}},
			{WeaponsSize.Medium, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Morningstar, null}}},
			{WeaponsSize.Light, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Hammer, null}}}
		}},
		{WeaponsDamageCat.Piercing, new Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>(){
			{WeaponsSize.Heavy, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Spear, null}}},
			{WeaponsSize.Medium, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Rapier, null}}},
			{WeaponsSize.Light, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Dagger, null}}}
		}},
		{WeaponsDamageCat.Ranged, new Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>(){
			{WeaponsSize.Heavy, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.LongBow, null}}},
			{WeaponsSize.Medium, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Crossbow, null}}},
			{WeaponsSize.Light, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.ShortBow, null}}}
		}},
		{WeaponsDamageCat.Thrown, new Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>(){
			{WeaponsSize.Heavy, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.ThrowingAxe, null}}},
			{WeaponsSize.Medium, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Javalin, null}}},
			{WeaponsSize.Light, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.ThrowingKnife, null}}}
		}},
		{WeaponsDamageCat.None, new Dictionary<WeaponsSize, Dictionary<WeaponsBase, Power>>(){
			{WeaponsSize.Heavy, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Unarmed, null}}},
			{WeaponsSize.Medium, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Unarmed, null}}},
			{WeaponsSize.Light, new Dictionary<WeaponsBase, Power>(){
				{WeaponsBase.Unarmed, null}}}
		}}
	};

	public static Dictionary<WeaponsDamageCat, Dictionary<string, string>> WeaponsSkills = new Dictionary<WeaponsDamageCat, Dictionary<string, string>>(){
		{WeaponsDamageCat.Cutting, new Dictionary<string,string>() {
			{"attackSkill", "Swordmanship"},
			{"attackAttr", "Strength"},
			{"damageAttr", "Strength"},
			{"range", "3"}}},
		{WeaponsDamageCat.Smashing, new Dictionary<string,string>() {
			{"attackSkill", "Heavyweaponship"},
			{"attackAttr", "Strength"},
			{"damageAttr", "Strength"},
			{"range", "3"}}},
		{WeaponsDamageCat.Piercing, new Dictionary<string,string>() {
			{"attackSkill", "Fencing"},
			{"attackAttr", "Agility"},
			{"damageAttr", "Agility"},
			{"range", "3"}}},
		{WeaponsDamageCat.Ranged, new Dictionary<string,string>() {
			{"attackSkill", "Archery"},
			{"attackAttr", "Agility"},
			{"damageAttr", "Agility"},
			{"range", "6"}}},
		{WeaponsDamageCat.Thrown, new Dictionary<string,string>() {
			{"attackSkill", "Archery"},
			{"attackAttr", "Agility"},
			{"damageAttr", "Atrength"},
			{"range", "6"}}},
		{WeaponsDamageCat.Catalyst, new Dictionary<string,string>() {
			{"attackSkill", "Magery"},
			{"attackAttr", "Intuition"},
			{"damageAttr", "Wisdom"},
			{"range", "9"}}},
		{WeaponsDamageCat.None, new Dictionary<string,string>() {
			{"attackSkill", "Wrestling"},
			{"attackAttr", "Vitality"},
			{"damageAttr", "Vitality"},
			{"range", "2"}}}
	};
		

	public static Dictionary<BonusList, int> WeaponsBonuses = new Dictionary<BonusList, int>(){
		{BonusList.AttackDamage, 2},
		{BonusList.Attack, 1},
		{BonusList.Evasion, 1},
		{BonusList.PowerDamage, 1},
		{BonusList.AttackSpeed, 1},
		{BonusList.Critical, 1},
		{BonusList.Resiliense, 1}
	};

	public string name;
	public int lvl;
	public WeaponsBase weapon;
	public WeaponsSize size;
	public WeaponsDamageCat category;
	public float range;
	public Skills attackSkill;
	public Attributes attackAttr;
	public Attributes damageAttr;
	public float minDamage;
	public float maxDamage;
	public Bonuses bonusDamage;
	public float attackSpeed;
	public float hitMod;
	public float armorPen;
	public float multDamage;
	public float multAttr;
	public float counterRating;
	public float attackCooldown = 0f;


	public static Weapons CreateWeaponBySize(WeaponsSize size, int lvl) {
		WeaponsDamageCat[] catlist = System.Enum.GetValues(typeof(WeaponsDamageCat)) as WeaponsDamageCat[];
		WeaponsDamageCat cat = catlist[UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(WeaponsDamageCat)).Length)];
		//Debug.Log(cat);
		WeaponsBase wep = WeaponsList[cat][size].Keys.ElementAt((int)UnityEngine.Random.Range(0, WeaponsList[cat][size].Count-1));
		return new Weapons(wep, lvl);
	}
	public Weapons(WeaponsBase weapon, int lvl){
        this.slot = Slots.Mainhand;
		this.weapon = weapon;
		this.lvl = lvl;
		getWeaponProp(weapon);
		this.minDamage = WeaponsData[size]["minDamage"];
		this.maxDamage = WeaponsData[size]["maxDamage"];
		this.attackSpeed = WeaponsData[size]["attackSpeed"];
		this.hitMod = WeaponsData[size]["hitMod"];
		this.armorPen = WeaponsData[size]["armorPen"] * lvl;
		this.multDamage = WeaponsData[size]["multDamage"];
		this.multAttr = WeaponsData[size]["multAttr"];
		this.counterRating = WeaponsData[size]["counterRating"];
		bonuses = new List<Bonus>();
		createBonuses();

	}

	private void createBonuses() {
		this.bonusDamage = new Bonuses();
        bonusDamage.addBonus(new Bonus(BonusList.AttackDamage, BonusType.Equipment, 10*(this.lvl-1)));
		foreach (KeyValuePair<BonusList, int> bon in WeaponsBonuses) {
			Bonus bonus = new Bonus(bon.Key, BonusType.Equipment, bon.Value*(this.lvl-1));
			if (bon.Key == BonusList.AttackDamage) {
				this.bonusDamage.addBonus(bonus);
			} else {
				bonuses.Add(bonus);
			}

		}
	}

	private void getWeaponProp(WeaponsBase baseWep) {
		foreach (KeyValuePair<WeaponsDamageCat, Dictionary<WeaponsSize,Dictionary<WeaponsBase, Power>>> cat in WeaponsList) {
			foreach (KeyValuePair<WeaponsSize, Dictionary<WeaponsBase, Power>> size in cat.Value) {
				foreach(KeyValuePair<WeaponsBase, Power> weapon in size.Value) {
					if (weapon.Key == baseWep) {
						this.category = cat.Key;
					//	Debug.Log(cat.Key);
						this.size = size.Key;
					
						float.TryParse(WeaponsSkills[this.category]["range"], out this.range);
					//	Debug.Log(size.Key);
					}
				}
			}
		}
		for (int x = 0; x < Enum.GetNames(typeof(Attributes)).Length; x++) {
			if (WeaponsSkills[this.category]["attackAttr"] == Enum.GetName(typeof(Attributes), x)) {
				this.attackAttr = (Attributes)x;
			}
		}
		for (int x = 0; x < Enum.GetNames(typeof(Attributes)).Length; x++) {
			if (WeaponsSkills[this.category]["damageAttr"] == Enum.GetName(typeof(Attributes), x)) {
				this.attackAttr = (Attributes)x;
			}
		}
		for (int x = 0; x < Enum.GetNames(typeof(Skills)).Length; x++) {
			if (WeaponsSkills[this.category]["attackSkill"] == Enum.GetName(typeof(Skills), x)) {
				this.attackSkill = (Skills)x;
			}
		}
		//this.attackSkill = WeaponsSkills[this.category]["attackSkill"];
		//Debug.Log(this.attackSkill);
		//this.attackAttr = WeaponsSkills[this.category]["attackAttr"];
		//Debug.Log(this.attackAttr);
		//this.damageAttr = WeaponsSkills[this.category]["damageAttr"];
		//Debug.Log(this.damageAttr);

	}
}
