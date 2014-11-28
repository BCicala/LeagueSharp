using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using LeagueSharp;
using LeagueSharp.Common;
namespace MasterYiByPrunes
{
    class Program
    {
        public const string ChampName = "MasterYi";
        public static Orbwalking.Orbwalker Orbwalker;
        public static Obj_AI_Base Player = ObjectManager.Player;
        public static Spell Q, W, E, R;
        private static Items.Item tiamatItem, hydraItem, botrkItem, bilgeItem, randuinsItem, GhostbladeItem;
        public static Menu Config;
        public static List<Spell> SpellList = new List<Spell>();

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (Player.BaseSkinName != ChampName) return;

            Q = new Spell(SpellSlot.Q, 600);
            Q.SetTargetted(0.5f, 2000);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R);

            SpellList.Add(Q);
            SpellList.Add(W);
            SpellList.Add(E);
            SpellList.Add(R);

            bilgeItem= new Items.Item(3144, 475f);
            botrkItem = new Items.Item(3153, 425f);
            hydraItem = new Items.Item(3074, 250f);
            tiamatItem = new Items.Item(3077, 250f);
            randuinsItem = new Items.Item(3143, 490f);
            GhostbladeItem = new Items.Item(3142, 590f);


            Config = new Menu("Prunes" + ChampName, ChampName, true);
            var ts = new Menu("Target Selector", "Target Selector");
            SimpleTs.AddToMenu(ts);
            Config.AddSubMenu(ts);
            Config.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));
            Orbwalker = new Orbwalking.Orbwalker(Config.SubMenu("Orbwalking"));

            Config.AddSubMenu(new Menu("Combo", "Combo"));
            Config.SubMenu("Combo").AddItem(new MenuItem("useQ", "Use Q?").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("useW", "Use W?").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("useE", "Use E?").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("useR", "Use R?").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("ComboActive", "Combo").SetValue(new KeyBind(32, KeyBindType.Press)));
            Config.SubMenu("Combo").AddItem(new MenuItem("Combo2", "Combo Without Magnet").SetValue(new KeyBind(67, KeyBindType.Press)));
            Config.AddToMainMenu();

            Game.OnGameUpdate += Game_OnGameUpdate;
            GameObject.OnCreate += GameObject_OnCreate;

            Game.PrintChat("<font color='#00FFFF'>Master Yi</font><font color='#008000'> By Prunes</font>");
        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            if (Config.Item("ComboActive").GetValue<KeyBind>().Active)
            {
                Combo();
            }
            if (Config.Item("Combo2").GetValue<KeyBind>().Active)
            {
                Combo2();
            }
        }


        public static void Combo()
        {
  
            var target = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Physical);
            if (target == null) return;
            var target2 = SimpleTs.GetTarget(300, SimpleTs.DamageType.Physical);
            

            if (target.IsValidTarget(Q.Range) && R.IsReady() && Config.Item("useR").GetValue<bool>())
            {
                R.Cast();
            }
            if (target.IsValidTarget(Q.Range) && Q.IsReady() && Config.Item("useQ").GetValue<bool>())
            {
                Q.CastOnUnit(target);
            }
            if (target.IsValidTarget(Q.Range) && E.IsReady() && Config.Item("useE").GetValue<bool>())
            {
                E.Cast();
            }
            else if (target.IsValidTarget(Q.Range) && W.IsReady() && Orbwalking.InAutoAttackRange(target) && Config.Item("useW").GetValue<bool>())
            {
               // Player.IssueOrder(GameObjectOrder.AttackTo, target);
                Utility.DelayAction.Add(350, () => W.Cast());
               // Player.IssueOrder(GameObjectOrder.AttackTo, target);
                Orbwalking.ResetAutoAttackTimer();
            }
            if (tiamatItem.IsReady() && target.IsValidTarget(tiamatItem.Range))
            {
                tiamatItem.Cast();
            }
            if (hydraItem.IsReady() && target.IsValidTarget(tiamatItem.Range))
            {
                hydraItem.Cast();
            }
            if (botrkItem.IsReady() && target.IsValidTarget(botrkItem.Range))
            {
                botrkItem.Cast(target);
            }
            if (bilgeItem.IsReady() && target.IsValidTarget(bilgeItem.Range))
            {
                bilgeItem.Cast(target);
            }
            if (GhostbladeItem.IsReady() && target.IsValidTarget(Q.Range))
            {
                GhostbladeItem.Cast();
            }
            if (randuinsItem.IsReady() && target.IsValidTarget(randuinsItem.Range))
            {
                randuinsItem.Cast();
            }
            /*
            if (Orbwalking.InAutoAttackRange(target2))
            {
                Orbwalker.SetMovement(false);
            }
            if (!Orbwalking.InAutoAttackRange(target2) || target2 == null)
            {
                Orbwalker.SetMovement(true);
            }
             */
            
            else if (target2.IsEnemy && target2.IsValidTarget() && !target2.IsMinion)
            {
                Player.IssueOrder(GameObjectOrder.AttackUnit, target2);
            }
        }

        public static void Combo2()
        {

            var target = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Physical);
            if (target == null) return;
            var target2 = SimpleTs.GetTarget(300, SimpleTs.DamageType.Physical);


            if (target.IsValidTarget(Q.Range) && R.IsReady() && Config.Item("useR").GetValue<bool>())
            {
                R.Cast();
            }
            if (target.IsValidTarget(Q.Range) && Q.IsReady() && Config.Item("useQ").GetValue<bool>())
            {
                Q.CastOnUnit(target);
            }
            if (target.IsValidTarget(Q.Range) && E.IsReady() && Config.Item("useE").GetValue<bool>())
            {
                E.Cast();
            }
            else if (target.IsValidTarget(Q.Range) && W.IsReady() && Orbwalking.InAutoAttackRange(target) && Config.Item("useW").GetValue<bool>())
            {
                 Player.IssueOrder(GameObjectOrder.AttackTo, target);
                Utility.DelayAction.Add(350, () => W.Cast());
                 Player.IssueOrder(GameObjectOrder.AttackTo, target);
                Orbwalking.ResetAutoAttackTimer();
            }
            if (tiamatItem.IsReady() && target.IsValidTarget(tiamatItem.Range))
            {
                tiamatItem.Cast();
            }
            if (hydraItem.IsReady() && target.IsValidTarget(tiamatItem.Range))
            {
                hydraItem.Cast();
            }
            if (botrkItem.IsReady() && target.IsValidTarget(botrkItem.Range))
            {
                botrkItem.Cast(target);
            }
            if (bilgeItem.IsReady() && target.IsValidTarget(bilgeItem.Range))
            {
                bilgeItem.Cast(target);
            }
            if (GhostbladeItem.IsReady() && target.IsValidTarget(Q.Range))
            {
                GhostbladeItem.Cast();
            }
            if (randuinsItem.IsReady() && target.IsValidTarget(randuinsItem.Range))
            {
                randuinsItem.Cast();
            }
        }


        public static void Qlogic() //not implemented yet
        {
            var target = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Physical);

            if (target.IsDashing() || target.LastCastedSpellName() == "SummonerFlash")
            {
                Q.CastOnUnit(target);
            }
            if (Player.Health < Player.MaxHealth/2)
            {
                Q.CastOnUnit(target);
            }

        }

        static void GameObject_OnCreate(GameObject turret, EventArgs args)
        {
            var target = ObjectManager.Get<Obj_AI_Minion>().First(it => it.IsValidTarget(Q.Range));
            var champion = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Physical);

          
            if (Q.IsReady() && turret is Obj_SpellMissile && champion.IsDead)
            {
                var attack = turret as Obj_SpellMissile;
                if (attack.SpellCaster is Obj_AI_Turret && attack.SpellCaster.IsEnemy && attack.Target.IsMe)
                {
                    Q.CastOnUnit(target);
                }
                  
            }
        }

    }
}