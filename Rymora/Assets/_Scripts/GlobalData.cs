using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {

	public static string[] attributes = {"strength", "agility", 
									"vitality", "wisdom", 
									"intuition", "charisma"};
	public static string[] skills = {"alchemy", "anatomy", "animaltaming", "archery", "armorcrafting", "armslore",
    "awareness", "bowcrafting", "carpentery", "fencing", "gathering", "healing",
    "heavyweaponship", "jewelcrafting", "leatherworking", "lore", "lumberjacking",
    "magery", "mercantilism", "military", "mining", "parry", "reflex", "resist spells",
    "skinning", "stealth", "swordmanship", "spirit speaking", "tactics", "tailoring", "wrestling"};

	public static string[] resists = {"weakness", "slow", "stun", "blind", "curse"};

	public static string[] defenses = {"cutting", "smashing", "piercing", "magic"};

	public static string[] slots = {"mainhand", "offhand", "helmet", "neck", "armor", "wrist",
    "gauntlet", "ring1", "ring2", "belt", "boots", "extra"};

	public static string[] prop = {"critical","resilience","attack","special_attack",
                "evasion","sp", "life","st","protection","fortitude","at_damage",
                "sa_damage","attack_speed","cs","critical_damage","armor_penetration",
                "reaction","counter"};
}
