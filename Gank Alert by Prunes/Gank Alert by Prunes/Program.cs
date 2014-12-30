using System;
using System.Security.Cryptography.X509Certificates;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gank_Alert_by_Prunes
{
    internal class tracking
    {
        public static SpellSlot[] SummonerSpellSlots = { ((SpellSlot)4), ((SpellSlot)5) };
        public static SpellSlot[] SpellSlots = { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
        private static float previousxp;

        public static float Previousxp //tracks xp on the previous game tick
        {
            get { return previousxp; }
            set { previousxp = value; }
        }

        public struct Champ
        {
            public string Name;
            public float FlashCd;
            public float Qcd;
            public float Wcd;
            public float Ecd;
            public float Rcd;
        }



        public static void AddToMenu(Menu menu)
        {
            Game.OnGameUpdate += Game_OnGameUpdate;

        }

        private static void Game_OnGameUpdate(EventArgs args) //xp is shared within 1400 range, champs within 1500 range of zilean get 8% boost
        {
            //super minion: 97xp
            //siege minion: 92xp
            //caster: 32xp
            //melee: 64xp

            var ChampNumber = ObjectManager.Get<Obj_AI_Hero>().Count(hero => hero != null && hero.IsValid && hero.IsEnemy && hero.IsValidTarget(1500)); //gets number of nearby champs (1500 range)
            var Champ = ObjectManager.Get<Obj_AI_Hero>().First(hero => hero != null && hero.IsValid && hero.IsEnemy && hero.IsValidTarget(1500));
            var xp = Champ.Experience;

            var minion = ObjectManager.Get<Obj_AI_Minion>().Any(dying => dying.IsDead); //this might be able to draw circles around which minion gave xp?

            if (ChampNumber == 1)
            {
                if (xp - Previousxp == 46 || xp - Previousxp == 48)
                {
                    // 1 champ in bush
                }
                if (xp - Previousxp == 16)
                {
                    // 1 or 2 in bush
                }
                if (xp - Previousxp == 8)
                {
                    // 2 or 3 in bush
                }
                if (xp - Previousxp == 4 || xp - Previousxp == 23)
                {
                    // 3 champs in bush
                }
                
            }
            if (ChampNumber == 2)
            {
                if (xp - Previousxp == 30)
                {
                    // 1 champ in bush
                }
                if (xp - Previousxp == 8)
                {
                    // 1 or 2 in bush
                }

                if (xp - Previousxp == 4 || xp - Previousxp == 23)
                {
                    // 2 champs in bush
                }
                
            }
            if (ChampNumber == 3)
            {
                if (xp - Previousxp == 23)
                {
                    // 1 champ in bush
                }

                if (xp - Previousxp == 2 || xp - Previousxp == 18.4 || xp - Previousxp == 19.4)
                {
                    // 2 champs in bush
                }
                
            }

            else
            {
                Previousxp = xp;
            }


        }




    }







}


