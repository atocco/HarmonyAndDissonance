using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script controls the abilities cast by the characters and applies their effects to the boss
// attached to the rhythm bar

public class ability_controller : MonoBehaviour {

	// import the whole squad
	public GameObject panda;
	public GameObject bee;
	public GameObject bird;
	public GameObject rabbit;
	private time_keeper timekeeper;
	public GameObject boss;
	public GameObject fc;
	// get the logger output
	//public GameObject textOut;


	// this function is called to determine the ability to cast based on the input stack
	public void parseAbility(string[] stack){
		// do attack animation
		//panda.GetComponent<player_fight_animations> ().abilityCast ();
		//bee.GetComponent<player_fight_animations> ().abilityCast ();
		//bird.GetComponent<player_fight_animations> ().abilityCast ();
		//rabbit.GetComponent<player_fight_animations> ().abilityCast ();

		// reset temp variables
		int dmg = 0;
		int health = 0;

		switch(stack[0]){
		case "panda":
			switch (stack [1]) {
			case "bee":
				switch (stack [2]) {
				case "bird":
					// panda, bee, bird
					dmg = panda.GetComponent<player_object> ().attack () + 5;	// deal panda dmg
					health = bee.GetComponent<player_object> ().basedmg;		// heal the primary char
					panda.GetComponent<player_object> ().heal (health);
					panda.GetComponent<player_object> ().cured ();
					bird.GetComponent<player_object> ().defend ();				// bird tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);			// assign dmg
					fc.GetComponent<flashy_ability_controller> ().pandaCast ();

					break;
				case "rabbit":
					//panda, bee, rabbit
					dmg = panda.GetComponent<player_object> ().attack () + 5;	// deal panda dmg
					health = bee.GetComponent<player_object> ().basedmg;		// heal the primary char
					panda.GetComponent<player_object> ().heal (health);
					panda.GetComponent<player_object> ().cured ();
					boss.GetComponent<boss_object> ().poison ();				// rabbit tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);			// assign dmg
					fc.GetComponent<flashy_ability_controller> ().pandaCast ();

					break;
				}
				break;
			case "bird":
				switch (stack [2]) {
				case "bee":
					// panda, bird, bee
					dmg = panda.GetComponent<player_object> ().attack () + 5;	// deal panda dmg
					panda.GetComponent<player_object> ().empower ();			// empower primary char
					health = bee.GetComponent<player_object> ().basedmg * 2;	// heal the bee
					bee.GetComponent<player_object> ().heal (health);
					boss.GetComponent<boss_object> ().takeHit (dmg);			// hurt boss
					fc.GetComponent<flashy_ability_controller> ().pandaCast ();

					break;
				case "rabbit":
					// panda, bird, rabbit
					dmg = panda.GetComponent<player_object> ().attack () + 5;	// deal panda dmg
					panda.GetComponent<player_object> ().empower ();			// empower primary char
					boss.GetComponent<boss_object> ().poison ();				// rabbit tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);			// hurt boss
					fc.GetComponent<flashy_ability_controller> ().pandaCast ();

					break;
				}
				break;
			case "rabbit":
				switch (stack [2]) {
				case "bird":
					// panda, rabbit, bird
					dmg = panda.GetComponent<player_object> ().attack () + 5;	// deal panda dmg
					boss.GetComponent<boss_object> ().gimp ();				// rabbit secondary
					bird.GetComponent<player_object> ().defend ();				// bird tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);			// hurt boss
					fc.GetComponent<flashy_ability_controller> ().pandaCast ();

					break;
				case "bee":
					// panda, rabbit, bee
					dmg = panda.GetComponent<player_object> ().attack () + 5;	// deal panda dmg
					boss.GetComponent<boss_object> ().gimp ();				// rabbit secondary
					health = bee.GetComponent<player_object> ().basedmg * 2;	// heal the bee
					bee.GetComponent<player_object> ().heal (health);
					boss.GetComponent<boss_object> ().takeHit (dmg);			// hurt boss
					fc.GetComponent<flashy_ability_controller> ().pandaCast ();

					break;
				}
				break;
			}
			break;
		case "bee":
			switch (stack [1]) {
			case "panda":
				switch (stack [2]) {
				case "bird":
					// bee, panda, bird
					health = bee.GetComponent<player_object> ().basedmg;	// heal all three attackers
					panda.GetComponent<player_object> ().heal (health);
					panda.GetComponent<player_object> ().cured ();			// cure all three ailments
					bee.GetComponent<player_object> ().heal(health);
					bee.GetComponent<player_object> ().cured ();
					bird.GetComponent<player_object> ().heal(health);
					bird.GetComponent<player_object> ().cured ();
					bird.GetComponent<player_object> ().defend ();				// bird tertiary
					fc.GetComponent<flashy_ability_controller> ().beeCast ();
					// panda is useless here

					break;
				case "rabbit":
					// bee, panda, rabbit
					health = bee.GetComponent<player_object> ().basedmg;	// heal all three attackers
					panda.GetComponent<player_object> ().heal(health);
					panda.GetComponent<player_object> ().cured ();			// cure all three ailments
					bee.GetComponent<player_object> ().heal(health);
					bee.GetComponent<player_object> ().cured ();
					rabbit.GetComponent<player_object> ().heal(health);
					rabbit.GetComponent<player_object> ().cured ();
					boss.GetComponent<boss_object> ().poison ();				// rabbit tertiary
					fc.GetComponent<flashy_ability_controller> ().beeCast ();
					// panda is useless here

					break;
				}
				break;
			case "bird":
				switch (stack [2]) {
				case "panda":
					// bee, bird, panda
					health = bee.GetComponent<player_object> ().basedmg;	// heal all three attackers
					panda.GetComponent<player_object> ().heal(health);
					panda.GetComponent<player_object> ().cured ();			// cure all three ailments
					bee.GetComponent<player_object> ().heal(health);
					bee.GetComponent<player_object> ().cured ();
					bird.GetComponent<player_object> ().heal(health);
					bird.GetComponent<player_object> ().cured ();
					fc.GetComponent<flashy_ability_controller> ().beeCast ();
					// bird is useless here
					// panda is useless here

					break;
				case "rabbit":
					// bee, bird, rabbit
					health = bee.GetComponent<player_object> ().basedmg;	// heal all three attackers
					rabbit.GetComponent<player_object> ().heal(health);
					rabbit.GetComponent<player_object> ().cured ();			// cure all three ailments
					bee.GetComponent<player_object> ().heal(health);
					bee.GetComponent<player_object> ().cured ();
					bird.GetComponent<player_object> ().heal(health);
					bird.GetComponent<player_object> ().cured ();
					fc.GetComponent<flashy_ability_controller> ().beeCast ();
					// bird is useless here
					// rabbit is useless here

					break;
				}
				break;
			case "rabbit":
				switch (stack [2]) {
				case "bird":
					// bee, rabbit, bird
					health = bee.GetComponent<player_object> ().basedmg;	// heal all three attackers
					rabbit.GetComponent<player_object> ().heal(health);
					rabbit.GetComponent<player_object> ().cured ();			// cure all three ailments
					bee.GetComponent<player_object> ().heal(health);
					bee.GetComponent<player_object> ().cured ();
					bird.GetComponent<player_object> ().heal(health);
					bird.GetComponent<player_object> ().cured ();
					bird.GetComponent<player_object> ().defend ();				// bird tertiary
					fc.GetComponent<flashy_ability_controller> ().beeCast ();
					// rabbit is useless

					break;
				case "panda":
					// bee, rabbit, panda
					health = bee.GetComponent<player_object> ().basedmg;	// heal all three attackers
					panda.GetComponent<player_object> ().heal(health);
					panda.GetComponent<player_object> ().cured ();			// cure all three ailments
					bee.GetComponent<player_object> ().heal(health);
					bee.GetComponent<player_object> ().cured ();
					rabbit.GetComponent<player_object> ().heal(health);
					rabbit.GetComponent<player_object> ().cured ();
					fc.GetComponent<flashy_ability_controller> ().beeCast ();
					// rabbit is useless
					// panda is useless

					break;
				}
				break;
			}
			break;
		case "bird":
			switch (stack [1]) {
			case "bee":
				switch (stack [2]) {
				case "panda":
					// bird, bee, panda
					dmg = bird.GetComponent<player_object> ().attack ();	// bird primary hits
					bee.GetComponent<player_object> ().defend ();		// bird protecc
					panda.GetComponent<player_object> ().defend ();
					health = bee.GetComponent<player_object> ().basedmg;	// heal the primary char
					bird.GetComponent<player_object> ().cured ();			// cure primary char
					bird.GetComponent<player_object> ().heal (health);
					dmg = dmg + 5;											// panda tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().birdCast ();

					break;
				case "rabbit":
					// bird, bee, rabbit
					dmg = bird.GetComponent<player_object> ().attack ();	// bird primary hits
					bee.GetComponent<player_object> ().defend ();		// bird protecc
					rabbit.GetComponent<player_object> ().defend ();
					health = bee.GetComponent<player_object> ().basedmg;	// heal the primary char
					bird.GetComponent<player_object> ().cured ();			// cure primary char
					bird.GetComponent<player_object> ().heal (health);
					boss.GetComponent<boss_object> ().poison ();				// rabbit tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().birdCast ();

					break;
				}
				break;
			case "panda":
				switch (stack [2]) {
				case "bee":
					// bird, panda, bee
					dmg = bird.GetComponent<player_object> ().attack ();	// bird primary hits
					bee.GetComponent<player_object> ().defend ();		// bird protecc
					panda.GetComponent<player_object> ().defend ();
					dmg = dmg + 10;											// panda secondary
					health = bee.GetComponent<player_object> ().basedmg * 2;	// heal the bee
					bee.GetComponent<player_object> ().heal (health);
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().birdCast ();

					break;
				case "rabbit":
					// bird, panda, rabbit
					dmg = bird.GetComponent<player_object> ().attack ();	// bird primary hits
					rabbit.GetComponent<player_object> ().defend ();		// bird protecc
					panda.GetComponent<player_object> ().defend ();
					dmg = dmg + 10;											// panda secondary
					boss.GetComponent<boss_object> ().poison ();				// rabbit tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().birdCast ();

					break;
				}
				break;
			case "rabbit":
				switch (stack [2]) {
				case "bee":
					// bird, rabbit, bee
					dmg = bird.GetComponent<player_object> ().attack ();	// bird primary hits
					bee.GetComponent<player_object> ().defend ();		// bird protecc
					rabbit.GetComponent<player_object> ().defend ();
					boss.GetComponent<boss_object> ().gimp ();				// rabbit secondary
					health = bee.GetComponent<player_object> ().basedmg * 2;	// heal the bee
					bee.GetComponent<player_object> ().heal (health);
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().birdCast ();

					break;
				case "panda":
					// bird, rabbit, panda
					dmg = bird.GetComponent<player_object> ().attack ();	// bird primary hits
					rabbit.GetComponent<player_object> ().defend ();		// bird protecc
					panda.GetComponent<player_object> ().defend ();
					boss.GetComponent<boss_object> ().gimp ();				// rabbit secondary
					dmg = dmg + 5; 											// panda tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().birdCast ();

					break;
				}
				break;
			}
			break;
		case "rabbit":
			switch (stack [1]) {
			case "bee":
				switch (stack [2]) {
				case "bird":
					// rabbit, bee, bird
					dmg = rabbit.GetComponent<player_object> ().attack ();	// rabbit primary hits
					boss.GetComponent<boss_object> ().stun ();				// rabbit primary effect
					health = bee.GetComponent<player_object> ().basedmg;	// heal the primary char
					rabbit.GetComponent<player_object> ().cured ();			// cure primary char
					rabbit.GetComponent<player_object> ().heal (health);
					bird.GetComponent<player_object> ().defend ();				// bird tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().rabbitCast ();

					break;
				case "panda":
					// rabbit, bee, panda
					dmg = rabbit.GetComponent<player_object> ().attack ();	// rabbit primary hits
					boss.GetComponent<boss_object> ().stun ();				// rabbit primary effect
					health = bee.GetComponent<player_object> ().basedmg;	// heal the primary char
					rabbit.GetComponent<player_object> ().heal (health);
					rabbit.GetComponent<player_object> ().cured ();			// cure primary char
					dmg = dmg + 5;											// panda tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().rabbitCast ();

					break;
				}
				break;
			case "bird":
				switch (stack [2]) {
				case "bee":
					// rabbit, bird, bee
					dmg = rabbit.GetComponent<player_object> ().attack ();	// rabbit primary hits
					boss.GetComponent<boss_object> ().stun ();				// rabbit primary effect
					rabbit.GetComponent<player_object> ().empower ();			// empower primary char
					health = bee.GetComponent<player_object> ().basedmg * 2;	// heal the bee
					bee.GetComponent<player_object> ().heal (health);
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().rabbitCast ();

					break;
				case "panda":
					// rabbit, bird, panda
					dmg = rabbit.GetComponent<player_object> ().attack ();	// rabbit primary hits
					boss.GetComponent<boss_object> ().stun ();				// rabbit primary effect
					rabbit.GetComponent<player_object> ().empower ();			// empower primary char
					dmg = dmg + 5;											// panda tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().rabbitCast ();

					break;
				}
				break;
			case "panda":
				switch (stack [2]) {
				case "bird":
					// rabbit, panda, bird
					dmg = rabbit.GetComponent<player_object> ().attack ();	// rabbit primary hits
					boss.GetComponent<boss_object> ().stun ();				// rabbit primary effect
					dmg = dmg + 10;											// panda secondary
					bird.GetComponent<player_object> ().defend ();			// bird tertiary
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().rabbitCast ();

					break;
				case "bee":
					// rabbit, panda, bee
					dmg = rabbit.GetComponent<player_object> ().attack ();	// rabbit primary hits
					boss.GetComponent<boss_object> ().stun ();				// rabbit primary effect
					dmg = dmg + 10;											// panda secondary
					health = bee.GetComponent<player_object> ().basedmg * 2;	// heal the bee
					bee.GetComponent<player_object> ().heal (health);
					boss.GetComponent<boss_object> ().takeHit (dmg);		// hurt boss
					fc.GetComponent<flashy_ability_controller> ().rabbitCast ();

					break;
				}
				break;
			}
			break;
		default:
			Debug.Log ("ability parser failed on first entry");
			break;
		}
	}
}